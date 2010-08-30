using System;
using System.IO;
using System.Xml;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.ServiceModel.Syndication;


namespace TwitterWall
{
    public class TwitterSearch
    {
        private const string ApiUrl = "http://search.twitter.com/search.atom?q={0}&since_id={1}";
        private string _sinceId;
        private string _keyword;
        private IDisposable timer;

        public void Search(string keyword, Action<List<Tweet>> callback)
        {
            _sinceId = string.Empty;

            if(timer != null)
                timer.Dispose();
            
            var interval = Observable.Interval(TimeSpan.FromMilliseconds(1500)).TimeInterval();
            timer = interval.Subscribe(x =>
                                     {
                                         var url = new Uri(string.Format(ApiUrl, keyword, _sinceId));

                                         var client = new WebClient();
                                         client.DownloadStringCompleted += (o, e) => callback(ParseXml(e.Result));
                                         client.DownloadStringAsync(url);
                                     });
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