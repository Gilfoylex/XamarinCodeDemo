﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinCodeDemo.Pages;

namespace XamarinCodeDemo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new TestNestedScrollPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}