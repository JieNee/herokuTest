using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace herokuTest.Models
{
    public class LineWebhookModels
    {
        public string destination { get; set; }
        public List<events> events { get; set; }
    }

    public class events
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
