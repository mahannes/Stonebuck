using MongoDB.Bson;
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
        const string NEWSAPP_DATABASEFORTEST_NAME = "NewsAppDBForTest";

        const string USER_COLLECTION = "Users";
        const string FEED_SUBSCRIPTION_COLLECTION = "FeedSubscriptions";
        // TODO Move the connectionstring to a config file
        const string MONGODB_CONNECTION_STRING = "mongodb://localhost:27017";

        public NewsAppDataAccess(bool useTestDatabase = false)
        {
            _client = new MongoClient(MONGODB_CONNECTION_STRING);
            var dbName = useTestDatabase 
                ? NEWSAPP_DATABASEFORTEST_NAME 
                : NEWSAPP_DATABASE_NAME;
            _db = _client.GetDatabase(dbName);
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
            var user = await userCollection.Find(filter).FirstOrDefaultAsync();

            if (user != null)
            {
                var feedSubscriptionIds = user.FeedSubscriptionIds;
                var feedSubscriptionCollection = _db.GetCollection<FeedSubscription>(FEED_SUBSCRIPTION_COLLECTION);

                //Find all feedsubscriptions the user has
                var builder2 = Builders<FeedSubscription>.Filter;
                var feedFilter = builder2.In(x => x.Id, feedSubscriptionIds);

                var feedSubscriptions = await feedSubscriptionCollection.FindAsync(feedFilter);
                user.FeedSubscriptions = feedSubscriptions.ToEnumerable();
            }
            return user;
        }

        //TODO Make async
        public void AddNewsAppUser(NewsAppUser user)
        {
            var feedSubscriptionCollection = _db.GetCollection<FeedSubscription>(FEED_SUBSCRIPTION_COLLECTION);
            var builder = Builders<FeedSubscription>.Filter;

            //Make sure the feedsubscription ids match the feedsubscriptions
            var ids = user.FeedSubscriptions.Select(f => f.Id).ToHashSet();
            user.FeedSubscriptionIds = ids.ToArray();

            //TODO Validate data
            var userCollection = _db.GetCollection<NewsAppUser>(USER_COLLECTION);
            userCollection.InsertOne(user);
        }

        //TODO Make Async
        public void DeleteNewsAppUser(NewsAppUser user)
        {
            var collection = _db.GetCollection<NewsAppUser>(USER_COLLECTION);
            var builder = Builders<NewsAppUser>.Filter;
            var filter = builder.Eq(x => x.UserName, user.UserName);
            collection.FindOneAndDelete<NewsAppUser>(filter);
        }

        public async Task AddFeedSubscriptionForUser(NewsAppUser user, FeedSubscription fs)
        {
            //TODO Make this better
            var ids = user.FeedSubscriptionIds.ToList();
            ids.Add(fs.Id);
            user.FeedSubscriptionIds = ids.ToArray();
            var userCollection = _db.GetCollection<NewsAppUser>(USER_COLLECTION);

            var filter = Builders<NewsAppUser>.Filter.Eq(u => u.Id, user.Id);
            var update = Builders<NewsAppUser>.Update.AddToSet(u => u.FeedSubscriptionIds, fs.Id);
            await userCollection.UpdateOneAsync(filter, update);
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
