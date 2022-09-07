using System;
using System.Collections.Generic;
using KITAB.Products.Application.Notificators;
using KITAB.Products.Domain.Models;
using KITAB.Products.Infra.Excel;
using KITAB.Products.Infra.Products;

namespace KITAB.Products.Application.Products
{
    public class ExportedProductService : BaseService, IExportedProductService
    {
        private readonly IExcelRepository _excelRepository;
        private readonly IExportedProductRepository _exportedproductRepository;

        public ExportedProductService(IExcelRepository excelRepository, IExportedProductRepository exportedProductRepository, INotificatorService notificatorService) : base(notificatorService)
        {
            _excelRepository = excelRepository;
            _exportedproductRepository = exportedProductRepository;
        }

        public void ExportToEXCEL(ref List<ExportedProduct> p_products, ref string p_fileName)
        {
            try
            {
                _excelRepository.ExportProducts(ref p_products, ref p_fileName);
            }
            catch (Exception ex)
            {
                Notify(ex.Message);
            }

            return;
        }

        public List<ExportedProduct> GetAll()
        {
            List<ExportedProduct> products = null;

            try
            {
                products = _exportedproductRepository.GetAll();
            }
            catch (Exception ex)
            {
                Notify(ex.Message);
            }

            return products;
        }
    }
}
