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
using System.Windows.Media.Animation;

namespace eceshowcase
{
    /// <summary>
    /// Interaction logic for SurfaceWindow1.xaml
    /// </summary>
    public partial class FrontPage : Page
    {
        private Dictionary<TouchDevice, Point> currentTouchDevices = new Dictionary<TouchDevice, Point>();

        private ShowcaseWindow window;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public FrontPage(ShowcaseWindow w)
        {
            InitializeComponent();
            window = w;
            window.hidePage = (window.Resources["SlideAndFadeRightOut"] as Storyboard).Clone();
            window.showPage = (window.Resources["SlideAndFadeRightIn"] as Storyboard).Clone();
        }

        private void Electrical_Click(object sender, RoutedEventArgs e)
        {
            window.ShowPage(new DetailPage(window, "EEE"));
        }

        private void Software_Click(object sender, RoutedEventArgs e)
        {
            window.ShowPage(new DetailPage(window, "SE"));
        }

        private void Computer_Click(object sender, RoutedEventArgs e)
        {
            window.ShowPage(new DetailPage(window, "CSE"));
        }

        private void Map_Click(object sender, RoutedEventArgs e)
        {
            window.ShowPage(new MapPage(window));
        }

        private void News_Click(object sender, RoutedEventArgs e)
        {
            window.ShowPage(new NewsPage(window));
        }

        private void Future_Click(object sender, RoutedEventArgs e)
        {
            window.ShowPage(new FutureStudentsPage(window));
        }

        private void welcome_carousel_TouchMove(object sender, TouchEventArgs e)
        {
            if (currentTouchDevices.Count == 1)
            {
                int isLeft = 0;
                int isRight = 0;
                foreach (KeyValuePair<TouchDevice, Point> td in currentTouchDevices)
                {
                    if (td.Key != null && e.TouchDevice.GetTouchPoint(this).Position.X - td.Value.X > 100)
                        isRight++;
                    else if (td.Key != null && td.Value.X - e.TouchDevice.GetTouchPoint(this).Position.X > 100)
                        isLeft++;
                    else
                        return;
                }

                if (isLeft == 1 && isRight == 0)
                {
                    if (!welcome_carousel.CanGoToNextPage)
                    {
                        welcome_carousel.FirstPage();
                    }
                    else
                    {
                        welcome_carousel.NextPage();
                    }
                    currentTouchDevices.Clear();
                    return;
                }
                else if (isRight == 1 && isLeft == 0)
                {
                    if (!welcome_carousel.CanGoToPreviousPage)
                    {
                        welcome_carousel.LastPage();
                    }
                    else
                    {
                        welcome_carousel.PreviousPage();
                    }
                    currentTouchDevices.Clear();
                    return;
                }
            }
        }

        private void welcome_carousel_TouchDown(object sender, TouchEventArgs e)
        {
            currentTouchDevices.Add(e.TouchDevice, e.TouchDevice.GetTouchPoint(this).Position);
        }

        private void welcome_carousel_TouchUp(object sender, TouchEventArgs e)
        {
            currentTouchDevices.Remove(e.TouchDevice);
        }
    }
}