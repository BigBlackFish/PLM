using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLM.Models
{
    public class ImageFileModel
    {
        public string id { get; set; }
        public string fileUrl { get; set; }
        public string fileName { get; set; }
        public string fileSize { get; set; }
        public string fileNewName { get; set; }
        public string fileDesc { get; set; }
        public string fileHash { get; set; }
        public List<string> tags { get; set; }
    }
}
