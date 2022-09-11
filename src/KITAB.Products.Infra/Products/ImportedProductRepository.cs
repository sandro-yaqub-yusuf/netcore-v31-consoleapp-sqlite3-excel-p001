using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dapper;
using KITAB.Products.Domain.Models;

namespace KITAB.Products.Infra.Products
{
    public class ImportedProductRepository : Repository, IImportedProductRepository
    {
        public List<ImportedProduct> GetAll()
        {
            List<ImportedProduct> products = null;

            if (File.Exists(DbFile))
            {
                using var cnn = SimpleDbConnection();

                try
                {
                    cnn.Open();

                    var sql = "SELECT * FROM ImportedProduct ORDER BY Id ASC;";

                    products = cnn.Query<ImportedProduct>(sql).ToList();
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

        public void ExecuteSQL(ref string p_sql)
        {
            if (File.Exists(DbFile))
            {
                using var cnn = SimpleDbConnection();

                cnn.Open();

                using (var transaction = cnn.BeginTransaction())
                {
                    try
                    {
                        // Executa as instruções sql na tabela "Product"
                        cnn.Execute(p_sql);

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();

                        throw new Exception(ex.Message);
                    }
                }

                cnn?.Close();
                cnn?.Dispose();
            }
        }

        public void SaveAll(ref List<ImportedProduct> p_products)
        {
            try
            {
                var sql = "DELETE FROM ImportedProduct; ";

                foreach (var product in p_products)
                {
                    sql += string.Format(@"INSERT INTO ImportedProduct (Id, Name, Description, Inventory, CostPrice, SalePrice) 
                                           VALUES ({0}, '{1}', '{2}', {3}, {4}, {5}); ", product.Id, product.Name, product.Description,
                                           product.Inventory, product.CostPrice, product.SalePrice);
                }

                ExecuteSQL(ref sql);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
