using ThreeNET.Objects.Geometry;
using Microsoft.JSInterop;
using ThreeNET.Objects.Meshes;

namespace ThreeNET.Objects
{
	internal class ThreeScene : ThreeObject
	{
		public ThreeScene(IJSObjectReference jsObject, IJSRuntime js)
			: base(jsObject, js)
		{
		}

		public async Task Add(ThreeMesh geometry)
		{
			await jsObject.InvokeVoidAsync("add", geometry.jsObject);
		}
	}
}