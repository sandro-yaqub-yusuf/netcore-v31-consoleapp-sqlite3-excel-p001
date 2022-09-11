using Microsoft.Extensions.DependencyInjection;
using KITAB.Products.Application.Notificators;
using KITAB.Products.Application.Products;
using KITAB.Products.Infra.Excel;
using KITAB.Products.Infra.Products;

namespace KITAB.Products.ConsoleApp
{
    class Program
    {
        public static void Main()
        {
            // Create service collection and configure our services
            var services = ConfigureServices();

            // Generate a provider
            var serviceProvider = services.BuildServiceProvider();

            // Kick off our actual code
            serviceProvider.GetService<ConsoleApplication>().Run();
        }

        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddScoped<INotificatorService, NotificatorService>();
            services.AddScoped<IExportedProductService, ExportedProductService>();
            services.AddScoped<IExportedProductRepository, ExportedProductRepository>();
            services.AddScoped<IImportedProductService, ImportedProductService>();
            services.AddScoped<IImportedProductRepository, ImportedProductRepository>();
            services.AddScoped<IExcelRepository, ExcelRepository>();

            // IMPORTANT: Register our application entry point
            services.AddScoped<ConsoleApplication>();

            return services;
        }
    }
}
