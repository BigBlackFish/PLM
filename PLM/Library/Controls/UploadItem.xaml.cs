using PLM.Common;
using PLM.Models;
using PLM.Models.ViewModels;
using PLM.Service;
using System;
using System.Collections.Specialized;
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
                viewModel.FileViews.CollectionChanged += FileViews_CollectionChanged;
                foreach (FileViewModel item in viewModel.FileViews)
                {
                    item.PropertyChanged += Item_PropertyChanged;
                }
                AddLayoutMessage();
            }
        }

        private void FileViews_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (object item in e.NewItems)
                    {
                        (item as FileViewModel).PropertyChanged += Item_PropertyChanged;
                    }
                    viewModel.TransferComplete = false;
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (object item in e.OldItems)
                    {
                        (item as FileViewModel).PropertyChanged -= Item_PropertyChanged;
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    break;
                case NotifyCollectionChangedAction.Move:
                    break;
                case NotifyCollectionChangedAction.Reset:
                    break;
                default:
                    break;
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
            AddTerminalLayoutFileModel addTerminal = new AddTerminalLayoutFileModel()
            {
                LayoutInfo = viewModel.Message,
                Remark = viewModel.Remark,
                TerminalSourceFileId = Guid.NewGuid().ToString("D"),
                TerminalSummaryFileId = Guid.NewGuid().ToString("D")
            };
            foreach (FileViewModel item in viewModel.FileViews)
            {
                if (item.DataType)
                {
                    FileInfo fileInfo = new FileInfo(item.Path);
                    addTerminal.SourceFileSize = fileInfo.Length / 1024;
                    addTerminal.SourceFileMd5 = item.MD5;
                    addTerminal.SourceFilePwd = item.SavePath;
                    addTerminal.SourceFileName = item.SaveName;
                    addTerminal.SourceFileType = item.FileType;
                    addTerminal.SourceContentType = ClassHelper.GetMimeMapping(fileInfo.FullName);
                    addTerminal.SourceFileUrl = string.Empty;
                }
                else
                {
                    FileInfo fileInfo = new FileInfo(item.Path);
                    addTerminal.SummaryFileSize = fileInfo.Length / 1024;
                    addTerminal.SummaryFileMd5 = item.MD5;
                    addTerminal.SummaryFilePwd = item.SavePath;
                    addTerminal.SummaryFileName = item.SaveName;
                    addTerminal.SummaryFileType = item.FileType;
                    addTerminal.SummaryContentType = ClassHelper.GetMimeMapping(fileInfo.FullName);
                    addTerminal.SummaryFileUrl = string.Empty;
                }
            }
            if ((await AdminService.TerminalLayoutFileAdd(addTerminal)) is APIResult result)
            {
                if (result.Code == 0)
                {
                    Dispatcher.Invoke(delegate
                    {
                        UploadingPageViewModel uploading = ClassHelper.uploadingPage.DataContext as UploadingPageViewModel;
                        uploading.Files.Remove(viewModel);
                    });
                    ClassHelper.MessageAlert(ClassHelper.MainWindow.GetType(), 0, ClassHelper.FindResource<string>("SuccessfullyAdded"));
                }
            }
        }
        #endregion
    }
}
