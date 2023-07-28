using System.Numerics;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ThreeNET.Data;

namespace ThreeNET.Objects
{
	public class ThreeWebGLRenderer : ThreeObjectWithReference
	{
		public ValueTask SetSize(Data.Vector2 size)
			=> SetSize(size.X, size.Y);

		public ValueTask SetSize(float width, float height)
			=> ObjectReference.InvokeVoidAsync("setSize", width, height);

		public ValueTask<Data.Vector2> GetSize()
			=> ObjectReference.InvokeAsync<Data.Vector2>("getSize");

		public  ValueTask Render(ThreeScene scene, ThreeCamera camera)
			=> ObjectReference.InvokeVoidAsync("render", scene.ObjectReference, camera.ObjectReference);

		public ValueTask PlaceInDOM(ElementReference element)
			=> HelperReference.InvokeVoidAsync("replaceDomElement", element, ObjectReference);
	}
}