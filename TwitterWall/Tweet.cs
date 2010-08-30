using System;
using System.Windows.Media;

namespace TwitterWall
{
    public class Tweet
    {
        public string Id { get; set; }        
        public string FromUser { get; set; }
        public string Text { get; set; }
        public string ProfileImageUrl { get; set; }
        public ImageSource ProfileImage { get; set; }
        public DateTime CreatedAt { set; get; }
    }
}