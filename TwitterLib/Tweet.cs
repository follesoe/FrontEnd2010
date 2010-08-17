using System;
using System.Runtime.Serialization;

namespace TwitterLib
{
    [DataContract]
    public class Tweet
    {
        [DataMember]
        public string id;

        [DataMember]
        public string profile_image_url;

        [DataMember]
        public DateTime created_at;

        [DataMember]
        public string from_user;

        [DataMember]
        public string text;
    }
}
