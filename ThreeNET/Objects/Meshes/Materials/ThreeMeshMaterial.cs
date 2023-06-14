﻿using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeNET.Objects.Meshes.Materials
{
	internal abstract class ThreeMeshMaterial : ThreeObject
	{
		public ThreeMeshMaterial(IJSObjectReference jsObject, IJSRuntime js) : base(jsObject, js)
		{
		}
	}

	internal class ThreeMeshBasicMaterial : ThreeMeshMaterial
	{
		public ThreeMeshBasicMaterial(IJSObjectReference jsObject, IJSRuntime js) : base(jsObject, js)
		{
		}
	}
}
