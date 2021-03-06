using Microsoft.Win32;
using PLM.Common;
using PLM.Models;
using PLM.Models.ViewModels;
using PLM.Service;
using System;
using System.IO;
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



        private async void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is PageFileListViewModel model)
            {
                viewModel = model;
                if (!string.IsNullOrEmpty(model.SummaryFilePwd) && !string.IsNullOrEmpty(model.PageInfomation) && !string.IsNullOrEmpty(model.SummaryFileName))
                {
                    viewModel.Image = await ClassHelper.DownloadImage(model.SummaryFilePwd, model.PageInfomation, model.SummaryFileName);
                }
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
            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string outDir = folderBrowserDialog.SelectedPath;

                FileGroupViewModel fileGroupView = new FileGroupViewModel
                {
                    Message = viewModel.PageInfomation,
                    Remark = viewModel.Remarksinfomation,
                    IsUpload = false
                };
                if (!string.IsNullOrEmpty(viewModel.SourceFilePwd) && await ClassHelper.ExistServiceFile(viewModel.SourceFilePwd))
                {
                    FileViewModel fileView1 = new FileViewModel
                    {
                        Name = viewModel.SourceFileName,
                        Path = viewModel.SourceFilePwd,
                        FileType = viewModel.SourceFileType,
                        Message = viewModel.PageInfomation,
                        Remark = viewModel.Remarksinfomation,
                        Size = Math.Round((double)viewModel.SourceFileSize / 1024 / 1024, 4),
                        OutPath = outDir
                    };
                    fileGroupView.FileViews.Add(fileView1);
                    fileGroupView.SourceFile = fileView1;
                }
                if (!string.IsNullOrEmpty(viewModel.SummaryFilePwd) && await ClassHelper.ExistServiceFile(viewModel.SummaryFilePwd))
                {
                    FileViewModel fileView2 = new FileViewModel
                    {
                        Name = viewModel.SummaryFileName,
                        Path = viewModel.SummaryFilePwd,
                        FileType = viewModel.SummaryFileType,
                        Message = viewModel.PageInfomation,
                        Remark = viewModel.Remarksinfomation,
                        Size = Math.Round((double)viewModel.SummaryFileSize / 1024 / 1024, 4),
                        OutPath = outDir
                    };
                    fileGroupView.FileViews.Add(fileView2);
                    fileGroupView.SummaryFile = fileView2;
                }
                if (fileGroupView.FileViews.Count > 0)
                {
                    downloading.Files.Add(fileGroupView);
                    ClassHelper.MessageAlert(ClassHelper.MainWindow.GetType(), 0, $"{fileGroupView.FileViews[0].Message} {ClassHelper.FindResource<string>("StartDownload")}");
                }
            }
        }
    }
}
