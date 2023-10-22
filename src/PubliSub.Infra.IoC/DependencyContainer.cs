using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PubliSub.Banking.Application.Interfaces;
using PubliSub.Banking.Application.Services;
using PubliSub.Banking.Data.Context;
using PubliSub.Banking.Data.Repository;
using PubliSub.Banking.Domain.Interfaces;
using PubliSub.Domain.Core.Bus;
using PubliSub.Infra.Bus;

namespace PubliSub.Infra.IoC
{
    public static class DependencyContainer
    {
        public static void AddPubliSubDependencyGroup(this IServiceCollection services)
        {
            services.AddTransient<IEventBus, RabbitMQBus>();

            services.AddTransient<IAccountService, AccountService>();

            services.AddTransient<IAccountRepository, AccoutRepository>();
            services.AddTransient<BankingDbContext>();
        }
    }
}
