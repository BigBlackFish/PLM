using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLM.Models.ViewModels
{
    public class FileViewModel : ModelBase
    {
        private int progress;

        /// <summary>
        /// 文件名
        /// </summary>
        public string Name { get; set; }

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
        public int Progress
        {
            get => progress;
            set
            {
                progress = value;
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
    }
}
