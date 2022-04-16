using PLM.Models.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace PLM.Library.Controls
{
    /// <summary>
    /// PageListItem.xaml 的交互逻辑
    /// </summary>
    public partial class PageListItem : UserControl
    {
        private PageFileListViewModel viewModel;
        public PageListItem()
        {
            InitializeComponent();
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is PageFileListViewModel model)
            {
                viewModel = model;
            }
        }
    }
}
