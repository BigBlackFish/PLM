using FluentFTP;
using PLM.Common;
using System;

namespace PLM.Models.ViewModels
{
    public class FileViewModel : ModelBase
    {
        private FtpClient ftpClient = new FtpClient(ClassHelper.ftpPath, ClassHelper.ftpUsername, ClassHelper.ftppassword);
        private double progress;

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
            set => progress = value;
        }

        public override void InitializeVariable()
        {
            Name = string.Empty;
            FileType = string.Empty;
            Message = string.Empty;
            Size = 0;
            progress = 0;
        }

        public async void FileUpload()
        {
            // define the progress tracking callback
            Progress<FtpProgress> progress = new Progress<FtpProgress>(p => {
                if (p.Progress == 1)
                {
                    // all done!
                }
                else
                {
                    Progress = p.Progress * 100;
                     // p.TransferSpeed 上传速度
                }
            });
            await ftpClient.ConnectAsync();
            await ftpClient.UploadFileAsync(Path, "服务器路径", FtpRemoteExists.Overwrite, false, FtpVerify.None, progress);
        }
    }
}
