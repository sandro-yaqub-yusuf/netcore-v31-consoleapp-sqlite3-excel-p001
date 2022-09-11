using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using AutoMapper;
using KITAB.Products.Application.Notificators;
using KITAB.Products.Application.Products;
using KITAB.Products.ConsoleApp.Configurations;

namespace KITAB.Products.ConsoleApp
{
    public class ConsoleApplication
    {
        private readonly INotificatorService _notificatorService;
        private readonly IExportedProductService _exportedProductService;
        private readonly IImportedProductService _importedProductService;
        private readonly IMapper _mapper;

        public ConsoleApplication(INotificatorService notificatorService, IExportedProductService exportedProductService, IImportedProductService importedProductService, IMapper mapper)
        {
            _notificatorService = notificatorService;
            _exportedProductService = exportedProductService;
            _importedProductService = importedProductService;
            _mapper = mapper;
        }

        public void Run()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile($"appsettings.json");

            var configuration = builder.Build();

            var excelConfigurations = new ExcelConfigurations();

            new ConfigureFromConfigurationOptions<ExcelConfigurations>(configuration.GetSection("ExcelConfigurations")).Configure(excelConfigurations);

            Console.WriteLine("Iniciando o processamento...");
            Console.WriteLine("");

            while (true)
            {
                if (!Produtos_Exportar_Tabela(ref excelConfigurations)) break;
                if (!Produtos_Importar_Tabela(ref excelConfigurations)) break;

                break;
            }

            // Verifica se há notificações vinda da camada de negócio
            // Caso tenha notificações, devem ser exibidas ao usuário
            if (_notificatorService.HaveNotification()) Listar_Notificacoes();

            Console.WriteLine("Processo concluído...");
            Console.ReadKey(true);
        }

        private bool Produtos_Exportar_Tabela(ref ExcelConfigurations p_excelConfigurations)
        {
            Console.WriteLine("Listando todos os produtos da tabela de Exportação de Produtos...");
            Console.WriteLine("");

            // Pega todos os produtos na tabela "ExportedProduct"
            var products = _exportedProductService.GetAll();
            var fileName = p_excelConfigurations.FileName;

            if (!_notificatorService.HaveNotification())
            {
                foreach (var product in products)
                {
                    Console.WriteLine("ID: " + product.Id + " - Produto: " + product.Description + " - " +
                                      "Quantidade: " + product.Inventory + " - Preço de Venda: " + product.SalePrice);
                }

                Console.WriteLine("");
                Console.WriteLine("Exportando todos os produtos para o arquivo [products.xlsx]...");
                Console.WriteLine("");

                _exportedProductService.ExportToEXCEL(ref products, ref fileName);
            }

            return (!_notificatorService.HaveNotification());
        }

        private bool Produtos_Importar_Tabela(ref ExcelConfigurations p_excelConfigurations)
        {
            Console.WriteLine("Importando todos os produtos para do arquivo [products.xlsx] para a tabela...");
            Console.WriteLine("");

            var fileName = p_excelConfigurations.FileName;

            _importedProductService.ImportFromEXCEL(ref fileName);

            Console.WriteLine("Listando todos os produtos da tabela de Importação de Produtos...");
            Console.WriteLine("");

            // Pega todos os produtos na tabela "ImportedProduct"
            var products = _importedProductService.GetAll();

            if (!_notificatorService.HaveNotification())
            {
                foreach (var product in products)
                {
                    Console.WriteLine("ID: " + product.Id + " - Produto: " + product.Description + " - " +
                                      "Quantidade: " + product.Inventory + " - Preço de Venda: " + product.SalePrice);
                }
            }

            Console.WriteLine("");

            return (!_notificatorService.HaveNotification());
        }

        private void Listar_Notificacoes()
        {
            Console.WriteLine("Ocorreu um ERRO !!! Listando as notificações...");
            Console.WriteLine("");

            var notifications = _notificatorService.GetAll();

            foreach (var notification in notifications)
            {
                Console.WriteLine(notification.Message);
            }

            Console.WriteLine("");
        }
    }
}
