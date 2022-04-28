namespace PLM.Models.ViewModels
{
    public class LoginWindowViewModel : ModelBase
    {
        private string userName;
        private string password;
        private bool isSelect;

        public string UserName
        {
            get => userName;
            set
            {
                userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public bool Isselect
        {
            get => isSelect;
            set
            {
                isSelect = value;
                OnPropertyChanged(nameof(Isselect));
            }
        }

        public override void InitializeVariable()
        {
            UserName = "13138107500";
            Password = "plm@2022";
            Isselect = false;
        }
    }
}
