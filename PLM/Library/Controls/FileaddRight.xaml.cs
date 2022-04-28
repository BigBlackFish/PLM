using Microsoft.Win32;
using PLM.Common;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PLM.Library.Controls
{
    /// <summary>
    /// FileaddRight.xaml 的交互逻辑
    /// </summary>
    public partial class FileaddRight : UserControl
    {
        public string FilePath
        {
            get => (string)GetValue(FilePathProperty);
            set => SetValue(FilePathProperty, value);
        }

        // Using a DependencyProperty as the backing store for FilePath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilePathProperty =
            DependencyProperty.Register("FilePath", typeof(string), typeof(FileaddRight), new PropertyMetadata(TagChange));


        private static void TagChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ("".Equals(e.NewValue))
            {
                FileaddRight fileadd = d as FileaddRight;
                if (fileadd != null)
                {
                    fileadd.imgAdd.Tag = "0";
                    fileadd.Tip.Visibility = Visibility.Visible;
                    fileadd.FileName.Visibility = Visibility.Collapsed;
                }
            }
        }


        public FileaddRight()
        {
            InitializeComponent();
        }

        private void ImgAdd_PointerDown(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Multiselect = false,
                Filter = "jpg,png(*.jpg,*.png)|*.jpg;*.png"
            };
            dialog.ShowDialog();
            Suffixjudgment(dialog.FileName);
        }

        private void GriDrag_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Link;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void GriDrag_Drop(object sender, DragEventArgs e)
        {
            string fileName = ((Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            Suffixjudgment(fileName);
        }

        private void Suffixjudgment(string fileName)
        {
            string extension = System.IO.Path.GetExtension(fileName).ToLower();
            if (extension != ".png" && extension != ".jpg" && !string.IsNullOrEmpty(extension))
            {
                ClassHelper.MessageAlert(ClassHelper.MainWindow.GetType(), 1, "不支持上传该格式文件");
                imgAdd.Tag = "0";
                FilePath = string.Empty;
                FileName.Visibility = Visibility.Collapsed;
                Tip.Visibility = Visibility.Visible;
                return;
            }
            else if (string.IsNullOrEmpty(extension))
            {
                ClassHelper.MessageAlert(ClassHelper.MainWindow.GetType(), 1, "未选择文件");
            }
            FilePath = fileName;
            imgAdd.Tag = extension == ".png" ? "1" : "2";
            btndelete.Tag = "1";
            FileName.Text = System.IO.Path.GetFileNameWithoutExtension(FilePath);
            FileName.Visibility = Visibility.Visible;
            Tip.Visibility = Visibility.Collapsed;
        }

        private void Btndelete_PointerDown(object sender, RoutedEventArgs e)
        {
            btndelete.Tag = "0";
            imgAdd.Tag = "0";
            FilePath = string.Empty;
            FileName.Visibility = Visibility.Collapsed;
            Tip.Visibility = Visibility.Visible;
        }
    }
}
