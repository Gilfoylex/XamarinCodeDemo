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
using XamarinCodeDemo.Droid.Renderers;

namespace XamarinCodeDemo.Droid.Utils
{
    public class FindTarget
    {
        public static RecyclerView FindChildScrollTarget(ViewGroup contentView)
        {
            if (contentView == null) return null;
            for (var i = 0; i < contentView.ChildCount; i++)
            {
                var view = contentView.GetChildAt(i);
                switch (view)
                {
                    case NestedChildCollectionViewRenderer recyclerView:
                        return recyclerView;
                    case ViewGroup group:
                    {
                        var target = FindChildScrollTarget(group);
                        if(target != null){
                            return target;
                        }

                        break;
                    }
                }
            }
            return null;
        }

        public static RecyclerView FindParentScrollTarget(View view)
        {
            var parent = view.Parent;
            while (parent != null && !(parent is NestedParentCollectionViewRenderer )){
                parent = parent.Parent;
            }

            return (RecyclerView) parent;
        }
    }
}