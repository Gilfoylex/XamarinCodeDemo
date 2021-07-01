using Android.Content;
using Android.Views;
using AndroidX.Core.View;
using AndroidX.RecyclerView.Widget;
using Java.Lang;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinCodeDemo.Controls;
using XamarinCodeDemo.Droid.Renderers;
using XamarinCodeDemo.Droid.Utils;
using View = Android.Views.View;

[assembly: ExportRenderer(typeof(NestedParentCollectionView), typeof(NestedParentCollectionViewRenderer))]
namespace XamarinCodeDemo.Droid.Renderers
{
    /// <summary>
    /// 嵌套滚动 父 RecyclerView
    /// 代码参考 自：https://github.com/solartcc/NestedCeilingEffect
    /// </summary>
    public sealed class NestedParentCollectionViewRenderer : CollectionViewRenderer, INestedScrollingParent3
    {
        private RecyclerView.LayoutManager mLayoutManager;
        private RecyclerView.Adapter mAdapter;
        private NestedScrollingParentHelper mParentHelper;
        private FlingHelper mFlingHelper;
        private ViewGroup mContentView;
        private int mTotalDy = 0;
        // 记录y轴加速度
        private int mVelocityY = 0;
        private float mLastY = 0f;
        private int mNestedYOffsets = 0;
        private bool mIsStartChildFling = false;
        private bool mIsChildAttachedToTop = false;
        private bool mIsChildDetachedFromTop = true;

        public NestedParentCollectionViewRenderer(Context context) : base(context)
        {
            mParentHelper = new NestedScrollingParentHelper(this);
            mFlingHelper = new FlingHelper(context);
            OverScrollMode = OverScrollMode.Never;
        }

        //public override void SetAdapter(Adapter adapter)
        //{
        //    base.SetAdapter(adapter);
        //    mAdapter = adapter;
        //}

        // 通过 Xamarin.Forms 的源码可知 是通过 SwapAdapter 设置 adapter的，并不是用的 SetAdapter
        public override void SwapAdapter(Adapter adapter, bool removeAndRecycleExistingViews)
        {
            base.SwapAdapter(adapter, removeAndRecycleExistingViews);
            mAdapter = adapter;
        }

        public override void SetLayoutManager(LayoutManager layout)
        {
            base.SetLayoutManager(layout);
            mLayoutManager = layout;
        }

        /**
        * 判断是否是嵌套滑动布局的位置，可覆写该方法定义位置条件
        * 默认最后一个 item 的位置
        *
        * @param child
        * @return
        */
        private bool IsTargetPosition(View child)
        {
            if (mLayoutManager != null && mAdapter != null)
            {
                var position = mLayoutManager.GetPosition(child);
                return position + 1 == mAdapter.ItemCount;
            }

            return false;
        }

        public override void OnChildAttachedToWindow(View child)
        {
            if (IsTargetPosition(child))
            {
                var lp = child.LayoutParameters;
                lp.Height = MeasuredHeight;
                child.LayoutParameters = lp;
                mContentView = (ViewGroup) child;
            }
        }

        public override void OnChildDetachedFromWindow(View child)
        {
            if (IsTargetPosition(child))
            {
                mContentView = null;
            }
        }

        public override bool OnInterceptTouchEvent(MotionEvent ev)
        {
            var isTouchInChildArea = (mContentView != null) && (ev.GetY() >= mContentView.Top);
            // 此控件滑动到底部或者触摸区域在子嵌套布局不拦截事件
            if (IsScrollEnd() || isTouchInChildArea)
            {
                return false;
            }

            return base.OnInterceptTouchEvent(ev);
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            if (e.Action == MotionEventActions.Down)
            {
                mLastY = e.GetY();
                mNestedYOffsets = 0;
                mVelocityY = 0;
                StopScroll();
            }

            var child = FindTarget.FindChildScrollTarget(mContentView);
            var handle = false;
            if (child != null)
            {
                // 如果此控件已经滑动到底部，需要让子嵌套布局滑动剩余的距离
                // 或者子嵌套布局向下还未到顶部，也需要让子嵌套布局先滑动一段距离
                if (IsScrollEnd() || (handle = !IsChildScrollTop(child)))
                {
                    var deltaY = (int)(mLastY - e.GetY());
                    child.ScrollBy(0, deltaY);
                    if (handle)
                    {
                        // 子嵌套布局向下滑动时，要记录y轴的偏移量
                        mNestedYOffsets += deltaY;
                    }
                }
            }
            mLastY = e.GetY();
            // 更新触摸事件的偏移位置，以保证视图平滑的连贯性
            e.OffsetLocation(0, mNestedYOffsets);
            return handle || base.OnTouchEvent(e);
        }

        private bool IsScrollEnd()
        {
            return !CanScrollVertically(1);
        }

        private bool IsChildScrollTop(RecyclerView child)
        {
            return !child.CanScrollVertically(-1);
        }

        public override void OnScrolled(int dx, int dy)
        {
            if (mIsStartChildFling)
            {
                mTotalDy = 0;
                mIsStartChildFling = false;
            }
            mTotalDy += dy;

            //var attached = dy > 0 && IsScrollEnd();
            //if (attached && mIsChildDetachedFromTop)
            //{
            //    mIsChildAttachedToTop = true;
            //    mIsChildDetachedFromTop = false;
            //    var listenerCount = mOnChildAttachStateListeners.size();
            //    for (int i = 0; i < listenerCount; i++)
            //    {
            //        OnChildAttachStateListener listener = mOnChildAttachStateListeners.get(i);
            //        listener.onChildAttachedToTop();
            //    }
            //}

            //var detached = dy < 0 && !IsScrollEnd();
            //if (detached && mIsChildAttachedToTop)
            //{
            //    var child = FindTarget.FindChildScrollTarget(mContentView);
            //    if (child == null || IsChildScrollTop(child))
            //    {
            //        mIsChildDetachedFromTop = true;
            //        mIsChildAttachedToTop = false;
            //        var listenerCount = mOnChildAttachStateListeners.size();
            //        for (int i = 0; i < listenerCount; i++)
            //        {
            //            OnChildAttachStateListener listener = mOnChildAttachStateListeners.get(i);
            //            listener.onChildDetachedFromTop();
            //        }
            //    }
            //}
        }

        public override void OnScrollStateChanged(int state)
        {
            if (state == ScrollStateIdle)
            {
                DispatchChildFling();
            }
        }

        private void DispatchChildFling()
        {
            if (mVelocityY != 0)
            {
                var splineFlingDistance = mFlingHelper.GetSplineFlingDistance(mVelocityY);
                if (splineFlingDistance > mTotalDy)
                {
                    ChildFling(mFlingHelper.GetVelocityByDistance(splineFlingDistance - mTotalDy));
                }
            }

            mTotalDy = 0;
            mVelocityY = 0;
        }

        private void ChildFling(int velocityY)
        {
            var child = FindTarget.FindChildScrollTarget(mContentView);
            if (child != null)
            {
                child.Fling(0, velocityY);
            }
        }

        #region INestedScrollingParent3

        public void OnNestedScroll(View target, int dxConsumed, int dyConsumed, int dxUnconsumed, int dyUnconsumed, int type, int[] consumed)
        {
            OnNestedScrollInternal(dyUnconsumed, type, consumed);
        }

        private void OnNestedScrollInternal(int dyUnconsumed, int type, int[] consumed)
        {
            var oldScrollY = ComputeVerticalScrollOffset();
            ScrollBy(0, dyUnconsumed);
            var myConsumed = ComputeVerticalScrollOffset() - oldScrollY;

            if (consumed != null)
            {
                consumed[1] += myConsumed;
            }

            var myUnconsumed = dyUnconsumed - myConsumed;

            DispatchNestedScroll(0, myConsumed, 0, myUnconsumed, null, type, consumed);
        }

        #endregion


        #region INestedScrollingParent2

        public bool OnStartNestedScroll(View child, View target, int axes, int type)
        {
            return (axes & ViewCompat.ScrollAxisVertical) != 0;
        }

        public void OnNestedScrollAccepted(View child, View target, int axes, int type)
        {
            mParentHelper.OnNestedScrollAccepted(child, target, axes, type);
            StartNestedScroll(ScrollAxis.Vertical, type);
        }

        public void OnStopNestedScroll(View target, int type)
        {
            mParentHelper.OnStopNestedScroll(target, type);
            StopNestedScroll(type);
        }

        public void OnNestedScroll(View target, int dxConsumed, int dyConsumed, int dxUnconsumed, int dyUnconsumed, int type)
        {
            OnNestedScrollInternal(dyUnconsumed, type, null);
        }

        public void OnNestedPreScroll(View target, int dx, int dy, int[] consumed, int type)
        {
            var isParentScroll = DispatchNestedPreScroll(dx, dy, consumed, null, type);
            // 在父嵌套布局没有滑动时，处理此控件是否需要滑动
            if (!isParentScroll)
            {
                // 向上滑动且此控件没有滑动到底部时，需要让此控件继续滑动以保证滑动连贯一致性
                var needKeepScroll = dy > 0 && !IsScrollEnd();
                if (needKeepScroll)
                {
                    ScrollBy(0, dy);
                    consumed[1] = dy;
                }
            }
        }

        #endregion
    }
}