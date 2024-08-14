using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using Microsoft.Xaml.Behaviors;
using ProgressBar = System.Windows.Controls.ProgressBar;

namespace BashkirTheatre14.Utilities
{
    public class ProgressBarAnimateBehavior : Behavior<ProgressBar>
    {
        private bool _isAnimating = false;

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.ValueChanged += ProgressBar_ValueChanged;
        }

        private void ProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_isAnimating || e.OldValue == e.NewValue)
                return;

            _isAnimating = true;

            var progressBar = (ProgressBar)sender;

            var doubleAnimation = new DoubleAnimation
            {
                From = e.OldValue,
                To = e.NewValue,
                Duration = new Duration(TimeSpan.FromSeconds(0.3)),
                FillBehavior = FillBehavior.Stop
            };

            doubleAnimation.Completed += (s, args) => _isAnimating = false;

            progressBar.BeginAnimation(ProgressBar.ValueProperty, doubleAnimation);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.ValueChanged -= ProgressBar_ValueChanged;
        }
    }
}