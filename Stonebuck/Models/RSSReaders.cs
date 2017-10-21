//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.ServiceModel.Syndication;
//using System.Web;
//using System.Xml;

//namespace Stonebuck.Models
//{
//    public interface IFeedReader
//    {
//        IEnumerable<ArticleFaceModel> ReadFeed(int maxNumberOfArticles);

//        string FeedUrl { get; }
//    }

//    public class AftonbladetFeedReader : IFeedReader
//    {
//        public string FeedUrl { get { return "http://www.aftonbladet.se/nyheter/rss.xml"; } }

//        public string FeedName = "Aftonbladet";

//        public string FeedLogoUrl = "/Images/aftonbladet_logo.png";

//        public IEnumerable<ArticleFaceModel> ReadFeed(int maxNumberOfArticles)
//        {
//            var articleFaces = RSSHelper.CreateArticleFaces(FeedUrl, 
//                maxNumberOfArticles, 
//                FeedName,
//                FeedLogoUrl);
            
//            return articleFaces;
//        }
//    }


//    public class ExpressenFeedReader : IFeedReader
//    {
//        public string FeedUrl { get { return "http://expressen.se/rss/nyheter "; } }

//        public string FeedName = "Expressen";

//        private string LogoPath = "/Images/expressen_logo.png";

//        public IEnumerable<ArticleFaceModel> ReadFeed(int maxNumberOfArticles)
//        {
//            var articleFaces = RSSHelper.CreateArticleFaces(FeedUrl, 
//                maxNumberOfArticles,
//                FeedName,
//                LogoPath);
            
//            return articleFaces;
//        }
//    }

//    public class SydsvenskanFeedReader : IFeedReader
//    {
//        public string FeedUrl { get { return "https://www.sydsvenskan.se/rss.xml?latest=1"; } }

//        public string FeedName = "Sydsvenskan";

//        private string FeedLogoPath = "/Images/sydsvenskan_logo.png";

//        public IEnumerable<ArticleFaceModel> ReadFeed(int maxNumberOfArticles)
//        {
//            return RSSHelper.CreateArticleFaces(FeedUrl, 
//                maxNumberOfArticles, 
//                FeedName,
//                FeedLogoPath);
//        }
//    }

//    public static class RSSHelper
//    {
//        public static IEnumerable<ArticleFaceModel> CreateArticleFaces(string feedUrl, 
//            int maxNumberToTake,
//            string feedName,
//            string feedImageUrl = null)
//        {
//            var articleFaces = new List<ArticleFaceModel>();

//            using (XmlReader reader = XmlReader.Create(feedUrl))
//            {
//                SyndicationFeed feed = SyndicationFeed.Load(reader);

//                foreach (SyndicationItem item in feed.Items.Take(maxNumberToTake).ToList())
//                {
//                    var url = item.Links.First().Uri.AbsoluteUri;

//                    var author = item.Authors.FirstOrDefault();
//                    var authorName = author != null 
//                        ? author.Name 
//                        : null;


//                    var header = item.Title.Text;

//                    string imgLink = feedImageUrl;
//                    var articleFace = new ArticleFaceModel()
//                    {
//                        Author = authorName,
//                        Header = header,
//                        Link = url,
//                        ImageLink = imgLink,
//                        Source = feedName
//                    };
//                    if (item.Categories != null)
//                        foreach (var category in item.Categories)
//                            articleFace.Categories.Add(category.Name);

//                    articleFaces.Add(articleFace);
//                }
//            }
//            return articleFaces;
//        } 
//    }


//}