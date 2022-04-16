using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLM.Models.ViewModels
{
    public class PageFileListViewModel:ModelBase
    {

        public int Id { get; set; }

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
        public int AssociationId { get; set; }

        /// <summary>
        /// 版面信息
        /// </summary>
        public string PageInfomation { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        public string remarksinfomation { get; set; }

        /// <summary>
        /// 上传时间
        /// </summary>
        public string UploadDate { get; set; }

       /// <summary>
       /// 上传者
       /// </summary>
        public string Uploader { get; set; }


        public override void InitializeVariable()
        {
            Id = 0;
            Image = string.Empty;
            ImageInfomation = string.Empty;
            AssociationId= 0;
            PageInfomation = string.Empty;
            remarksinfomation= string.Empty;
            UploadDate = string.Empty;
            Uploader = string.Empty;
        }
    }
}
