using System;
using System.Windows;
using System.Windows.Controls;

namespace PLM.Library.Controls
{
    /// <summary>
    /// PasswordInputBox.xaml 的交互逻辑
    /// </summary>
    public partial class PasswordInputBox : UserControl
    {
        public string Password
        {
            get => (string)GetValue(PasswordProperty);
            set => SetValue(PasswordProperty, value);
        }

        // Using a DependencyProperty as the backing store for Password.  This enables animation, styling, binding, etc...
        public static DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(PasswordInputBox), new PropertyMetadata(string.Empty));

        private bool _isPasswordShow = false;

        public PasswordInputBox()
        {
            InitializeComponent();
        }

        private void BrdDisplay_PointerUp(object sender, EventArgs e)
        {
            if (_isPasswordShow)
            {
                txtPassword.Visibility = Visibility.Collapsed;
                pstPassword.Visibility = Visibility.Visible;
                pstPassword.Password = txtPassword.Text;
                pstPassword.GetType().GetMethod("Select", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).Invoke(pstPassword, new object[] { pstPassword.Password.Length, 1 });
                pstPassword.Focus();
                imgEye.Tag = "0";
            }
            else
            {
                txtPassword.Visibility = Visibility.Visible;
                pstPassword.Visibility = Visibility.Collapsed;
                txtPassword.Text = pstPassword.Password;
                txtPassword.SelectionStart = txtPassword.Text.Length;
                txtPassword.Focus();
                imgEye.Tag = "1";
            }
            _isPasswordShow = !_isPasswordShow;
        }

        private void PstPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            Password = pstPassword.Password;
        }

        private void TxtPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            Password = txtPassword.Text;
        }
    }
}
