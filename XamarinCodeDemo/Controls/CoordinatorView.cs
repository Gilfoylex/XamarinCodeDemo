using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;
using XamarinCodeDemo.ViewModel;

namespace XamarinCodeDemo.Controls
{

    /// <summary>
    /// 模仿京东首页的 CoordinatorView，即嵌套布局，目前只支持垂直滚动的嵌套
    /// 通过两层collectionView嵌套实现
    /// </summary>
    public class CoordinatorView : ContentView
    {
        #region 绑定属性

        public static readonly BindableProperty HeaderViewProperty = BindableProperty.Create(
            nameof(HeaderView),
            typeof(View),
            typeof(CoordinatorView)
        );

        /// <summary>
        /// 顶部布局
        /// </summary>
        public View HeaderView
        {
            get => (View)GetValue(HeaderViewProperty);
            set => SetValue(HeaderViewProperty, value);
        }

        public static readonly BindableProperty AdsorptionViewProperty = BindableProperty.Create(
            nameof(AdsorptionView),
            typeof(View),
            typeof(CoordinatorView)
        );

        /// <summary>
        /// 吸附布局
        /// </summary>
        public View AdsorptionView
        {
            get => (View)GetValue(AdsorptionViewProperty);
            set => SetValue(AdsorptionViewProperty, value);
        }

        //public static readonly BindableProperty NestedCollectionViewProperty = BindableProperty.Create(
        //    nameof(NestedCollectionView),
        //    typeof(NestedChildCollectionView),
        //    typeof(CoordinatorView)
        //);

        ///// <summary>
        ///// 内嵌的垂直滚动布局
        ///// </summary>
        //public NestedChildCollectionView NestedCollectionView
        //{
        //    get => (NestedChildCollectionView)GetValue(NestedCollectionViewProperty);
        //    set => SetValue(NestedCollectionViewProperty, value);
        //}


        public static readonly BindableProperty NestedChildViewProperty = BindableProperty.Create(
            nameof(NestedChildView),
            typeof(View),
            typeof(CoordinatorView)
        );

        /// <summary>
        /// 内嵌的垂直滚动布局
        /// </summary>
        public View NestedChildView
        {
            get => (View)GetValue(NestedChildViewProperty);
            set => SetValue(NestedChildViewProperty, value);
        }

        #endregion
        private NestedParentCollectionView _nestedParentCollectionView;
        //private NestedChildCollectionView _nestedChildCollectionView;
        private StackLayout _nestedStackLayout;

        private View _headerView;
        private View _adsorptionView;
        private View _nestedChildView;

        public CoordinatorView()
        {
            //VerticalOptions = LayoutOptions.FillAndExpand;
            _nestedParentCollectionView = new NestedParentCollectionView();

            // _nestedParentCollectionView 的header 作为 CoordinatorView 的 headerview
            // 吸附布局和嵌套的子滚动布局组合到一起作为 _nestedParentCollectionView的一个item 
            _nestedParentCollectionView.ItemsSource = new List<string> {"Adsorption + NestedChildView"};

            _nestedParentCollectionView.ItemsLayout = new GridItemsLayout(ItemsLayoutOrientation.Vertical)
            {
                VerticalItemSpacing = 0, Span = 1,
            };

            _nestedStackLayout = new StackLayout{ Spacing = 0 };

            _nestedParentCollectionView.ItemTemplate = new DataTemplate(() => _nestedStackLayout);

            Content = _nestedParentCollectionView;

            this.BindingContextChanged += CoordinatorView_BindingContextChanged;

        }


        // 更新继承的viewModel, 要不然嵌套的滑动布局找不到外层的viewModel
        private void CoordinatorView_BindingContextChanged(object sender, EventArgs e)
        {
            _nestedParentCollectionView.ItemsSource = new List<object>() { BindingContext };
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (string.IsNullOrWhiteSpace(propertyName)) return;

            if (propertyName == HeaderViewProperty.PropertyName)
            {
                ReplaceHeaderView();
            }

            if (propertyName == AdsorptionViewProperty.PropertyName)
            {
                ReplaceAdsorptionView();
            }

            if (propertyName == NestedChildViewProperty.PropertyName)
            {
                ReplaceNestedView();
            }
        }

        private void ReplaceHeaderView()
        {
            if (_headerView != null)
                _nestedParentCollectionView.Header = null;

            if (HeaderView != null)
            {
                _nestedParentCollectionView.Header = HeaderView;
                _headerView = HeaderView;
            }
        }

        private void ReplaceAdsorptionView()
        {
            if (_nestedChildView != null)
                _nestedStackLayout.Children.Remove(_nestedChildView);

            if (_adsorptionView != null)
                _nestedStackLayout.Children.Remove(_adsorptionView);

            if (AdsorptionView != null)
            {
                _nestedStackLayout.Children.Add(AdsorptionView);
                _adsorptionView = AdsorptionView;
            }

            if (NestedChildView != null)
            {
                _nestedStackLayout.Children.Add(NestedChildView);
                _nestedChildView = NestedChildView;
            }
        }

        private void ReplaceNestedView()
        {
            if (_nestedChildView != null)
                _nestedStackLayout.Children.Remove(_nestedChildView);

            if (NestedChildView != null)
            {
                _nestedStackLayout.Children.Add(NestedChildView);
                _nestedChildView = NestedChildView;
            }
        }
    }
}
