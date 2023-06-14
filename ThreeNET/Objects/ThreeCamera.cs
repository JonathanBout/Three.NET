using ThreeNET.Data;
using Microsoft.JSInterop;
using Microsoft.JSInterop.Implementation;

namespace ThreeNET.Objects
{
	internal class ThreeCamera : ThreeWorldObject
	{
		public ThreeCamera(IJSObjectReference jsObject, IJSRuntime js) : base(jsObject, js)
		{
		}
	}
}