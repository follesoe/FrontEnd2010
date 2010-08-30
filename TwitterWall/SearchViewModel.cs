using System;
using System.IO;
using System.Xml;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;
using System.ServiceModel.Syndication;

using GalaSoft.MvvmLight;

namespace TwitterWall
{
    public class SearchViewModel : ViewModelBase
    {
        private const string ApiUrl = "http://search.twitter.com/search.atom?q={0}&since_id={1}";

        public ObservableCollection<Tweet> Tweets { get; private set; }
        private string _sinceId;
        private string _keyword;
        
        public string Keyword
        {
            get { return _keyword; }
            set
            {
                _keyword = value; 
                RaisePropertyChanged("Keyword");
            }

        }        
        
        public SearchViewModel()
        {
            Tweets = new ObservableCollection<Tweet>();      
        }

        private void AddTweets(List<Tweet> newTweets)
        {
            newTweets.ForEach(t => Tweets.Insert(0, t));
        }

        public void Search()
        {
            var url = new Uri(string.Format(ApiUrl, _keyword, _sinceId));

            var client = new WebClient();
            client.DownloadStringCompleted += (o, e) => AddTweets(ParseXml(e.Result));
            client.DownloadStringAsync(url);
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
            if (tweets.Count > 0)
                _sinceId = tweets.First().Id;

            return tweets;
        }
    }
}