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

namespace eceshowcase.Pages
{
    /// <summary>
    /// Interaction logic for SoftwareEngineeringPage.xaml
    /// </summary>
    public partial class Software : UserControl
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Software()
        {
            Switcher.pageSwitcher.TransitionType = PageTransitionType.SlideAndFadeLeft;

            InitializeComponent();

            Overview.Visibility = Visibility.Visible;
            Courses.Visibility = Visibility.Hidden;
            Faculty.Visibility = Visibility.Hidden;
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

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new MainMenu());
        }
    }
}