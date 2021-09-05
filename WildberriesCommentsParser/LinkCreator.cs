using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WildberriesCommentsParser
{
    public static class LinkCreator
    {
        public static string CreateLink(string vendorCode)
        {
            if (Regex.IsMatch(vendorCode, @"^\d+$"))
            {
                return $"https://www.wildberries.ru/catalog/{vendorCode}/detail.aspx";
            }
            throw new ArgumentException("The given string is not a vendor code.");
        }
    }
}
