using ThreeNET.Data;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeNET.Objects
{
	public abstract class ThreeWorldObject : ThreeObjectWithReference
	{
		const string setPropertyFunction = "setProperty";

		public async ValueTask SetRotation(float? x = null, float? y = null, float? z = null)
		{

			var (xNull, yNull, zNull) = ValuesCheck(x, y, z);


			if (!xNull && !yNull && !zNull)
				await HelperReference.InvokeVoidAsync(setPropertyFunction, ObjectReference, "rotation", new ThreeVector3(x!.Value, y!.Value, z!.Value));
			else
			{
				if (!xNull)
					await HelperReference.InvokeVoidAsync(setPropertyFunction, ObjectReference, "rotation.x", x);
				if (!yNull)
					await HelperReference.InvokeVoidAsync(setPropertyFunction, ObjectReference, "rotation.y", y);
				if (!zNull)
					await HelperReference.InvokeVoidAsync(setPropertyFunction, ObjectReference, "rotation.z", z);
			}
		}
		public async ValueTask SetPosition(float? x = null, float? y = null, float? z = null)
		{
			var (xNull, yNull, zNull) = ValuesCheck(x, y, z);

			if (!xNull && !yNull && !zNull)
				await HelperReference.InvokeVoidAsync(setPropertyFunction, ObjectReference, "position", new ThreeVector3(x!.Value, y!.Value, z!.Value));
			else
			{
				if (!xNull)
					await HelperReference.InvokeVoidAsync(setPropertyFunction, ObjectReference, "position.x", x);
				if (!yNull)
					await HelperReference.InvokeVoidAsync(setPropertyFunction, ObjectReference, "position.y", y);
				if (!zNull)
					await HelperReference.InvokeVoidAsync(setPropertyFunction, ObjectReference, "position.z", z);
			}
		}
		public async ValueTask SetScale(float? x = null, float? y = null, float? z = null)
		{
			var (xNull, yNull, zNull) = ValuesCheck(x, y, z);

			if (!xNull && !yNull && !zNull)
				await HelperReference.InvokeVoidAsync(setPropertyFunction, ObjectReference, "scale", new ThreeVector3(x!.Value, y!.Value, z!.Value));
			else
			{
				if (!xNull)
					await HelperReference.InvokeVoidAsync(setPropertyFunction, ObjectReference, "scale.x", x);
				if (!yNull)
					await HelperReference.InvokeVoidAsync(setPropertyFunction, ObjectReference, "scale.y", y);
				if (!zNull)
					await HelperReference.InvokeVoidAsync(setPropertyFunction, ObjectReference, "scale.z", z);
			}
		}

		public async ValueTask Rotate(float? x = null, float? y = null, float? z = null)
		{
			var (xNull, yNull, zNull) = ValuesCheck(x, y, z);

			if (!xNull)
				await ObjectReference.InvokeVoidAsync("rotateX", x);
			if (!yNull)
				await ObjectReference.InvokeVoidAsync("rotateY", y);
			if (!zNull)
				await ObjectReference.InvokeVoidAsync("rotateZ", z);
		}
		public async ValueTask Translate(float? x = null, float? y = null, float? z = null)
		{
			var (xNull, yNull, zNull) = ValuesCheck(x, y, z);

			if (!xNull)
				await ObjectReference.InvokeVoidAsync("translateX", x);
			if (!yNull)
				await ObjectReference.InvokeVoidAsync("translateY", y);
			if (!zNull)
				await ObjectReference.InvokeVoidAsync("translateZ", z);
		}

		static (bool xNull, bool yNull, bool zNull) ValuesCheck(double? x, double? y, double? z)
		{
			var xNull = x is null;
			var yNull = y is null;
			var zNull = z is null;
			if (xNull && yNull && zNull) throw new InvalidOperationException("You should specify at least one value.");
			return (xNull, yNull, zNull);
		}
	}
}
