using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stonebuck.Models
{
    public class FeedSubscription : IFeedSubscription
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("Url")]
        public string Url { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
    }
}
