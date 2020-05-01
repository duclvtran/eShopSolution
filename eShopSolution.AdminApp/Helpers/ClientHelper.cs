using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Helpers
{
    public class ClientHelper
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ClientHelper(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> CallApi(string url, string method, object request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:5001");
            var response = await client.PostAsync(url, httpContent);
            var token = await response.Content.ReadAsStringAsync();
            return token;
        }

        /// <summary>
        /// Hàm gọi api từ các hệ thống khác thông qua RESTFUL API
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="request"></param>
        /// <param name="isReturnCatch">Thêm để trả về null khi call API bị lỗi</param>
        /// <param name="headers">Thêm headaer khi goi cac api yeu cau co header</param>
        /// <returns></returns>
        public List<T> CallAPIList<T>(string url, string method, object request, bool isReturnCatch = false, Dictionary<String, String> headers = null) where T : class
        {
            string json = "";
            List<T> data = new List<T>();
            try
            {
                HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(url);
                Request.Method = method.ToString();
                Request.KeepAlive = false;
                Request.ContentType = "application/json; charset=UTF-8";

                if (headers != null)
                {
                    foreach (KeyValuePair<string, string> entry in headers)
                    {
                        Request.Headers.Add(entry.Key, entry.Value);
                    }
                }

                if (method.ToUpper() == "POST")
                {
                    Byte[] byteArray = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(request));
                    Request.ContentLength = byteArray.Length;
                    Stream dataStream = Request.GetRequestStream();
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Close();
                }

                /*Kiểm tra kết quả trả về */
                WebResponse Response = Request.GetResponse();
                HttpStatusCode ResponseCode = ((HttpWebResponse)Response).StatusCode;
                if (ResponseCode.Equals(HttpStatusCode.Unauthorized) || ResponseCode.Equals(HttpStatusCode.Forbidden))
                {
                    // Log lỗi chứng thực khi gọi API
                }
                else if (!ResponseCode.Equals(HttpStatusCode.OK))
                {
                    //Log khi gọi API Lỗi
                }
                else
                {
                    StreamReader Reader = new StreamReader(Response.GetResponseStream());
                    json = Reader.ReadToEnd();
                    Reader.Close();
                    data = JsonSerializer.Deserialize<List<T>>(json);

                    //Log khi gọi API thành công
                }
            }
            catch (Exception ex)
            {
                //Log
            }

            return data;
        }

        /// <summary>
        /// Hàm gọi api từ các hệ thống khác thông qua RESTFUL API
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="request"></param>
        /// <param name="isReturnCatch">Thêm để trả về null khi call API bị lỗi</param>
        /// <param name="headers">Thêm headaer khi goi cac api yeu cau co header</param>
        /// <returns></returns>
        public T CallAPIModel<T>(string url, string method, object request, bool isReturnCatch = false, Dictionary<string, string> headers = null) where T : class
        {
            //Log trước khi gọi API

            string json = "";
            T response = null;

            try
            {
                HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(url);
                Request.Method = method.ToString();
                Request.KeepAlive = false;
                Request.ContentType = "application/json; charset=UTF-8";

                if (headers != null)
                {
                    foreach (KeyValuePair<string, string> entry in headers)
                    {
                        Request.Headers.Add(entry.Key, entry.Value);
                    }
                }

                if (method.ToUpper() == "POST")
                {
                    Byte[] byteArray = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(request));
                    Request.ContentLength = byteArray.Length;
                    Stream dataStream = Request.GetRequestStream();
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Close();
                }
                /*Kiểm tra kết quả trả về */
                WebResponse Response = Request.GetResponse();
                HttpStatusCode ResponseCode = ((HttpWebResponse)Response).StatusCode;
                if (ResponseCode.Equals(HttpStatusCode.Unauthorized) || ResponseCode.Equals(HttpStatusCode.Forbidden))
                {
                    // Log lỗi chứng thực khi gọi API
                }
                else if (!ResponseCode.Equals(HttpStatusCode.OK))
                {
                    //Log khi gọi API Lỗi
                }
                else
                {
                    StreamReader Reader = new StreamReader(Response.GetResponseStream());
                    json = Reader.ReadToEnd();
                    Reader.Close();
                    response = JsonSerializer.Deserialize<T>(json);

                    //Log return API
                }
            }
            catch (Exception ex)
            {
                //Log try catch
            }

            return response;
        }
    }
}