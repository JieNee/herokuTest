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
        public ActionResult qwe([FromBody] LineMessage x = null)
        {
            if (x == null) return BadRequest();
            
            return Ok(x);
        }


        [Route("api/Test")]
        [HttpPost]
        public string  qwe()
        {
            return "asd";
        }

        [Route("api/Test2")]
        [HttpGet]
        public LineMessage asd([FromBody] LineMessage x = null)
        {
            return x;
        }
    }

    public class asho
    {
        public string ID { get; set; }
    }

    public class LineMessage
    {
        public string replyToken { get; set; }
        public string type { get; set; }
        public string timestamp { get; set; }
        public source source { get; set; }
        public message message { get; set; }
    }

    public class source
    {
        public string type { get; set; }
        public string userId { get; set; }
    }
    public class message
    {
        public string id { get; set; }
        public string type { get; set; }
        public string text { get; set; }
    }
}