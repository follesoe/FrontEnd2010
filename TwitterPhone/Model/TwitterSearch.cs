using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Xml;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using System.ServiceModel.Syndication;

namespace TwitterPhone.Model
{
    public class TwitterSearch
    {
        private const string ApiUrl = "http://search.twitter.com/search.atom?q={0}&since_id={1}";
        private string _sinceId;
        private string _keyword;

        public void Search(string keyword, Action<List<Tweet>> callback)
        {
            if (keyword != _keyword)
                _sinceId = "0";

            _keyword = keyword;
            
            var url = new Uri(string.Format(ApiUrl, keyword, _sinceId));

            var client = new WebClient();
            client.DownloadStringCompleted += (o, e) => callback(e.Error == null ? ParseXml(e.Result) : ReportError(e.Error));
            client.DownloadStringAsync(url);
        }

        private static List<Tweet> ReportError(Exception ex)
        {
            return new List<Tweet>
                       {
                           new Tweet {CreatedAt = DateTime.Now, FromUser = "Error", Text = ex.Message}
                       };        
        }

        private List<Tweet> ParseXml(string xmlString)
        {
            var reader = XmlReader.Create(new StringReader(xmlString));

            var feed = SyndicationFeed.Load(reader);
            var tweetQuery = from item in feed.Items 
                             select new Tweet
                             {
                                  Text = item.Title.Text,
                                  CreatedAt = item.PublishDate.DateTime,
                                  FromUser = item.Authors.First().Name,
                                  ProfileImageUrl = item.Links.Where(l => l.RelationshipType == "image").Single().Uri,
                                  ProfileImage = new BitmapImage(item.Links.Where(l => l.RelationshipType == "image").Single().Uri),
                                  Id = item.Id.Split(',')[1].Split(':')[1]
                             };

            var tweets = tweetQuery.ToList();
            if(tweets.Count > 0)
                _sinceId = tweets.First().Id;

            return tweets;
        }
    }
}