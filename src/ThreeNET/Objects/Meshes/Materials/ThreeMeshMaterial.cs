using Microsoft.JSInterop;

namespace ThreeNET.Objects.Meshes.Materials
{
    internal abstract class ThreeMeshMaterial : ThreeObject
	{
		public ThreeMeshMaterial(IJSObjectReference jsObject, IJSRuntime js) : base(jsObject, js)
		{
		}
	}
}
