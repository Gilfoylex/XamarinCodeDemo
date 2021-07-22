using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinCodeDemo.Model;

namespace XamarinCodeDemo.Selector
{
    class MediaItemSelector : DataTemplateSelector
    {
        public DataTemplate ImageItemTemplate { get; set; }

        private DataTemplate xx = null;

        public DataTemplate VideoItemTemplate
        {
            get=> xx; 
            set=> xx = value;
        }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is MediaInfo data)
            {
                switch (data.Type)
                {
                    case MediaType.TypeVideo:
                        return xx;
                    case MediaType.TypeImage:
                        return ImageItemTemplate;
                    default: return ImageItemTemplate;
                }
            }

            return ImageItemTemplate;
        }
    }
}
