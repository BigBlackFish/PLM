using Microsoft.Win32;
using PLM.Common;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PLM.Library.Controls
{
    /// <summary>
    /// Fileadd.xaml 的交互逻辑
    /// </summary>
    public partial class Fileadd : UserControl
    {
        public string FilePath
        {
            get => (string)GetValue(FilePathProperty);
            set => SetValue(FilePathProperty, value);
        }


        // Using a DependencyProperty as the backing store for FilePath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilePathProperty =
            DependencyProperty.Register("FilePath", typeof(string), typeof(Fileadd), new PropertyMetadata(TagChange));



        private static void TagChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ("".Equals(e.NewValue))
            {
                Fileadd fileadd = d as Fileadd;
                if (fileadd != null)
                {
                    fileadd.imgAdd.Tag = "0";
                    fileadd.Tip.Visibility = Visibility.Visible;
                    fileadd.FileName.Visibility = Visibility.Collapsed;
                }
            }
        }

        public Fileadd()
        {
            InitializeComponent();
        }

        private void ImgAdd_PointerDown(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Multiselect = false,
                Filter = "tiff,psd(*.tiff,*.psd)|*.tiff;*.psd"
            };
            dialog.ShowDialog();
            Suffixjudgment(dialog.FileName);
        }


        private void GriDrag_Drop(object sender, DragEventArgs e)
        {
            string fileName = ((Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            Suffixjudgment(fileName);
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

        //后缀名判断
        private void Suffixjudgment(string fileName)
        {
            string extension = System.IO.Path.GetExtension(fileName).ToLower();
            if (extension != ".tiff" && extension != ".psd" && extension != ".psb"&&!string.IsNullOrEmpty(extension))
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
            //imgAdd.Tag = extension == ".tiff" ? "1" : "2";
            if (extension == ".tiff")
            {
                imgAdd.Tag = "1";
            }
            else if (extension == ".psd")
            {
                imgAdd.Tag = "2";
            }
            else if (extension == ".psb")
            {
                imgAdd.Tag = "3";
            }
            base.Tag = "1";
            btndelete.Tag = "1";
            FileName.Text = System.IO.Path.GetFileNameWithoutExtension(FilePath);
            FileName.Visibility = Visibility.Visible;
            Tip.Visibility = Visibility.Collapsed;
        }

        private void Btndelete_PointerDown(object sender, EventArgs e)
        {
            btndelete.Tag = "0";
            imgAdd.Tag = "0";
            FilePath = string.Empty;
            FileName.Visibility = Visibility.Collapsed;
            Tip.Visibility = Visibility.Visible;
        }
    }
}
