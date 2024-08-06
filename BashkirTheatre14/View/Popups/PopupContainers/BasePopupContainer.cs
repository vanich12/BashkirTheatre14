using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace BashkirTheatre14.View.Popups.PopupContainers
{
    public abstract class BasePopupContainer:ContentControl
    {
        public static readonly DependencyProperty DurationProperty = DependencyProperty.Register(
            nameof(Duration), typeof(Duration), typeof(BasePopupContainer), new PropertyMetadata(default(Duration)));

        public Duration Duration
        {
            get { return (Duration)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        public static readonly DependencyProperty EasingFunctionProperty = DependencyProperty.Register(
            nameof(EasingFunction), typeof(IEasingFunction), typeof(BasePopupContainer), new PropertyMetadata(default(IEasingFunction)));

        public IEasingFunction EasingFunction
        {
            get { return (PowerEase)GetValue(EasingFunctionProperty); }
            set { SetValue(EasingFunctionProperty, value); }
        }

        public BasePopupContainer()
        {
            this.Loaded += ContainerLoaded;
        }

        protected abstract void ContainerLoaded(object sender, RoutedEventArgs e);
        public abstract void Close();

        protected abstract void StartAnimation(params Timeline[] animations);
        

        public static readonly RoutedEvent ClosedEvent = EventManager.RegisterRoutedEvent(
            name: "Closed",
            routingStrategy: RoutingStrategy.Tunnel,
            handlerType: typeof(RoutedEventHandler),
            ownerType: typeof(BasePopupContainer));

        public event RoutedEventHandler Closed
        {
            add { AddHandler(ClosedEvent, value); }
            remove { RemoveHandler(ClosedEvent, value); }
        }
    }
}
