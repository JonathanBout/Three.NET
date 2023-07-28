using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeNET.Objects;

namespace ThreeNET.Data
{
	public abstract class CameraOptions
	{
		public static CameraOptions PerspectiveCamera(
			int fov,
			double aspect,
			double near,
			double far)
		{
			return new PerspectiveCameraOptions
			{
				FieldOfView = fov,
				AspectRatio = aspect,
				NearClip = near,
				FarClip = far,
			};
		}

		public static CameraOptions OrtographicCamera(
			double left,
			double right,
			double top,
			double bottom,
			double aspect,
			double near,
			double far)
		{
			return new OrtographicCameraOptions
			{
				Left = left,
				Right = right,
				Top = top,
				Bottom = bottom,
				AspectRatio = aspect,
				NearClip = near,
				FarClip = far,
			};
		}

		internal abstract Task<ThreeCamera> CreateCamera(ThreeJSInterop interop);
	}

	file class PerspectiveCameraOptions : CameraOptions
	{
		public int FieldOfView { get; set; }
		public double AspectRatio { get; set; }
		public double NearClip { get; set; }
		public double FarClip { get; set; }

		internal override async Task<ThreeCamera> CreateCamera(ThreeJSInterop interop)
		{
			return await interop.Create<ThreePerspectiveCamera>(FieldOfView, AspectRatio, NearClip, FarClip);
		}
	}

	file class OrtographicCameraOptions : CameraOptions
	{
		public double Left { get; set; }
		public double Right { get; set; }
		public double Top { get; set; }
		public double Bottom { get; set; }
		public double AspectRatio { get; set; }
		public double NearClip { get; set; }
		public double FarClip { get; set; }

		internal override async Task<ThreeCamera> CreateCamera(ThreeJSInterop interop)
		{
			return await interop.Create<ThreeOrtographicCamera>(Right, Left, Top, Bottom, AspectRatio, NearClip, FarClip);
		}
	}
}
