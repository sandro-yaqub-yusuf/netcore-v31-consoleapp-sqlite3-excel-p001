using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using ClosedXML.Excel;
using KITAB.Products.Domain.Models;

namespace KITAB.Products.Infra.Excel
{
    public class ExcelRepository : IExcelRepository
    {
        public void ExportProducts(ref List<ExportedProduct> p_products, ref string p_fileName)
        {
            string pathFileName = GetPathFileName(ref p_fileName, true);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Products");

                worksheet.Cell(1, 1).Value = "ID";
                worksheet.Cell(1, 2).Value = "NAME";
                worksheet.Cell(1, 3).Value = "DESCRIPTION";
                worksheet.Cell(1, 4).Value = "INVENTORY";
                worksheet.Cell(1, 5).Value = "COST PRICE";
                worksheet.Cell(1, 6).Value = "SALE PRICE";

                int count = 0;

                while (p_products.Count > count)
                {
                    worksheet.Cell((count + 2), 1).Value = p_products[count].Id;
                    worksheet.Cell((count + 2), 2).Value = p_products[count].Name;
                    worksheet.Cell((count + 2), 3).Value = p_products[count].Description;
                    worksheet.Cell((count + 2), 4).Value = p_products[count].Inventory;
                    worksheet.Cell((count + 2), 5).Value = p_products[count].CostPrice;
                    worksheet.Cell((count + 2), 6).Value = p_products[count].SalePrice;

                    count++;
                }

                worksheet.Cell((count + 2), 3).Value = "Total";
                worksheet.Cell((count + 2), 4).FormulaA1 = "=SUM(D2:D" + (count + 1) + ")";

                worksheet.Range(1, 1, (count + 2), 6).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
                worksheet.Range(1, 1, (count + 2), 6).Style.Border.SetInsideBorder(XLBorderStyleValues.Thin);
                worksheet.Range(1, 1, (count + 2), 6).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                worksheet.Range(1, 1, (count + 2), 6).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
                worksheet.Range(1, 1, (count + 2), 6).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);
                worksheet.Range(1, 1, (count + 2), 6).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

                worksheet.Range(1, 1, 1, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                worksheet.Range(1, 1, 1, 6).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                worksheet.Range(1, 1, 1, 6).Style.Font.SetBold(true);
                worksheet.Range(1, 1, 1, 6).Style.Font.SetFontSize(13);
                worksheet.Range(1, 1, 1, 6).Style.Fill.SetBackgroundColor(XLColor.LightGray);
                worksheet.Range((count + 2), 1, (count + 2), 6).Style.Fill.SetBackgroundColor(XLColor.LightSkyBlue);

                workbook.SaveAs(pathFileName);
            }
        }

        public void ImportProducts(ref string p_fileName)
        {
            throw new NotImplementedException();
        }

        public string GetPathFileName(ref string p_fileName, bool p_deleteFile = false)
        {
            var curDir = Environment.CurrentDirectory;

            if (curDir.IndexOf("KITAB.Products.ConsoleApp", 0) > 0)
            {
                curDir = curDir.Substring(0, (curDir.Length - (curDir.Length - curDir.IndexOf("KITAB.Products.ConsoleApp", 0))));
            }
            else
            {
                curDir += "\\";
            }

            var pathFilename = (curDir + "Files\\" + p_fileName);

            if (p_deleteFile)
            {
                if (File.Exists(pathFilename)) File.Delete(pathFilename);

                Thread.Sleep(2000);
            }

            return pathFilename;
        }
    }
}
