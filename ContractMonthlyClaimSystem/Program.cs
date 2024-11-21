using ContractMonthlyClaimSystem.Data;
using Microsoft.AspNetCore.Hosting;

namespace ContractMonthlyClaimSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // Seed database (optional)
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<ApplicationDbContext>();
            context.Database.EnsureCreated();

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<StartUp>(); });
    }
}
