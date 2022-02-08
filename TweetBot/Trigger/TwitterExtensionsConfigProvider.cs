using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Extensions.Configuration;
using TweetBot.TweetInvi;

namespace TweetBot.Trigger
{
	public class TwitterExtensionsConfigProvider : IExtensionConfigProvider
	{
		private readonly TwitterClientFactory _twitterClientFactory;

		private readonly IConfiguration _configuration;

		public TwitterExtensionsConfigProvider(
			TwitterClientFactory twitterClientFactory,
			IConfiguration configuration)
		{
			_twitterClientFactory = twitterClientFactory;
			_configuration = configuration;
		}

		public TwitterTriggerContext CreateTriggerContext(TwitterTriggerAttribute triggerAttribute)
		{
			return new TwitterTriggerContext(triggerAttribute, _twitterClientFactory.CreateClient(
				GetValueOrSecretFromConfig(triggerAttribute.ConsumerKey),
				GetValueOrSecretFromConfig(triggerAttribute.ConsumerSecret),
				GetValueOrSecretFromConfig(triggerAttribute.AccessToken),
				GetValueOrSecretFromConfig(triggerAttribute.AccessTokenSecret)));
		}

		public void Initialize(ExtensionConfigContext context)
		{
			var triggerRule = context.AddBindingRule<TwitterTriggerAttribute>();
			triggerRule.BindToTrigger(new TwitterTriggerBindingProvider(this));
		}

		private string GetValueOrSecretFromConfig(string value)
		{
			if (value.StartsWith("%") && value.EndsWith("%"))
			{
				return _configuration[value[1..^1]];
			}

			return value;
		}
	}
}