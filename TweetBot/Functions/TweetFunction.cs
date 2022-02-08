using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using TweetBot.Trigger;
using Tweetinvi.Models.V2;

namespace TweetBot.Functions
{
	public static class TweetFunction
	{
		[FunctionName("HandleTweet")]
		public static async Task HandleTweet([TwitterTrigger("dotnet", "%ConsumerKey%", "%ConsumerKeySecret%", "%AccessToken%", "%AccessTokenSecret%")] TweetV2 tweet)
		{
		}
	}
}