using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Tweetinvi;

[assembly: FunctionsStartup(typeof(TweetBot.Startup))]

namespace TweetBot
{
	public class Startup : FunctionsStartup
	{
		public override void Configure(IFunctionsHostBuilder builder)
		{
			var configuration = builder.GetContext().Configuration;

			builder.Services.AddSingleton<ITwitterClient>(new TwitterClient(
				configuration["ConsumerKey"],
				configuration["ConsumerKeySecret"],
				configuration["AccessToken"],
				configuration["AccessTokenSecret"]));
		}
	}
}