using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using Stonebuck.Models;
using Stonebuck.Models.DatabaseAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StonebuckTest.DataAccess
{
    [TestClass]
    public class NewsAppDataAccessTest
    {
        const string TestDataName = "Test name in NewsAppDataAccessTest";
        const string TestUserName = "Test user name in NewsAppDataAccessTest";

        [TestMethod]
        public void TestDatabase_AddAndDeleteFeedSubscription()
        {
            var da = new NewsAppDataAccess(true);
            
            //Make sure DB is in right state
            var feedSubscriptionsInitial = GetAllFeedSubscriptionsSync(da).ToList();
            var testFs = feedSubscriptionsInitial.FirstOrDefault(f => f.Name == TestDataName);
            Assert.IsNull(testFs, "Cannot execute test, test data is already added to DB");

            // Add test data
            var fs = new FeedSubscription()
            {
                Url = "TestUrl",
                Name = TestDataName

            };
            da.AddFeedSubscription(fs);

            // Assert that it has been added
            var feedSubscriptions = GetAllFeedSubscriptionsSync(da).ToList();
            var fsFromDb = feedSubscriptions.FirstOrDefault(f => f.Name == TestDataName);
            Assert.IsTrue(fsFromDb != null, "Failed to Add to DB");

            // Delete the test data
            da.DeleteFeedSubscription(fsFromDb);

            // Assert that it has been deleted
            var feedsubscriptionsAfterDeletion = GetAllFeedSubscriptionsSync(da).ToList();
            bool hasBeenDeleted = !feedsubscriptionsAfterDeletion.Any(f => f.Name == TestDataName);
            Assert.IsTrue(hasBeenDeleted, "Failed to remove from DB");
        }

        //[TestMethod]
        public void DeletePollingTestData()
        {
            var da = new NewsAppDataAccess(true);
            var fs = new FeedSubscription()
            {
                Url = "TestUrl",
                Name = TestDataName

            };
            // Delete the test data
            da.DeleteFeedSubscription(fs);
        }

        private IEnumerable<FeedSubscription> GetAllFeedSubscriptionsSync(NewsAppDataAccess da)
        {
            var getTask = da.GetAllFeedSubscriptions();
            getTask.Wait();
            var feedSubscriptions = getTask.Result;
            return feedSubscriptions;
        }


        [TestMethod]
        public void TestDataAccess_AddAndDeleteUser()
        {
            var da = new NewsAppDataAccess(true);

            var feedSubscriptionsInitial = GetAllFeedSubscriptionsSync(da).ToList();
            var testFs = feedSubscriptionsInitial.FirstOrDefault(f => f.Name == TestDataName);
            if (testFs != null)
            {
                da.DeleteFeedSubscription(testFs);
                testFs = null;
            }
            Assert.IsNull(testFs, "Cannot execute test, test data is already added to DB");

            var userInitialTask = da.GetUserByUserName(TestUserName);
            userInitialTask.Wait();
            var userInitial = userInitialTask.Result;
            if (userInitial != null)
            {
                da.DeleteNewsAppUser(userInitial);
                userInitial = null;
            }
            Assert.IsNull(userInitial, "Cannot execute test, test data is already added to DB");

            var fsId = ObjectId.GenerateNewId();
            var fs = new FeedSubscription()
            {
                Url = "testurl",
                Name = TestDataName,
                Id = fsId
            };
            da.AddFeedSubscription(fs);

            var user = new NewsAppUser()
            {
                UserName = TestUserName,
                Name = "Sven",
                FeedSubscriptions = new[] { fs }
            };
            da.AddNewsAppUser(user);

            var userFromDbTask = da.GetUserByUserName(TestUserName);
            userFromDbTask.Wait();
            var userFromDb = userFromDbTask.Result;

            Assert.AreEqual(user.Name, userFromDb.Name);
            Assert.AreEqual(user.UserName, userFromDb.UserName);

            var userFromDbFeedId = userFromDb.FeedSubscriptionIds.First();
            Assert.AreEqual(fsId, userFromDbFeedId);

            var userFromDbFeed = userFromDb.FeedSubscriptions.First();
            Assert.AreEqual(fs.Name, userFromDbFeed.Name);

            da.DeleteNewsAppUser(user);
            da.DeleteFeedSubscription(fs);
        }
        
    }
}


