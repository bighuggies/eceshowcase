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
using Microsoft.Maps.MapControl.WPF;
using System.Windows.Media.Animation;

namespace eceshowcase
{
    /// <summary>
    /// Interaction logic for MapPage.xaml
    /// </summary>
    public partial class MapPage : Page
    {
        private Dictionary<TouchDevice, Point> currentTouchDevices = new Dictionary<TouchDevice, Point>();
        ShowcaseWindow window;
        private string direction;

        public MapPage(ShowcaseWindow w)
        {
            InitializeComponent();
            window = w;

            window.hidePage = (window.Resources["SlideAndFadeLeftOut"] as Storyboard).Clone();
            window.showPage = (window.Resources["SlideAndFadeLeftIn"] as Storyboard).Clone();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            window.ShowPage(new FrontPage(window));
        }

        private void previousPage(object sender, RoutedEventArgs e)
        {
            direction = "previous";

            Storyboard hideInfo = (Storyboard)Resources["FadeOut"];
            hideInfo.Completed += hideInfo_Completed;
            hideInfo.Begin(welcome_carousel);
        }

        private void nextPage(object sender, RoutedEventArgs e)
        {
            direction = "next";

            Storyboard hideInfo = (Storyboard)Resources["FadeOut"];
            hideInfo.Completed += hideInfo_Completed;
            hideInfo.Begin(welcome_carousel);
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

                Storyboard hideInfo = (Storyboard)Resources["FadeOut"];
                hideInfo.Completed += hideInfo_Completed;
                hideInfo.Begin(welcome_carousel);

                if (isLeft == 1 && isRight == 0)
                {
                    if (!welcome_carousel.CanGoToNextPage)
                    {
                        direction = "first";
                    }
                    else
                    {
                        direction = "next";
                    }
                    currentTouchDevices.Clear();
                    return;
                }
                else if (isRight == 1 && isLeft == 0)
                {
                    if (!welcome_carousel.CanGoToPreviousPage)
                    {
                        direction = "last";
                    }
                    else
                    {
                        direction = "previous";
                    }
                    currentTouchDevices.Clear();
                    return;
                }
            }
        }

        private void hideInfo_Completed(object sender, EventArgs e)
        {
            switch (direction)
            {
                case "next":
                    welcome_carousel.NextPage();
                    direction = "";
                    break;

                case "previous":
                    welcome_carousel.PreviousPage();
                    direction = "";
                    break;

                case "first":
                    welcome_carousel.FirstPage();
                    direction = "";
                    break;

                case "last":
                    welcome_carousel.LastPage();
                    direction = "";
                    break;
            }

            if (direction == "")
            {
                Storyboard showInfo = (Storyboard)Resources["FadeIn"];
                showInfo.Begin(welcome_carousel);
            }
        }

        private void welcome_carousel_TouchDown(object sender, TouchEventArgs e)
        {
            if (!currentTouchDevices.ContainsKey(e.TouchDevice))
                currentTouchDevices.Add(e.TouchDevice, e.TouchDevice.GetTouchPoint(this).Position);
        }

        private void welcome_carousel_TouchUp(object sender, TouchEventArgs e)
        {
            currentTouchDevices.Remove(e.TouchDevice);
        }
    }
}
