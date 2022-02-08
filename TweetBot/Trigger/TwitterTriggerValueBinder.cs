using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Tweetinvi.Models.V2;

namespace TweetBot.Trigger
{
	public class TwitterTriggerValueBinder : IValueBinder
	{
		public Type Type => typeof(TweetV2);

		private object _value;

		public TwitterTriggerValueBinder(object value) => _value = value;

		public Task<object> GetValueAsync()
		{
			return Task.FromResult(_value);
		}

		public string ToInvokeString()
		{
			return _value.ToString() ?? "";
		}

		public Task SetValueAsync(object value, CancellationToken cancellationToken)
		{
			_value = value;
			return Task.CompletedTask;
		}
	}
}