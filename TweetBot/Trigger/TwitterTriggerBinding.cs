using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Listeners;
using Microsoft.Azure.WebJobs.Host.Protocols;
using Microsoft.Azure.WebJobs.Host.Triggers;
using Tweetinvi.Models.V2;

namespace TweetBot.Trigger
{
	/// <summary>
	/// Creates listener and binds trigger data
	/// </summary>
	public class TwitterTriggerBinding : ITriggerBinding
	{
		public Type TriggerValueType => typeof(TweetV2);

		public IReadOnlyDictionary<string, Type> BindingDataContract => new Dictionary<string, Type>();

		private readonly TwitterTriggerContext _context;

		public TwitterTriggerBinding(TwitterTriggerContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Called by the runtime once an event is received, binding it to the respective trigger data
		/// </summary>
		public Task<ITriggerData> BindAsync(object value, ValueBindingContext context)
		{
			var valueBinder = new TwitterTriggerValueBinder(value);

			return Task.FromResult<ITriggerData>(new TriggerData(valueBinder, new Dictionary<string, object>()));
		}

		/// <summary>
		/// Called by the runtime to create a listener
		/// </summary>
		public Task<IListener> CreateListenerAsync(ListenerFactoryContext context)
		{
			return Task.FromResult<IListener>(new TwitterTriggerListener(context.Executor, _context));
		}

		/// <summary>
		/// Called by the runtime to create parameter descriptions
		/// </summary>
		public ParameterDescriptor ToParameterDescriptor()
		{
			return new TriggerParameterDescriptor
			{
				Name = "Twitter",
				DisplayHints = new ParameterDisplayHints
				{
					Prompt = "Twitter",
					Description = "Twitter trigger"
				}
			};
		}
	}
}