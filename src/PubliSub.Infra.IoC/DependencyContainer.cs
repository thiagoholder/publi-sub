using Microsoft.Extensions.DependencyInjection;
using PubliSub.Domain.Core.Bus;
using PubliSub.Infra.Bus;

namespace PubliSub.Infra.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IEventBus, RabbitMQBus>();
        }
    }
}
