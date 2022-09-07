using System.Collections.Generic;
using KITAB.Products.Domain.Models;

namespace KITAB.Products.Application.Products
{
    public interface IExportedProductService
    {
        List<ExportedProduct> GetAll();
        void ExportToEXCEL(ref List<ExportedProduct> p_products, ref string p_fileName);
    }
}
