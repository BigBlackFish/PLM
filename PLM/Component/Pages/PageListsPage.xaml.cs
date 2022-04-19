using PLM.Models;
using PLM.Models.ViewModels;
using PLM.Service;
using System.Windows;
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

        private void PageListsMain_Loaded(object sender, RoutedEventArgs e)
        {
            SelectInfo();
        }

        private async void SelectInfo()
        {
            viewModel.Files.Clear();
            PageListsPageViewModel a = new PageListsPageViewModel();
            if ((await AdminService.GetLayoutFileList(1, 6, viewModel.FileName, viewModel.CreateStartTime, viewModel.CreateEndTime, viewModel.CreateNickName) is APIResult<Records> LayoutFileListInfo))
            {
                if (LayoutFileListInfo.Data == null)
                    return;
                if (LayoutFileListInfo.Data.list.Count > 0)
                {
                    viewModel.EmptyState = false;
                }
                foreach (var item in LayoutFileListInfo.Data.list)
                {
                    viewModel.Files.Add(new PageFileListViewModel
                    {
                        Id = item.id== null ? "" : item.id.ToString(),
                        ImageInfomation = item.summaryFileName,
                        AssociationId = item.terminalSummaryFileId== null?"": item.terminalSummaryFileId.ToString(),
                        PageInfomation = item.layoutInfo,
                        remarksinfomation = item.remark,
                        UploadDate = item.updateTime,
                        Uploader = item.updateUserName
                    }); ;
                }
            }
        }

        private async void Butselect_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SelectInfo();
        }

        private void ButReset_Click(object sender, RoutedEventArgs e)
        {
            viewModel.FileName = string.Empty;
            viewModel.CreateStartTime = string.Empty;
            viewModel.CreateEndTime = string.Empty;
            viewModel.CreateNickName = string.Empty;
        }
    }
}
