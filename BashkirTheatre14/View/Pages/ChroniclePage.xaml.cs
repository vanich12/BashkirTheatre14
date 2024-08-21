using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BashkirTheatre14.View.Components;
using BashkirTheatre14.ViewModel.Pages;
using UserControl = System.Windows.Controls.UserControl;

namespace BashkirTheatre14.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для ChroniclePage.xaml
    /// </summary>
    public partial class ChroniclePage : UserControl
    {
        public ChroniclePage()
        {
            InitializeComponent();
        }

        private void ScrollViewer_OnManipulationBoundaryFeedback(object? sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }

        private void ScrollViewer_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scrollviewer = sender as ScrollViewer;
            if (e.Delta > 0)
                scrollviewer.LineLeft();
            else
                scrollviewer.LineRight();
            e.Handled = true;
        }

        private double _offset;
        private async void ScrollViewer_OnScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            
            switch (e.HorizontalOffset)
            {
                case < 3000:
                    ScrollViewer.ScrollToHorizontalOffset(16000);
                    break;
                case > 17000:
                    ScrollViewer.ScrollToHorizontalOffset(4000);
                    break;
            }
            if(Math.Abs(e.HorizontalChange) > 3000) return;
            switch (_offset)
            {
                case <= 0.0 when e.HorizontalChange < 0 :
                case >= 0.0 when e.HorizontalChange > 0:
                    _offset += e.HorizontalChange;
                    break;
                default:
                    _offset = 0;
                    break;
            }
            
        }


        private void ScrollViewer_OnTouchUp(object? sender, TouchEventArgs e)
        {
            if (Math.Abs(_offset) is > 256 or < 3000)
            {
                switch (_offset)
                {
                    case <= -256:
                        (this.DataContext as ChroniclesPageViewModel).SlideLeftCommand.Execute(SliderUserControl);
                        break;
                    case >= 256:
                        (this.DataContext as ChroniclesPageViewModel).SlideRightCommand.Execute(SliderUserControl);
                        break;
                }
            }
            _offset = 0;
        }

        private void ScrollViewer_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (Math.Abs(_offset) is > 256 or < 3000)
            {
                switch (_offset)
                {
                    case <= -256:
                        (this.DataContext as ChroniclesPageViewModel).SlideLeftCommand.Execute(SliderUserControl);
                        break;
                    case >= 256:
                        (this.DataContext as ChroniclesPageViewModel).SlideRightCommand.Execute(SliderUserControl);
                        break;
                }
            }
            _offset = 0;
        }
    }
}
