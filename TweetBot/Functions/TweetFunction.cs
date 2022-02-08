using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Polly;
using Polly.Retry;
using TweetBot.Trigger;
using Tweetinvi;
using Tweetinvi.Exceptions;
using Tweetinvi.Models.V2;

namespace TweetBot.Functions
{
	public class TweetFunction
	{
		private readonly ITwitterClient _twitterClient;

		private static readonly AsyncRetryPolicy _retryPolicy = Policy
			.Handle<TwitterException>()
			.WaitAndRetryAsync(new[]
			{
				TimeSpan.FromSeconds(5),
				TimeSpan.FromSeconds(15),
				TimeSpan.FromSeconds(30),
			});

		public TweetFunction(ITwitterClient twitterClient)
		{
			_twitterClient = twitterClient;
		}

		[FunctionName("HandleTweet")]
		public async Task HandleTweet(
			[TwitterTrigger("dotnet", "%ConsumerKey%", "%ConsumerKeySecret%", "%AccessToken%", "%AccessTokenSecret%")] TweetV2 tweet)
		{
			await _retryPolicy.ExecuteAsync(() => _twitterClient.Tweets.PublishRetweetAsync(long.Parse(tweet.Id)));
		}
	}
}