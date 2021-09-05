using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WildberriesCommentsParser
{
    public class Comment
    {
        public enum StarsRating { OneStar, TwoStars, ThreeStars, FourStars, FiveStars}
        public string ProductName { get; set; }
        public string UserName { get; set; }
        public string CreationDate { get; set; }
        public string Text { get; set; }
        public StarsRating Rating { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
    }
}
