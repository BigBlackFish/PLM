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
                if (LayoutFileListInfo.Data.records.Count > 0)
                {
                    viewModel.EmptyState = false;
                }
                foreach (var item in LayoutFileListInfo.Data.records)
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

            //if ((await AdminService.GetUserinfomation()) is APIResult<UserInfomation> userinfo)
            //{

            //}
            //viewModel.Files.Add(new PageFileListViewModel
            //{
            //    Id = 154323,
            //    Image = "/Library/Image/jpg.png",
            //    ImageInfomation = "版面一缩略图",
            //    AssociationId = 1234556,
            //    PageInfomation = "抛光油版面一面",
            //    remarksinfomation = "用于新花色创建新的项目中？？？？？，用于新花色",
            //    UploadDate = "2022-04-17 03:28:00",
            //    Uploader = "BigBlackFish"
            //});
            //if (viewModel.Files.Count > 0)
            //{
            //    viewModel.EmptyState = false;
            //}
            //else
            //{
            //    viewModel.EmptyState = true;
            //}
        }

        private void PageListsMain_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {

        }
    }
}
