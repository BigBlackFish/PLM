using PLM.Models.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace PLM.Component.Pages
{
    /// <summary>
    /// DownloadingPage.xaml 的交互逻辑
    /// </summary>
    public partial class DownloadingPage : Page
    {
        private readonly DownloadingPageViewModel viewModel;

        public DownloadingPage()
        {
            InitializeComponent();
            viewModel = DataContext as DownloadingPageViewModel;
        }

        private void DownloadingMain_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
