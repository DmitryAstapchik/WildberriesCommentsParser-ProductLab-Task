using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using WildberriesCommentsParser;

namespace Tests
{
    [TestClass]
    public class FileReaderTests
    {
        [TestMethod]
        public void GetProductStrings_SeveralStrings_ReturnOnlyProductStrings()
        {
            var filePath = "Test text file.txt";
            using (StreamWriter sw = File.CreateText(filePath))
            {
                sw.WriteLine("not a product string");
                sw.WriteLine("2718282920");
                sw.WriteLine("https://www.wildberries.ru/catalog/27207364/detail.aspx?targetUrl=AB");
                sw.WriteLine("wildberries.ru/catalog/9844820/detail.aspx?targetUrl=BP");
            }

            var result = FileReader.GetProductStrings(filePath).ToArray();

            Assert.AreEqual(result.Length, 2);
            Assert.AreEqual("2718282920", result[0]); 
            Assert.AreEqual("https://www.wildberries.ru/catalog/27207364/detail.aspx?targetUrl=AB", result[1]);
        }
    }
}
