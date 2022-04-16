using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLM.Common
{
    //接受API结果的类
    public class APIResult
    {
        /// <summary>
        /// 0 成功
        /// </summary>
        public short code { get; set; }
        public string message { get; set; }
        public string path { get; set; }
        public object data { get; set; }
        public object extra { get; set; }
        public string timestamp { get; set; }
    }

    public class APIResult<T> where T : class
    {
        /// <summary>
        /// 0 成功
        /// </summary>
        public short code { get; set; }
        public string message { get; set; }
        public string path { get; set; }
        public T data { get; set; }
        public object extra { get; set; }
        public string timestamp { get; set; }
    }
}
