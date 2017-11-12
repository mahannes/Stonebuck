using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stonebuck.Models
{
    public class NewsAppUser
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("UserName")]
        public string UserName { get; set; }
        [BsonIgnore]
        public IEnumerable<FeedSubscription> FeedSubscriptions { get; set; }

        public IEnumerable<ObjectId> FeedSubscriptionIds { get; set; }
    }
}
