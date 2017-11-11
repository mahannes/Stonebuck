using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stonebuck.Models.DatabaseAccess
{
    public class NewsAppDataAccess : INewsAppDataAccess
    {
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
