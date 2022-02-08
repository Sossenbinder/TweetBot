using System;
using Microsoft.Azure.WebJobs.Description;

namespace TweetBot.Trigger
{
	/// <summary>
	/// Attribute placed on a function, providing connection information etc.
	/// </summary>
	[AttributeUsage(AttributeTargets.Parameter)]
	[Binding]
	public class TwitterTriggerAttribute : Attribute
	{
		public string HashTag { get; set; }

		public string ConsumerKey { get; set; }

		public string ConsumerSecret { get; set; }

		public string AccessToken { get; set; }

		public string AccessTokenSecret { get; set; }

		public TwitterTriggerAttribute(
			string hashTag,
			string consumerKey,
			string consumerSecret,
			string accessToken,
			string accessTokenSecret)
		{
			HashTag = hashTag;
			ConsumerKey = consumerKey;
			ConsumerSecret = consumerSecret;
			AccessToken = accessToken;
			AccessTokenSecret = accessTokenSecret;
		}
	}
}