using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Rss;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Linq;


namespace Stonebuck.Models
{
    public static class SyndicationItemExtensions
    {
        public static ArticleFaceViewModel ToArticleFace(this ISyndicationItem item)
        {
            string imageUrl;
            var hasImagePath = RSSHelper.TryGetImagePathFromDescription(item.Description, out imageUrl);
            return new ArticleFaceViewModel()
            {
                Title = item.Title,
                Url = item.Links.FirstOrDefault(li => li != null).Uri.AbsoluteUri,
                ImageUrl = imageUrl
            };
        }
    }

    public static class RSSHelper
    {
        public static async Task<IEnumerable<ArticleFaceViewModel>> GetArticleFacesFromFeed(IFeedSubscription feedSubscription, 
            int maxNumberOfArticles)
        {
            var syndicationItems = await RSSHelper.GetSyndicationItemsFromFeed(feedSubscription.Url,
                maxNumberOfArticles,
                feedSubscription.Name);
            var vms = syndicationItems.Select(i => i.ToArticleFace()).ToList();
            foreach (var viewModel in vms.Where(vm => vm.Author == null))
                viewModel.Author = feedSubscription.Name;
            return vms;
        }

        public async static Task<IEnumerable<ISyndicationItem>> GetSyndicationItemsFromFeed(string url,
            int maxNumberToTake,
            string feedName)
        {
            var syndicationItems = new List<ISyndicationItem>();

            // Create an XmlReader
            // Example: ..\tests\TestFeeds\rss20-2items.xml
            using (var xmlReader = XmlReader.Create(url, new XmlReaderSettings() { Async = true }))
            {
                // Instantiate an Rss20FeedReader using the XmlReader.
                // This will assign as default an Rss20FeedParser as the parser.
                var feedReader = new RssFeedReader(xmlReader);

                //
                // Read the feed
                while (await feedReader.Read())
                {
                    if (syndicationItems.Count >= maxNumberToTake)
                        break;

                    switch (feedReader.ElementType)
                    {
                        // Read category
                        case SyndicationElementType.Category:
                            ISyndicationCategory category = await feedReader.ReadCategory();
                            break;

                        // Read Image
                        case SyndicationElementType.Image:
                            ISyndicationImage image = await feedReader.ReadImage();
                            break;

                        // Read Item
                        case SyndicationElementType.Item:
                            ISyndicationItem item = await feedReader.ReadItem();
                            // TODO The item.Description contains the src to the image in some 
                            syndicationItems.Add(item);
                            break;

                            // Read link
                        case SyndicationElementType.Link:
                            ISyndicationLink link = await feedReader.ReadLink();
                            break;

                        // Read Person
                        case SyndicationElementType.Person:
                            ISyndicationPerson person = await feedReader.ReadPerson();
                            break;

                        // Read content
                        default:
                            ISyndicationContent content = await feedReader.ReadContent();
                            break;
                    }
                }
            }
            return syndicationItems;
        }

        public static bool TryGetImagePathFromDescription(string description, out string imageUrl)
        {
            if (description != null)
            {
                var match = Regex.Match(description, "<img.+?src=[\"'](.+?)[\"'].*?>", RegexOptions.IgnoreCase);
                if(match != null && match.Groups.Count > 1)
                {
                    var group = match.Groups[1];
                    if (group != null)
                    {
                        string matchString = group.Value;
                        if (Uri.IsWellFormedUriString(matchString, UriKind.RelativeOrAbsolute))
                        {
                            imageUrl = matchString;
                            return true;
                        }
                    }
                }
            }

            imageUrl = null;
            return false;
        }
    }

}