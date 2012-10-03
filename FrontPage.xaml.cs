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

namespace eceshowcase
{
    /// <summary>
    /// Interaction logic for SurfaceWindow1.xaml
    /// </summary>
    public partial class FrontPage : Page
    {
        private ShowcaseWindow window;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public FrontPage(ShowcaseWindow w)
        {
            InitializeComponent();
            window = w;
        }

        private void EEEButton_Click(object sender, RoutedEventArgs e)
        {
            window.ShowPage(new DetailPage(window, "EEE"));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            window.ShowPage(new MapPage(window));
        }

        private void SEButton_Click(object sender, RoutedEventArgs e)
        {
            window.ShowPage(new DetailPage(window, "SE"));
        }

        private void CSEButton_Click(object sender, RoutedEventArgs e)
        {
            window.ShowPage(new DetailPage(window, "CSE"));
        }
    }
}