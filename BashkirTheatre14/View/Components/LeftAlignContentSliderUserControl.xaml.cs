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

        public static readonly DependencyProperty AlignToCenterProperty = DependencyProperty.Register(
            nameof(AlignToCenter), typeof(bool), typeof(LeftAlignContentSliderUserControl), new PropertyMetadata(default(bool)));

        public bool AlignToCenter
        {
            get { return (bool)GetValue(AlignToCenterProperty); }
            set { SetValue(AlignToCenterProperty, value); }
        }

        public static readonly DependencyProperty DisplayItemsCountProperty = DependencyProperty.Register(
            nameof(DisplayItemsCount), typeof(int), typeof(LeftAlignContentSliderUserControl), new PropertyMetadata(1));

        public int DisplayItemsCount
        {
            get { return (int)GetValue(DisplayItemsCountProperty); }
            set { SetValue(DisplayItemsCountProperty, value); }
        }

        public static readonly DependencyProperty ArrowsPaddingProperty = DependencyProperty.Register(
            nameof(ArrowsPadding), typeof(Thickness), typeof(LeftAlignContentSliderUserControl), new PropertyMetadata(default(Thickness)));

        public Thickness ArrowsPadding
        {
            get { return (Thickness)GetValue(ArrowsPaddingProperty); }
            set { SetValue(ArrowsPaddingProperty, value); }
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

        public static readonly DependencyProperty ContentMarginProperty = DependencyProperty.Register(
            nameof(ContentMargin), typeof(Thickness), typeof(LeftAlignContentSliderUserControl), new PropertyMetadata(new Thickness()));

        public Thickness ContentMargin
        {
            get { return (Thickness)GetValue(ContentMarginProperty); }
            set { SetValue(ContentMarginProperty, value); }
        }


        private bool LeftCommandEnabled => CurrentItemIndex is not 0;
        private bool RightCommandEnabled => CurrentItemIndex<DisplayItemsCount ?
            CurrentItemIndex + DisplayItemsCount < ItemsSource?.Count :
            CurrentItemIndex + 1 != ItemsSource?.Count;

        private ICommand? _leftCommand;
        public ICommand LeftCommand => _leftCommand ??= new RelayCommand(_ => ScrollByStep(CurrentItemIndex > DisplayItemsCount ? -1 : -DisplayItemsCount));


        private ICommand? _rightCommand;
        public ICommand RightCommand => _rightCommand ??= new RelayCommand(_ => ScrollByStep(CurrentItemIndex == 0 ? DisplayItemsCount : 1));

        public LeftAlignContentSliderUserControl()
        {
            InitializeComponent();
        }

        private void ScrollByStep(int step)
        {
            if (CurrentItemIndex + step > ItemsSource?.Count - 1
                || CurrentItemIndex + step < 0) return;
            CurrentItemIndex += step;
            ScrollToIndex(CurrentItemIndex);
        }

        private static void ItemsSourcePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not LeftAlignContentSliderUserControl control)
                return;
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

            var offset = ItemWidth * index;

            if (animate)
            {
                AnimatedScrollViewer.Scroll(offset);
            }
            else
            {
                AnimatedScrollViewer.ScrollToHorizontalOffset(offset);
            }

            CurrentItemIndex = index;
            NotifyCanExecuteForArrows();
        }

        private void NotifyCanExecuteForArrows()
        {
            LeftButton.IsEnabled = LeftCommandEnabled;
            RightButton.IsEnabled = RightCommandEnabled;
        }

        private void AnimatedScrollViewer_OnTouchUp(object? sender, TouchEventArgs e)
        {
            if (sender is not ScrollViewer scrollViewer)
                return;

            var currentPosition = scrollViewer.HorizontalOffset / ItemWidth;
            var deltaOffset = currentPosition - CurrentItemIndex;
            var offsetLength = Math.Abs(deltaOffset);

            var currentItemIndex = CurrentItemIndex + (int)Math.Round(deltaOffset / offsetLength);

            ScrollToIndex(currentItemIndex);
        }

        private void AnimatedScrollViewer_OnManipulationBoundaryFeedback(object? sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }

        private void LeftAlignContentSliderUserControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (AlignToCenter)
                ContentMargin = new Thickness(
                    (Width - (ItemsControl.ItemContainerGenerator.ContainerFromIndex(0) as FrameworkElement)
                        .ActualWidth) / 2, 0,
                    (Width -
                     (ItemsControl.ItemContainerGenerator.ContainerFromIndex(ItemsSource.Count - 1) as FrameworkElement)
                     .ActualWidth) / 2, 0);
        }
    }
}
