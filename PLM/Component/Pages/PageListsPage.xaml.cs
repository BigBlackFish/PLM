using FluentFTP;
using PLM.Common;
using PLM.Library.Controls;
using PLM.Models;
using PLM.Models.ViewModels;
using PLM.Service;
using System;
using System.IO;
using System.Threading;
using System.Windows.Controls;

namespace PLM.Component.Pages
{
    /// <summary>
    /// PageListsPage.xaml 的交互逻辑
    /// </summary>
    public partial class PageListsPage : Page
    {
        private readonly FtpClient ftpClient = new FtpClient(ClassHelper.ftpPath, ClassHelper.ftpUsername, ClassHelper.ftppassword);
        private readonly PageListsPageViewModel viewModel;
        public PageListsPage()
        {
            InitializeComponent();
            viewModel = DataContext as PageListsPageViewModel;
            viewModel.PropertyChanged += ViewModel_PropertyChanged;
            PageListItem._ReFresh+= SelectInfo;
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(viewModel.SelectPage))
            {
                SelectInfo();
            }
        }

        private void PageListsMain_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            SelectInfo();
        }

        private void PageListsMain_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {

        }

        private async void SelectInfo()
        {
            viewModel.Files.Clear();
            if ((await AdminService.GetLayoutFileList(viewModel.SelectPage, 6, viewModel.FileName, viewModel.CreateStartTime, viewModel.CreateEndTime, viewModel.CreateNickName) is APIResult<Records> LayoutFileListInfo))
            {
                if (LayoutFileListInfo.Data == null)
                {
                    return;
                }

                viewModel.NumberofPages = int.Parse(LayoutFileListInfo.Data.totalPage.ToString());
                if (LayoutFileListInfo.Data.list.Count > 0)
                {
                    viewModel.EmptyState = false;
                }
                    
                foreach (PageListResultModel item in LayoutFileListInfo.Data.list)
                {
                    //ThreadPool.QueueUserWorkItem(s => DownLoadPicture(item));
                    viewModel.Files.Add(new PageFileListViewModel
                    {
                        Id = item.id == null ? "" : item.id.ToString(),
                        ImageInfomation = item.summaryFileName,
                        AssociationId = item.terminalSummaryFileId == null ? "" : item.terminalSummaryFileId.ToString(),
                        PageInfomation = item.layoutInfo,
                        Remarksinfomation = item.remark,
                        UploadDate = item.updateTime,
                        Uploader = item.updateUserName,
                        SummaryFileSize = Convert.ToInt64(item.summaryFileSize),
                        SummaryFileMd5 = item.summaryFileMd5,
                        SummaryFilePwd = item.summaryFilePwd,
                        SummaryFileName = item.summaryFileName,
                        SummaryFileType = item.summaryFileType,
                        SummaryContentType = item.summaryContentType,
                        SourceFileSize = Convert.ToInt64(item.sourceFileSize),
                        SourceFileMd5 = item.sourceFileMd5,
                        SourceFilePwd = item.sourceFilePwd,
                        SourceFileName = item.sourceFileName,
                        SourceFileType = item.sourceFileType,
                        SourceContentType = item.sourceContentType,
                    });
                }
            }
        }

        private void Butselect_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SelectInfo();
        }

        private void ButReset_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            viewModel.FileName = string.Empty;
            viewModel.CreateStartTime = string.Empty;
            viewModel.CreateEndTime = string.Empty;
            viewModel.CreateNickName = string.Empty;
        }

        private async void DownLoadPicture(PageListResultModel pageListResultModel)
        {
            try
            {
                string SavePath = System.IO.Path.Combine(ClassHelper.PageListPicturePath, pageListResultModel.id);
                await ftpClient.DownloadFileAsync(SavePath, pageListResultModel.summaryFileUrl, FtpLocalExists.Overwrite);
                
            }
            catch
            {

            }
        }

    }
}
