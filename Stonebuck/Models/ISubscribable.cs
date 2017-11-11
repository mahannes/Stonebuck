using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stonebuck.Models
{
    /// <summary>
    /// An item you can subscribe to (e.g. category, author, feed, story)
    /// </summary>
    public interface ISubscribable
    {
        string Name { get; }


    }
}
