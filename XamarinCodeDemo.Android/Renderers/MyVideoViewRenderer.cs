using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinCodeDemo.Controls;
using XamarinCodeDemo.Droid.Renderers;

[assembly: ExportRenderer(typeof(MyVideoView), typeof(MyVideoViewRenderer))]
namespace XamarinCodeDemo.Droid.Renderers
{
    class MyVideoViewRenderer : ViewRenderer
    {
        public MyVideoViewRenderer(Context context) : base(context)
        {
        }

        public void DetachedFromParent()
        {
            if (Element is MyVideoView ele)
                ele.StopVideo();
        }

        public void AttachedFromParent()
        {
            if (Element is MyVideoView ele)
                ele.PlayVideo();
        }
    }
}