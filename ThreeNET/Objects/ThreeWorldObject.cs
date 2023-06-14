using ThreeNET.Data;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeNET.Objects
{
	internal abstract class ThreeWorldObject : ThreeObject
	{
		public ThreeWorldObject(IJSObjectReference jsObject, IJSRuntime js)
			: base(jsObject, js)
		{
		}
		const string setPropertyFunction = "setProperty";

		public async ValueTask SetRotation(double? x = null, double? y = null, double? z = null)
		{
			var helper = await Helper();

			var (xNull, yNull, zNull) = ValuesCheck(x, y, z);


			if (!xNull && !yNull && !zNull)
				await helper.InvokeVoidAsync(setPropertyFunction, jsObject, "rotation", new ThreeVector3(x!.Value, y!.Value, z!.Value));
			else
			{
				if (!xNull)
					await helper.InvokeVoidAsync(setPropertyFunction, jsObject, "rotation.x", x);
				if (!yNull)
					await helper.InvokeVoidAsync(setPropertyFunction, jsObject, "rotation.y", y);
				if (!zNull)
					await helper.InvokeVoidAsync(setPropertyFunction, jsObject, "rotation.z", z);
			}
		}
		public async ValueTask SetPosition(double? x = null, double? y = null, double? z = null)
		{
			var helper = await Helper();

			var (xNull, yNull, zNull) = ValuesCheck(x, y, z);

			if (!xNull && !yNull && !zNull)
				await helper.InvokeVoidAsync(setPropertyFunction, jsObject, "position", new ThreeVector3(x!.Value, y!.Value, z!.Value));
			else
			{
				if (!xNull)
					await helper.InvokeVoidAsync(setPropertyFunction, jsObject, "position.x", x);
				if (!yNull)
					await helper.InvokeVoidAsync(setPropertyFunction, jsObject, "position.y", y);
				if (!zNull)
					await helper.InvokeVoidAsync(setPropertyFunction, jsObject, "position.z", z);
			}
		}
		public async ValueTask SetScale(double? x = null, double? y = null, double? z = null)
		{
			var helper = await Helper();

			var (xNull, yNull, zNull) = ValuesCheck(x, y, z);

			if (!xNull && !yNull && !zNull)
				await helper.InvokeVoidAsync(setPropertyFunction, jsObject, "scale", new ThreeVector3(x!.Value, y!.Value, z!.Value));
			else
			{
				if (!xNull)
					await helper.InvokeVoidAsync(setPropertyFunction, jsObject, "scale.x", x);
				if (!yNull)
					await helper.InvokeVoidAsync(setPropertyFunction, jsObject, "scale.y", y);
				if (!zNull)
					await helper.InvokeVoidAsync(setPropertyFunction, jsObject, "scale.z", z);
			}
		}

		public async ValueTask Rotate(double? x = null, double? y = null, double? z = null)
		{
			var (xNull, yNull, zNull) = ValuesCheck(x, y, z);

			if (!xNull)
				await jsObject.InvokeVoidAsync("rotateX", x);
			if (!yNull)
				await jsObject.InvokeVoidAsync("rotateY", y);
			if (!zNull)
				await jsObject.InvokeVoidAsync("rotateZ", z);
		}
		public async ValueTask Translate(double? x = null, double? y = null, double? z = null)
		{
			var (xNull, yNull, zNull) = ValuesCheck(x, y, z);

			if (!xNull)
				await jsObject.InvokeVoidAsync("translateX", x);
			if (!yNull)
				await jsObject.InvokeVoidAsync("translateY", y);
			if (!zNull)
				await jsObject.InvokeVoidAsync("translateZ", z);
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
