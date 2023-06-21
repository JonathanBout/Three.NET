using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeNET;

namespace ThreeNET.Objects
{
	internal abstract class ThreeObject : ThreeHelperReferenceHolder
	{
		internal readonly IJSObjectReference jsObject;

		protected ThreeObject(IJSObjectReference jsObject, IJSRuntime js)
			: base(js)
		{
			this.jsObject = jsObject;
		}
	}
}
