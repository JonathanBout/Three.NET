using Microsoft.Extensions.DependencyInjection;

namespace ThreeNET
{
	public static class ThreeNETExtensions
	{
		public static IServiceCollection AddThreeNET(this IServiceCollection serviceCollection)
		{
			return serviceCollection.AddScoped<ThreeNET>();
		}
	}
}