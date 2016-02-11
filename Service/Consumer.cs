using System;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Logging;
using Contract;

namespace Service
{
	public class Consumer : IConsumer<IRequest>
	{
		private readonly ILog log = Logger.Get<Consumer>();
		private readonly Random rand = new Random();

		public async Task Consume(ConsumeContext<IRequest> context)
		{
			var number = context.Message.Number;

			this.log.InfoFormat("{0} received", number);

			var newNumber = rand.Next(1, number);

			this.log.InfoFormat("Returning {0}...", newNumber);

			await context.RespondAsync(new Response { NewNumber = newNumber });
		}

		class Response : IResponse
		{
			public int NewNumber { get; set; }
		}
	}
}
