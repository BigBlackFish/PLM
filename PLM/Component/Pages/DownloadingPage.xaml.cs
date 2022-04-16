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
            FileGroupViewModel fileGroup = new FileGroupViewModel();
            fileGroup.FileViews.Add(new FileViewModel()
            {
                Name = "测试下载一",
                Message = "版面信息",
                Size = 123123
            });
            fileGroup.FileViews.Add(new FileViewModel()
            {
                Name = "测试二",
                Message = "版面信息",
                Size = 123123
            });
            viewModel.Files.Add(fileGroup);
        }
    }
}
