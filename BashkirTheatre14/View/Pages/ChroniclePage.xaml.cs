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
        private bool _isAnimated;
        private async void ScrollViewer_OnScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            switch (e.HorizontalOffset)
            {
                case < 2000:
                    ScrollViewer.ScrollToHorizontalOffset(6000);
                    break;
                case > 7000:
                    ScrollViewer.ScrollToHorizontalOffset(3000);
                    break;
            }
            if (_isAnimated) return;
            switch (_offset)
            {
                case <= 0.0 when e.HorizontalChange is < 0 and > -2000:
                case >= 0.0 when e.HorizontalChange is > 0 and < 2000:
                    _offset += e.HorizontalChange;
                    break;
                default:
                    _offset = 0;
                    break;
            }
            if (_isAnimated) return;
            switch (_offset)
            {
                case <= -128 and > -2000 when SliderUserControl.CurrentItemIndex > 0:
                    _offset = 0;
                    _isAnimated = true;
                    (this.DataContext as ChroniclesPageViewModel).SlideLeftCommand.Execute(SliderUserControl);
                    await Task.Delay(550);//anim
                    _isAnimated = false;
                    break;
                case >= 128 and < 2000 when SliderUserControl.CurrentItemIndex < (this.DataContext as ChroniclesPageViewModel).ChroniclesList.Count - 1:
                    _offset = 0;
                    _isAnimated = true;
                    (this.DataContext as ChroniclesPageViewModel).SlideRightCommand.Execute(SliderUserControl);
                    await Task.Delay(550);//anim
                    _isAnimated = false;
                    break;
            }


        }
    }
}
