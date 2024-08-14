using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Core;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using UserControl = System.Windows.Controls.UserControl;

namespace BashkirTheatre14.View.Components
{
    /// <summary>
    /// Логика взаимодействия для LeftAlignContentSliderUserControl.xaml
    /// </summary>
    public partial class LeftAlignContentSliderUserControl : UserControl
    {
        public static readonly DependencyProperty StartFromCenterProperty = DependencyProperty.Register(
            nameof(StartFromCenter), typeof(bool), typeof(LeftAlignContentSliderUserControl), new PropertyMetadata(default(bool)));

        public static readonly DependencyProperty ArrowsPaddingProperty = DependencyProperty.Register(
            nameof(ArrowsPadding), typeof(Thickness), typeof(LeftAlignContentSliderUserControl), new PropertyMetadata(default(Thickness)));

        public Thickness ArrowsPadding
        {
            get { return (Thickness)GetValue(ArrowsPaddingProperty); }
            set { SetValue(ArrowsPaddingProperty, value); }
        }
        public bool StartFromCenter
        {
            get { return (bool)GetValue(StartFromCenterProperty); }
            set { SetValue(StartFromCenterProperty, value); }
        }

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
            nameof(ItemsSource), typeof(IList), typeof(LeftAlignContentSliderUserControl),
            new PropertyMetadata(default(IList), ItemsSourcePropertyChangedCallback));

        public IList? ItemsSource
        {
            get => (IList)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public static readonly DependencyProperty ArrowsShowProperty = DependencyProperty.Register(
            nameof(ArrowsShow), typeof(Visibility), typeof(LeftAlignContentSliderUserControl), new PropertyMetadata(System.Windows.Visibility.Collapsed));

        public Visibility ArrowsShow
        {
            get { return (Visibility)GetValue(ArrowsShowProperty); }
            set { SetValue(ArrowsShowProperty, value); }
        }

        public static readonly DependencyProperty ArrowsWidthProperty = DependencyProperty.Register(
            nameof(ArrowsWidth), typeof(double), typeof(LeftAlignContentSliderUserControl), new PropertyMetadata(default(double)));

        public double ArrowsWidth
        {
            get { return (double)GetValue(ArrowsWidthProperty); }
            set { SetValue(ArrowsWidthProperty, value); }
        }

        public static readonly DependencyProperty ArrowsHeightProperty = DependencyProperty.Register(
            nameof(ArrowsHeight), typeof(double), typeof(LeftAlignContentSliderUserControl), new PropertyMetadata(default(double)));

        public double ArrowsHeight
        {
            get { return (double)GetValue(ArrowsHeightProperty); }
            set { SetValue(ArrowsHeightProperty, value); }
        }


        public static readonly DependencyProperty ScrollBarVisibilityProperty = DependencyProperty.Register(
            nameof(ScrollBarVisibility), typeof(ScrollBarVisibility), typeof(LeftAlignContentSliderUserControl), new PropertyMetadata(System.Windows.Controls.ScrollBarVisibility.Hidden));

        public ScrollBarVisibility ScrollBarVisibility
        {
            get { return (ScrollBarVisibility)GetValue(ScrollBarVisibilityProperty); }
            set { SetValue(ScrollBarVisibilityProperty, value); }
        }
        public static readonly DependencyProperty ItemTemplateProperty = DependencyProperty.Register(
            nameof(ItemTemplate), typeof(DataTemplate), typeof(LeftAlignContentSliderUserControl),
            new PropertyMetadata(default(DataTemplate)));

        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        public static readonly DependencyProperty ItemWidthProperty = DependencyProperty.Register(
            nameof(ItemWidth), typeof(double), typeof(LeftAlignContentSliderUserControl), new PropertyMetadata(default(double)));

        public double ItemWidth
        {
            get => (double)GetValue(ItemWidthProperty);
            set => SetValue(ItemWidthProperty, value);
        }

        public static readonly DependencyProperty CurrentItemIndexProperty = DependencyProperty.Register(
            nameof(CurrentItemIndex), typeof(int), typeof(LeftAlignContentSliderUserControl),
            new FrameworkPropertyMetadata(default(int), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                CurrentItemIndexPropertyChangedCallback));

        public int CurrentItemIndex
        {
            get => (int)GetValue(CurrentItemIndexProperty);
            set => SetValue(CurrentItemIndexProperty, value);
        }

        public static readonly DependencyProperty CurrentItemProperty = DependencyProperty.Register(
            nameof(CurrentItem), typeof(object), typeof(LeftAlignContentSliderUserControl), new PropertyMetadata(default(object)));

        public object? CurrentItem
        {
            get => (object?)GetValue(CurrentItemProperty);
            set => SetValue(CurrentItemProperty, value);
        }

        public static readonly DependencyProperty ScaleProperty = DependencyProperty.Register(
            nameof(Scale), typeof(double), typeof(LeftAlignContentSliderUserControl), new PropertyMetadata(1.0));

        public double Scale
        {
            get => (double)GetValue(ScaleProperty);
            set => SetValue(ScaleProperty, value);
        }

        private ICommand? _leftCommand;

        public ICommand LeftCommand => _leftCommand ??= new RelayCommand(f =>
        {
            if (CurrentItemIndex - 1 < 0) return;
            CurrentItemIndex--;
            ScrollToIndex(CurrentItemIndex);
        });

        private ICommand? _rightCommand;

        public ICommand RightCommand => _rightCommand ??= new RelayCommand(f =>
        {
            if (ItemsSource is not { } list) return;
            if (CurrentItemIndex + 1 > list.Count - 1) return;
            CurrentItemIndex++;
            ScrollToIndex(CurrentItemIndex);
        });

        public LeftAlignContentSliderUserControl()
        {
            InitializeComponent();
        }


        private static void ItemsSourcePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not LeftAlignContentSliderUserControl control)
                return;
            if (control.ItemsSource?.Count >= 2 && control.StartFromCenter) control.CurrentItemIndex = 1;
            control.UpdateCurrentItem();
            control.ScrollToIndex(control.CurrentItemIndex, false);
        }

        private static void CurrentItemIndexPropertyChangedCallback(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            if (d is not LeftAlignContentSliderUserControl control)
                return;

            control.UpdateCurrentItem();
        }

        private void UpdateCurrentItem()
        {
            if (ItemsSource is null || ItemsSource.Count == 0 || ItemsSource.Count==1)
            {
                LeftButton.Visibility = Visibility.Hidden;
                RightButton.Visibility = Visibility.Hidden;
            }
            else if(ArrowsShow==Visibility.Visible)
            {
                LeftButton.Visibility = Visibility.Visible;
                RightButton.Visibility = Visibility.Visible;
            }
            if (ItemsSource is not { } list || CurrentItemIndex < 0 || CurrentItemIndex >= list.Count) return;
            if (ArrowsShow == Visibility.Visible)
            {
                LeftButton.IsEnabled = StartFromCenter? CurrentItemIndex > 1:CurrentItemIndex>0;
                RightButton.IsEnabled = CurrentItemIndex + 1 != ItemsSource.Count;
            }
            
            CurrentItem = list[CurrentItemIndex];
        }

        public void ScrollToIndex(int index, bool animate = true)
        {
            if (index < 0 ||
                ItemsSource is not { Count: > 0 } list)
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

            if (ItemsSource is not { } list)
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
