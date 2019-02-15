using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace herokuTest.Controllers
{
    [Produces("application/json")]
    
    public class LineController : Controller
    {


        [Route("api/Line")]
        [HttpPost]
        public ActionResult qwe([FromBody] asho x = null)
        {
            return x != null ? Ok(x.ID) : Ok("good");
        }


        [Route("api/Test")]
        [HttpPost]
        public ActionResult qwe()
        {
            return Ok();
        }
    }

    public class asho
    {
        public string ID { get; set; }
    }
}