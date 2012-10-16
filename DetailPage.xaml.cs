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
    public partial class DetailPage : Page
    {
        private Dictionary<TouchDevice, Point> currentTouchDevices = new Dictionary<TouchDevice, Point>();
        private ShowcaseWindow window;
        private Page nextPage;
        private Page currentPage;
        private Storyboard showPanel = null;
        private List<string> navTabs = new List<string>();
        private string curTab = "overview";
        public string Identifier { get; private set; }

        public DetailPage(ShowcaseWindow w, String id)
        {
            InitializeComponent();
            window = w;
            Identifier = id;

            switch (id)
            {
                case "EEE":
                    PageTitle.Content = "electrical and electronic engineering";
                        break;
                case "SE":
                    PageTitle.Content = "software engineering";
                    break;
                case "CSE":
                    PageTitle.Content = "computer systems engineering";
                    break;
            }

            window.hidePage = (window.Resources["SlideAndFadeLeftOut"] as Storyboard).Clone();
            window.showPage = (window.Resources["SlideAndFadeLeftIn"] as Storyboard).Clone();

            navTabs.Add("overview");
            navTabs.Add("courses");
            navTabs.Add("faculty");

            OverviewButton.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            child.Content = new OverviewSubpage(this);
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
                case "overview":
                    nextPage = new OverviewSubpage(this);
                    currentPage = nextPage;
                    hidePanel = (Storyboard)Resources["SlideAndFadeRightOut"];
                    showPanel = (Storyboard)Resources["SlideAndFadeRightIn"];
                    hidePanel.Completed += hidePanel_Completed;
                    hidePanel.Begin(child);
                    OverviewButton.BorderBrush = whiteBrush;
                    curTab = "overview";
                    break;

                case "courses":
                    nextPage = new CoursesSubpage(this);
                    currentPage = nextPage;
                    if (child.Content is OverviewSubpage)
                    {
                        hidePanel = (Storyboard)Resources["SlideAndFadeLeftOut"];
                        showPanel = (Storyboard)Resources["SlideAndFadeLeftIn"];
                    }
                    else
                    {
                        hidePanel = (Storyboard)Resources["SlideAndFadeRightOut"];
                        showPanel = (Storyboard)Resources["SlideAndFadeRightIn"];
                    }
                    hidePanel.Completed += hidePanel_Completed;
                    hidePanel.Begin(child);
                    CoursesButton.BorderBrush = whiteBrush;
                    curTab = "courses";
                    break;

                case "faculty":
                    nextPage = new FacultySubpage(this);
                    currentPage = nextPage;
                    hidePanel = (Storyboard)Resources["SlideAndFadeLeftOut"];
                    showPanel = (Storyboard)Resources["SlideAndFadeLeftIn"];
                    hidePanel.Completed += hidePanel_Completed;
                    hidePanel.Begin(child);
                    FacultyButton.BorderBrush = whiteBrush;
                    curTab = "faculty";
                    break;
            }
        }

        private void hidePanel_Completed(object sender, EventArgs e)
        {
            child.Content = nextPage;
            showPanel.Begin(child);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            window.ShowPage(new FrontPage(window));
        }

        private void OverviewButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(currentPage is OverviewSubpage))
            {
                SwitchNavTab("overview");
            }
        }

        private void CoursesButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(currentPage is CoursesSubpage))
            {
                SwitchNavTab("courses");
            }
        }

        private void FacultyButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(currentPage is FacultySubpage))
            {
                SwitchNavTab("faculty");
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
                int next = (cur - 1);
                curTab = navTabs[next];
                SwitchNavTab(curTab);
            }
        }

        private void Grid_TouchDown(object sender, TouchEventArgs e)
        {
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
