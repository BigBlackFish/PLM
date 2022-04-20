namespace PLM.Models.ViewModels
{
    public class LoginWindowViewModel : ModelBase
    {
        private string userName;
        private string password;

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

        public override void InitializeVariable()
        {
            UserName = "13138107500";
            Password = "plm@2022";
        }
    }
}
