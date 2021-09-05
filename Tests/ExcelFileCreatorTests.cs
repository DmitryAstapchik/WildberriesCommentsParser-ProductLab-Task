using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using WildberriesCommentsParser;

namespace Tests
{
    [TestClass]
    public class ExcelFileCreatorTests
    {
        [TestMethod]
        public void CreateExcelFile_SeveralReviews_CreatesFileWithReviews()
        {
            var fileName = "Test excel file";
            var comments = new Comment[]
            {
                new Comment{
                        ProductName = "product #1",
                        UserName = "user #1",
                        CreationDate = DateTime.Now.ToShortDateString(),
                        Rating = Comment.StarsRating.FiveStars,
                        Text = "comment #1 text",
                        Likes = 10,
                        Dislikes = 22
                    },
                    new Comment{
                        ProductName = "product #2",
                        UserName = "user #2",
                        CreationDate = DateTime.Now.ToShortDateString(),
                        Rating = Comment.StarsRating.TwoStars,
                        Text = "comment #2 text",
                        Likes = 15,
                        Dislikes = 2
                    },
                    new Comment{
                        ProductName = "product #3",
                        UserName = "user #3",
                        CreationDate = DateTime.Now.ToShortDateString(),
                        Rating = Comment.StarsRating.ThreeStars,
                        Text = "comment #3 text",
                        Likes = 229,
                        Dislikes = 123
                    },
                    new Comment{
                        ProductName = "product #3",
                        UserName = "user #4",
                        CreationDate = DateTime.Now.ToShortDateString(),
                        Rating = Comment.StarsRating.FourStars,
                        Text = "comment #4 text",
                        Likes = 52,
                        Dislikes = 9
                    },
                    new Comment{
                        ProductName = "product #3",
                        UserName = "user #5",
                        CreationDate = DateTime.Now.ToShortDateString(),
                        Rating = Comment.StarsRating.OneStar,
                        Text = "comment #5 text",
                        Likes = 114,
                        Dislikes = 98
                    }
            };

            ExcelFileCreator.CreateExcelFile(comments, fileName);

            Assert.IsTrue(new FileInfo(fileName + ".xlsx").Length > 0);
            // manual check of the file content
        }
    }
}
