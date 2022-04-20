using PLM.Models.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace PLM.Component.Pages
{
    /// <summary>
    /// DownloadingPage.xaml 的交互逻辑
    /// </summary>
    public partial class DownloadingPage : Page
    {
        private readonly DownloadingPageViewModel viewModel;

        public DownloadingPage()
        {
            InitializeComponent();
            viewModel = DataContext as DownloadingPageViewModel;
        }

        private void DownloadingMain_Loaded(object sender, RoutedEventArgs e)
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
            foreach (FileGroupViewModel item in viewModel.Files)
            {
                if (item.IsSelect)
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
            foreach (FileGroupViewModel item in viewModel.Files)
            {
                if (item.IsSelect)
                {
                    foreach (FileViewModel file in item.FileViews)
                    {
                        file.Download_ContinueTransmission();
                    }
                }
            }
        }

        private void BtnAllCancel_Click(object sender, RoutedEventArgs e)
        {
            foreach (FileGroupViewModel item in viewModel.Files)
            {
                if (item.IsSelect)
                {
                    foreach (FileViewModel file in item.FileViews)
                    {
                        file.CancelTransmission();
                    }
                }
            }
        }

        private void BtnAllDelete_Click(object sender, RoutedEventArgs e)
        {
            foreach (FileGroupViewModel item in viewModel.Files)
            {
                if (item.IsSelect)
                {
                    foreach (FileViewModel file in item.FileViews)
                    {
                        file.CancelTransmission();
                    }
                }
            }
            for (int i = 0; i < viewModel.Files.Count; i++)
            {
                if (viewModel.Files[i].IsSelect)
                {
                    viewModel.Files.Remove(viewModel.Files[i]);
                    i--;
                }
            }
        }
    }
}
