using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Host.Triggers;

namespace TweetBot.Trigger
{
	public class TwitterTriggerBindingProvider : ITriggerBindingProvider
	{
		private readonly TwitterExtensionsConfigProvider _twitterExtensionsConfigProvider;

		public TwitterTriggerBindingProvider(TwitterExtensionsConfigProvider twitterExtensionsConfigProvider) =>
			_twitterExtensionsConfigProvider = twitterExtensionsConfigProvider;

		public Task<ITriggerBinding> TryCreateAsync(TriggerBindingProviderContext context)
		{
			var attribute = context.Parameter.GetCustomAttribute<TwitterTriggerAttribute>(false);

			return Task.FromResult<ITriggerBinding>(attribute is null
				? default!
				: new TwitterTriggerBinding(_twitterExtensionsConfigProvider.CreateTriggerContext(attribute)));
		}
	}
}