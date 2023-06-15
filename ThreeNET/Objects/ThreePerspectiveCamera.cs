using ThreeNET.Data;
using Microsoft.JSInterop;
using Microsoft.JSInterop.Implementation;

namespace ThreeNET.Objects
{
	internal class ThreePerspectiveCamera : ThreeWorldObject
	{
		public ThreePerspectiveCamera(IJSObjectReference jsObject, IJSRuntime js) : base(jsObject, js)
		{
		}
	}
}