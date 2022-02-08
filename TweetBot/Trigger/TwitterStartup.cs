using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using TweetBot.Trigger;

[assembly: WebJobsStartup(typeof(TwitterStartup))]

namespace TweetBot.Trigger
{
	/// <summary>
	/// IWebJobsStartup startup class
	/// </summary>
	public class TwitterStartup : IWebJobsStartup
	{
		public void Configure(IWebJobsBuilder builder)
		{
			builder.AddTwitter();
		}
	}
}