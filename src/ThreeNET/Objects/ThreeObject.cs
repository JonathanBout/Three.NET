using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeNET;

namespace ThreeNET.Objects
{
	public abstract class ThreeObject : IAsyncDisposable
	{
		public IJSObjectReference ObjectReference { get; private set; } = null!;

		public async ValueTask DisposeAsync()
		{
			await DisposeAsyncCore();
			await ObjectReference.DisposeAsync();

			if (this is ThreeObjectWithReference referenceHolder)
			{
				await referenceHolder.HelperReference.DisposeAsync();
			}

			GC.SuppressFinalize(this);
		}

		public virtual ValueTask DisposeAsyncCore() => ValueTask.CompletedTask;
		public virtual void SetObjectReference(IJSObjectReference objectReference)
		{
			ObjectReference = objectReference;
		}
	}

	public abstract class ThreeObjectWithReference : ThreeObject
	{
		internal IJSObjectReference HelperReference { get; set; } = null!;
	}
}
