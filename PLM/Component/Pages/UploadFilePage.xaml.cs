using PLM.Common;
using PLM.Models;
using PLM.Models.ViewModels;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace PLM.Component.Pages
{
    /// <summary>
    /// UploadFilePage.xaml 的交互逻辑
    /// </summary>
    public partial class UploadFilePage : Page
    {
        private readonly UploadFilePageViewModel viewModel;

        public UploadFilePage()
        {
            InitializeComponent();

            viewModel = DataContext as UploadFilePageViewModel;
        }

        private void BtnUploading_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(viewModel.FileLeft) && string.IsNullOrEmpty(viewModel.FileRight) || string.IsNullOrEmpty(viewModel.Message))
            {
                ClassHelper.MessageAlert(ClassHelper.MainWindow.GetType(), 1, ClassHelper.FindResource<string>("LackOfNecessaryInformation"));
            }
            else
            {
                if (ClassHelper.uploadingPage.DataContext is UploadingPageViewModel uploading)
                {
                    foreach (FileGroupViewModel item in uploading.Files)
                    {
                        foreach (FileViewModel file in item.FileViews)
                        {
                            if (file.Message == viewModel.Message && (file.Path == viewModel.FileLeft || file.Path == viewModel.FileRight))
                            {
                                string hint = ClassHelper.FindResource<string>("ReplaceHint");
                                string message = $"{ClassHelper.FindResource<string>("ReplaceMessage1")}{'“' + file.Name + '”'}{ClassHelper.FindResource<string>("ReplaceMessage2")}";
                                MessageBoxButtonModel messageBox = new MessageBoxButtonModel()
                                {
                                    Hint = ClassHelper.FindResource<string>("AgreeReplace"),
                                    Action = delegate
                                    {
                                        Dispatcher.Invoke(delegate
                                        {
                                            item.FileViews.Remove(file);
                                            string path = file.Path == viewModel.FileLeft ? viewModel.FileLeft : viewModel.FileRight;
                                            FileInfo fileInfo = new FileInfo(viewModel.FileLeft);
                                            FileViewModel fileView = new FileViewModel
                                            {
                                                Name = fileInfo.Name,
                                                Path = path,
                                                FileType = fileInfo.Extension.ToLower(),
                                                Message = viewModel.Message,
                                                Remark = viewModel.Remark,
                                                Size = Math.Round((double)fileInfo.Length / 1024 / 1024, 3),
                                            };
                                            item.FileViews.Add(fileView);
                                        });
                                    }
                                };
                                ClassHelper.AlertMessageBox(ClassHelper.MainWindow, ClassHelper.MessageBoxType.Select, hint, message, rightButton: messageBox);
                                return;
                            }
                        }
                    }
                    FileGroupViewModel fileGroupView = new FileGroupViewModel();
                    if (!string.IsNullOrEmpty(viewModel.FileLeft))
                    {
                        FileInfo fileLeft = new FileInfo(viewModel.FileLeft);
                        FileViewModel left = new FileViewModel
                        {
                            Name = fileLeft.Name,
                            Path = viewModel.FileLeft,
                            FileType = fileLeft.Extension.ToLower(),
                            Message = viewModel.Message,
                            Remark = viewModel.Remark,
                            Size = Math.Round((double)fileLeft.Length / 1024 / 1024, 3),
                        };
                        fileGroupView.FileViews.Add(left);
                    }
                    if (!string.IsNullOrEmpty(viewModel.FileRight))
                    {
                        FileInfo fileRight = new FileInfo(viewModel.FileRight);
                        FileViewModel right = new FileViewModel
                        {
                            Name = fileRight.Name,
                            Path = viewModel.FileRight,
                            FileType = fileRight.Extension.ToLower(),
                            Message = viewModel.Message,
                            Remark = viewModel.Remark,
                            Size = Math.Round((double)fileRight.Length / 1024 / 1024, 3),
                        };
                        fileGroupView.FileViews.Add(right);
                    }
                    uploading.Files.Add(fileGroupView);
                    ClassHelper.MessageAlert(ClassHelper.MainWindow.GetType(), 0, "开始上传");
                }
            }
        }
    }
}
