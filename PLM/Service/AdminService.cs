using Newtonsoft.Json;
using PLM.Common;
using PLM.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PLM.Service
{
    public static class AdminService
    {
        public static async Task<APIResult<LoginResultModel>> Login(string username, string password)
        {
            APIResult<LoginResultModel> result = null;
            List<KeyValuePair<string, string>> data = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password)
            };
            if ((await HttpHelper.SendFormPost($"{ClassHelper.servicePath}/admin/login/token", data)) is string str)
            {
                result = JsonConvert.DeserializeObject<APIResult<LoginResultModel>>(str);
            }
            return result;
        }
    }
}
