using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stonebuck.Models.DatabaseAccess
{
    public class NewsAppDataAccess : INewsAppDataAccess
    {
        IMongoClient _client;
        IMongoDatabase _db;

        const string NEWSAPP_DATABASE_NAME = "NewsAppDB";

        public NewsAppDataAccess()
        {
            _client = new MongoClient(); // TODO send in connection string here
            _db = _client.GetDatabase(NEWSAPP_DATABASE_NAME);
        }



        public Task<IEnumerable<ISubscribable>> GetAllSubscribables()
        {
            return Task.Run(() => GetExampleSubscriptions());
        }

        public Task<IEnumerable<ISubscribable>> GetSubscriptionsByUserName(string userName)
        {
            return Task.Run(() => GetExampleSubscriptions());
        }

        private IEnumerable<ISubscribable> GetExampleSubscriptions()
        {
            yield return new ExpressenFeedSubscription();
            yield return new SydsvenskanFeedSubscription();
        }
    }


}
