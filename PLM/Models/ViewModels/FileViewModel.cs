using FluentFTP;
using PLM.Common;
using System;
using System.Threading;
using System.Threading.Tasks;

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

        public override void InitializeVariable()
        {
            Name = string.Empty;
            FileType = string.Empty;
            Message = string.Empty;
            Size = 0;
            progress = 0;
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        public async void FileUpload()
        {
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
            await ftpClient.ConnectAsync();
            await ftpClient.UploadFileAsync(Path, Name, FtpRemoteExists.Overwrite, false, FtpVerify.None, progress, token);
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
        public async void Upload_ContinueTransmission()
        {
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
            await ftpClient.ConnectAsync();
            await ftpClient.UploadFileAsync(Path, Name, FtpRemoteExists.Resume, false, FtpVerify.None, progress, token);
        }

        /// <summary>
        /// 取消
        /// </summary>
        public async void CancelTransmission()
        {
            tokenSource.Cancel();
            await ftpClient.DeleteFileAsync(Name);
            Progress = 0;
            Speed ="0 MB/S";
            UploadCompleted = false;
        }
    }
}
