using PLM.Common;
using PLM.Models;
using PLM.Models.ViewModels;
using PLM.Service;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PLM.Library.Controls
{
    public delegate void ReFresh();//删除数据重新查询刷新
    /// <summary>
    /// PageListItem.xaml 的交互逻辑
    /// </summary>
    public partial class PageListItem : UserControl
    {
        public static event ReFresh _ReFresh;

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
                    _ReFresh?.Invoke();
                }
            }
        }

        private async void ImgDownload_PointerUp(object sender, System.EventArgs e)
        {
            DownloadingPageViewModel downloading = ClassHelper.downloadingPage.DataContext as DownloadingPageViewModel;
            foreach (FileGroupViewModel item in downloading.Files)
            {
                if (item.FileViews.Any(f => f.Message == viewModel.PageInfomation))
                {
                    ClassHelper.MessageAlert(ClassHelper.MainWindow.GetType(), 1, ClassHelper.FindResource<string>("TaskAlreadyExists"));
                    return;
                }
            }
            FileGroupViewModel fileGroupView = new FileGroupViewModel
            {
                IsUpload = false
            };
            if (await ClassHelper.ExistServiceFile(viewModel.SourceFilePwd))
            {
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
            }
            if (await ClassHelper.ExistServiceFile(viewModel.SummaryFilePwd))
            {
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
            }
            if (fileGroupView.FileViews.Count > 0)
            {
                downloading.Files.Add(fileGroupView);
                ClassHelper.MessageAlert(ClassHelper.MainWindow.GetType(), 0, $"{fileGroupView.FileViews[0].Message} {ClassHelper.FindResource<string>("StartDownload")}");
            }
        }
    }
}
