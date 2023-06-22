using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace ThreeNET.Objects
{
	public class ThreeWebGLRenderer : ThreeObjectWithReference
	{
		public async ValueTask SetSize(int width, int heigth)
		{
			await ObjectReference.InvokeVoidAsync("setSize", width, heigth);
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