using PLM.Common;
using PLM.Models;
using PLM.Models.ViewModels;
using PLM.Service;
using System.Windows;
using System.Windows.Controls;

namespace PLM.Library.Controls
{
    /// <summary>
    /// PageListItem.xaml 的交互逻辑
    /// </summary>
    public partial class PageListItem : UserControl
    {
        private PageFileListViewModel viewModel;
        public PageListItem()
        {
            InitializeComponent();
        }



        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is PageFileListViewModel model)
            {
                viewModel = model;
            }
        }

        private async void ImgDelete_PointerDown(object sender, System.EventArgs e)
        {
            if ((await AdminService.DeleteLayoutFileList(viewModel.Id.ToString()) is APIResult Result))
            {
                if (Result.Code == 0)
                {
                    ClassHelper.MessageAlert(ClassHelper.MainWindow.GetType(), 0, "删除成功");
                }
            }
        }

        private void ImgDownload_PointerUp(object sender, System.EventArgs e)
        {
            FileGroupViewModel fileGroupView = new FileGroupViewModel
            {
                IsUpload = false
            };
            FileViewModel fileView1 = new FileViewModel
            {
                Name = viewModel.SourceFileName,
                Path = viewModel.SourceFilePwd,
                FileType = viewModel.SourceFileType,
                Message = viewModel.PageInfomation,
                Remark = viewModel.Remarksinfomation,
                Size = viewModel.SourceFileSize / 1024
            };
            fileGroupView.FileViews.Add(fileView1);
            FileViewModel fileView2 = new FileViewModel
            {
                Name = viewModel.SummaryFileName,
                Path = viewModel.SummaryFilePwd,
                FileType = viewModel.SummaryFileType,
                Message = viewModel.PageInfomation,
                Remark = viewModel.Remarksinfomation,
                Size = viewModel.SummaryFileSize / 1024
            };
            fileGroupView.FileViews.Add(fileView2);
            (ClassHelper.downloadingPage.DataContext as DownloadingPageViewModel).Files.Add(fileGroupView);
        }
    }
}
