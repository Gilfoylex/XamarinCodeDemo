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
using Android.Util;
using AndroidX.RecyclerView.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinCodeDemo.Controls;
using XamarinCodeDemo.Droid.Renderers;

[assembly: ExportRenderer(typeof(ImMessageView), typeof(ImMessageViewRenderer))]
namespace XamarinCodeDemo.Droid.Renderers
{
    public class ImMessageViewRenderer : CollectionViewRenderer
    {
        private Context _context;
        public ImMessageViewRenderer(Context context) : base(context)
        {
            _context = context;
        }

    }

    public class MyLayoutManager : LinearLayoutManager
    {
        protected MyLayoutManager(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public MyLayoutManager(Context context) : base(context)
        {
        }

        public MyLayoutManager(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
        }

        public MyLayoutManager(Context context, int orientation, bool reverseLayout) : base(context, orientation, reverseLayout)
        {
        }

        public override void OnMeasure(RecyclerView.Recycler recycler, RecyclerView.State state, int widthSpec, int heightSpec)
        {
            //base.OnMeasure(recycler, state, widthSpec, heightSpec);

            SetMeasuredDimension(100, 100);
        }
    }

}