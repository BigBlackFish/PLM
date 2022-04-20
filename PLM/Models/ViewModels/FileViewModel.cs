using FluentFTP;
using PLM.Common;
using System;
using System.Threading;

namespace PLM.Models.ViewModels
{
    public class FileViewModel : ModelBase
    {
        private readonly FtpClient ftpClient = new FtpClient(ClassHelper.ftpPath, ClassHelper.ftpUsername, ClassHelper.ftppassword);
        private CancellationTokenSource tokenSource;
        private CancellationToken token;
        private double progress;
        private string speed;
        private bool uploadCompleted;
        private bool stop = false;
        private bool cancel = false;
        private bool downloadCompleted;
        private bool update;

        /// <summary>
        /// 文件名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        public string FileType { get; set; }

        /// <summary>
        /// 版面信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// MD5值
        /// </summary>
        public string MD5 { get; set; }

        /// <summary>
        /// 保存路径
        /// </summary>
        public string SavePath { get; set; }

        /// <summary>
        /// 保存名称
        /// </summary>
        public string SaveName { get; set; }

        /// <summary>
        /// 文件大小（MB）
        /// </summary>
        public double Size { get; set; }

        /// <summary>
        /// 上传或下载进度（0～100）
        /// </summary>
        public double Progress
        {
            get => progress;
            set
            {
                progress = value;
                OnPropertyChanged(nameof(Progress));
            }
        }

        /// <summary>
        /// 传输速度
        /// </summary>
        public string Speed
        {
            get => speed;
            set
            {
                speed = value;
                OnPropertyChanged(nameof(Speed));
            }
        }

        /// <summary>
        /// 上传成功
        /// </summary>
        public bool UploadCompleted
        {
            get => uploadCompleted;
            set
            {
                if (uploadCompleted != value)
                {
                    uploadCompleted = value;
                    OnPropertyChanged(nameof(UploadCompleted));
                }
            }
        }

        /// <summary>
        /// 下载成功
        /// </summary>
        public bool DownloadCompleted
        {
            get => downloadCompleted;
            set
            {
                if (downloadCompleted != value)
                {
                    downloadCompleted = value;
                    OnPropertyChanged(nameof(DownloadCompleted));
                }
            }
        }

        public override void InitializeVariable()
        {
            Name = string.Empty;
            FileType = string.Empty;
            Message = string.Empty;
            Size = 0;
            progress = 0;
            ftpClient.Connect();
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        public async void FileUpload()
        {
            try
            {
                update = true;
                tokenSource = new CancellationTokenSource();
                token = tokenSource.Token;
                Progress<FtpProgress> progress = new Progress<FtpProgress>(p =>
                {
                    if (p.Progress == 100)
                    {
                        Progress = 100;
                        Speed = ClassHelper.FindResource<string>("UploadComplete");
                        UploadCompleted = true;
                    }
                    else
                    {
                        Progress = Math.Round(p.Progress, 2);
                        Speed = p.TransferSpeedToString();
                    }
                });
                #region 基本信息
                SavePath = $"{Message}/{Name}";
                SaveName = Name;
                #endregion
                await ftpClient.UploadFileAsync(Path, SavePath, FtpRemoteExists.Overwrite, true, FtpVerify.None, progress, token);
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        public async void FileDownload()
        {
            try
            {
                update = false;
                tokenSource = new CancellationTokenSource();
                token = tokenSource.Token;
                Progress<FtpProgress> progress = new Progress<FtpProgress>(p =>
                {
                    if (p.Progress == 100)
                    {
                        Progress = 100;
                        Speed = ClassHelper.FindResource<string>("DownloadComplete");
                        DownloadCompleted = true;
                    }
                    else
                    {
                        Progress = Math.Round(p.Progress, 2);
                        Speed = p.TransferSpeedToString();
                    }
                });
                #region 基本信息
                SavePath = System.IO.Path.Combine(ClassHelper.AttachmentsPath, Name);
                SaveName = Name;
                #endregion
                await ftpClient.DownloadFileAsync(SavePath, Path, FtpLocalExists.Overwrite, FtpVerify.None, progress, token);
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// 暂停
        /// </summary>
        public void SuspendTransmission()
        {
            if (!stop)
            {
                tokenSource.Cancel();
                stop = true;
            }
        }

        /// <summary>
        /// 继续_上传
        /// </summary>
        public async void Upload_ContinueTransmission()
        {
            if (stop || cancel)
            {
                if (stop)
                {
                    try
                    {
                        stop = false;
                        cancel = false;
                        update = true;
                        tokenSource = new CancellationTokenSource();
                        token = tokenSource.Token;
                        Progress<FtpProgress> progress = new Progress<FtpProgress>(p =>
                        {
                            if (p.Progress == 100)
                            {
                                Progress = 100;
                                Speed = ClassHelper.FindResource<string>("UploadComplete");
                                UploadCompleted = true;
                            }
                            else
                            {
                                Progress = Math.Round(p.Progress, 2);
                                Speed = p.TransferSpeedToString();
                            }
                        });
                        await ftpClient.UploadFileAsync(Path, SavePath, FtpRemoteExists.Resume, true, FtpVerify.None, progress, token);
                    }
                    catch (Exception)
                    {

                    }
                }
                else
                {
                    stop = false;
                    cancel = false;
                    FileUpload();
                }
            }
        }

        /// <summary>
        /// 继续_下载
        /// </summary>
        public async void Download_ContinueTransmission()
        {
            if (stop || cancel)
            {
                if (stop)
                {
                    try
                    {
                        stop = false;
                        cancel = false;
                        update = false;
                        tokenSource = new CancellationTokenSource();
                        token = tokenSource.Token;
                        Progress<FtpProgress> progress = new Progress<FtpProgress>(p =>
                        {
                            if (p.Progress == 100)
                            {
                                Progress = 100;
                                Speed = ClassHelper.FindResource<string>("DownloadComplete");
                                DownloadCompleted = true;
                            }
                            else
                            {
                                Progress = Math.Round(p.Progress, 2);
                                Speed = p.TransferSpeedToString();
                            }
                        });
                        await ftpClient.DownloadFileAsync(SavePath, Path, FtpLocalExists.Resume, FtpVerify.None, progress, token);
                    }
                    catch (Exception)
                    {

                    }
                }
                else
                {
                    stop = false;
                    cancel = false;
                    FileDownload();
                }
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        public async void CancelTransmission()
        {
            if (!cancel)
            {
                tokenSource.Cancel();
                if (update)
                {
                    if (await ftpClient.FileExistsAsync(SavePath))
                    {
                        await ftpClient.DeleteFileAsync(SavePath);
                    }
                }
                Progress = 0;
                Speed = "0 MB/S";
                UploadCompleted = false;
                DownloadCompleted = false;
                cancel = true;
            }
        }
    }
}
