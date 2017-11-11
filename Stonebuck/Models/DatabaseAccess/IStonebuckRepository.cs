using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stonebuck.Models.DatabaseAccess
{
    public interface INewsAppDataAccess
    {
        Task<IEnumerable<ISubscribable>> GetAllSubscribables();

        Task<IEnumerable<ISubscribable>> GetSubscriptionsByUserName(string userName);
    }
}
