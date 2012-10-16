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
        private ShowcaseWindow window;
        private Grid nextPage;
        private Grid currentPage;
        private Storyboard showPanel = null;

        public FutureStudentsPage(ShowcaseWindow w)
        {
            InitializeComponent();
            window = w;
            //child.Content = new OverviewSubpage(this);
            undergrad.Visibility = Visibility.Visible;
            UndergraduateButton.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            currentPage = undergrad;

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
                    break;

                case "scholarship":
                    nextPage = schol;
                    currentPage = nextPage;
                    hidePanel = (Storyboard)Resources["SlideAndFadeLeftOut"];
                    showPanel = (Storyboard)Resources["SlideAndFadeLeftIn"];
                    hidePanel.Completed += hidePanel_Completed;
                    hidePanel.Begin(child);
                    ScholarshipButton.BorderBrush = whiteBrush;
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
    }
}