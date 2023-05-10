using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhoto
{
    public class PhotoMetaData
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
    public class Photo
    {
        public string URL { get; set; }
        public List<PhotoMetaData> MetaData { get; set; }
        public Photo()
        {
            MetaData = new List<PhotoMetaData>();
        }
    }
}
