using Microsoft.JSInterop;

namespace ThreeNET
{
	abstract class ThreeHelperReferenceHolder : IAsyncDisposable
	{
		private readonly Lazy<Task<IJSObjectReference>> __threeJSHelperTask;
		public Task<IJSObjectReference> Helper()
		{
			return __threeJSHelperTask.Value;
		}

		public ThreeHelperReferenceHolder(IJSRuntime jsRuntime)
		{
			__threeJSHelperTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
				"import", "./_content/ThreeNET/threeHelpers.js").AsTask());
		}

		public async ValueTask DisposeAsync()
		{
			await DisposeAsyncCore().ConfigureAwait(false);

			GC.SuppressFinalize(this);
		}

		protected virtual async ValueTask DisposeAsyncCore()
		{
			if (__threeJSHelperTask.IsValueCreated)
				await (await __threeJSHelperTask.Value).DisposeAsync().ConfigureAwait(false);
		}
	}
}