using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dapper;
using KITAB.Products.Domain.Models;

namespace KITAB.Products.Infra.Products
{
    public class ExportedProductRepository : Repository, IExportedProductRepository
    {
        public List<ExportedProduct> GetAll()
        {
            List<ExportedProduct> products = null;

            if (File.Exists(DbFile))
            {
                using var cnn = SimpleDbConnection();

                try
                {
                    cnn.Open();

                    var sql = "SELECT * FROM ExportedProduct ORDER BY Id ASC;";

                    products = cnn.Query<ExportedProduct>(sql).ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                cnn?.Close();
                cnn?.Dispose();
            }

            return products;
        }
    }
}
