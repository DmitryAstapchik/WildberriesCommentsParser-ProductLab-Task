using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WildberriesCommentsParser;

namespace Tests
{
    [TestClass]
    public class LinkCreatorTests
    {
        [TestMethod]
        public void CreateLink_VendorCode_ReturnCorrectLink()
        {
            var vendorCode = "7383910833";

            var link = LinkCreator.CreateLink(vendorCode);

            Assert.AreEqual("https://www.wildberries.ru/catalog/7383910833/detail.aspx", link);
        }

        [TestMethod]
        public void CreateLink_NotAVendorCode_ThrowsArgumentException()
        {
            var vendorCode = "Not a vendor code";

            Assert.ThrowsException<ArgumentException>(() => LinkCreator.CreateLink(vendorCode));
        }
    }
}
