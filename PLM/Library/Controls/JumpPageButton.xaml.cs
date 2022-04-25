using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PLM.Library.Controls
{
    /// <summary>
    /// JumpPageButton.xaml 的交互逻辑
    /// </summary>
    public partial class JumpPageButton : UserControl
    {

        public int TotalPages
        {
            get => (int)GetValue(TotalPagesProperty);
            set => SetValue(TotalPagesProperty, value);
        }

        public int SelectPage
        {
            get => (int)GetValue(SelectPageProperty);
            set => SetValue(SelectPageProperty, value);
        }

        public static readonly DependencyProperty TotalPagesProperty = DependencyProperty.Register("TotalPages", typeof(int), typeof(JumpPageButton), new PropertyMetadata(0, BttonVisity));
        public static readonly DependencyProperty SelectPageProperty = DependencyProperty.Register("SelectPage", typeof(int), typeof(JumpPageButton), new PropertyMetadata(1));

        public JumpPageButton()
        {
            InitializeComponent();
        }

        private static void BttonVisity(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            JumpPageButton jumpPageButton=d as JumpPageButton;
            jumpPageButton.ButtonOne.Visibility = jumpPageButton.TotalPages > int.Parse(jumpPageButton.ButtonOne.Content.ToString()) ? Visibility.Visible : Visibility.Collapsed;
            jumpPageButton.ButtonTwo.Visibility = jumpPageButton.TotalPages > int.Parse(jumpPageButton.ButtonTwo.Content.ToString()) ? Visibility.Visible : Visibility.Collapsed;
            jumpPageButton.ButtonThree.Visibility = jumpPageButton.TotalPages > int.Parse(jumpPageButton.ButtonThree.Content.ToString()) ? Visibility.Visible : Visibility.Collapsed;
            jumpPageButton.ButtonFour.Visibility = jumpPageButton.TotalPages > int.Parse(jumpPageButton.ButtonFour.Content.ToString()) ? Visibility.Visible : Visibility.Collapsed;
            jumpPageButton.ButtonFive.Visibility = jumpPageButton.TotalPages > int.Parse(jumpPageButton.ButtonFive.Content.ToString()) ? Visibility.Visible : Visibility.Collapsed;

        }



        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Buttonnumberless()
        {
            ButtonOne.Content = (int.Parse(ButtonOne.Content.ToString()) - 1);
            ButtonTwo.Content = (int.Parse(ButtonTwo.Content.ToString()) - 1);
            ButtonThree.Content = (int.Parse(ButtonThree.Content.ToString()) - 1);
            ButtonFour.Content = (int.Parse(ButtonFour.Content.ToString()) - 1);
            ButtonFive.Content = (int.Parse(ButtonFive.Content.ToString()) - 1);
        }

        private void Buttonnumberadd()
        {
            ButtonOne.Content = (int.Parse(ButtonOne.Content.ToString()) + 1);
            ButtonTwo.Content = (int.Parse(ButtonTwo.Content.ToString()) + 1);
            ButtonThree.Content = (int.Parse(ButtonThree.Content.ToString()) + 1);
            ButtonFour.Content = (int.Parse(ButtonFour.Content.ToString()) + 1);
            ButtonFive.Content = (int.Parse(ButtonFive.Content.ToString()) + 1);
        }

        private void ButtonOne_Click(object sender, RoutedEventArgs e)
        {
            ButtonOne.Tag = "1";
            ButtonTwo.Tag = "0";
            ButtonThree.Tag= "0";
            ButtonFour.Tag = "0";
            ButtonFive.Tag= "0";
            ButtonLast.Tag = "0";
            SelectPage = int.Parse(ButtonOne.Content.ToString());
            if (int.Parse(ButtonOne.Content.ToString()) > 1)
            {
                Buttonnumberless();
            }
        }

        private void ButtonTwo_Click(object sender, RoutedEventArgs e)
        {
            ButtonOne.Tag = "0";
            ButtonTwo.Tag = "1";
            ButtonThree.Tag = "0";
            ButtonFour.Tag = "0";
            ButtonFive.Tag = "0";
            ButtonLast.Tag = "0";
            SelectPage = int.Parse(ButtonTwo.Content.ToString());
        }

        private void ButtonThree_Click(object sender, RoutedEventArgs e)
        {
            ButtonOne.Tag = "0";
            ButtonTwo.Tag = "0";
            ButtonThree.Tag = "1";
            ButtonFour.Tag = "0";
            ButtonFive.Tag = "0";
            ButtonLast.Tag = "0";
            SelectPage = int.Parse(ButtonThree.Content.ToString());
        }

        private void ButtonFour_Click(object sender, RoutedEventArgs e)
        {
            ButtonOne.Tag = "0";
            ButtonTwo.Tag = "0";
            ButtonThree.Tag = "0";
            ButtonFour.Tag = "1";
            ButtonFive.Tag = "0";
            ButtonLast.Tag = "0";
            SelectPage = int.Parse(ButtonFour.Content.ToString());
        }

        private void ButtonFive_Click(object sender, RoutedEventArgs e)
        {
            ButtonOne.Tag = "0";
            ButtonTwo.Tag = "0";
            ButtonThree.Tag = "0";
            ButtonFour.Tag = "0";
            ButtonFive.Tag = "1";
            ButtonLast.Tag = "0";
            SelectPage = int.Parse(ButtonFive.Content.ToString());
            if (int.Parse(ButtonFive.Content.ToString()) < TotalPages - 1)
            {
                Buttonnumberadd();
            }
        }

        private void ButtonLast_Click(object sender, RoutedEventArgs e)
        {
            ButtonOne.Tag = "0";
            ButtonTwo.Tag = "0";
            ButtonThree.Tag = "0";
            ButtonFour.Tag = "0";
            ButtonFive.Tag = "0";
            ButtonLast.Tag = "1";
            SelectPage = TotalPages;
            ButtonOne.Content = TotalPages - 5;
            ButtonTwo.Content = TotalPages - 4;
            ButtonThree.Content = TotalPages - 3;
            ButtonFour.Content = TotalPages - 2;
            ButtonFive.Content = TotalPages - 1;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ButtonOne.Visibility = TotalPages > int.Parse(ButtonOne.Content.ToString()) ? Visibility.Visible : Visibility.Collapsed;
            ButtonTwo.Visibility = TotalPages > int.Parse(ButtonTwo.Content.ToString()) ? Visibility.Visible : Visibility.Collapsed;
            ButtonThree.Visibility = TotalPages > int.Parse(ButtonThree.Content.ToString()) ? Visibility.Visible : Visibility.Collapsed;
            ButtonFour.Visibility = TotalPages > int.Parse(ButtonFour.Content.ToString()) ? Visibility.Visible : Visibility.Collapsed;
            ButtonFive.Visibility = TotalPages > int.Parse(ButtonFive.Content.ToString()) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void LastOneButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectPage - 1 >0)
                SelectPage = SelectPage - 1;
            if (SelectPage == int.Parse(ButtonOne.Content.ToString()) - 1 && SelectPage > 0)
            {
                Buttonnumberless();
            }
            SetBackground(SelectPage);
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if(SelectPage+1<=TotalPages)
            SelectPage = SelectPage + 1;
            if (SelectPage == int.Parse(ButtonFive.Content.ToString()) + 1 && SelectPage < TotalPages)
            {
                Buttonnumberadd();
            }
            SetBackground(SelectPage);
        }

        //根据上一页下一页给按钮上色
        private void SetBackground(int Sp)
        {
            if (int.Parse(ButtonOne.Content.ToString()) == SelectPage)
            {
                ButtonOne.Tag = "1";
                ButtonTwo.Tag = "0";
                ButtonThree.Tag = "0";
                ButtonFour.Tag = "0";
                ButtonFive.Tag = "0";
                ButtonLast.Tag = "0";
            }
            else if (int.Parse(ButtonTwo.Content.ToString()) == SelectPage)
            {
                ButtonOne.Tag = "0";
                ButtonTwo.Tag = "1";
                ButtonThree.Tag = "0";
                ButtonFour.Tag = "0";
                ButtonFive.Tag = "0";
                ButtonLast.Tag = "0";
            }
            else if (int.Parse(ButtonThree.Content.ToString()) == SelectPage)
            {
                ButtonOne.Tag = "0";
                ButtonTwo.Tag = "0";
                ButtonThree.Tag = "1";
                ButtonFour.Tag = "0";
                ButtonFive.Tag = "0";
                ButtonLast.Tag = "0";
            }
            else if (int.Parse(ButtonFour.Content.ToString()) == SelectPage)
            {
                ButtonOne.Tag = "0";
                ButtonTwo.Tag = "0";
                ButtonThree.Tag = "0";
                ButtonFour.Tag = "1";
                ButtonFive.Tag = "0";
                ButtonLast.Tag = "0";
            }
            else if (int.Parse(ButtonFive.Content.ToString()) == SelectPage)
            {
                ButtonOne.Tag = "0";
                ButtonTwo.Tag = "0";
                ButtonThree.Tag = "0";
                ButtonFour.Tag = "0";
                ButtonFive.Tag = "1";
                ButtonLast.Tag = "0";
            }
            else if (int.Parse(ButtonLast.Content.ToString()) == SelectPage)
            {
                ButtonOne.Tag = "0";
                ButtonTwo.Tag = "0";
                ButtonThree.Tag = "0";
                ButtonFour.Tag = "0";
                ButtonFive.Tag = "0";
                ButtonLast.Tag = "1";
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(JumppageTextBox.Text.Trim().ToString()))
            {
                return;
            }

            if (int.Parse(JumppageTextBox.Text) <= TotalPages && int.Parse(JumppageTextBox.Text) > 0)
            {
                SelectPage = int.Parse(JumppageTextBox.Text);
                if (SelectPage > 5&& SelectPage < TotalPages&& TotalPages>6)
                {
                    ButtonFive.Content = SelectPage;
                    ButtonFour.Content = SelectPage-1;
                    ButtonThree.Content = SelectPage - 2;
                    ButtonTwo.Content= SelectPage - 3;
                    ButtonOne.Content= SelectPage - 4;
                }

                if (SelectPage > 5 && SelectPage == TotalPages && TotalPages > 6)
                {
                    ButtonOne.Content = TotalPages - 5;
                    ButtonTwo.Content = TotalPages - 4;
                    ButtonThree.Content = TotalPages - 3;
                    ButtonFour.Content = TotalPages - 2;
                    ButtonFive.Content = TotalPages - 1;
                }

                if (SelectPage < 6 && TotalPages > 6)
                {
                    ButtonOne.Content = 1;
                    ButtonTwo.Content = 2;
                    ButtonThree.Content = 3;
                    ButtonFour.Content = 4;
                    ButtonFive.Content = 5;
                }
                SetBackground(SelectPage);
            }
        }
    }
}
