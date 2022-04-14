using PLM.Common;
using PLM.Models.ViewModels;
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

        private void BrdLogin_PointerUp(object sender, EventArgs e)
        {
            Console.WriteLine(viewModel);

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
