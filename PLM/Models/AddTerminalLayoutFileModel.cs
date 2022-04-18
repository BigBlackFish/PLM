namespace PLM.Models
{
    public class AddTerminalLayoutFileModel
    {
        /// <summary>
        /// 版面信息
        /// </summary>
        public string LayoutInfo { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remark { get; set; }

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
        /// 终端缩略文件ID（引用终端ID）
        /// </summary>
        public string TerminalSummaryFileId { get; set; }

        /// <summary>
        /// 缩略文件URL
        /// </summary>
        public string SummaryFileUrl { get; set; }

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

        /// <summary>
        /// 终端源文件ID（引用终端ID）
        /// </summary>
        public string TerminalSourceFileId { get; set; }

        /// <summary>
        /// 源文件URL
        /// </summary>
        public string SourceFileUrl { get; set; }

    }
}
