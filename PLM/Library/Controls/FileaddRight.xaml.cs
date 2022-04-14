using Microsoft.Win32;
using PLM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        private void imgAdd_PointerDown(object sender, EventArgs e)
        {
            //OpenFileDialog dialog = new OpenFileDialog();
            //dialog.Multiselect = false;
            //dialog.Filter = "jpg,png(*.jpg,*.png)|*.jpg;*.png";
            //dialog.ShowDialog();
            //string foldPath = dialog.FileName;
            if (imgAdd.Tag.ToString() == "0")
            {
                imgAdd.Tag = "2";
            }
            else
            {
                imgAdd.Tag = "0";
            }
        }

        private void griDrag_DragEnter(object sender, DragEventArgs e)
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

        private void griDrag_Drop(object sender, DragEventArgs e)
        {
            string fileName = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            ClassHelper.MessageAlert(ClassHelper.MainWindow.GetType(), 0, fileName);
            if (imgAdd.Tag.ToString() == "0")
            {
                imgAdd.Tag = "2";
            }
            else
            {
                imgAdd.Tag = "0";
            }
        }
    }
}
