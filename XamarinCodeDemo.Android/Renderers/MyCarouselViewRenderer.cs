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
using AndroidX.RecyclerView.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.CollectionView;
using XamarinCodeDemo.Controls;
using XamarinCodeDemo.Droid.Renderers;
using View = Android.Views.View;

[assembly: ExportRenderer(typeof(MyCarouselView), typeof(MyCarouselViewRenderer))]
namespace XamarinCodeDemo.Droid.Renderers
{
    public class MyCarouselViewRenderer : CarouselViewRenderer
    {
        public MyCarouselViewRenderer(Context context) : base(context)
        {
        }

        public override void OnChildAttachedToWindow(View child)
        {
            var lp = child.LayoutParameters;
            lp.Height = LinearLayout.LayoutParams.MatchParent;
            child.LayoutParameters = lp;
            //base.OnChildAttachedToWindow(child);
            
            Console.WriteLine("fuck Attached to window");
        }

        public override void OnChildDetachedFromWindow(View child)
        {
            base.OnChildDetachedFromWindow(child);
            Console.WriteLine("fuck child Detached from windows");
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
            Console.WriteLine($"fuck OnMeasure  width={widthMeasureSpec} , height={heightMeasureSpec}");
        }

        private RecyclerView.LayoutManager mLayoutManager;

        public override void SetLayoutManager(LayoutManager layout)
        {
            //layout.Height = LinearLayout.LayoutParams.WrapContent; 
            base.SetLayoutManager(layout);
            mLayoutManager = layout;
        }
    }
}