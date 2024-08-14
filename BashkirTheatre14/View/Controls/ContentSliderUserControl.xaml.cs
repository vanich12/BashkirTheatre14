using System.Collections;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using CommunityToolkit.Mvvm.Input;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using Panel = System.Windows.Controls.Panel;
using UserControl = System.Windows.Controls.UserControl;

namespace BashkirTheatre14.View.Controls
{
    /// <summary>
    /// Логика взаимодействия для ContentSliderUserControl.xaml
    /// </summary>
    public partial class ContentSliderUserControl : UserControl
    {
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
            nameof(ItemsSource), typeof(IEnumerable), typeof(ContentSliderUserControl),
            new PropertyMetadata(default(IEnumerable), ItemsSourcePropertyChangedCallback));

        public IEnumerable ItemsSource
        {
            get => (IEnumerable)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public static readonly DependencyProperty ArrowsShowProperty = DependencyProperty.Register(
            nameof(ArrowsShow), typeof(Visibility), typeof(ContentSliderUserControl), new PropertyMetadata(System.Windows.Visibility.Collapsed));

        public Visibility ArrowsShow
        {
            get { return (Visibility)GetValue(ArrowsShowProperty); }
            set { SetValue(ArrowsShowProperty, value); }
        }

        public static readonly DependencyProperty ScrollBarVisibilityProperty = DependencyProperty.Register(
            nameof(ScrollBarVisibility), typeof(ScrollBarVisibility), typeof(ContentSliderUserControl), new PropertyMetadata(System.Windows.Controls.ScrollBarVisibility.Hidden));

        public ScrollBarVisibility ScrollBarVisibility
        {
            get { return (ScrollBarVisibility)GetValue(ScrollBarVisibilityProperty); }
            set { SetValue(ScrollBarVisibilityProperty, value); }
        }
        public static readonly DependencyProperty ItemTemplateProperty = DependencyProperty.Register(
            nameof(ItemTemplate), typeof(DataTemplate), typeof(ContentSliderUserControl),
            new PropertyMetadata(default(DataTemplate)));

        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        public static readonly DependencyProperty ItemWidthProperty = DependencyProperty.Register(
            nameof(ItemWidth), typeof(double), typeof(ContentSliderUserControl), new PropertyMetadata(default(double)));

        public double ItemWidth
        {
            get => (double)GetValue(ItemWidthProperty);
            set => SetValue(ItemWidthProperty, value);
        }

        public static readonly DependencyProperty CurrentItemIndexProperty = DependencyProperty.Register(
            nameof(CurrentItemIndex), typeof(int), typeof(ContentSliderUserControl),
            new FrameworkPropertyMetadata(default(int), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                CurrentItemIndexPropertyChangedCallback));

        public int CurrentItemIndex
        {
            get => (int)GetValue(CurrentItemIndexProperty);
            set => SetValue(CurrentItemIndexProperty, value);
        }

        public static readonly DependencyProperty CurrentItemProperty = DependencyProperty.Register(
            nameof(CurrentItem), typeof(object), typeof(ContentSliderUserControl), new PropertyMetadata(default(object)));

        public object? CurrentItem
        {
            get => (object?)GetValue(CurrentItemProperty);
            set => SetValue(CurrentItemProperty, value);
        }

        public static readonly DependencyProperty ScaleProperty = DependencyProperty.Register(
            nameof(Scale), typeof(double), typeof(ContentSliderUserControl), new PropertyMetadata(1.0));

        public double Scale
        {
            get => (double)GetValue(ScaleProperty);
            set => SetValue(ScaleProperty, value);
        }

        [RelayCommand]
        private void Left()
        {
            if (CurrentItemIndex - 1 < 0) return;
            CurrentItemIndex--;
            ScrollToIndex(CurrentItemIndex);
        }

        [RelayCommand]
        private void Right()
        {
            if (ItemsSource is not IList list) return;
            if (CurrentItemIndex + 1 > list.Count - 1) return;
            CurrentItemIndex++;
            ScrollToIndex(CurrentItemIndex);
        }
        [RelayCommand]
        private void ScrollToImage(object f)
        {
            if (ItemsSource is not IList list) return;
            CurrentItemIndex = list.IndexOf(f);
            ScrollToIndex(CurrentItemIndex);
        }
        public ContentSliderUserControl()
        {
            InitializeComponent();
        }


        private static void ItemsSourcePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not ContentSliderUserControl control)
                return;

            control.UpdateCurrentItem();
            control.ScrollToIndex(control.CurrentItemIndex, false);
        }

        private static void CurrentItemIndexPropertyChangedCallback(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            if (d is not ContentSliderUserControl control)
                return;

            control.UpdateCurrentItem();
        }

        private async void UpdateCurrentItem()
        {
            if (ItemsSource is not IList list || CurrentItemIndex < 0 || CurrentItemIndex >= list.Count) return;
            //var max = Math.Min(CurrentItemIndex + 2, list.Count);
            //var min = Math.Max(CurrentItemIndex - 2, 0);
            //for (var i = 0; i < list.Count; i++)
            //{
            //    if (i >= min && i <= max)
            //    {
            //        if(list[i].Image?.Source is null)
            //            list[i].Image = new DisposableImage(list[i].ImagePath,height: 2049);
            //    }
            //    else
            //    {
            //        list[i].Image?.Dispose();
            //    }
            //}
            if (ArrowsShow == Visibility.Visible)
            {
                LeftButton.Visibility = CurrentItemIndex == 0 ? Visibility.Hidden : Visibility.Visible;
                RightButton.Visibility = CurrentItemIndex + 1 == list.Count ? Visibility.Hidden : Visibility.Visible;
            }
            
            CurrentItem = list[CurrentItemIndex];
        }

        private void ScrollToIndex(int index, bool animate = true)
        {
            if (index < 0 ||
                ItemsSource is not IList { Count: > 0 } list)
            {
                index = 0;
            }
            else if (index > list.Count - 1)
            {
                index = list.Count - 1;
            }

            var offset = ItemWidth * Scale * index;

            if (animate)
            {
                AnimatedScrollViewer.Scroll(offset);
            }
            else
            {
                AnimatedScrollViewer.ScrollToHorizontalOffset(offset);
            }

            CurrentItemIndex = index;
        }

        private void AnimatedScrollViewer_OnScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            _scrollChanged = true;

            for (var i = 0; i < ContentItemsControl.Items.Count; i++)
            {
                var element = ContentItemsControl.ItemContainerGenerator.ContainerFromIndex(i) as FrameworkElement;

                if (element is null)
                    continue;

                var value = GetElementIntersectionWithCenterGrid(element);
                var scale = Scale + (1.0 - Scale) * value;

                element.LayoutTransform = new ScaleTransform(scale, scale);
                element.Opacity = Math.Pow(scale,2);
                Panel.SetZIndex(element,(int)(scale * 10));
            }
        }



        private double GetElementIntersectionWithCenterGrid(FrameworkElement element)
        {
            if (!element.IsVisible)
                return 0.0;

            var elementBoundsRect = element.TransformToAncestor(AnimatedScrollViewer)
                .TransformBounds(new Rect(0.0, 0.0, element.ActualWidth, element.ActualHeight));
            var containerRect = new Rect(LeftGrid.ActualWidth, 0.0, ItemWidth, AnimatedScrollViewer.ActualHeight);

            if (containerRect.Contains(elementBoundsRect.TopLeft) &&
                containerRect.Contains(elementBoundsRect.BottomRight))
                return 1.0;

            if (containerRect.Contains(elementBoundsRect.BottomRight))
                return 1.0 - (containerRect.Left - elementBoundsRect.Left) / elementBoundsRect.Width;

            if (containerRect.Contains(elementBoundsRect.TopLeft))
                return 1.0 - (elementBoundsRect.Right - containerRect.Right) / elementBoundsRect.Width;

            return 0.0;
        }

        private bool _scrollChanged;

        private void AnimatedScrollViewer_OnTouchDown(object? sender, TouchEventArgs e)
        {
            _scrollChanged = false;
        }

        private void AnimatedScrollViewer_OnTouchUp(object? sender, TouchEventArgs e)
        {
            if (sender is not ScrollViewer scrollViewer)
                return;

            var currentPosition = scrollViewer.HorizontalOffset / (ItemWidth * Scale);
            var deltaOffset = currentPosition - CurrentItemIndex;
            var offsetLength = Math.Abs(deltaOffset);

            var currentItemIndex = offsetLength is >= 0.2 and < 0.5
                ? CurrentItemIndex + (int)Math.Round(deltaOffset / offsetLength)
                : (int)Math.Round(currentPosition);

            ScrollToIndex(currentItemIndex);
        }

        private bool _mouseMoved;

        private void Element_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            _scrollChanged = false;
            _mouseMoved = false;

            if (sender is not ContentPresenter contentPresenter)
                return;

            var content = contentPresenter.Content;

            if (content.Equals(CurrentItem))
                return;

            e.Handled = true;
        }

        private void Element_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_scrollChanged || _mouseMoved || sender is not ContentPresenter contentPresenter)
                return;

            var content = contentPresenter.Content;

            if (content == CurrentItem)
                return;

            if (ItemsSource is not IList list)
                return;

            var index = list.IndexOf(content);

            if (index < 0)
                return;

            ScrollToIndex(index);
        }

        private void Element_OnMouseMove(object sender, MouseEventArgs e)
        {
            _mouseMoved = true;
        }

        private void AnimatedScrollViewer_OnManipulationBoundaryFeedback(object? sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }


        
    }
}
