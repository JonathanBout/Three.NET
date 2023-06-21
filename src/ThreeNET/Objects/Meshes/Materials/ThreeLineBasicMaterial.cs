using Microsoft.JSInterop;

namespace ThreeNET.Objects.Meshes.Materials
{
    internal class ThreeLineBasicMaterial : ThreeMeshMaterial
	{
		public ThreeLineBasicMaterial(IJSObjectReference jsObject, IJSRuntime js) : base(jsObject, js)
		{
		}
	}
}
