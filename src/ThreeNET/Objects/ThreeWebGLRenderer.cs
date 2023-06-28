using System.Numerics;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ThreeNET.Data;

namespace ThreeNET.Objects
{
	public class ThreeWebGLRenderer : ThreeObjectWithReference
	{
		public async ValueTask SetSize(ThreeVector2 size)
		{
		 	await ObjectReference.InvokeVoidAsync("setSize", size.X, size.Y);
		}

		public async ValueTask<ThreeVector2> GetSize()
		{
			return await ObjectReference.InvokeAsync<ThreeVector2>("getSize");
		}

		public async ValueTask Render(ThreeScene scene, ThreePerspectiveCamera camera)
		{
			await ObjectReference.InvokeVoidAsync("render", scene.ObjectReference, camera.ObjectReference);
		}

		public async ValueTask PlaceInDOM(ElementReference element)
		{
			await HelperReference.InvokeVoidAsync("replaceDomElement", element, ObjectReference);
		}
	}
}