using System;
using System.Collections.Generic;
using System.Text;

namespace DEM.App
{
    
    public class DataResponeCommon
    {
        public StatuCodeEnum Statu { get; set; }
        public string Message { get; set; }
    }
    public class DataResponeCommon<T> where T : class
    {
        public StatuCodeEnum Statu { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public int Total { get; set; }
    }
}
