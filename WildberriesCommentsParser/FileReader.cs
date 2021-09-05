using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WildberriesCommentsParser
{
    public static class FileReader
    {
        public static string[] GetProductStrings(string filePath)
        {
            var productStrings = new List<string>();
            string line;
            StreamReader file = new StreamReader(filePath);
            while ((line = file.ReadLine()) != null)
            {
                if (line.StartsWith("http") || Regex.IsMatch(line, @"^\d+$"))
                {
                    productStrings.Add(line);
                }
            }
            file.Close();
            return productStrings.ToArray();
        }
    }
}
