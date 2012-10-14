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
        private ShowcaseWindow window;
        private Page nextPage;
        private Page currentPage;
        private Storyboard showPanel = null;
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
                    break;

                case "faculty":
                    nextPage = new FacultySubpage(this);
                    currentPage = nextPage;
                    hidePanel = (Storyboard)Resources["SlideAndFadeLeftOut"];
                    showPanel = (Storyboard)Resources["SlideAndFadeLeftIn"];
                    hidePanel.Completed += hidePanel_Completed;
                    hidePanel.Begin(child);
                    FacultyButton.BorderBrush = whiteBrush;
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
    }
}
