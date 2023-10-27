using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;
using PubliSub.Banking.Application.Interfaces;
using PubliSub.Banking.Application.Services;
using PubliSub.Banking.Data.Context;
using PubliSub.Banking.Data.Repository;
using PubliSub.Banking.Domain.CommandHandlers;
using PubliSub.Banking.Domain.Commands;
using PubliSub.Banking.Domain.Interfaces;
using PubliSub.Domain.Core.Bus;
using PubliSub.Infra.Bus;
using PubliSub.Transfer.Application.Interfaces;
using PubliSub.Transfer.Application.Services;
using PubliSub.Transfer.Data.Context;
using PubliSub.Transfer.Data.Repository;
using PubliSub.Transfer.Domain.EventHandlers;
using PubliSub.Transfer.Domain.Events;
using PubliSub.Transfer.Domain.Interfaces;
using RabbitMQ.Client;

namespace PubliSub.Infra.IoC
{
    public static class DependencyContainer
    {
        public static void AddPubliSubDependencyGroup(this IServiceCollection services, IConfiguration config)
        {

            //Domain.Bus
            services.AddSingleton(serviceprovider =>
            {
                var rabbitSettings = new RabbitMQSettings();
                config.GetSection($"RabbitMQ").Bind(rabbitSettings);
                return new ConnectionFactory
                {
                    HostName = rabbitSettings.HostName,
                    Password = rabbitSettings.Password,
                    UserName = rabbitSettings.UserName,
                };
            });
            services.AddSingleton<IEventBus, RabbitMQBus>(sp =>
            {
                var scopeFacatory = sp.GetRequiredService<IServiceScopeFactory>();
                var scopeConnection = sp.GetRequiredService<ConnectionFactory>();
                return new RabbitMQBus(sp.GetService<IMediator>(), scopeFacatory, scopeConnection);
            });

            //Subscriptions
            services.AddTransient<TransferEventHandler>();

            //Application Services
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<ITransferService, TransferService>();

            //Data
            services.AddTransient<IAccountRepository, AccoutRepository>();
            services.AddTransient<ITransferRepository, TransferRepository>();
            services.AddTransient<BankingDbContext>();
            services.AddTransient<TransferDbContext>();

            //Domain Events
            services.AddTransient<IEventHandler<TransferCreatedEvent>, TransferEventHandler>();

            //Domain Banking Comands
            services.AddTransient<IRequestHandler<CreateTransferCommand, bool>, TransferCommandHandler>();

            
        }
    }
}
