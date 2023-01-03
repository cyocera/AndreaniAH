using ApiGeo.Data.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Plain.RabbitMQ;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace ApiGeo.Configurations
{
    public class DataCollector : IHostedService
    {

        private readonly ISubscriber subscriber;
        private readonly IServiceScopeFactory scopeFactory;

        public DataCollector(ISubscriber subscriber, IServiceScopeFactory scopeFactory)
        {
            this.subscriber = subscriber;
            this.scopeFactory = scopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            subscriber.Subscribe(ProcessMessage);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private bool ProcessMessage(string message, IDictionary<string, object> headers)
        {
            var result = true;
            try
            {
                using var sp = scopeFactory.CreateScope();
                var uow = sp.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var pmessage = JsonConvert.DeserializeObject<List<ProcessorMessage>>(message);
                if (pmessage.Count != 0)
                {
                    var info = pmessage.FirstOrDefault();
                    var model = uow.addressHistory.GetById(info.id).Result;
                    model.longitude = info.longitude;
                    model.latitud = info.latitude;
                    model.status = info.status;

                    uow.addressHistory.Update(model).Wait();
                    uow.Save();
                }

            }
            catch
            {
                result = false;
            }
            return result;
        }
    }
}
