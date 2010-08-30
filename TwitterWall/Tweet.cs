using System;
using System.Windows.Media.Imaging;

namespace TwitterWall
{
    public class Tweet
    {
        public string Id { get; set; }        
        public string FromUser { get; set; }
        public string Text { get; set; }
        public Uri ProfileImageUrl { get; set; }
        public BitmapImage ProfileImage { get; set; }
        public DateTime CreatedAt { set; get; }
    }
}