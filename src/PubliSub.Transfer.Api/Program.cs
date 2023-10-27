using Microsoft.EntityFrameworkCore;
using PubliSub.Domain.Core.Bus;
using PubliSub.Infra.IoC;
using PubliSub.Transfer.Data.Context;
using PubliSub.Transfer.Domain.EventHandlers;
using PubliSub.Transfer.Domain.Events;
using System.Reflection;

namespace PubliSub.Transfer.Api
{

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("TransferDbConnection");

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddPubliSubDependencyGroup();
            builder.Services.AddDbContext<TransferDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                using (var scope = app.Services.CreateScope())
                {
                    var transferDbContext = scope.ServiceProvider.GetRequiredService<TransferDbContext>();
                    transferDbContext.Database.EnsureCreated();
                    transferDbContext.Seed();
                   
                }
            }

            ConfigureEventBus(app);

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        private static void ConfigureEventBus(WebApplication app)
        {
            var eventBus = app.Services.GetRequiredService<IEventBus>();
            eventBus.Subscribe<TransferCreatedEvent, TransferEventHandler>();
        }
    }
}