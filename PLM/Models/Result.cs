namespace PLM.Models
{
    //接受API结果的类
    public class APIResult
    {
        /// <summary>
        /// 0 成功
        /// </summary>
        public short Code { get; set; }
        public string Message { get; set; }
        public string Path { get; set; }
        public object Data { get; set; }
        public object Extra { get; set; }
        public string Timestamp { get; set; }
    }

    public class APIResult<T> where T : class
    {
        /// <summary>
        /// 0 成功
        /// </summary>
        public short Code { get; set; }
        public string Message { get; set; }
        public string Path { get; set; }
        public T Data { get; set; }
        public object Extra { get; set; }
        public string Timestamp { get; set; }
    }
}
