using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using herokuTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace herokuTest.Controllers
{
    [Produces("application/json")]
    
    public class LineController : Controller
    {


        [Route("api/Line")]
        [HttpPost]
        public ActionResult qwe([FromBody] LineWebhookModels x = null)
        {
            if (x == null) return BadRequest();
            else
            {
                try
                {
                    LineReply(x);
                    return Ok(x);
                }
                catch(Exception ex)
                {
                    return BadRequest(ex);
                }
            }          
        }

        public void LineReply(LineWebhookModels x)
        {
            string ApiUrl = "https://api.line.me/v2/bot/message/reply";
            string AccessToken = "PQ8Lm5Wj/R1dD3KO4crlrEfeKN7axCE51n+1Ww61gT2YxxhmvFCkb3GxxlNU8pAth2vWYdkqg6y3OF6YUJJUerEktH+BGA/1DD0tcEFI5heQWLQx1SjUBDDpaAB2AOI9Yp5RsbptVn9fKFcGOX0OrAdB04t89/1O/w1cDnyilFU=";
            string replyText = "你媽的".IndexOf(x.events[0].message.text) > -1 ? "別說髒話" : "你說了:" + x.events[0].message.text;


            string strData = @"
               {
                    ""replyToken"":"""+ x.events[0].replyToken + @""",
                    ""messages"":[
                        {
                            ""type"":""text"",
                            ""text"":"""+ replyText + @"""
                        }
                    ]
                }
            ";
            
            Encoding encoding = Encoding.UTF8;
            byte[] data = encoding.GetBytes(strData);

            try
            {
                HttpWebRequest httpWebReq = HttpWebRequest.Create(ApiUrl) as HttpWebRequest;  //初始化新的HttpWebRequest物件
                httpWebReq.Method = "POST";
                httpWebReq.ContentType = "application/json";  //設定Content-type HTTP標頭的值
                httpWebReq.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + AccessToken);
                httpWebReq.Timeout = 120000;  //設定GetResponse和GetRequestStream方法的逾時毫秒數(預設約為100秒)秒數(預設約為100秒)
                                              //httpWebReq.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + "asdasdasd");

                httpWebReq.ContentLength = data.Length;  //設定Content-length HTTP標頭
                using (Stream reqStream = httpWebReq.GetRequestStream())  //取得用來寫入要求資料的Stream物件
                {
                    reqStream.Write(data, 0, data.Length);  //將一連串的位元組寫入目前的資料流
                }

                using (WebResponse wr = httpWebReq.GetResponse())  //傳回來自網際網路資源的資料流
                {
                    //對接收到的頁面內容進行處理
                    //using (StreamReader sr = new StreamReader(wr.GetResponseStream(), encoding))
                    //{
                    //    responseData = sr.ReadToEnd();
                    //    LineLoginToken ToKenObj = JsonConvert.DeserializeObject<LineLoginToken>(responseData);
                    //    LineLogin_WebRequest_GetUserProfile(ToKenObj.access_token);
                    //}
                }
            }
            catch (WebException ex)
            {
                HttpWebResponse response = (HttpWebResponse)ex.Response;
                if (response != null && response.ContentLength != 0)
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                    {
                        string strError = sr.ReadToEnd();
                        //return strError;
                    }
                }
            }


        }
    }

    
}