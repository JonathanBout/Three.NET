﻿using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace ThreeNET.Objects
{
	internal class ThreeWebGLRenderer : ThreeObject
	{
		public ThreeWebGLRenderer(IJSObjectReference jsObject, IJSRuntime jsRuntime)
			: base(jsObject, jsRuntime)
		{
		}

		public async ValueTask SetSize(int width, int heigth)
		{
			await jsObject.InvokeVoidAsync("setSize", width, heigth);
		}

		public async ValueTask Render(ThreeScene scene, ThreePerspectiveCamera camera)
		{
			await jsObject.InvokeVoidAsync("render", scene.jsObject, camera.jsObject);
		}

		public async ValueTask PlaceInDOM(ElementReference element)
		{
			var helper = await Helper();
			await helper.InvokeVoidAsync("replaceDomElement", element, jsObject);
		}
	}
}