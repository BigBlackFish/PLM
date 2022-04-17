using PLM.Models.ViewModels;
using System;
using System.Threading.Tasks;
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
        private bool stop = false;
        private bool cancel = false;

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
            if (!stop)
            {
                foreach (FileViewModel item in viewModel.FileViews)
                {
                    item.SuspendTransmission();
                }
                stop = true;
                stpSuspend.Visibility = Visibility.Collapsed;
                stpContinue.Visibility = Visibility.Visible;
            }
        }

        private void StpContinue_PointerUp(object sender, EventArgs e)
        {
            if (stop || cancel)
            {
                foreach (FileViewModel item in viewModel.FileViews)
                {
                    if (stop)
                    {
                        item.Upload_ContinueTransmission();
                    }
                    else
                    {
                        item.FileUpload();
                    }
                }
                stop = false;
                cancel = false;
                stpContinue.Visibility = Visibility.Collapsed;
                stpSuspend.Visibility = Visibility.Visible;
            }
        }

        private void StpCancel_PointerUp(object sender, EventArgs e)
        {
            if (!cancel)
            {
                foreach (FileViewModel item in viewModel.FileViews)
                {
                    item.CancelTransmission();
                }
                cancel = true;
                stpSuspend.Visibility = Visibility.Collapsed;
                stpContinue.Visibility = Visibility.Visible;
            }
        }
    }
}
