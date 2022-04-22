using PLM.Common;
using PLM.Models.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace PLM.Component.Pages
{
    /// <summary>
    /// UploadingPage.xaml 的交互逻辑
    /// </summary>
    public partial class UploadingPage : Page
    {
        private readonly UploadingPageViewModel viewModel;

        public UploadingPage()
        {
            InitializeComponent();

            viewModel = DataContext as UploadingPageViewModel;
        }

        private void UploadingMain_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void CheAll_Checked(object sender, RoutedEventArgs e)
        {
            foreach (FileGroupViewModel item in viewModel.Files)
            {
                item.IsSelect = true;
            }
        }

        private void CheAll_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (FileGroupViewModel item in viewModel.Files)
            {
                item.IsSelect = false;
            }
        }

        private void BtnAllStop_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.Files.Count == 0)
            {
                ClassHelper.MessageAlert(ClassHelper.MainWindow.GetType(), 1, "没有进行中的任务。");
                return;
            }
            foreach (FileGroupViewModel item in viewModel.Files)
            {
                if (item.IsSelect && !item.TransferComplete)
                {
                    foreach (FileViewModel file in item.FileViews)
                    {
                        file.SuspendTransmission();
                    }
                }
            }
        }

        private void BtnAllStart_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.Files.Count == 0)
            {
                ClassHelper.MessageAlert(ClassHelper.MainWindow.GetType(), 1, "没有进行中的任务。");
                return;
            }
            foreach (FileGroupViewModel item in viewModel.Files)
            {
                if (item.IsSelect && !item.TransferComplete)
                {
                    foreach (FileViewModel file in item.FileViews)
                    {
                        file.Upload_ContinueTransmission();
                    }
                }
            }
        }

        private void BtnAllCancel_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.Files.Count == 0)
            {
                ClassHelper.MessageAlert(ClassHelper.MainWindow.GetType(), 1, "没有进行中的任务。");
                return;
            }
            ClassHelper.AlertMessageBox(ClassHelper.MainWindow, ClassHelper.MessageBoxType.Select, "提示", "是否全部取消？", rightButton: new Models.MessageBoxButtonModel
            {
                Hint = "确认",
                Action = () =>
                {
                    foreach (FileGroupViewModel item in viewModel.Files)
                    {
                        if (item.IsSelect && !item.TransferComplete)
                        {
                            foreach (FileViewModel file in item.FileViews)
                            {
                                file.CancelTransmission();
                            }
                        }
                    }
                }
            });
        }

        private void BtnAllDelete_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.Files.Count == 0)
            {
                ClassHelper.MessageAlert(ClassHelper.MainWindow.GetType(), 1, "没有进行中的任务。");
                return;
            }
            ClassHelper.AlertMessageBox(ClassHelper.MainWindow, ClassHelper.MessageBoxType.Select, "提示", "是否全部删除？", rightButton: new Models.MessageBoxButtonModel
            {
                Hint = "确认",
                Action = () =>
                {
                    foreach (FileGroupViewModel item in viewModel.Files)
                    {
                        if (item.IsSelect && !item.TransferComplete)
                        {
                            foreach (FileViewModel file in item.FileViews)
                            {
                                file.CancelTransmission();
                            }
                        }
                    }
                    Dispatcher.Invoke(delegate
                    {
                        for (int i = 0; i < viewModel.Files.Count; i++)
                        {
                            if (viewModel.Files[i].IsSelect && !viewModel.Files[i].TransferComplete)
                            {
                                viewModel.Files.Remove(viewModel.Files[i]);
                                i--;
                            }
                        }
                    });
                }
            });
        }
    }
}
