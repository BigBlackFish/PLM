using PLM.Common;
using System.Windows;
using System.Windows.Controls;

namespace PLM.Component.Pages
{
    /// <summary>
    /// UploadFilePage.xaml 的交互逻辑
    /// </summary>
    public partial class UploadFilePage : Page
    {
        public UploadFilePage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ClassHelper.MessageAlert(ClassHelper.MainWindow.GetType(), 0, "测试消息");
        }
    }
}
