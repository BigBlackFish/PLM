using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLM.Models.ViewModels
{
    public class MainWimdowViewModel:ModelBase
    {
        private string userName;

        private string identity;

        private string phone;

        private string loginTime;

        private string imgurl;

        public string UserName
        {
            get => userName;
            set
            {
                userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        public string Identity
        {
            get => identity;
            set
            {
                identity = value;
                OnPropertyChanged(nameof(Identity));
            }
        }

        public string Phone
        {
            get => phone;
            set
            {
                phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }

        public string LoginTime
        {
            get => loginTime;
            set
            {
                loginTime = value;
                OnPropertyChanged(nameof(LoginTime));
            }
        }

        public string ImgUrl
        {
            get => imgurl;
            set
            {
                imgurl = value;
                OnPropertyChanged(nameof(ImgUrl));
            }
        }


        public override void InitializeVariable()
        {
            userName = "未知";
            identity = "0";
            phone = "0";
            loginTime = "0";
            imgurl = "/Library/Image/boy.png";

        }
    }
}
