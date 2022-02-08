using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.DependencyInjection;
using TweetBot.TweetInvi;

namespace TweetBot.Trigger
{
	public static class TwitterTriggerExtensions
	{
		public static IWebJobsBuilder AddTwitter(this IWebJobsBuilder builder)
		{
			builder.AddExtension<TwitterExtensionsConfigProvider>();

			builder.Services.AddSingleton<TwitterClientFactory>();

			return builder;
		}
	}
}