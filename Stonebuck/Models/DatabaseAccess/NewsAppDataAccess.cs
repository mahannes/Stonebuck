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
        const string USER_COLLECTION = "Users";
        const string FEED_SUBSCRIPTION_COLLECTION = "FeedSubscriptions";
        // TODO Move the connectionstring to a config file
        const string MONGODB_CONNECTION_STRING = "mongodb://localhost:27017";

        public NewsAppDataAccess()
        {
            _client = new MongoClient(MONGODB_CONNECTION_STRING);
            _db = _client.GetDatabase(NEWSAPP_DATABASE_NAME);
        }


        #region FeedSubscriptions
        public async Task<IEnumerable<FeedSubscription>> GetAllFeedSubscriptions()
        {
            var collection = _db.GetCollection<FeedSubscription>(FEED_SUBSCRIPTION_COLLECTION);
            var filter = Builders<FeedSubscription>.Filter.Empty;

            var feedSubscriptions = await collection.FindAsync(filter);
            return feedSubscriptions.ToEnumerable();
        }

        public void AddFeedSubscription(FeedSubscription feedSubscription)
        {
            var collection = _db.GetCollection<FeedSubscription>(FEED_SUBSCRIPTION_COLLECTION);
            collection.InsertOne(feedSubscription);
            
        }

        public void DeleteFeedSubscription(FeedSubscription feedSubscription)
        {
            var collection = _db.GetCollection<FeedSubscription>(FEED_SUBSCRIPTION_COLLECTION);
            var builder = Builders<FeedSubscription>.Filter;
            var filter = builder.Eq(x => x.Name, feedSubscription.Name);
            collection.FindOneAndDelete(filter);
        }
        #endregion

        #region Users
        public async Task<NewsAppUser> GetUserByUserName(string userName)
        {
            var userCollection = _db.GetCollection<NewsAppUser>(USER_COLLECTION);
            var builder = Builders<NewsAppUser>.Filter;
            var filter = builder.Eq(x => x.UserName, userName);
            return await userCollection.Find(filter).FirstOrDefaultAsync();
        }

        public void AddNewsAppUser(NewsAppUser user)
        {
            //TODO Validate data
            var userCollection = _db.GetCollection<NewsAppUser>(USER_COLLECTION);
            userCollection.InsertOne(user);
        }

        public void AddFeedSubscriptionForUser(NewsAppUser user, FeedSubscription fs)
        {

        }
        #endregion

        #region Example data
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
        #endregion

    }


}
