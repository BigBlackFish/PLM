using System.Windows;
using System.Windows.Controls;

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

        public static readonly DependencyProperty TotalPagesProperty = DependencyProperty.Register("TotalPages", typeof(int), typeof(JumpPageButton), new PropertyMetadata(0));
        public static readonly DependencyProperty SelectPageProperty = DependencyProperty.Register("SelectPage", typeof(int), typeof(JumpPageButton), new PropertyMetadata(1));

        public JumpPageButton()
        {
            InitializeComponent();
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
            SelectPage = int.Parse(ButtonOne.Content.ToString());
            if (int.Parse(ButtonOne.Content.ToString()) > 1)
            {
                Buttonnumberless();
            }
        }

        private void ButtonTwo_Click(object sender, RoutedEventArgs e)
        {
            SelectPage = int.Parse(ButtonTwo.Content.ToString());
        }

        private void ButtonThree_Click(object sender, RoutedEventArgs e)
        {
            SelectPage = int.Parse(ButtonThree.Content.ToString());
        }

        private void ButtonFour_Click(object sender, RoutedEventArgs e)
        {
            SelectPage = int.Parse(ButtonFour.Content.ToString());
        }

        private void ButtonFive_Click(object sender, RoutedEventArgs e)
        {
            SelectPage = int.Parse(ButtonFive.Content.ToString());
            if (int.Parse(ButtonFive.Content.ToString()) < TotalPages - 1)
            {
                Buttonnumberadd();
            }
        }

        private void ButtonLast_Click(object sender, RoutedEventArgs e)
        {
            SelectPage = TotalPages;
            ButtonOne.Content = TotalPages - 5;
            ButtonTwo.Content = TotalPages - 4;
            ButtonThree.Content = TotalPages - 3;
            ButtonFour.Content = TotalPages - 2;
            ButtonFive.Content = TotalPages-1;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            TotalPages = 30;
            ButtonOne.Visibility=TotalPages>int.Parse(ButtonOne.Content.ToString()) ?Visibility.Visible : Visibility.Collapsed;
            ButtonTwo.Visibility = TotalPages > int.Parse(ButtonTwo.Content.ToString()) ? Visibility.Visible : Visibility.Collapsed;
            ButtonThree.Visibility = TotalPages > int.Parse(ButtonThree.Content.ToString()) ? Visibility.Visible : Visibility.Collapsed;
            ButtonFour.Visibility = TotalPages > int.Parse(ButtonFour.Content.ToString()) ? Visibility.Visible : Visibility.Collapsed;
            ButtonFive.Visibility = TotalPages > int.Parse(ButtonFive.Content.ToString()) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void LastOneButton_Click(object sender, RoutedEventArgs e)
        {
            SelectPage = SelectPage - 1;
            if (SelectPage == int.Parse(ButtonOne.Content.ToString())-1&& SelectPage>1)
            {
                Buttonnumberless();
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {

            SelectPage = SelectPage + 1;
            if (SelectPage == int.Parse(ButtonFive.Content.ToString())+1&& SelectPage<TotalPages)
            {
                Buttonnumberadd();
            }
        }
    }
}
