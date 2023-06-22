using ThreeNET.Objects.Geometry;
using Microsoft.JSInterop;
using ThreeNET.Objects.Meshes;

namespace ThreeNET.Objects
{
	public class ThreeScene : ThreeObject
	{
		public async Task Add(ThreeMesh geometry)
		{
			await ObjectReference.InvokeVoidAsync("add", geometry.ObjectReference);
		}
	}
}