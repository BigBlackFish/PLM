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
        public Fileadd()
        {
            InitializeComponent();
        }

        private void ImgAdd_PointerDown(object sender, EventArgs e)
        {
            //OpenFileDialog dialog = new OpenFileDialog();
            //dialog.Multiselect = false;
            //dialog.Filter = "tiff,psd(*.tiff,*.psd)|*.tiff;*.psd";
            //dialog.ShowDialog();
            //string foldPath = dialog.FileName;
            if (imgAdd.Tag.ToString() == "0")
            {
                imgAdd.Tag = "1";
            }
            else
            {
                imgAdd.Tag = "0";
            }
        }


        private void GriDrag_Drop(object sender, DragEventArgs e)
        {
            string fileName = ((Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            ClassHelper.MessageAlert(ClassHelper.MainWindow.GetType(), 0, fileName);
            if (imgAdd.Tag.ToString() == "0")
            {
                imgAdd.Tag = "1";
            }
            else
            {
                imgAdd.Tag = "0";
            }
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
    }
}
