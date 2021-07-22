using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinCodeDemo.Model
{
    public enum MediaType
    {
        TypeImage, TypeVideo
    }
    public class MediaInfo
    {
        public MediaType Type { get; set; }

        public string Thumb { get; set; }

        public string MediaUrl { get; set; }
    }
}
