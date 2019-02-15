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
            string ChannelAccessToken = "PQ8Lm5Wj/R1dD3KO4crlrEfeKN7axCE51n+1Ww61gT2YxxhmvFCkb3GxxlNU8pAth2vWYdkqg6y3OF6YUJJUerEktH+BGA/1DD0tcEFI5heQWLQx1SjUBDDpaAB2AOI9Yp5RsbptVn9fKFcGOX0OrAdB04t89/1O/w1cDnyilFU=";

            try
            {
                //取得 http Post RawData(should be JSON)
                string postData = Request.Body.ToString();
                //剖析JSON
                var ReceivedMessage = isRock.LineBot.Utility.Parsing(postData);
                //回覆訊息
                string Message;
                Message = "你說了:" + ReceivedMessage.events[0].message.text;
                //回覆用戶
                isRock.LineBot.Utility.ReplyMessage(ReceivedMessage.events[0].replyToken, Message, ChannelAccessToken);
                //回覆API OK
                return Ok();
            }
            catch (Exception ex)
            {
                return Ok();
            }
        }
    }
}