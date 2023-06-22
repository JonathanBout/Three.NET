using Microsoft.JSInterop;

namespace ThreeNET
{
	public abstract class ThreeHelperReferenceHolder : IAsyncDisposable
	{
		private readonly Lazy<Task<IJSObjectReference>> threeJSHelperTask;
		public Task<IJSObjectReference> Helper()
		{
			return threeJSHelperTask.Value;
		}

		public ThreeHelperReferenceHolder(IJSRuntime jsRuntime)
		{
			threeJSHelperTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
				"import", "./_content/ThreeNET/threeHelpers.js").AsTask());
		}

		public async ValueTask DisposeAsync()
		{
			await DisposeAsyncCore().ConfigureAwait(false);

			GC.SuppressFinalize(this);
		}

		protected virtual async ValueTask DisposeAsyncCore()
		{
			if (threeJSHelperTask.IsValueCreated)
				await (await threeJSHelperTask.Value).DisposeAsync().ConfigureAwait(false);
		}
	}
}