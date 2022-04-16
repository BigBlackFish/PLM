using PLM.Models.ViewModels;
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
    }
}
