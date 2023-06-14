using Microsoft.JSInterop;
using ThreeNET.Objects;

namespace ThreeNET.Objects.Meshes
{
	internal class ThreeMesh : ThreeWorldObject
	{
		public ThreeMesh(IJSObjectReference jsObject, IJSRuntime js)
			: base(jsObject, js) { }
	}
}
