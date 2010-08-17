using System;
using System.Collections.Generic;
using System.Net;

namespace TwitterLib
{
    public class TwitterSearch
    {
        private string _apiUrl = "http://search.twitter.com/search.atom?q={0}&since_id={1}";
        private string _prevId;

        public void Search(string keyword, Action<List<Tweet>> callback)
        {
            var url = new Uri(string.Format(_apiUrl, keyword, _prevId));

            var client = new WebClient();
            
        }            
    }
}