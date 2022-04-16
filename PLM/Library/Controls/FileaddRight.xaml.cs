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
            if (extension != ".png" && extension != ".jpg")
            {
                ClassHelper.MessageAlert(ClassHelper.MainWindow.GetType(), 0, "不支持上传该格式文件");
                imgAdd.Tag = "0";
                return;
            }
            imgAdd.Tag = extension == ".png" ? "1" : "2";
            btndelete.Tag = "1";
        }

        private void Btndelete_PointerDown(object sender, EventArgs e)
        {
            btndelete.Tag = "0";
            imgAdd.Tag = "0";
        }
    }
}
