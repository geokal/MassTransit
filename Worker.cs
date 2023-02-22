using System;
using System.Threading;
using System.Threading.Tasks;
using Contracts;
using MassTransit;
using Microsoft.Extensions.Hosting;

namespace MassTransit
{
    public class Worker : BackgroundService
    {
        private readonly IBus _bus;

        public Worker(IBus bus)
        {
            _bus = bus;
        }

        protected override async Task ExecuteAsync(CancellationToken stopToken)
        {
            while (!stopToken.IsCancellationRequested)
            {
                await _bus.Publish(new HelloMessage
                {
                    Value = $"The time is {DateTimeOffset.Now}",
                }, stopToken);

                await Task.Delay(1000, stopToken);
            }
        }
    }
}