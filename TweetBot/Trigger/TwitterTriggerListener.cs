using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Host.Executors;
using Microsoft.Azure.WebJobs.Host.Listeners;
using Tweetinvi;
using Tweetinvi.Iterators;
using Tweetinvi.Models.V2;

namespace TweetBot.Trigger
{
	/// <summary>
	/// Listener listening to twitter events, and triggering the function executor once something happens respectively
	/// </summary>
	public class TwitterTriggerListener : IListener
	{
		private readonly ITriggeredFunctionExecutor _executor;

		private readonly ITwitterClient _twitterClient;

		private readonly string _hashTag;

		private CancellationTokenSource _cts = default!;

		public TwitterTriggerListener(
			ITriggeredFunctionExecutor executor,
			TwitterTriggerContext context)
		{
			_executor = executor;

			var (triggerAttribute, twitterClient) = context;
			_twitterClient = twitterClient;
			_hashTag = triggerAttribute.HashTag;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			_cts = new CancellationTokenSource();
			// ReSharper disable once MethodSupportsCancellation
			_ = Task.Run(() => RunListener(_cts.Token));

			return Task.CompletedTask;
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			_cts.Cancel();
			return Task.CompletedTask;
		}

		public void Cancel()
		{
			_cts.Cancel();
		}

		public void Dispose()
		{
		}

		private async Task RunListener(CancellationToken token)
		{
			while (!token.IsCancellationRequested)
			{
				var iterator = _twitterClient.SearchV2.GetSearchTweetsV2Iterator($"#{_hashTag}");
				while (!iterator.Completed)
				{
					var page = await iterator.NextPageAsync();
					var tweets = page.Content.Tweets;

					await Task.WhenAll(tweets.Select(x => _executor.TryExecuteAsync(new TriggeredFunctionData
					{
						TriggerValue = x
					}, token)));
				}
			}
		}
	}
}