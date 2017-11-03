using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stonebuck.Models
{
    public class ExpressenFeedSubscription : IFeedSubscription
    {
        public ExpressenFeedSubscription()
        {
            this.Name = "Expressen";
            this.Url = "http://expressen.se/rss/nyheter";
        }

        private string LogoPath = "/Images/expressen_logo.png";

        public string Url { get; private set; }

        public int Id { get; private set; }

        public string Name { get; private set; }
    }

    public class AftonbladetFeedSubscription : IFeedSubscription
    {
        public AftonbladetFeedSubscription()
        {
            this.Name = "Aftonbladet";
            this.Url = "http://www.aftonbladet.se/nyheter/rss.xml";
        }

        public string Url { get; private set; }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public string FeedLogoUrl = "/Images/aftonbladet_logo.png";

    }

    public class SydsvenskanFeedSubscription : IFeedSubscription
    {
        public SydsvenskanFeedSubscription()
        {
            Url = "https://www.sydsvenskan.se/rss.xml?latest=1";
            Name = "Sydsvenskan";
        }

        public string Url { get; private set; }

        public int Id { get; private set; }

        public string Name { get; private set; }

        private string FeedLogoPath = "/Images/sydsvenskan_logo.png";
    }

}
