using PLM.Common;
using PLM.Models.ViewModels;
using System;
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
                item.Download_ContinueTransmission();
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
            if (ClassHelper.downloadingPage.DataContext is DownloadingPageViewModel downloading)
            {
                downloading.Files.Remove(viewModel);
            }
        }
    }
}
