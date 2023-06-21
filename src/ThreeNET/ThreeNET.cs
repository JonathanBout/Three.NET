using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
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
		private static Regex ClassnameReplacementRegex { get; }
			= new("^Three", RegexOptions.Compiled);
		private readonly IJSRuntime javaScript;
		private readonly ILogger logger;
		private readonly NavigationManager navigation;

		private DotNetObjectReference<InteropActionExecutor>? _animationFrameExecutorReference;
		private DotNetObjectReference<InteropActionExecutor>? AnimationFrameExecutorReference
		{
			get
			{
				return _animationFrameExecutorReference;
			}
			set
			{
				_animationFrameExecutorReference?.Dispose();
				_animationFrameExecutorReference = value;
			}
		}

		public ThreeNET(IJSRuntime jsRuntime, ILogger<ThreeNET> logger, NavigationManager navigation)
			: base(jsRuntime)
		{
			javaScript = jsRuntime;
			this.logger = logger;
			this.navigation = navigation;
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
					const string functionName = nameof(Create);
					const string objectName = nameof(ThreeObject);
					const string jorName = nameof(IJSObjectReference);
					// log it as warning
					logger.LogWarning("A provided argument of the {functionName} function is" +
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
			}
			catch (Exception ex)
			when (ex is JSException or NullReferenceException)
			{
				throw new InvalidOperationException(
					"You might have chosen an invalid class, as trying to create a THREE." +
					$"{name} did not return a valid object.");
			}
		}


		public async ValueTask RequestAnimationFrame(Func<Task> action)
		{
			var executor = new InteropActionExecutor
			{
				Function = action
			};
			AnimationFrameExecutorReference = DotNetObjectReference.Create(executor);
			var helper = await Helper();
			await helper.InvokeVoidAsync(
				"helperRequestAnimationFrame",
				AnimationFrameExecutorReference,
				"Execute");
		}

		class InteropActionExecutor
		{
			[DynamicDependency(nameof(Execute))]
			public InteropActionExecutor() { }
			public Func<Task>? Function { get; set; }
			[JSInvokable]
			public async Task Execute()
			{
				await (Function?.Invoke() ?? Task.CompletedTask);
			}
		}
	}
}