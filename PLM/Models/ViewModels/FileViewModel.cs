using FluentFTP;
using PLM.Common;
using System;
using System.IO;
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
        private bool downloadCompleted;
        private bool uploadFail;
        private bool downloadFail;
        // 重试次数
        private int retry = 0;

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
                uploadCompleted = value;
                OnPropertyChanged(nameof(UploadCompleted));
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
                downloadCompleted = value;
                OnPropertyChanged(nameof(DownloadCompleted));
            }
        }

        /// <summary>
        /// 上传失败
        /// </summary>
        public bool UploadFail
        {
            get => uploadFail;
            set
            {
                uploadFail = value;
                OnPropertyChanged(nameof(UploadFail));
            }
        }

        /// <summary>
        /// 下载失败
        /// </summary>
        public bool DownloadFail
        {
            get => downloadFail;
            set
            {
                downloadFail = value;
                OnPropertyChanged(nameof(DownloadFail));
            }
        }

        /// <summary>
        /// 数据类型（true 原图 false 缩略）
        /// </summary>
        public bool DataType { get; set; }

        public string OutPath { get; set; }

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
            if (File.Exists(Path))
            {
                UploadFail = false;
                tokenSource = new CancellationTokenSource();
                token = tokenSource.Token;
                Progress<FtpProgress> progress = new Progress<FtpProgress>(p =>
                {
                    Progress = Math.Round(p.Progress, 2);
                    Speed = p.TransferSpeedToString();
                });
                #region 基本信息
                SavePath = $"{ClassHelper.defaultPath}/{Message.Trim()}{(retry == 0 ? string.Empty : retry.ToString())}/{Name}";
                SaveName = Name;
                #endregion
                FtpStatus ftpStatus = await ftpClient.UploadFileAsync(Path, SavePath, await ftpClient.FileExistsAsync(SavePath) ? FtpRemoteExists.Resume : FtpRemoteExists.Overwrite, true, FtpVerify.None, progress, token);
                if (ftpStatus == FtpStatus.Success || ftpStatus == FtpStatus.Skipped)
                {
                    Progress = 100;
                    Speed = ClassHelper.FindResource<string>("UploadComplete");
                    UploadCompleted = true;
                }
                else
                {
                    UploadFail = true;
                    retry++;
                }
            }
            else
            {
                ClassHelper.MessageAlert(ClassHelper.MainWindow.GetType(), 3, ClassHelper.FindResource<string>("LocalFileNotExist"));
            }
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        public async void FileDownload()
        {
            if (await ftpClient.FileExistsAsync(Path))
            {
                tokenSource = new CancellationTokenSource();
                token = tokenSource.Token;
                Progress<FtpProgress> progress = new Progress<FtpProgress>(p =>
                {
                    Progress = Math.Round(p.Progress, 2);
                    Speed = p.TransferSpeedToString();
                });
                #region 基本信息
                SavePath = System.IO.Path.Combine(OutPath, $"{Message.Trim()}{(retry == 0 ? string.Empty : retry.ToString())}", Name);
                SaveName = Name;
                #endregion
                FtpStatus ftpStatus = await ftpClient.DownloadFileAsync(SavePath, Path, File.Exists(SavePath) ? FtpLocalExists.Resume : FtpLocalExists.Overwrite, FtpVerify.None, progress, token);
                if (ftpStatus == FtpStatus.Success || ftpStatus == FtpStatus.Skipped)
                {
                    Progress = 100;
                    Speed = ClassHelper.FindResource<string>("DownloadComplete");
                    DownloadCompleted = true;
                }
                else
                {
                    DownloadFail = true;
                    retry++;
                }
            }
            else
            {
                ClassHelper.MessageAlert(ClassHelper.MainWindow.GetType(), 3, ClassHelper.FindResource<string>("ServerFileNotExist"));
            }
        }

        /// <summary>
        /// 暂停
        /// </summary>
        public void SuspendTransmission()
        {
            tokenSource.Cancel();
        }

        /// <summary>
        /// 继续_上传
        /// </summary>
        public void Upload_ContinueTransmission()
        {
            if (token.IsCancellationRequested)
            {
                FileUpload();
            }
        }

        /// <summary>
        /// 继续_下载
        /// </summary>
        public void Download_ContinueTransmission()
        {
            if (token.IsCancellationRequested)
            {
                FileDownload();
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        public void CancelTransmission()
        {
            if (!token.IsCancellationRequested)
            {
                tokenSource.Cancel();
            }
            Progress = 0;
            Speed = "0 MB/S";
        }
    }
}
