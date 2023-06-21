using Microsoft.JSInterop;

namespace ThreeNET.Objects.Geometry
{
	internal class ThreeBoxGeometry : ThreeGeometry
	{
		public ThreeBoxGeometry(IJSObjectReference jsObject, IJSRuntime js)
			: base(jsObject, js) { }
	}
}
