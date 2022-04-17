using PLM.Models;
using PLM.Models.ViewModels;
using PLM.Service;
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

        private async void PageListsMain_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            PageListsPageViewModel a = new PageListsPageViewModel();
            if ((await AdminService.GetLayoutFileList(1, 6, "", "") is APIResult<Records> LayoutFileListInfo))
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
                        Id= long.Parse(item.id),
                        ImageInfomation=item.summaryFileName,
                        AssociationId= long.Parse(item.terminalSummaryFileId),
                        PageInfomation=item.layoutInfo,
                        remarksinfomation=item.remark,
                        UploadDate=item.updateTime,
                        Uploader=item.updateUserName
                    });
                }
            }
        }

        private void PageListsMain_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {

        }
    }
}
