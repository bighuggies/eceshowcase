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
    /// Interaction logic for DetailPage.xaml
    /// </summary>
    public partial class FutureStudentsPage : Page
    {
        private Dictionary<TouchDevice, Point> currentTouchDevices = new Dictionary<TouchDevice, Point>();
        private ShowcaseWindow window;
        private Grid nextPage;
        private Grid currentPage;
        private Storyboard showPanel = null;
        private List<string> navTabs = new List<string>();
        private string curTab = "undergraduate";

        public FutureStudentsPage(ShowcaseWindow w)
        {
            InitializeComponent();
            window = w;
            //child.Content = new OverviewSubpage(this);
            undergrad.Visibility = Visibility.Visible;
            UndergraduateButton.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            currentPage = undergrad;

            navTabs.Add("undergrad");
            navTabs.Add("postgrad");
            navTabs.Add("accommodation");
            navTabs.Add("scholarship");

            window.hidePage = (window.Resources["SlideAndFadeLeftOut"] as Storyboard).Clone();
            window.showPage = (window.Resources["SlideAndFadeLeftIn"] as Storyboard).Clone();
        }

        public void SwitchNavTab(String panelName)
        {
            foreach (UIElement elem in NavButtons.Children)
            {
                Button b = elem as Button;
                b.BorderBrush = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            }

            Storyboard hidePanel = null;

            Brush whiteBrush = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));

            switch (panelName)
            {
                case "undergrad":
                    nextPage = undergrad;
                    currentPage = nextPage;
                    hidePanel = (Storyboard)Resources["SlideAndFadeRightOut"];
                    showPanel = (Storyboard)Resources["SlideAndFadeRightIn"];
                    hidePanel.Completed += hidePanel_Completed;
                    hidePanel.Begin(child);
                    UndergraduateButton.BorderBrush = whiteBrush;
                    curTab = "undergrad";
                    break;

                case "postgrad":
                    nextPage = postgrad;
                    if (currentPage == undergrad)
                    {
                        hidePanel = (Storyboard)Resources["SlideAndFadeLeftOut"];
                        showPanel = (Storyboard)Resources["SlideAndFadeLeftIn"];
                    }
                    else
                    {
                        hidePanel = (Storyboard)Resources["SlideAndFadeRightOut"];
                        showPanel = (Storyboard)Resources["SlideAndFadeRightIn"];                       
                    }
                    currentPage = nextPage;
                    hidePanel.Completed += hidePanel_Completed;
                    hidePanel.Begin(child);
                    PostgraduateButton.BorderBrush = whiteBrush;
                    curTab = "postgrad";
                    break;

                case "accommodation":
                    nextPage = accommodation;
                    if (currentPage == schol)
                    {
                        hidePanel = (Storyboard)Resources["SlideAndFadeRightOut"];
                        showPanel = (Storyboard)Resources["SlideAndFadeRightIn"];
                    }
                    else
                    {
                        hidePanel = (Storyboard)Resources["SlideAndFadeLeftOut"];
                        showPanel = (Storyboard)Resources["SlideAndFadeLeftIn"];
                    }
                    currentPage = nextPage;
                    hidePanel.Completed += hidePanel_Completed;
                    hidePanel.Begin(child);
                    AccommodationButton.BorderBrush = whiteBrush;
                    curTab = "accommodation";
                    break;

                case "scholarship":
                    nextPage = schol;
                    currentPage = nextPage;
                    hidePanel = (Storyboard)Resources["SlideAndFadeLeftOut"];
                    showPanel = (Storyboard)Resources["SlideAndFadeLeftIn"];
                    hidePanel.Completed += hidePanel_Completed;
                    hidePanel.Begin(child);
                    ScholarshipButton.BorderBrush = whiteBrush;
                    curTab = "scholarship";
                    break;
            }
        }

        private void hidePanel_Completed(object sender, EventArgs e)
        {
            foreach (Grid grid in child.Children)
            {
                grid.Visibility = Visibility.Hidden;
            }
            nextPage.Visibility = Visibility.Visible;
            showPanel.Begin(child);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            window.ShowPage(new FrontPage(window));
        }

        private void UnderButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage != undergrad)
            {
                SwitchNavTab("undergrad");
            }
        }

        private void PostButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage != postgrad)
            {
                SwitchNavTab("postgrad");
            }
        }

        private void AccommButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage != accommodation)
            {
                SwitchNavTab("accommodation");
            }
        }

        private void ScholButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage != schol)
            {
                SwitchNavTab("scholarship");
            }
        }

        private void NextNavTab()
        {
            int cur = navTabs.IndexOf(curTab);
            if (cur != navTabs.Count - 1)
            {
                int next = cur + 1;
                curTab = navTabs[next];
                SwitchNavTab(curTab);
            }
        }

        private void PrevNavTab()
        {
            int cur = navTabs.IndexOf(curTab);
            if (cur != 0)
            {
                int next = cur - 1;
                curTab = navTabs[next];
                SwitchNavTab(curTab);
            }
        }

        private void Grid_TouchDown(object sender, TouchEventArgs e)
        {
            if (!currentTouchDevices.ContainsKey(e.TouchDevice))
                currentTouchDevices.Add(e.TouchDevice, e.TouchDevice.GetTouchPoint(this).Position);
        }

        private void Grid_TouchMove(object sender, TouchEventArgs e)
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
                    NextNavTab();
                    currentTouchDevices.Clear();
                    return;
                }
                else if (isRight == 1 && isLeft == 0)
                {
                    PrevNavTab();
                    currentTouchDevices.Clear();
                    return;
                }
            }
        }

        private void Grid_TouchUp(object sender, TouchEventArgs e)
        {
            currentTouchDevices.Remove(e.TouchDevice);
        }
    }
}