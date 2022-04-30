using PLM.Common;
using PLM.Models.ViewModels;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace PLM.Library.Controls
{
    /// <summary>
    /// DownloadItem.xaml 的交互逻辑
    /// </summary>
    public partial class DownloadItem : UserControl
    {
        private FileGroupViewModel viewModel;

        public DownloadItem()
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
            if (e.PropertyName == "DownloadCompleted")
            {
                DeleteTask();
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
                item.Download_ContinueTransmission();
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
            if (ClassHelper.downloadingPage.DataContext is DownloadingPageViewModel downloading)
            {
                downloading.Files.Remove(viewModel);
            }
        }

        #region 执行事件
        private void DeleteTask()
        {
            foreach (FileViewModel item in viewModel.FileViews)
            {
                if (!item.DownloadCompleted)
                {
                    return;
                }
            }
            ClassHelper.MessageAlert(ClassHelper.MainWindow.GetType(), 0, $"{viewModel.FileViews[0].Message} {ClassHelper.FindResource<string>("DownloadComplete")}");
        }
        #endregion
    }
}
