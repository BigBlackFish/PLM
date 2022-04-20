using System.Collections.Generic;

namespace PLM.Models
{
    public class UserInfomation
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public   List<authoritieslist> Authorities { get; set; }
        public bool AccountNonLocked { get; set; }
        public bool AccountNonExpired { get; set; }
        public bool Enabled { get; set; }
        public bool CredentialsNonExpired { get; set; }
        public string ClientId { get; set; }
        public string Domain { get; set; }
        public string NickName { get; set; }
        public string Avatar { get; set; }
        public string AccountId { get; set; }
        public string AccountType { get; set; }
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public object Attrs { get; set; }
        public bool Admin { get; set; }
        public bool Company { get; set; }
        public bool User { get; set; }
        public object RoleList { get; set; }
        public string Tag { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string UserType { get; set; }
        public int Status { get; set; }
        public string OrgType { get; set; }
        public bool ClientOnly { get; set; }
    }

    public class authoritieslist
    {
        public string authorityId { get; set; }
        public string authority { get; set; }
        public string expireTime { get; set; }
        public string owner { get; set; }
        public bool isExpired { get; set; }
    }
}
