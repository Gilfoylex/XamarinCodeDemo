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
using Android.Bluetooth;
using Xamarin.Essentials;

namespace XamarinCodeDemo.Droid.Utils
{
    public class FlingHelper
    {
        private static float DECELERATION_RATE = ((float)(Math.Log(0.78d) / Math.Log(0.9d)));
        private static float mFlingFriction = ViewConfiguration.ScrollFriction;
        private static float mPhysicalCoeff;
        private int mMaxFlingVelocity;

        public FlingHelper(Context context, float factor = 0.38f)
        {
            mPhysicalCoeff = (float)DeviceDisplay.MainDisplayInfo.Density * 160.0f * 386.0878f * 0.84f;
            mMaxFlingVelocity = (int)(ViewConfiguration.Get(context).ScaledMaximumFlingVelocity * factor);
        }

        private double GetSplineDeceleration(int i)
        {
            return Math.Log((0.35f * ((float)Math.Abs(i))) / (mFlingFriction * mPhysicalCoeff));
        }

        private double GetSplineDecelerationByDistance(double d)
        {
            return ((((double)DECELERATION_RATE) - 1.0d) * Math.Log(d / ((double)(mFlingFriction * mPhysicalCoeff)))) / ((double)DECELERATION_RATE);
        }

        public double GetSplineFlingDistance(int i)
        {
            return Math.Exp(GetSplineDeceleration(i) * (((double)DECELERATION_RATE) / (((double)DECELERATION_RATE) - 1.0d))) * ((double)(mFlingFriction * mPhysicalCoeff));
        }

        public int GetVelocityByDistance(double d)
        {
            return Math.Abs((int)(((Math.Exp(GetSplineDecelerationByDistance(d)) * ((double)mFlingFriction)) * ((double)mPhysicalCoeff)) / 0.3499999940395355d));
        }

        public int GetSplineFlingDuration(int velocity)
        {
            double l = GetSplineDeceleration(velocity); 
            double decelMinusOne = DECELERATION_RATE - 1.0;
            return (int)(1000.0 * Math.Exp(l / decelMinusOne));
        }

        /* 限制加速的最大值 */
        public int GetFlingVelocity(int velocity)
        {
            return Math.Max(-mMaxFlingVelocity, Math.Min(velocity, mMaxFlingVelocity));
        }
    }
}