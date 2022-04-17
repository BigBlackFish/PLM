using PLM.Common;
using PLM.Models.ViewModels;
using System;
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
            }
        }

        private void StpSuspend_PointerUp(object sender, EventArgs e)
        {
            foreach (FileViewModel item in viewModel.FileViews)
            {
                item.SuspendTransmission();
            }
            stpSuspend.Visibility = Visibility.Collapsed;
            stpContinue.Visibility = Visibility.Visible;
        }

        private void StpContinue_PointerUp(object sender, EventArgs e)
        {
            foreach (FileViewModel item in viewModel.FileViews)
            {
                item.Upload_ContinueTransmission();
            }
            stpContinue.Visibility = Visibility.Collapsed;
            stpSuspend.Visibility = Visibility.Visible;
        }

        private void StpCancel_PointerUp(object sender, EventArgs e)
        {
            foreach (FileViewModel item in viewModel.FileViews)
            {
                item.CancelTransmission();
            }
            stpSuspend.Visibility = Visibility.Collapsed;
            stpContinue.Visibility = Visibility.Visible;
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
    }
}
