using Microsoft.JSInterop;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using ThreeNET.Data;
using ThreeNET.Objects;
using ThreeNET.Objects.Geometry;
using ThreeNET.Objects.Meshes;
using ThreeNET.Objects.Meshes.Materials;

namespace ThreeNET
{
	internal class ThreeNET : ThreeHelperReferenceHolder
	{
		readonly List<DotNetObjectReference<InteropActionExecutor>> executors = new();
		private static Regex ClassnameReplacementRegex { get; } 
			= new("^Three", RegexOptions.Compiled);
		private readonly IJSRuntime javaScript;
		public ThreeNET(IJSRuntime jsRuntime)
			: base(jsRuntime)
		{
			javaScript = jsRuntime;
		}

		public async Task<T> Create<T>(params object[] additionalArguments) 
			where T : ThreeObject
		{
			var type = typeof(T);
			var helper = await Helper();
			var name = ClassnameReplacementRegex.Replace(type.Name, "");
			try
			{
				var objectRef = await helper.InvokeAsync<IJSObjectReference>("create",
					name, additionalArguments);
				return (T)Activator.CreateInstance(type,
					new object[]
					{
						objectRef,
						javaScript
					})!;
			} catch (Exception ex)
			when (ex is JSException or NullReferenceException)
			{
				throw new InvalidOperationException(
					"You might have chosen an invalid class, as trying to create a THREE." +
					$"{name} did not return a valid object.");
			}
		}

		protected override async ValueTask DisposeAsyncCore()
		{
			var helper = await Helper();
			await helper.InvokeVoidAsync("clearAnimationFrameRequests");
			executors.ForEach(x => x.Dispose());
			await base.DisposeAsyncCore();
		}

		public async ValueTask RegisterAnimateFunction(Func<Task> action)
		{
			var executor = new InteropActionExecutor
			{
				Function = action
			};
			var reference = DotNetObjectReference.Create(executor);
			executors.Add(reference);
			var helper = await Helper();
			await helper.InvokeVoidAsync(
				"helperRequestAnimationFrame",
				reference,
				"Execute",
				true);
		}

		class InteropActionExecutor
		{
			[DynamicDependency(nameof(Execute))]
			public InteropActionExecutor() { }
			public Func<Task> Function { get; set; } = () => Task.CompletedTask;

			[JSInvokable]
			public async Task Execute()
			{
				await (Function?.Invoke() ?? Task.CompletedTask);
			}
		}
	}
}