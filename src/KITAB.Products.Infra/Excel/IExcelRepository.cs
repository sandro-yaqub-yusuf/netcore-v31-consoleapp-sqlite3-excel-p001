using System.Collections.Generic;
using KITAB.Products.Domain.Models;

namespace KITAB.Products.Infra.Excel
{
    public interface IExcelRepository
    {
        string GetPathFileName(ref string p_fileName, bool p_deleteFile = false);
        void ExportProducts(ref List<ExportedProduct> p_products, ref string p_fileName);
        List<ImportedProduct> ImportProducts(ref string p_fileName);
    }
}
