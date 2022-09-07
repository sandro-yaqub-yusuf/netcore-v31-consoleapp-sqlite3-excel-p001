using System.Collections.Generic;
using KITAB.Products.Domain.Models;

namespace KITAB.Products.Application.Products
{
    public interface IImportedProductService
    {
        List<ImportedProduct> GetAll();
        void ImportFromEXCEL(ref string p_fileName);
    }
}
