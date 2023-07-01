using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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

		public static ValueTask GetAwaiterOrDefault(this ValueTask? task, ValueTask? defaultValue = null)
		{
			return task ?? defaultValue ?? ValueTask.CompletedTask;
		}

		public static async Task GetAwaiterOrDefault(this Task? task, Task? defaultValue = null)
		{
			await (task ?? defaultValue ?? Task.CompletedTask);
		}
	}
}
