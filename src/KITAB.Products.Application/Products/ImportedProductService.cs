using System;
using System.Collections.Generic;
using KITAB.Products.Application.Notificators;
using KITAB.Products.Domain.Models;
using KITAB.Products.Infra.Excel;
using KITAB.Products.Infra.Products;

namespace KITAB.Products.Application.Products
{
    public class ImportedProductService : BaseService, IImportedProductService
    {
        private readonly IExcelRepository _excelRepository;
        private readonly IImportedProductRepository _importedproductRepository;

        public ImportedProductService(IExcelRepository excelRepository, IImportedProductRepository importedProductRepository, INotificatorService notificatorService) : base(notificatorService)
        {
            _excelRepository = excelRepository;
            _importedproductRepository = importedProductRepository;
        }

        public void ImportFromEXCEL(ref string p_fileName)
        {
            try
            {
                List<ImportedProduct> products = _excelRepository.ImportProducts(ref p_fileName);

                _importedproductRepository.SaveAll(ref products);
            }
            catch (Exception ex)
            {
                Notify(ex.Message);
            }

            return;
        }

        public List<ImportedProduct> GetAll()
        {
            List<ImportedProduct> products = null;

            try
            {
                products = _importedproductRepository.GetAll();
            }
            catch (Exception ex)
            {
                Notify(ex.Message);
            }

            return products;
        }
    }
}
