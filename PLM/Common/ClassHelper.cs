using PLM.Component.Pages;
using PLM.Component.Windows;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace PLM.Common
{
    public delegate void MessageEvent(int messageType, string message);
    public delegate void RouteEvent(ClassHelper.PageType pageName);

    public static class ClassHelper
    {
        #region 常量
        // 服务器地址
        public const string servicePath = "";
        #endregion

        #region 变量
        // 程序主线程
        public static Dispatcher Dispatcher { get; set; }
        // 登录窗体
        public static LoginWindow LoginWindow { get; set; }
        // 主窗体
        public static MainWindow MainWindow { get; set; }
        #endregion

        #region 枚举
        // Page类型
        public enum PageType
        {
            // 上传文件
            UploadFile,
            // 正在上传
            Uploading,
            // 正在下载
            Downloading,
            // 版面列表
            PageLists
        }
        #endregion

        #region 页面
        // 上传文件
        public static readonly UploadFilePage uploadFilePage = new UploadFilePage();
        // 正在上传
        public static readonly UploadingPage uploadingPage = new UploadingPage();
        // 正在下载
        public static readonly DownloadingPage downloadingPage = new DownloadingPage();
        // 版面列表
        public static readonly PageListsPage pageListsPage = new PageListsPage();
        #endregion

        #region 事件
        // 消息提醒
        public static event MessageEvent MessageHint;
        // 改变路由
        public static event RouteEvent RoutedChanged;
        #endregion

        /// <summary>
        /// 记录异常信息
        /// </summary>
        /// <param name="type">异常类</param>
        /// <param name="exception">异常信息</param>
        public static void RecordException(Type type, Exception exception)
        {
            string method = string.Empty;
            string location = string.Empty;
            if (exception.TargetSite != null)
            {
                method = exception.TargetSite.Name;
            }
            if (exception.StackTrace != null)
            {
                location = exception.StackTrace.ToString();
            }
            MessageAlert(null, 3, exception.Message);
            LogHelper.WriteErrorLog(type, method, location, exception.Message);
        }

        /// <summary>
        /// 窗体消息通知
        /// </summary>
        /// <param name="window">显示窗体</param>
        /// <param name="messageType">0 成功 1 警告 2 默认 3 错误</param>
        /// <param name="message">提示信息</param>
        public static void MessageAlert(Type window, int messageType, string message)
        {
            if (window != null)
            {
                foreach (Delegate item in (MessageHint?.GetInvocationList()).Where(item => item.Target.GetType() == window))
                {
                    item.DynamicInvoke(messageType, message);
                }
            }
            else
            {
                MessageHint?.Invoke(messageType, message);
            }
        }

        /// <summary>
        /// 查找资源
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        public static T FindResource<T>(string key)
        {
            return (T)Application.Current?.Resources[key];
        }

        /// <summary>
        /// 切换路由
        /// </summary>
        /// <param name="routeName">页面名称</param>
        public static void SwitchRoute(PageType pageName)
        {
            RoutedChanged?.Invoke(pageName);
        }
    }
}
