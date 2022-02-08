using Tweetinvi;

namespace TweetBot.Trigger
{
	public record TwitterTriggerContext(TwitterTriggerAttribute TwitterTriggerAttribute, ITwitterClient TwitterClient);
}