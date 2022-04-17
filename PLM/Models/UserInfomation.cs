using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLM.Models
{
    public class UserInfomation
    {
        public int userId { get; set; }
        public string username { get; set; }
        public List<string> authorities { get; set; }
        public bool accountNonLocked { get; set; }
        public bool accountNonExpired { get; set; }
        public bool enabled  { get; set; }
        public bool credentialsNonExpired { get; set; }
        public string clientId { get; set; }
        public string domain { get; set; }
        public string nickName { get; set; }
        public string avatar { get; set; }
        public string accountId { get; set; }
        public string accountType { get; set; }
        public string companyId { get; set; }
        public string companyName { get; set; }
        public Nullable<int>  departmentId { get; set; }
        public string departmentName { get; set; }
        public object attrs { get; set; }
        public bool admin { get; set; }
        public bool company { get; set; }
        public bool user { get; set; }
        public object roleList { get; set; }
        public string tag { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string userType { get; set; }
        public int status { get; set; }
        public string orgType { get; set; }
        public bool clientOnly { get; set; }
    }
}
