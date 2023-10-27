namespace PubliSub.Banking.Api
{
    using Microsoft.EntityFrameworkCore;
    using PubliSub.Banking.Data.Context;
    using PubliSub.Infra.IoC;
    using System.Reflection;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("BankingDbConnection");

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddPubliSubDependencyGroup();
            builder.Services.AddDbContext<BankingDbContext>(options =>
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
                    var bankingDbContext = scope.ServiceProvider.GetRequiredService<BankingDbContext>();
                    bankingDbContext.Database.EnsureCreated();
                   // bankingDbContext.Seed();
                }
            }

            app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();
        }
    }
}