using PLM.Common;
using PLM.Models;
using PLM.Models.ViewModels;
using System.Threading;
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
            FileGroupViewModel fileGroup = new FileGroupViewModel();
            fileGroup.FileViews.Add(new FileViewModel()
            {
                Name = "测试一",
                Message = "版面信息",
                Size = 123123
            });
            fileGroup.FileViews.Add(new FileViewModel()
            {
                Name = "测试二",
                Message = "版面信息",
                Size = 123123
            });
            viewModel.Files.Add(fileGroup);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxButtonModel messageBoxButton = new MessageBoxButtonModel
            {
                Hint = "自定义确定按钮",
                Action = delegate
                {
                    Thread.Sleep(1000);
                    ClassHelper.MessageAlert(ClassHelper.MainWindow.GetType(), 0, "在线程中执行了一个事件");
                }
            };
            ClassHelper.AlertMessageBox(ClassHelper.MainWindow, ClassHelper.MessageBoxType.Select, "测试提示", "消息测试消息测试消息测试消息测试消息测测试消息测试消息测试消息", rightButton: messageBoxButton);
        }
    }
}
