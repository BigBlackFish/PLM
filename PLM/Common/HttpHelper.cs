using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PLM.Common
{
    public static class HttpHelper
    {
        // POST请求(Form)
        public static async Task<string> SendFormPost(string url, List<KeyValuePair<string, string>> data, bool authentication = false)
        {
            try
            {
                byte[] dataBytes = Encoding.UTF8.GetBytes(data != null ? data.ToString() : string.Empty);
                HttpClient request = new HttpClient();
                if (authentication)
                {
                    request.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ClassHelper.Token);
                }
                FormUrlEncodedContent content = new FormUrlEncodedContent(data);
                return await (await request.PostAsync(url, content)).Content.ReadAsStringAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST请求(Json)
        public static async Task<string> SendJsonPost(string url, JObject data, bool authentication = false)
        {
            try
            {
                byte[] dataBytes = Encoding.UTF8.GetBytes(data != null ? data.ToString() : string.Empty);
                HttpClient request = new HttpClient();
                if (authentication)
                {
                    request.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ClassHelper.Token);
                }
                ByteArrayContent jsonContent = new ByteArrayContent(Encoding.UTF8.GetBytes(data != null ? data.ToString() : string.Empty));
                jsonContent.Headers.Add("Content-Type", "application/json");
                return await (await request.PostAsync(url, jsonContent)).Content.ReadAsStringAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST请求单独一个文件上传(Form)
        public static async Task<string> UploadFormPost(string url, List<KeyValuePair<string, string>> data, bool authentication = false)
        {
            try
            {
                HttpClient request = new HttpClient();
                if (authentication)
                {
                    request.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ClassHelper.Token);
                }
                var fileName = Path.GetFileName(data[0].Value);
                string type=Path.GetExtension(data[0].Value);
                MultipartFormDataContent multipartFormDataContent = new MultipartFormDataContent() {
                    //{new ByteArrayContent(File.ReadAllBytes(data[0].Value)), data[0].Key, fileName },
                    {new StreamContent(File.OpenRead(data[0].Value),(int)File.OpenRead(data[0].Value).Length),data[0].Key,fileName }
                };
                multipartFormDataContent.Add(new StringContent(type==".jpg"? "image/jpg": "image/png"), "contentType");
                return await (await request.PostAsync(url, multipartFormDataContent)).Content.ReadAsStringAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET请求
        public static async Task<string> SendGet(string url, bool authentication = false)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                if (authentication)
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ClassHelper.Token);
                }
                return await (await httpClient.GetAsync(url)).Content.ReadAsStringAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
