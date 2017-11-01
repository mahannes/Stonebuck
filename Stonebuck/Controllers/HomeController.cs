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
            vms.AddRange(await new AftonbladetFeedReader().ReadFeed(5));
            vms.AddRange(await new ExpressenFeedReader().ReadFeed(5));
            vms.AddRange(await new SydsvenskanFeedReader().ReadFeed(5));
            
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
