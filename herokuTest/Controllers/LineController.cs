using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace herokuTest.Controllers
{
    [Produces("application/json")]
    [Route("api/Line")]
    public class LineController : Controller
    {
        [HttpPost]
        public ActionResult webhook()
        {
            return Ok("ok");
        }
    }
}