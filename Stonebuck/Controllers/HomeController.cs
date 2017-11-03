using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stonebuck.Models;

namespace Stonebuck.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var vms = new List<ArticleFaceViewModel>();
            vms.AddRange(await RSSHelper.GetArticleFacesFromFeed(new AftonbladetFeedSubscription(), 5));
            vms.AddRange(await RSSHelper.GetArticleFacesFromFeed(new ExpressenFeedSubscription(), 5));
            vms.AddRange(await RSSHelper.GetArticleFacesFromFeed(new SydsvenskanFeedSubscription(), 5));

            return View(vms.ToHashSet());
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
