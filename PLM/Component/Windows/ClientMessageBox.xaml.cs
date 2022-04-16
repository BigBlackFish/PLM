using PLM.Common;
using PLM.Models;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media.Animation;

namespace PLM.Component.Windows
{
    /// <summary>
    /// ClientMessageBox.xaml 的交互逻辑
    /// </summary>
    public partial class ClientMessageBox : Window
    {
        public ClassHelper.MessageBoxCloseType CloseType { get; set; }

        private readonly MessageBoxButtonModel _leftButton;
        private readonly MessageBoxButtonModel _rightButton;
        private bool closeState;

        public ClientMessageBox(ClassHelper.MessageBoxType messageBoxType,string hint, string message, MessageBoxButtonModel leftButton, MessageBoxButtonModel rightButton)
        {
            InitializeComponent();

            if (messageBoxType == ClassHelper.MessageBoxType.Inform)
            {
                btnLeft.Visibility = Visibility.Collapsed;
            }
            TxbHint.Text = hint;
            txbMessage.Text = message;
            _leftButton = leftButton;
            _rightButton = rightButton;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (!closeState)
            {
                e.Cancel = true;
                DoubleAnimation doubleAnimation = new DoubleAnimation()
                {
                    From = 1,
                    To = 0,
                    Duration = new TimeSpan(0, 0, 0, 0, 300)
                };
                doubleAnimation.Completed += delegate
                {
                    closeState = true;
                    Close();
                };
                BeginAnimation(OpacityProperty, doubleAnimation);
            }
        }

        private void ClientMessageBoxMain_Loaded(object sender, RoutedEventArgs e)
        {
            IntPtr windIntPtr = new WindowInteropHelper(this).Handle;
            HwndSource hwndSource = HwndSource.FromHwnd(windIntPtr);
            hwndSource.AddHook(WndProc);

            btnLeft.Content = _leftButton.Hint;
            btnRight.Content = _rightButton.Hint;
        }

        private async void BtnValid_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            CloseType = (ClassHelper.MessageBoxCloseType)Convert.ToInt32(button.Tag);
            grdMask.Visibility = Visibility.Visible;
            button.IsEnabled = false;
            switch (CloseType)
            {
                case ClassHelper.MessageBoxCloseType.Close:
                    break;
                case ClassHelper.MessageBoxCloseType.Left:
                    if (_leftButton.Action != null)
                    {
                        await Task.Run(_leftButton.Action);
                    }
                    break;
                case ClassHelper.MessageBoxCloseType.Right:
                    if (_rightButton.Action != null)
                    {
                        await Task.Run(_rightButton.Action);
                    }
                    break;
                default:
                    break;
            }
            Close();
        }

        private void TxbClose_PointerUp(object sender, EventArgs e)
        {
            CloseType = ClassHelper.MessageBoxCloseType.Close;
            Close();
        }

        #region 执行事件
        // 消息钩子
        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (((msg == ClassHelper.wmSystemMenu) && (wParam.ToInt32() == ClassHelper.wpSystemMenu)) || msg == 165)
            {
                handled = true;
            }
            return IntPtr.Zero;
        }
        #endregion
    }
}
