using PLM.Common;
using PLM.Library.Controls;
using PLM.Models;
using PLM.Models.ViewModels;
using PLM.Service;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace PLM.Component.Windows
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainWimdowViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();

            viewModel = DataContext as MainWimdowViewModel;
            ClassHelper.MainWindow = this;
            ClassHelper.MessageHint += ClassHelper_MessageHint;
            ClassHelper.RoutedChanged += ClassHelper_RoutedChanged;
            ClassHelper.AccordingMask += ClassHelper_AccordingMask;
        }

        private void AppMain_Loaded(object sender, RoutedEventArgs e)
        {
            ThreadPool.QueueUserWorkItem(Load);
        }

        private void AppMain_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        private void AppMain_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                Thickness thickness = SystemParameters.WindowResizeBorderThickness;
                grdMain.Margin = new Thickness(thickness.Left + 4, thickness.Top + 4, thickness.Right + 4, thickness.Bottom + 4);
            }
            else
            {
                grdMain.Margin = new Thickness(0);
            }
        }

        private void FemRouteMain_Navigated(object sender, NavigationEventArgs e)
        {
            if (femRouteMain.CanGoBack)
            {
                _ = femRouteMain.RemoveBackEntry();
            }
            foreach (Border item in stpSelect.Children)
            {
                if ((item.Child as FrameworkElement).Tag.ToString() == e.Content.GetType().Name)
                {
                    item.Tag = "1";
                }
                else
                {
                    item.Tag = "0";
                }
            }
        }

        private void ClassHelper_MessageHint(int messageType, string message)
        {
            Dispatcher.Invoke(delegate
            {
                ToastMessage toastMessage = new ToastMessage(message, messageType);
                toastMessage.stdLoaded.Completed += (object sender, EventArgs e) =>
                {
                    stpHint.Children.Remove(toastMessage);
                };
                stpHint.Children.Add(toastMessage);
            });
        }

        // 切换路由回调
        private void ClassHelper_RoutedChanged(ClassHelper.PageType pageName)
        {
            Dispatcher.Invoke(delegate
            {
                if (femRouteMain.Content?.GetType().Name == pageName.ToString())
                {
                    return;
                }
                switch (pageName)
                {
                    case ClassHelper.PageType.UploadFile:
                        femRouteMain.Navigate(ClassHelper.uploadFilePage);
                        break;
                    case ClassHelper.PageType.Uploading:
                        femRouteMain.Navigate(ClassHelper.uploadingPage);
                        break;
                    case ClassHelper.PageType.Downloading:
                        femRouteMain.Navigate(ClassHelper.downloadingPage);
                        break;
                    case ClassHelper.PageType.PageLists:
                        femRouteMain.Navigate(ClassHelper.pageListsPage);
                        break;
                    default:
                        ClassHelper.MessageAlert(GetType(), 3, ClassHelper.FindResource<string>("PageDoesNotExist"));
                        break;
                }
            });
        }

        // 切换页面
        private void BrdSelectPage_PointerUp(object sender, EventArgs e)
        {
            Border border = (Border)sender;
            string data = (border.Child as FrameworkElement).Tag.ToString();
            data = data.Remove(data.Length - 4, 4);
            ClassHelper.SwitchRoute((ClassHelper.PageType)Enum.Parse(typeof(ClassHelper.PageType), data));
        }

        private void ClassHelper_AccordingMask(bool show, bool loading)
        {
            Dispatcher.Invoke(delegate
            {
                brdMask.Visibility = show ? Visibility.Visible : Visibility.Collapsed;
                conLoading.Visibility = loading ? Visibility.Visible : Visibility.Collapsed;
            });
        }

        #region 执行事件
        private void Load(object data)
        {
            ClassHelper.ShowMask(true);
            GetUserInfo();
            ClassHelper.SwitchRoute(ClassHelper.PageType.UploadFile);
            ClassHelper.ShowMask(false);
        }

        private async void GetUserInfo()
        {
            if ((await AdminService.GetUserinfomation() is APIResult<UserInfomation> userinfo))
            {
                viewModel.UserName = userinfo.Data.NickName;
                viewModel.Identity = userinfo.Data.Tag;
                viewModel.Phone = userinfo.Data.Mobile;
                viewModel.LoginTime = DateTime.Now.ToString() + "登录";
                viewModel.ImgUrl = "/Library/Image/boy.png";
            }
        }
        #endregion

    }
}
