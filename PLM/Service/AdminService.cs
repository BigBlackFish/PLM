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

        public static async Task<APIResult<Records>> GetLayoutFileList(int current, int size, string sourceFileName, string summaryFileName)
        {
            APIResult<Records> result = null;
            if ((await HttpHelper.SendGet($"{ClassHelper.servicePath}/plm/terminalLayoutFile/search?{current},{size}",true)) is string str)
            {
                result = JsonConvert.DeserializeObject<APIResult<Records>>(str);
            }
            return result;
        }

        public static async Task<APIResult<UserInfomation>> GetUserinfomation()
        {
            APIResult<UserInfomation> result = null;
            if ((await HttpHelper.SendGet($"{ClassHelper.servicePath}/admin/current/user", true)) is string str)
            {
                result = JsonConvert.DeserializeObject<APIResult<UserInfomation>>(str);
            }
            return result;
        }
    }
}
