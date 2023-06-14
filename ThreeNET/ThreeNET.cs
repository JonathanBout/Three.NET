using Microsoft.JSInterop;
using System.Diagnostics.CodeAnalysis;
using ThreeNET.Data;
using ThreeNET.Objects;
using ThreeNET.Objects.Geometry;
using ThreeNET.Objects.Meshes;
using ThreeNET.Objects.Meshes.Materials;

namespace ThreeNET
{
	internal class ThreeNET : ThreeHelperReferenceHolder
	{
		private readonly IJSRuntime javaScript;
		public ThreeNET(IJSRuntime jsRuntime)
			: base(jsRuntime)
		{
			javaScript = jsRuntime;
		}

		public async Task<ThreeScene> CreateScene()
		{
			var helper = await Helper();
			return new ThreeScene(await helper.InvokeAsync<IJSObjectReference>("create", "Scene"), javaScript);
		}

		public async Task<ThreeCamera> CreateCamera(int fieldOfView, int aspectRatio, double nearClip, decimal farClip)
		{
			var helper = await Helper();
			return new ThreeCamera(await helper.InvokeAsync<IJSObjectReference>("create", "PerspectiveCamera", fieldOfView, aspectRatio, nearClip, farClip), javaScript);
		}

		public async Task<ThreeRenderer> CreateRenderer()
		{
			var helper = await Helper();
			var renderer = new ThreeRenderer(await helper.InvokeAsync<IJSObjectReference>("create", "WebGLRenderer"), javaScript);
			await renderer.SetSize(400, 400);
			return renderer;
		}

		public async Task<ThreeBoxGeometry> CreateBoxGeometry(int width, int height, int depth)
		{
			var helper = await Helper();
			return new ThreeBoxGeometry(await helper.InvokeAsync<IJSObjectReference>("create", "BoxGeometry", width, height, depth), javaScript);
		}
		public async Task<ThreeMeshBasicMaterial> CreateMaterial(ThreeMeshMaterialOptions options)
		{
			var helper = await Helper();
			return new ThreeMeshBasicMaterial(await helper.InvokeAsync<IJSObjectReference>("create", "MeshBasicMaterial", options), javaScript);
		}
		public async Task<ThreeMesh> CreateMesh(ThreeGeometry geometry, ThreeMeshMaterial material)
		{
			var helper = await Helper();
			return new ThreeMesh(await helper.InvokeAsync<IJSObjectReference>("create", "Mesh", geometry.jsObject, material.jsObject), javaScript);
		}

		public async ValueTask RegisterAnimateFunction(Func<Task> action)
		{
			var executor = new InteropActionExecutor
			{
				Function = action
			};
			var reference = DotNetObjectReference.Create(executor);
			var helper = await Helper();
			await helper.InvokeVoidAsync("helperRequestAnimationFrame", reference, "Execute", true);
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