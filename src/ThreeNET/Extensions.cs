using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeNET
{
	internal static class Extensions
	{
		public static ValueTask DisposeOrDefaultAsync(this IAsyncDisposable? asyncDisposable, ValueTask? defaultValue = null)
		{
			return asyncDisposable?.DisposeAsync() ?? defaultValue ?? ValueTask.CompletedTask;
		}
	}
}
