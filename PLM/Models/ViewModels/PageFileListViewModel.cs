namespace PLM.Models.ViewModels
{
    public class PageFileListViewModel : ModelBase
    {

        public string Id { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// 图片信息
        /// </summary>
        public string ImageInfomation { get; set; }

        /// <summary>
        /// 图片关联id
        /// </summary>
        public string AssociationId { get; set; }

        /// <summary>
        /// 版面信息
        /// </summary>
        public string PageInfomation { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remarksinfomation { get; set; }

        /// <summary>
        /// 上传时间
        /// </summary>
        public string UploadDate { get; set; }

        /// <summary>
        /// 上传者
        /// </summary>
        public string Uploader { get; set; }

        /// <summary>
        /// 缩略文件大小
        /// </summary>
        public long SummaryFileSize { get; set; }

        /// <summary>
        /// 缩略文件MD5
        /// </summary>
        public string SummaryFileMd5 { get; set; }

        /// <summary>
        /// 缩略文件目录位置
        /// </summary>
        public string SummaryFilePwd { get; set; }

        /// <summary>
        /// 缩略文件名称
        /// </summary>
        public string SummaryFileName { get; set; }

        /// <summary>
        /// 缩略文件类型 例如jpg
        /// </summary>
        public string SummaryFileType { get; set; }

        /// <summary>
        /// 缩略文件格式 例如image/jpeg
        /// </summary>
        public string SummaryContentType { get; set; }

        /// <summary>
        /// 源文件大小
        /// </summary>
        public long SourceFileSize { get; set; }

        /// <summary>
        /// 源文件MD5
        /// </summary>
        public string SourceFileMd5 { get; set; }

        /// <summary>
        /// 源文件目录位置
        /// </summary>
        public string SourceFilePwd { get; set; }

        /// <summary>
        /// 源文件名称
        /// </summary>
        public string SourceFileName { get; set; }

        /// <summary>
        /// 源文件类型
        /// </summary>
        public string SourceFileType { get; set; }

        /// <summary>
        /// 源文件格式
        /// </summary>
        public string SourceContentType { get; set; }


        public override void InitializeVariable()
        {
            Id = "0";
            Image = string.Empty;
            ImageInfomation = string.Empty;
            AssociationId = "0";
            PageInfomation = string.Empty;
            Remarksinfomation = string.Empty;
            UploadDate = string.Empty;
            Uploader = string.Empty;
        }
    }
}
