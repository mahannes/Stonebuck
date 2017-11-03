using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stonebuck.Models.DatabaseAccess
{
    public interface IStonebuckRepository
    {
        Task<IEnumerable<ISubscribable>> GetAllSubscribables();

        Task<IEnumerable<ISubscribable>> GetSubscriptionsByUserName(string userName);
    }
}
