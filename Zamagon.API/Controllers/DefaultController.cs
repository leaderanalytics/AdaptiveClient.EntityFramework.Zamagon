using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Zamagon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : Controller
    {
        [HttpGet]
        [HttpGet("Index")]
        public ActionResult Index()
        {
            return View("~/index.html");
        }


        [HttpGet]
        [Route("api")]
        public async Task Get()
        {

        }
    }
}