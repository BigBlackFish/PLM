using PLM.Models.ViewModels;
using System.Windows.Controls;

namespace PLM.Component.Pages
{
    /// <summary>
    /// PageListsPage.xaml 的交互逻辑
    /// </summary>
    public partial class PageListsPage : Page
    {
        private readonly PageListsPageViewModel viewModel;
        public PageListsPage()
        {
            InitializeComponent();
            viewModel = DataContext as PageListsPageViewModel;
        }

        private void PageListsMain_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            PageListsPageViewModel a = new PageListsPageViewModel();
            viewModel.Files.Add(new PageFileListViewModel
            {
                Id = 154323,
                Image = "/Library/Image/Jpg.png",
                ImageInfomation = "版面一缩略图",
                AssociationId = 1234556,
                PageInfomation = "抛光油版面一面",
                remarksinfomation = "用于新花色创建新的项目中？？？？？，用于新花色",
                UploadDate = "2022-04-17 03:28:00",
                Uploader = "BigBlackFish"
            });
            if (viewModel.Files.Count > 0)
            {
                viewModel.EmptyState = false;
            }
            else
            {
                viewModel.EmptyState = true;
            }
        }

        private void PageListsMain_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {

        }
    }
}
