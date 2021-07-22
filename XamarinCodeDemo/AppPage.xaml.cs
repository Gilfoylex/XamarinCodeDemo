﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinCodeDemo.Pages;

namespace XamarinCodeDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppPage : ContentPage
    {
        public AppPage()
        {
            InitializeComponent();
        }

        private async void Nested_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TestNestedScrollPage());
        }

        private async void Life_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TestLoadUnloadPage());
        }

        private async void Carousel_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TestCarouselViewPage());
        }

        private async void TestVideo_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TestVideoPage());
        }

        private async void TestVideo2_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TestVideoPage2());
        }

        private async void TestTabBtn_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TestTabbarBtnPage());
        }

        private async void TestBadgeBtn_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TestBadgeViewPage());
        }

        private async void TestUpload_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UploadAnimation());
        }
    }
}