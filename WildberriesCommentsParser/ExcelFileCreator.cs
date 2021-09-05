using OfficeOpenXml;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WildberriesCommentsParser
{
    public static class ExcelFileCreator
    {
        public static void CreateExcelFile(IEnumerable<Comment> comments, string fileName)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {
                var sheet = package.Workbook.Worksheets.Add("Product reviews");
                sheet.Cells["A1"].LoadFromCollection(comments, true, TableStyles.Light4);
                package.SaveAs(new FileInfo(fileName + ".xlsx"));
            }
        }
    }
}
