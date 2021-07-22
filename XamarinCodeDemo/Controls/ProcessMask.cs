using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinCodeDemo.Controls
{
    public class ProcessMask : Grid
    {
        public static readonly BindableProperty ProcessProperty = BindableProperty.Create(
            nameof(Process),
            typeof(int),
            typeof(ProcessMask)
        );

        public int Process
        {
            get => (int)GetValue(ProcessProperty);
            set => SetValue(ProcessProperty, value);
        }

        private Label _text = null;
        public ProcessMask()
        {
            Opacity = 0.5;
            _text = new Label
            {
                TextColor = Color.White,
                Text = $"{Process}%",
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };

            Children.Add(_text);
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == ProcessProperty.PropertyName)
            {
                var pro = Math.Max(0, Process);
                pro = Math.Min(pro, 100);

                Opacity = 0.5 * (1 - pro / 100f);
                IsVisible = pro < 100;
                _text.Text = $"{pro}%";
            }
        }
    }
}
