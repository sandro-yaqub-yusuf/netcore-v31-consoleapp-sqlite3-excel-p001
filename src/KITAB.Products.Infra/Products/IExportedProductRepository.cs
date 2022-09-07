using System.Collections.Generic;
using KITAB.Products.Domain.Models;

namespace KITAB.Products.Infra.Products
{
    public interface IExportedProductRepository
    {
        List<ExportedProduct> GetAll();
    }
}
