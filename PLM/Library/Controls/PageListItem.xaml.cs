using PLM.Common;
using PLM.Models;
using PLM.Models.ViewModels;
using PLM.Service;
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

        private async void ImgDelete_PointerDown(object sender, System.EventArgs e)
        {
            if ((await AdminService.DeleteLayoutFileList(viewModel.Id.ToString()) is APIResult Result))
            {
                if (Result.Code == 0)
                {
                    ClassHelper.MessageAlert(ClassHelper.MainWindow.GetType(), 0, "删除成功");
                }
            }
        }
    }
}
