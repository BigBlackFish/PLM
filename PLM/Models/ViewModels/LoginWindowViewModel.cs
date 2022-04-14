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
            UserName = string.Empty;
            Password = string.Empty;
        }
    }
}
