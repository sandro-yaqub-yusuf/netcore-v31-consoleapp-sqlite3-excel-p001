using System.Collections.Generic;
using KITAB.Products.Domain.Models;

namespace KITAB.Products.Infra.Products
{
    public interface IImportedProductRepository
    {
        List<ImportedProduct> GetAll();
        void ExecuteSQL(ref string p_sql);
        void SaveAll(ref List<ImportedProduct> p_products);
    }
}
