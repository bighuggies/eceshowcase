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
    /// Interaction logic for DetailPage.xaml
    /// </summary>
    public partial class DetailPage : Page
    {
        private ShowcaseWindow window;
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

            SwitchNavTab("overview");
        }

        public void SwitchNavTab(String panelName)
        {
            foreach (UIElement elem in NavButtons.Children)
            {
                Button b = elem as Button;
                b.BorderBrush = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            }

            Brush whiteBrush = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));

            child.Content = null;

            switch (panelName)
            {
                case "overview":
                    OverviewButton.BorderBrush = whiteBrush;
                    child.Content = new OverviewSubpage(this);
                    break;

                case "courses":
                    CoursesButton.BorderBrush = whiteBrush;
                    child.Content = new CoursesSubpage(this);
                    break;

                case "faculty":
                    FacultyButton.BorderBrush = whiteBrush;
                    child.Content = new FacultySubpage(this);
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            window.ShowPage(new FrontPage(window));
        }

        private void OverviewButton_Click(object sender, RoutedEventArgs e)
        {
            SwitchNavTab("overview");
        }

        private void CoursesButton_Click(object sender, RoutedEventArgs e)
        {
            SwitchNavTab("courses");
        }

        private void FacultyButton_Click(object sender, RoutedEventArgs e)
        {
            SwitchNavTab("faculty");
        }
    }
}
