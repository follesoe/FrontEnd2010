using System;

namespace TwitterPhone.Model
{
    public class Tweet
    {
        public string Id { get; set; }        
        public string FromUser { get; set; }
        public string Text { get; set; }
        public Uri ProfileImageUrl { get; set; }
        public DateTime CreatedAt { set; get; }
    }
}