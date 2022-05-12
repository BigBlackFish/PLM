using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
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

        public static async Task<APIResult<Records>> GetLayoutFileList(int current, int size, string fileName, string createStartTime, string createEndTime, string createNickName)
        {
            APIResult<Records> result = null;
            if ((await HttpHelper.SendGet($"{ClassHelper.servicePath}/plm/terminalLayoutFile/search?current={current}&size={size}&fileName={fileName}&createStartTime={createStartTime}&createEndTime={createEndTime}&createNickName={createNickName}", true)) is string str)
            {
                result = JsonConvert.DeserializeObject<APIResult<Records>>(str);
            }
            return result;
        }

        public static async Task<APIResult> DeleteLayoutFileList(string ids)
        {
            APIResult result = null;
            List<KeyValuePair<string, string>> data = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("ids",ids),
            };
            if ((await HttpHelper.SendFormPost($"{ClassHelper.servicePath}/plm/terminalLayoutFile/batch/remove", data, true)) is string str)
            {
                result = JsonConvert.DeserializeObject<APIResult>(str);
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

        public static async Task<APIResult> TerminalLayoutFileAdd(AddTerminalLayoutFileModel data)
        {
            APIResult result = null;
            if ((await HttpHelper.SendJsonPost($"{ClassHelper.servicePath}/plm/terminalLayoutFile/add", JObject.FromObject(data, new JsonSerializer() { ContractResolver = new CamelCasePropertyNamesContractResolver() }), true)) is string str)
            {
                result = JsonConvert.DeserializeObject<APIResult>(str);
            }
            return result;
        }

        public static async Task<APIResult<ImageFileModel>>  UpLoadimage(string url)
        {
            APIResult<ImageFileModel> result = null;
            List<KeyValuePair<string, string>> data = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("file",url),
            };
            if ((await HttpHelper.UploadFormPost($"{ClassHelper.servicePath}/filecenter/file/upload", data, true)) is string str)
            {
                result = JsonConvert.DeserializeObject<APIResult<ImageFileModel>>(str);
            }
            return result;
        }
    }
}
