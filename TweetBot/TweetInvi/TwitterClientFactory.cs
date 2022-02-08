using Tweetinvi;

namespace TweetBot.TweetInvi
{
	public class TwitterClientFactory
	{
		public ITwitterClient CreateClient(string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret) => new TwitterClient(consumerKey, consumerSecret, accessToken, accessTokenSecret);
	}
}