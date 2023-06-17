using Microsoft.Extensions.Logging;
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
		readonly List<(InteropActionExecutor executor, DotNetObjectReference<InteropActionExecutor> reference)> executors = new();
		private static Regex ClassnameReplacementRegex { get; }
			= new("^Three", RegexOptions.Compiled);
		private readonly IJSRuntime javaScript;
		private readonly ILogger logger;
		public ThreeNET(IJSRuntime jsRuntime, ILogger<ThreeNET> logger)
			: base(jsRuntime)
		{
			javaScript = jsRuntime;
			this.logger = logger;
		}

		public async Task<T> Create<T>(params object[] additionalArguments)
			where T : ThreeObject
		{
#if DEBUG
			// In debug mode only:
			// Do a check on the arguments.
			foreach (var argument in additionalArguments)
			{
				// if argument is derived from ThreeObject you
				// likely forgot to use it's "jsObject"
				// (I spent hours debugging because I forgot to do that :facepalm:)
				if (argument is ThreeObject)
				{
					var functionName = nameof(Create);
					var objectName = nameof(ThreeObject);
					var jorName = nameof(IJSObjectReference);
					// log it as warning
					logger.LogWarning("An argument of the {functionName} function is" +
						" derived from {objectName}. Did you mean to use it's {jorName}?", functionName, objectName, jorName);
				}
			}
#endif
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
			//var helper = await Helper();
			//await helper.InvokeVoidAsync("clearAnimationFrameRequests");
			executors.ForEach(x =>
			{
				x.reference.Dispose();
				x.executor.Dispose();
			});
			await base.DisposeAsyncCore();
		}

		public async ValueTask RegisterAnimateFunction(Func<Task> action)
		{
			var executor = new InteropActionExecutor
			{
				Function = action
			};
			var reference = DotNetObjectReference.Create(executor);
			executors.Add((executor, reference));
			var helper = await Helper();
			await helper.InvokeVoidAsync(
				"helperRequestAnimationFrame",
				reference,
				"Execute",
				true);
		}

		class InteropActionExecutor : IDisposable
		{
			[DynamicDependency(nameof(Execute))]
			public InteropActionExecutor() { }
			public Func<Task> Function { get; set; } = () => Task.CompletedTask;

			public void Dispose()
			{
				GC.SuppressFinalize(this);
			}

			[JSInvokable]
			public async Task Execute()
			{
				await (Function?.Invoke() ?? Task.CompletedTask);
			}
		}
	}
}