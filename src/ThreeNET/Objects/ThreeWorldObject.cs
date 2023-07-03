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
		private const string SetPropertyFunction = "setProperty";

		public ValueTask SetRotation(float? x = null, float? y = null, float? z = null)
		{

			var (xNull, yNull, zNull) = ValuesCheck(x, y, z);


			if (!xNull && !yNull && !zNull)
				return SetProperty("rotation", new ThreeVector3(x!.Value, y!.Value, z!.Value));
			if (!xNull)
				return SetProperty("rotation.x", x);
			if (!yNull)
				return SetProperty("rotation.y", y);
			if (!zNull)
				return SetProperty("rotation.z", z);
			return ValueTask.CompletedTask;
		}

		public ValueTask SetPosition(float? x = null, float? y = null, float? z = null)
		{
			var (xNull, yNull, zNull) = ValuesCheck(x, y, z);

			if (!xNull && !yNull && !zNull)
				return SetProperty("position", new ThreeVector3(x!.Value, y!.Value, z!.Value));
			if (!xNull)
				return SetProperty("position.x", x);
			if (!yNull)
				return SetProperty("position.y", y);
			if (!zNull)
				return SetProperty("position.z", z);
			return ValueTask.CompletedTask;
		}

		public ValueTask SetScale(float? x = null, float? y = null, float? z = null)
		{
			var (xNull, yNull, zNull) = ValuesCheck(x, y, z);

			if (!xNull && !yNull && !zNull)
				return SetProperty("scale", new ThreeVector3(x!.Value, y!.Value, z!.Value));
			if (!xNull)
				return SetProperty("scale.x", x);
			if (!yNull)
				return SetProperty("scale.y", y);
			if (!zNull)
				return SetProperty("scale.z", z);
			return ValueTask.CompletedTask;
		}

		public ValueTask Rotate(ThreeVector3 rotation)
		{
			return ObjectReference.InvokeVoidAsync("rotateOnWorldAxis", rotation.Normalize(), rotation.Magnitude);
		}
		public ValueTask Translate(ThreeVector3 translation)
		{
			return ObjectReference.InvokeVoidAsync("translateOnAxis", translation.Normalize(), translation.Magnitude);
		}

		private ValueTask SetProperty(string propertyName, params object?[]? args)
		{
			return HelperReference.InvokeVoidAsync(SetPropertyFunction, ObjectReference, propertyName, args);
		}

		private static (bool xNull, bool yNull, bool zNull) ValuesCheck(double? x, double? y, double? z)
		{
			var xNull = x is null;
			var yNull = y is null;
			var zNull = z is null;
			if (xNull && yNull && zNull) throw new ArgumentException("You should specify at least one value.");
			return (xNull, yNull, zNull);
		}
	}
}
