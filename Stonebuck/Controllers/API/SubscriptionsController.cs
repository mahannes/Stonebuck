using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Stonebuck.Models.DatabaseAccess;

namespace Stonebuck.Controllers.API
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Subscriptions")]
    public class SubscriptionsController : Controller
    {
        private readonly INewsAppDataAccess _repository;

        public SubscriptionsController(INewsAppDataAccess repository)
        {
            this._repository = repository;
        }
        // GET: api/Subscriptions
        [HttpGet]
        public async Task<JsonResult> Get()
        {
            var subscribables = await _repository.GetAllSubscribables();
            return Json(subscribables);
        }

        //// GET: api/Subscriptions/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // TODO Add possibility for user to subscribe to subcribable 
        //// POST: api/Subscriptions
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        // TODO Add possibility for user to unsubscribe from subcribable 
        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
