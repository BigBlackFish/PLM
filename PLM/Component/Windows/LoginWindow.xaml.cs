using PLM.Common;
using PLM.Models;
using PLM.Models.ViewModels;
using PLM.Service;
using System;
using System.Windows;

namespace PLM.Component.Windows
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly LoginWindowViewModel viewModel;

        public LoginWindow()
        {
            InitializeComponent();

            ClassHelper.LoginWindow = this;
            viewModel = DataContext as LoginWindowViewModel;
        }

        private void LoginMain_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void LoginMain_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        private async void BrdLogin_PointerUp(object sender, EventArgs e)
        {
            if ((await AdminService.Login(viewModel.UserName, viewModel.Password)) is APIResult<LoginResultModel> result)
            {
                if (result.Code == 0)
                {
                    ClassHelper.Token = result.Data.TokenType;
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    Close();
                }
                else
                {
                    ClassHelper.AlertMessageBox(this, ClassHelper.MessageBoxType.Inform, ClassHelper.FindResource<string>("Inform"), result.Message);
                }
            }
        }
    }
}
