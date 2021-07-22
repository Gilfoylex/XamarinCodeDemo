using System;
using System.Collections.Generic;
using System.Text;
using XamarinCodeDemo.Model;

namespace XamarinCodeDemo.ViewModel
{
    public class MediaPreviewViewModel : ViewModelBase
    {
        public List<MediaInfo> MediaInfos { get; set; } = new List<MediaInfo>
        {
            new MediaInfo
            {
                Type = MediaType.TypeVideo,
                MediaUrl =
                    "https://athena-voco-test.oss-cn-guangzhou.aliyuncs.com/Voco/0ef1b1b8-b98f-4431-92bc-61c89fca16f4"
            },

            new MediaInfo
            {
                Type = MediaType.TypeVideo,
                MediaUrl =
                    "https://athena-voco-test.oss-cn-guangzhou.aliyuncs.com/Voco/b60a0dc6-e695-4b0e-88e3-c5697c699f6d"

            },

            new MediaInfo
            {
                Type = MediaType.TypeVideo,
                MediaUrl =
                    "https://athena-voco-test.oss-cn-guangzhou.aliyuncs.com/Voco/bcc5ef80-fcb1-4f28-b16a-8dfb22737ef5"

            }
        };
    }
}
