using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLM.Common
{
    public class HttpRequester
    {
        private static string BaseUrl;
        private const int TimeOut = 3000;

        public static void SetURL(string ip, ushort port)
        {
            BaseUrl = $"http://{ip}:{port}";
        }

        public static T PostForm<T>(string apiName, Dictionary<string, object> paramKeyValue)
        {
            return PostFormRequset<T>(apiName, paramKeyValue);
        }

        public static T GetForm<T>(string apiName, Dictionary<string, object> paramKeyValue)
        {
            return GetFormRequset<T>(apiName, paramKeyValue);
        }


        private static T PostFormRequset<T>(string apiName, Dictionary<string, object> paramKeyValue)
        {
            try
            {
                var client = new RestClient(BaseUrl);
                var request = new RestRequest(apiName, Method.POST);
                client.Timeout = TimeOut;
                request.RequestFormat = DataFormat.Xml;
                foreach (var item in paramKeyValue)
                {
                    request.AddParameter(item.Key, item);
                }
                var response = client.Execute(request);
                if (response.ResponseStatus == ResponseStatus.Completed)
                {
                    if (string.IsNullOrWhiteSpace(response.Content))
                    {
                        return default(T);
                    }
                    else
                        return JsonHelper.DeserializeJsonToObject<T>(response.Content);
                }
                else
                    throw response.ErrorException;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private static T GetFormRequset<T>(string apiName, Dictionary<string, object> paramKeyValue)
        {
            try
            {
                var client = new RestClient(BaseUrl);
                var request = new RestRequest(apiName, Method.GET);
                client.Timeout = TimeOut;
                request.RequestFormat = DataFormat.Xml;
                foreach (var item in paramKeyValue)
                {
                    request.AddParameter(item.Key, item);
                }
                var response = client.Execute(request);
                if (response.ResponseStatus == ResponseStatus.Completed)
                {
                    if (string.IsNullOrWhiteSpace(response.Content))
                    {
                        return default(T);
                    }
                    else
                        return JsonHelper.DeserializeJsonToObject<T>(response.Content);
                }
                else
                    throw response.ErrorException;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
