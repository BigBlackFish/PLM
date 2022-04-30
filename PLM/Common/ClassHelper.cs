using FluentFTP;
using Microsoft.Win32;
using PLM.Component.Pages;
using PLM.Component.Windows;
using PLM.Models;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace PLM.Common
{
    public delegate void MessageEvent(int messageType, string message);
    public delegate void RouteEvent(ClassHelper.PageType pageName);
    public delegate void AccordingMaskEvent(bool show, bool loading);

    public static class ClassHelper
    {
        #region 常量
        // 服务器地址
        public const string servicePath = "https://plm-sit.newpearl.com/api";
        public const uint wpSystemMenu = 0x02;
        public const uint wmSystemMenu = 0xa4;
        public const string ftpPath = "192.170.30.53";
        public const string ftpUsername = "anonymous";
        public const string ftppassword = "";
        public const string defaultPath = "data";
        // 附件路径
        public static readonly string AttachmentsPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Library");

        // 版面缩略图路径
        public static readonly string PageListPicturePath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Pictures");
        #endregion

        #region 变量
        // 程序主线程
        public static Dispatcher Dispatcher { get; set; }
        // 登录窗体
        public static LoginWindow LoginWindow { get; set; }
        // 主窗体
        public static MainWindow MainWindow { get; set; }
        // 令牌
        public static string Token { get; set; }
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
        // MessageBox模式
        public enum MessageBoxType
        {
            Inform,
            Select
        }
        // MessageBox关闭方式
        public enum MessageBoxCloseType
        {
            Close,
            Left,
            Right
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
        // 显示蒙版
        public static event AccordingMaskEvent AccordingMask;
        #endregion

        #region 动态库
        // 设置指定窗口的显示状态
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        #endregion

        static ClassHelper()
        {
            if (!Directory.Exists(AttachmentsPath))
            {
                Directory.CreateDirectory(AttachmentsPath);
            }
        }

        /// <summary>
        /// 保存密码
        /// </summary>
        /// <param name="password"></param>
        public static void SaveSettings(string password)
        {
            var Config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            Config.AppSettings.Settings["PassWord"].Value = password.Trim();
            Config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

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

        /// <summary>
        /// 消息盒子
        /// </summary>
        /// <param name="window">父窗体</param>
        /// <param name="messageBoxType">消息类型</param>
        /// <param name="hint">提示信息</param>
        /// <param name="message">消息</param>
        /// <param name="leftButton">左侧按钮(可空)</param>
        /// <param name="rightButton">右侧按钮(可空)</param>
        /// <returns></returns>
        public static MessageBoxCloseType AlertMessageBox(Window window, MessageBoxType messageBoxType, string hint, string message, MessageBoxButtonModel leftButton = null, MessageBoxButtonModel rightButton = null)
        {
            if (leftButton == null)
            {
                leftButton = new MessageBoxButtonModel()
                {
                    Hint = FindResource<string>("Cancel")
                };
            }
            else if (string.IsNullOrEmpty(leftButton.Hint))
            {
                leftButton.Hint = FindResource<string>("Cancel");
            }
            if (rightButton == null)
            {
                rightButton = new MessageBoxButtonModel()
                {
                    Hint = FindResource<string>("Confirm")
                };
            }
            else if (string.IsNullOrEmpty(rightButton.Hint))
            {
                rightButton.Hint = FindResource<string>("Confirm");
            }
            MessageBoxCloseType messageBoxCloseType = MessageBoxCloseType.Close;
            Dispatcher.Invoke(delegate
            {
                ClientMessageBox messageBox = new ClientMessageBox(messageBoxType, hint, message, leftButton, rightButton)
                {
                    Owner = window.IsActive ? window : null
                };
                messageBox.ShowDialog();
                messageBoxCloseType = messageBox.CloseType;
            });
            return messageBoxCloseType;
        }

        /// <summary>
        /// 获取文件Content Type
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <returns></returns>
        public static string GetMimeMapping(string fileName)
        {
            string mimeType = "application/octet-stream";
            string ext = Path.GetExtension(fileName).ToLower();
            RegistryKey regKey = Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
            {
                mimeType = regKey.GetValue("Content Type").ToString();
            }
            return mimeType;
        }

        /// <summary>
        /// 显示蒙版
        /// </summary>
        /// <param name="show">显示隐藏</param>
        public static void ShowMask(bool show, bool loading = true)
        {
            AccordingMask?.Invoke(show, loading);
        }

        /// <summary>
        /// 判断服务器是否存在该文件
        /// </summary>
        /// <param name="path">服务器文件路径</param>
        /// <returns></returns>
        public static async Task<bool> ExistServiceFile(string path)
        {
            FtpClient ftpClient = new FtpClient(ftpPath, ftpUsername, ftppassword);
            await ftpClient.ConnectAsync();
            bool state = await ftpClient.FileExistsAsync(path);
            await ftpClient.DisconnectAsync();
            return state;
        }

        public static async Task<string> DownloadImage(string path, string messgae, string name)
        {
            string outPath = Path.Combine(Path.GetTempPath(), messgae.Trim(), name);
            if (!File.Exists(outPath))
            {
                FtpClient ftpClient = new FtpClient(ftpPath, ftpUsername, ftppassword);
                await ftpClient.ConnectAsync();
                await ftpClient.DownloadFileAsync(outPath, path, FtpLocalExists.Overwrite);
            }
            return outPath;
        }
    }
}
