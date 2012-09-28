using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Surface;
using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;
using Microsoft.Surface.Presentation.Input;

namespace SurfaceApplication2
{
    /// <summary>
    /// Interaction logic for SoftwareEngineeringPage.xaml
    /// </summary>
    public partial class SoftwareEngineeringPage : SurfaceWindow
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public SoftwareEngineeringPage()
        {
            InitializeComponent();

            // Add handlers for window availability events
            AddWindowAvailabilityHandlers();
            Overview.Visibility = Visibility.Visible;
            Courses.Visibility = Visibility.Hidden;
            Faculty.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Occurs when the window is about to close. 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            // Remove handlers for window availability events
            RemoveWindowAvailabilityHandlers();
        }

        /// <summary>
        /// Adds handlers for window availability events.
        /// </summary>
        private void AddWindowAvailabilityHandlers()
        {
            // Subscribe to surface window availability events
            ApplicationServices.WindowInteractive += OnWindowInteractive;
            ApplicationServices.WindowNoninteractive += OnWindowNoninteractive;
            ApplicationServices.WindowUnavailable += OnWindowUnavailable;
        }

        /// <summary>
        /// Removes handlers for window availability events.
        /// </summary>
        private void RemoveWindowAvailabilityHandlers()
        {
            // Unsubscribe from surface window availability events
            ApplicationServices.WindowInteractive -= OnWindowInteractive;
            ApplicationServices.WindowNoninteractive -= OnWindowNoninteractive;
            ApplicationServices.WindowUnavailable -= OnWindowUnavailable;
        }

        /// <summary>
        /// This is called when the user can interact with the application's window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowInteractive(object sender, EventArgs e)
        {
            //TODO: enable audio, animations here
        }

        /// <summary>
        /// This is called when the user can see but not interact with the application's window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowNoninteractive(object sender, EventArgs e)
        {
            //TODO: Disable audio here if it is enabled

            //TODO: optionally enable animations here
        }

        /// <summary>
        /// This is called when the application's window is not visible or interactive.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowUnavailable(object sender, EventArgs e)
        {
            //TODO: disable audio, animations here
        }

        private void ShowOverview()
        {
            Overview.Visibility = Visibility.Visible;
            Courses.Visibility = Visibility.Hidden;
            Faculty.Visibility = Visibility.Hidden;
        }
        private void OverView_Click(object sender, RoutedEventArgs e)
        {
            ShowOverview();
        }

        private void Courses_Click(object sender, RoutedEventArgs e)
        {
            Overview.Visibility = Visibility.Hidden;
            Courses.Visibility = Visibility.Visible;
            Faculty.Visibility = Visibility.Hidden;
        }

        private void Faculty_Click(object sender, RoutedEventArgs e)
        {
            Overview.Visibility = Visibility.Hidden;
            Courses.Visibility = Visibility.Hidden;
            Faculty.Visibility = Visibility.Visible;
        }
    }
}