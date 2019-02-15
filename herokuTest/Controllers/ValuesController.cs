using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace herokuTest.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "ca", "b" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public string Post(string value)
        {
            string x = value;
            return x;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        public class DecryptParameter
        {
            public string EncKey { get; set; }
            public List<byte[]> EncData { get; set; }
        }
        [Route("api/BatchDecryptData")]
        [HttpPost]
        public ActionResult BatchDecryptData(DecryptParameter decData)
        {
           
            return Json(new ApiResult<List<string>>(
                CodecModule.DecryptData(decData.EncKey, decData.EncData)));
        }

        public class ApiResult<T>
        {
            /// <summary>
            /// 執行成功與否
            /// </summary>
            public bool Succ { get; set; }
            /// <summary>
            /// 結果代碼(0000=成功，其餘為錯誤代號)
            /// </summary>
            public string Code { get; set; }
            /// <summary>
            /// 錯誤訊息
            /// </summary>
            public string Message { get; set; }
            /// <summary>
            /// 資料時間
            /// </summary>
            public DateTime DataTime { get; set; }
            /// <summary>
            /// 資料本體
            /// </summary>
            public T Data { get; set; }


            public ApiResult() { }

            /// <summary>
            /// 建立成功結果
            /// </summary>
            /// <param name="data"></param>
            public ApiResult(T data)
            {
                Code = "0000";
                Succ = true;
                DataTime = DateTime.Now;
                Data = data;
            }
        }

        public class ApiError : ApiResult<object>
        {
            /// <summary>
            /// 建立失敗結果
            /// </summary>
            /// <param name="code"></param>
            /// <param name="message"></param>
            public ApiError(string code, string message)
            {
                Code = code;
                Succ = false;
                this.DataTime = DateTime.Now;
                Message = message;
            }
        }



    }

    public class CodecModule
    {

        public static byte[] EncrytString(string encKey, string rawText)
        {
            return DESEncrypt(encKey, Encoding.UTF8.GetBytes(rawText));
        }

        public static List<string> DecryptData(string encKey, List<byte[]> data)
        {
            return data.Select(o =>
            {
                return Encoding.UTF8.GetString(DESDecrypt(o, encKey));
            }).ToList();
        }

        //REF: https://dotblogs.com.tw/supershowwei/2016/01/11/135230
        static byte[] HashByMD5(string source)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            return md5.ComputeHash(Encoding.UTF8.GetBytes(source));
        }

        static byte[] DESEncrypt(string key, byte[] data)
        {
            var des = new DESCryptoServiceProvider();
            Rfc2898DeriveBytes rfc2898 = new Rfc2898DeriveBytes(key, HashByMD5(key));
            des.Key = rfc2898.GetBytes(des.KeySize / 8);
            des.IV = rfc2898.GetBytes(des.BlockSize / 8);
            using (MemoryStream ms = new MemoryStream())
            using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cs.Write(data, 0, data.Length);
                cs.FlushFinalBlock();

                return ms.ToArray();
            }
        }

        static byte[] DESDecrypt(byte[] encData, string encKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            Rfc2898DeriveBytes rfc2898 = new Rfc2898DeriveBytes(encKey, HashByMD5(encKey));
            des.Key = rfc2898.GetBytes(des.KeySize / 8);
            des.IV = rfc2898.GetBytes(des.BlockSize / 8);
            using (MemoryStream ms = new MemoryStream())
            using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
            {
                cs.Write(encData, 0, encData.Length);
                cs.FlushFinalBlock();

                return ms.ToArray();
            }
        }

    }
}
