using System.Collections.Generic;

namespace PLM.Models
{
    public class PageListResultModel
    {
        public string createTime { get; set; }
        public string updateTime { get; set; }
        public string createUserId { get; set; }
        public string createUserName { get; set; }
        public string updateUserId { get; set; }
        public string updateUserName { get; set; }
        public string id { get; set; }
        public string layoutInfo { get; set; }
        public string remark { get; set; }
        public string summaryFileSize { get; set; }
        public string summaryFileMd5 { get; set; }
        public string summaryFilePwd { get; set; }
        public string summaryFileName { get; set; }
        public string summaryFileType { get; set; }
        public string summaryContentType { get; set; }
        public string terminalSummaryFileId { get; set; }
        public string summaryFileUrl { get; set; }
        public string sourceFileSize { get; set; }
        public string sourceFileMd5 { get; set; }
        public string sourceFilePwd { get; set; }
        public string sourceFileName { get; set; }
        public string sourceFileType { get; set; }
        public string sourceContentType { get; set; }
        public string terminalSourceFileId { get; set; }
        public string sourceFileUrl { get; set; }
        public int isDelete { get; set; }
    }

    public class Records
    {
        public string current { get; set; }
        public List<PageListResultModel> list { get; set; }
        public string size { get; set; }
        public string total { get; set; }
        public string totalPage { get; set; }
    }
}
