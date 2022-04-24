using PLM.Common;
using PLM.Models;
using PLM.Models.ViewModels;
using PLM.Service;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace PLM.Library.Controls
{
    /// <summary>
    /// UploadItem.xaml 的交互逻辑
    /// </summary>
    public partial class UploadItem : UserControl
    {
        private FileGroupViewModel viewModel;

        public UploadItem()
        {
            InitializeComponent();
        }

        private void UserControlMain_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is FileGroupViewModel model)
            {
                viewModel = model;
                foreach (FileViewModel item in viewModel.FileViews)
                {
                    item.PropertyChanged += Item_PropertyChanged;
                }
            }
        }

        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "UploadCompleted")
            {
                AddLayoutMessage();
            }
        }

        private void StpSuspend_PointerUp(object sender, EventArgs e)
        {
            foreach (FileViewModel item in viewModel.FileViews)
            {
                item.SuspendTransmission();
            }
            viewModel.IsTransfer = false;
        }

        private void StpContinue_PointerUp(object sender, EventArgs e)
        {
            foreach (FileViewModel item in viewModel.FileViews)
            {
                item.Upload_ContinueTransmission();
            }
            viewModel.IsTransfer = true;
        }

        private void StpCancel_PointerUp(object sender, EventArgs e)
        {
            foreach (FileViewModel item in viewModel.FileViews)
            {
                item.CancelTransmission();
            }
            viewModel.IsTransfer = false;
        }

        private void StpDelete_PointerUp(object sender, EventArgs e)
        {
            foreach (FileViewModel item in viewModel.FileViews)
            {
                item.CancelTransmission();
            }
            if (ClassHelper.uploadingPage.DataContext is UploadingPageViewModel uploading)
            {
                uploading.Files.Remove(viewModel);
            }
        }

        #region 执行事件
        private async void AddLayoutMessage()
        {
            foreach (FileViewModel item in viewModel.FileViews)
            {
                if (!item.UploadCompleted)
                {
                    return;
                }
            }
            viewModel.TransferComplete = true;
            FileViewModel fileView1 = viewModel.FileViews[0];
            FileViewModel fileView2 = viewModel.FileViews[1];
            AddTerminalLayoutFileModel addTerminal = new AddTerminalLayoutFileModel()
            {
                LayoutInfo = fileView1.Message,
                Remark = fileView1.Remark,
                TerminalSourceFileId = string.Empty,
                TerminalSummaryFileId = string.Empty
            };
            #region 源文件
            FileInfo fileInfo1 = new FileInfo(fileView1.Path);
            addTerminal.SourceFileSize = fileInfo1.Length / 1024;
            addTerminal.SourceFileMd5 = fileView1.MD5;
            addTerminal.SourceFilePwd = fileView1.SavePath;
            addTerminal.SourceFileName = fileView1.SaveName;
            addTerminal.SourceFileType = fileView1.FileType;
            addTerminal.SourceContentType = ClassHelper.GetMimeMapping(fileInfo1.FullName);
            addTerminal.SourceFileUrl = string.Empty;
            #endregion
            #region 缩略图
            FileInfo fileInfo2 = new FileInfo(fileView2.Path);
            addTerminal.SummaryFileSize = fileInfo2.Length / 1024;
            addTerminal.SummaryFileMd5 = fileView2.MD5;
            addTerminal.SummaryFilePwd = fileView2.SavePath;
            addTerminal.SummaryFileName = fileView2.SaveName;
            addTerminal.SummaryFileType = fileView2.FileType;
            addTerminal.SummaryContentType = ClassHelper.GetMimeMapping(fileInfo2.FullName);
            addTerminal.SummaryFileUrl = string.Empty;
            #endregion
            if ((await AdminService.TerminalLayoutFileAdd(addTerminal)) is APIResult result)
            {
                if (result.Code == 0)
                {
                    ClassHelper.MessageAlert(ClassHelper.MainWindow.GetType(), 0, ClassHelper.FindResource<string>("SuccessfullyAdded"));
                }
            }
        }
        #endregion
    }
}
