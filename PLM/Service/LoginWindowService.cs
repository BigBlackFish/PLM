using PLM.Common;
using PLM.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLM.Service
{
    public class LoginWindowService
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="deviceNum">用户名</param>
        /// <param name="meetingId">密码</param>
        /// <returns></returns>
        public APIResult<LoginWindowViewModel> Longin(string username, string password)
        {
            Dictionary<string, object> paramKeyValue = new Dictionary<string, object>();
            paramKeyValue.Add("username", username);
            paramKeyValue.Add("password", password);
            return HttpRequester.PostForm<APIResult<LoginWindowViewModel>>("admin/login", paramKeyValue);
        }

    }
}
