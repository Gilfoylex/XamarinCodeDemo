using System;
using System.Collections.Generic;
using System.Text;
using FFImageLoading.Work;
using Xamarin.Forms;
using ImageSource = Xamarin.Forms.ImageSource;

namespace XamarinCodeDemo.Controls
{
    interface ISelectElement
    {
        //note to implementor: implement this property publicly
        bool IsSelected  { get; }

        string Text { get; }

        ImageSource Icon { get; }
        ImageSource SelectedIcon { get; }

        Color TextColor { get; }
        Color SelectedTextColor { get; }

        double FontSize { get; }
        double SelectedFontSize { get; }

        FontAttributes TextAttributes { get; }
        FontAttributes SelectedTextAttributes { get; }

        //note to implementor: but implement this method explicitly
        void OnIsSelectedPropertyChanged(bool oldValue, bool newValue);
    }

    public class SelectElement
    {
        public static readonly BindableProperty IsSelectedProperty =
            BindableProperty.Create(nameof(ISelectElement.IsSelected), typeof(bool), typeof(ISelectElement), false, propertyChanged: OnIsSelectedPropertyChanged);

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(ISelectElement.Text), typeof(string), typeof(ISelectElement), "");

        public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(ISelectElement.Icon), typeof(ImageSource), typeof(ISelectElement), null);

        public static readonly BindableProperty SelectedIconProperty = BindableProperty.Create(nameof(ISelectElement.SelectedIcon), typeof(ImageSource), typeof(ISelectElement), null);

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(ISelectElement.TextColor), typeof(Color), typeof(ISelectElement), Color.Default);

        public static readonly BindableProperty SelectedTextColorProperty = BindableProperty.Create(nameof(ISelectElement.SelectedTextColor), typeof(Color), typeof(ISelectElement), Color.Default);

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(ISelectElement.FontSize), typeof(double), typeof(ISelectElement), 12D);

        public static readonly BindableProperty SelectedFontSizeProperty = BindableProperty.Create(nameof(ISelectElement.SelectedFontSize), typeof(double), typeof(ISelectElement), -1D);

        public static readonly BindableProperty TextAttributesProperty = BindableProperty.Create(nameof(ISelectElement.TextAttributes), typeof(FontAttributes), typeof(ISelectElement), FontAttributes.None);

        public static readonly BindableProperty SelectedTextAttributesProperty = BindableProperty.Create(nameof(ISelectElement.SelectedTextAttributes), typeof(FontAttributes), typeof(ISelectElement), null);

        public static void OnIsSelectedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((ISelectElement)bindable).OnIsSelectedPropertyChanged((bool)oldValue, (bool)newValue);
        }
    }

    public class TabBarButton : StackLayout, ISelectElement
    {
        public static readonly BindableProperty IsSelectedProperty = SelectElement.IsSelectedProperty;

        public static readonly BindableProperty TextProperty = SelectElement.TextProperty;

        public static readonly BindableProperty IconProperty = SelectElement.IconProperty;

        public static readonly BindableProperty SelectedIconProperty = SelectElement.SelectedIconProperty;

        public static readonly BindableProperty TextColorProperty = SelectElement.TextColorProperty;

        public static readonly BindableProperty SelectedTextColorProperty = SelectElement.SelectedTextColorProperty;

        public static readonly BindableProperty FontSizeProperty = SelectElement.FontSizeProperty;

        public static readonly BindableProperty SelectedFontSizeProperty = SelectElement.SelectedFontSizeProperty;

        public static readonly BindableProperty TextAttributesProperty = SelectElement.TextAttributesProperty;

        public static readonly BindableProperty SelectedTextAttributesProperty = SelectElement.SelectedTextAttributesProperty;

        public bool IsSelected
        {
            get => (bool) GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public ImageSource Icon
        {
            get => (ImageSource) GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public ImageSource SelectedIcon
        {
            get => (ImageSource) GetValue(SelectedIconProperty);
            set => SetValue(SelectedIconProperty, value);
        }

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public Color SelectedTextColor
        {
            get => (Color)GetValue(SelectedTextColorProperty);
            set => SetValue(SelectedTextColorProperty, value);
        }

        [TypeConverter(typeof(FontSizeConverter))]
        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        [TypeConverter(typeof(FontSizeConverter))]
        public double SelectedFontSize
        {
            get => (double)GetValue(SelectedFontSizeProperty);
            set => SetValue(SelectedFontSizeProperty, value);
        }

        public FontAttributes TextAttributes
        {
            get => (FontAttributes)GetValue(TextAttributesProperty);
            set => SetValue(TextAttributesProperty, value);
        }

        public FontAttributes SelectedTextAttributes
        {
            get => (FontAttributes)GetValue(SelectedTextAttributesProperty);
            set => SetValue(SelectedTextAttributesProperty, value);
        }

        public void OnIsSelectedPropertyChanged(bool oldValue, bool newValue)
        {
            if (newValue)
            {
                _image.Source = SelectedIconProperty.DefaultValue == SelectedIcon ? Icon : SelectedIcon;

                _label.TextColor = SelectedTextColor.IsDefault
                    ? TextColor
                    : SelectedTextColor;

                _label.FontSize = SelectedFontSize > 0D
                    ? SelectedFontSize
                    : FontSize;

                _label.FontAttributes = (FontAttributes)SelectedTextAttributesProperty.DefaultValue == SelectedTextAttributes
                    ? TextAttributes
                    : SelectedTextAttributes;

                this.ForceLayout();
            }
            else
            {
                _image.Source = Icon;

                _label.TextColor = TextColor;
                
                _label.FontSize = FontSize;

                _label.FontAttributes = TextAttributes;
            }
        }

        private Image _image;
        private Label _label;

        public TabBarButton()
        {
            Margin = new Thickness(0);
            Spacing = 0;
            Padding = new Thickness(0, 4, 0, 0);
            _image = new Image
            {
                VerticalOptions = LayoutOptions.Center,
            };

            _label = new Label
            {
                HorizontalOptions = LayoutOptions.Center,
            };

            Children.Add(_image);
            Children.Add(_label);
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);


            if (propertyName == TextProperty.PropertyName)
            {
                _label.Text = Text;
            }

            if (propertyName == IconProperty.PropertyName)
            {
                _image.Source = Icon;
            }

            if (IsSelected && propertyName == SelectedIconProperty.PropertyName)
            {
                _image.Source = SelectedIcon;
            }

            if (propertyName == TextColorProperty.PropertyName)
            {
                _label.TextColor = TextColor;
            }

            if (IsSelected && propertyName == SelectedTextColorProperty.PropertyName)
            {
                _label.TextColor = SelectedTextColor;
            }

            if (propertyName == FontSizeProperty.PropertyName)
            {
                _label.FontSize = FontSize;
            }

            if (IsSelected && propertyName == SelectedFontSizeProperty.PropertyName)
            {
                _label.FontSize = SelectedFontSize;
            }

            if (propertyName == TextAttributesProperty.PropertyName)
            {
                _label.FontAttributes = TextAttributes;
            }

            if (IsSelected && propertyName == SelectedTextAttributesProperty.PropertyName)
            {
                _label.FontAttributes = SelectedTextAttributes;
            }
        }
    }
}
