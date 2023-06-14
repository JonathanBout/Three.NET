using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeNET.Objects.Geometry
{
	internal abstract class ThreeGeometry : ThreeObject
	{
		public ThreeGeometry(IJSObjectReference jsObject, IJSRuntime js)
			: base(jsObject, js) { }
	}
}
