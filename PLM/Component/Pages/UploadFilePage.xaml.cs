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

        private void ClearData()
        { 
            viewModel.FileLeft=string.Empty;
            viewModel.Remark=string.Empty;
            viewModel.FileRight = string.Empty;
            viewModel.Message=string.Empty;
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
                        bool leftChanged = false;
                        bool rightChanged = false;
                        bool changed = false;
                        bool cancel = false;
                        MessageBoxButtonModel leftButton = new MessageBoxButtonModel
                        {
                            Hint = ClassHelper.FindResource<string>("Cancel"),
                            Action = delegate
                            {
                                Dispatcher.Invoke(delegate
                                {
                                    cancel = true;
                                });
                            }
                        };
                        if (item.Message == viewModel.Message)
                        {
                            if (!string.IsNullOrEmpty(viewModel.FileLeft))
                            {
                                FileInfo fileLeft = new FileInfo(viewModel.FileLeft);
                                if (item.SourceFile == null)
                                {
                                    changed = true;
                                    FileViewModel left = new FileViewModel
                                    {
                                        Name = fileLeft.Name,
                                        Path = viewModel.FileLeft,
                                        FileType = fileLeft.Extension.ToLower(),
                                        Message = viewModel.Message,
                                        Remark = viewModel.Remark,
                                        Size = Math.Round((double)fileLeft.Length / 1024 / 1024, 3),
                                        DataType = true
                                    };
                                    item.FileViews.Add(left);
                                    item.SourceFile = left;
                                }
                                else if (item.SourceFile.Path != viewModel.FileLeft)
                                {
                                    double size = Math.Round((double)fileLeft.Length / 1024 / 1024, 3);
                                    if (item.SourceFile.Size != size)
                                    {
                                        string hint = ClassHelper.FindResource<string>("ReplaceHint");
                                        string message = $"{ClassHelper.FindResource<string>("ReplaceMessage1")}{'“' + item.SourceFile.Name + '”'}{ClassHelper.FindResource<string>("ReplaceMessage2")}";
                                        MessageBoxButtonModel messageBox = new MessageBoxButtonModel()
                                        {
                                            Hint = ClassHelper.FindResource<string>("AgreeReplace"),
                                            Action = delegate
                                            {
                                                changed = true;
                                                Dispatcher.Invoke(delegate
                                                {
                                                    item.FileViews.Remove(item.SourceFile);
                                                    FileViewModel fileView = new FileViewModel
                                                    {
                                                        Name = fileLeft.Name,
                                                        Path = viewModel.FileLeft,
                                                        FileType = fileLeft.Extension.ToLower(),
                                                        Message = viewModel.Message,
                                                        Remark = viewModel.Remark,
                                                        Size = size,
                                                    };
                                                    item.FileViews.Add(fileView);
                                                    item.SourceFile = fileView;
                                                });
                                            }
                                        };
                                        ClassHelper.AlertMessageBox(ClassHelper.MainWindow, ClassHelper.MessageBoxType.Select, hint, message, leftButton: leftButton, rightButton: messageBox);
                                    }
                                }
                                else
                                {
                                    leftChanged = true;
                                }
                            }
                            else
                            {
                                if (item.SourceFile != null)
                                {
                                    changed = true;
                                    item.FileViews.Remove(item.SourceFile);
                                    item.SourceFile = null;
                                }
                                else
                                {
                                    leftChanged = true;
                                }
                            }
                            if (!string.IsNullOrEmpty(viewModel.FileRight))
                            {
                                FileInfo fileRight = new FileInfo(viewModel.FileRight);
                                if (item.SummaryFile == null)
                                {
                                    changed = true;
                                    FileViewModel right = new FileViewModel
                                    {
                                        Name = fileRight.Name,
                                        Path = viewModel.FileRight,
                                        FileType = fileRight.Extension.ToLower(),
                                        Message = viewModel.Message,
                                        Remark = viewModel.Remark,
                                        Size = Math.Round((double)fileRight.Length / 1024 / 1024, 3),
                                        DataType = false
                                    };
                                    item.FileViews.Add(right);
                                    item.SummaryFile = right;
                                }
                                else if (item.SummaryFile.Path != viewModel.FileRight)
                                {
                                    double size = Math.Round((double)fileRight.Length / 1024 / 1024, 3);
                                    if (item.SummaryFile.Size != size)
                                    {
                                        string hint = ClassHelper.FindResource<string>("ReplaceHint");
                                        string message = $"{ClassHelper.FindResource<string>("ReplaceMessage1")}{'“' + item.SummaryFile.Name + '”'}{ClassHelper.FindResource<string>("ReplaceMessage2")}";
                                        MessageBoxButtonModel messageBox = new MessageBoxButtonModel()
                                        {
                                            Hint = ClassHelper.FindResource<string>("AgreeReplace"),
                                            Action = delegate
                                            {
                                                Dispatcher.Invoke(delegate
                                                {
                                                    changed = true;
                                                    item.FileViews.Remove(item.SummaryFile);
                                                    FileViewModel fileView = new FileViewModel
                                                    {
                                                        Name = fileRight.Name,
                                                        Path = viewModel.FileRight,
                                                        FileType = fileRight.Extension.ToLower(),
                                                        Message = viewModel.Message,
                                                        Remark = viewModel.Remark,
                                                        Size = Math.Round((double)fileRight.Length / 1024 / 1024, 3),
                                                    };
                                                    item.FileViews.Add(fileView);
                                                    item.SummaryFile = fileView;
                                                });
                                            }
                                        };
                                        ClassHelper.AlertMessageBox(ClassHelper.MainWindow, ClassHelper.MessageBoxType.Select, hint, message, leftButton: leftButton, rightButton: messageBox);
                                    }
                                }
                                else
                                {
                                    rightChanged = true;
                                }
                            }
                            else
                            {
                                if (item.SummaryFile != null)
                                {
                                    changed = true;
                                    item.FileViews.Remove(item.SummaryFile);
                                    item.SummaryFile = null;
                                }
                                else
                                {
                                    rightChanged = true;
                                }
                            }
                        }
                        if (cancel)
                        {
                            return;
                        }
                        if (changed || (leftChanged && rightChanged))
                        {
                            return;
                        }
                    }
                    FileGroupViewModel fileGroupView = new FileGroupViewModel()
                    {
                        Message = viewModel.Message,
                        Remark = viewModel.Remark,
                    };
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
                            DataType = true
                        };
                        fileGroupView.FileViews.Add(left);
                        fileGroupView.SourceFile = left;
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
                            DataType = false
                        };
                        fileGroupView.FileViews.Add(right);
                        fileGroupView.SummaryFile = right;
                    }
                    uploading.Files.Add(fileGroupView);
                    ClassHelper.MessageAlert(ClassHelper.MainWindow.GetType(), 0, "开始上传");
                    ClearData();
                }
            }
        }
    }
}
