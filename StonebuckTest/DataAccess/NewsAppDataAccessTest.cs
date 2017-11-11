using Microsoft.VisualStudio.TestTools.UnitTesting;
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

        [TestMethod]
        public void TestDatabase()
        {
            var da = new NewsAppDataAccess();
            
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
            var da = new NewsAppDataAccess();
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
    }
}
