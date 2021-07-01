using System;
using Android.Content;
using Android.Views;
using AndroidX.RecyclerView.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinCodeDemo.Controls;
using XamarinCodeDemo.Droid.Renderers;
using XamarinCodeDemo.Droid.Utils;

[assembly: ExportRenderer(typeof(NestedChildCollectionView), typeof(NestedChildCollectionViewRenderer))]
namespace XamarinCodeDemo.Droid.Renderers
{
    /// <summary>
    /// 嵌套滚动 子 RecyclerView
    /// 代码参考自：https://github.com/solartcc/NestedCeilingEffect
    /// </summary>
    public sealed class NestedChildCollectionViewRenderer : CollectionViewRenderer
    {
        private FlingHelper mFlingHelper;
        private bool mIsParentStartFling = false;
        private int mTotalDy = 0;
        private int mVelocity = 0;
        public NestedChildCollectionViewRenderer(Context context) : base(context)
        {
            mFlingHelper = new FlingHelper(context);
            OverScrollMode = OverScrollMode.Never;
        }

        public override void OnScrollStateChanged(int state)
        {
            if (state == ScrollStateIdle)
            {
                DisPatchParentFling();    
            }
        }

        private void DisPatchParentFling()
        {
            if (!IsScrollTop() || mVelocity == 0) return;

            //此控件已经滑动到顶部，且竖直方向加速度不为0，如果有多余的距离则交由父嵌套布局继续fling
            var flingDistance = mFlingHelper.GetSplineFlingDistance(mVelocity);
            if (flingDistance > (Math.Abs(mTotalDy)))
            {
                var parent = FindTarget.FindParentScrollTarget(this);
                parent?.Fling(0, -mFlingHelper.GetVelocityByDistance(flingDistance + mTotalDy));
            }

            mTotalDy = 0;
            mVelocity = 0;
        }

        public override void OnScrolled(int dx, int dy)
        {
            if (mIsParentStartFling)
            {
                mTotalDy = 0;
                mIsParentStartFling = false;
            }

            mTotalDy += dy;
        }

        public override bool Fling(int velocityX, int velocityY)
        {
            if (!IsAttachedToWindow) return false;

            velocityY = mFlingHelper.GetFlingVelocity(velocityY);
            var fling = base.Fling(velocityX, velocityY);
            if (!fling || velocityY >= 0)
            {
                mVelocity = 0;
            }
            else
            {
                mIsParentStartFling = true;
                mVelocity = velocityY;
            }

            return fling;
        }

        private bool IsScrollTop()
        {
            return !CanScrollVertically(-1);
        }
    }
}