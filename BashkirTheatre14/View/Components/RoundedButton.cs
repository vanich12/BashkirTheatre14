using System.Windows;
using Button=System.Windows.Controls;

namespace BashkirTheatre14.View.Components
{
    public class RoundedButton:Button.Button
    {
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
            nameof(CornerRadius), typeof(CornerRadius), typeof(RoundedButton), new PropertyMetadata(default(CornerRadius)));

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
    }
}
