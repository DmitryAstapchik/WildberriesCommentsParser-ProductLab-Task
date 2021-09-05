using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WildberriesCommentsParser
{
    static class Parser
    {
        private static readonly string commentsUrl1 = "https://public-feedbacks.wildberries.ru/api/v1/summary/full";
        private static readonly string commentsUrl2 = "https://public-feedbacks.wildberries.ru/api/v1/feedbacks/site";
        public static void Parse(string inputFilePath, string outputDirectoryPath)
        {
            var productStrings = FileReader.GetProductStrings(inputFilePath);
            var links = new List<string>();
            foreach (var productString in productStrings)
            {
                if (productString.StartsWith("http"))
                {
                    links.Add(productString);
                }
                else
                {
                    try
                    {
                        links.Add(LinkCreator.CreateLink(productString));
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
            }
            var comments = new List<Comment>();
            foreach (var link in links)
            {
                try
                {
                    comments.AddRange(ParseLink(link));
                }
                catch (Exception)
                {
                    continue;
                }
            }
            ExcelFileCreator.CreateExcelFile(comments, outputDirectoryPath + "\\Wildberries products comments");
        }

        private static Comment[] ParseLink(string link)
        {
            bool result = Uri.TryCreate(link, UriKind.Absolute, out Uri uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            if (!result)
            {
                throw new ArgumentException("Trying to parse an invalid link.");
            }
            string stringResponse, jsonResponse, productId;
            using (var webClient = new WebClient { Encoding = Encoding.UTF8 })
            {
                stringResponse = webClient.DownloadString(link);
                var searchString = "\"productCard\":{\"link\":";
                var index = stringResponse.IndexOf(searchString);
                var regex = new Regex(@"\d+");
                productId = regex.Match(stringResponse, index + searchString.Length, 10).Value;
                jsonResponse = webClient.UploadString(commentsUrl1, JsonConvert.SerializeObject(new { ImtId = int.Parse(productId), Skip = 0, Take = 20 }));
            }
            JObject responseObj = JObject.Parse(jsonResponse);
            var commentsCount = int.Parse(responseObj["feedbackCount"].ToString());
            var comments = new List<Comment>();
            var first20Comments = responseObj["feedbacks"].Select(t => FormComment(t));
            comments.AddRange(first20Comments);
            if (commentsCount > 20)
            {
                using (var webClient = new WebClient { Encoding = Encoding.UTF8 })
                {
                    jsonResponse = webClient.UploadString(commentsUrl2, JsonConvert.SerializeObject(new { ImtId = int.Parse(productId), Skip = 20, Take = commentsCount - 20 }));
                }
                comments.AddRange(JObject.Parse(jsonResponse)["feedbacks"].Select(t => FormComment(t)));
            }
            return comments.ToArray();
        }

        private static Comment FormComment(JToken token)
        {
            return new Comment
            {
                CreationDate = DateTime.Parse(token["createdDate"].ToString()).ToString("dd.MM.yyyy"),
                Dislikes = token["votes"] != null ? int.Parse(token["votes"]["minuses"].ToString()) : 0,
                Likes = token["votes"] != null ? int.Parse(token["votes"]["pluses"].ToString()) : 0,
                ProductName = token["productDetails"]["productName"].ToString(),
                Rating = (Comment.StarsRating)(int.Parse(token["productValuation"].ToString()) - 1),
                Text = token["text"].ToString(),
                UserName = token["wbUserDetails"]["name"].ToString()
            };
        }
    }
}
