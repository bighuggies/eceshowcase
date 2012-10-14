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
    public partial class FutureStudentsPage : Page
    {
        private ShowcaseWindow window;
        Brush transBrush = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
        Brush whiteBrush = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));

        public FutureStudentsPage(ShowcaseWindow w)
        {
            InitializeComponent();
            window = w;
            //child.Content = new OverviewSubpage(this);
            undergradGrid.Visibility = Visibility.Visible;
            undergradButton.BorderBrush = whiteBrush;

            postgradGrid.Visibility = Visibility.Hidden;
            accommodationGrid.Visibility = Visibility.Hidden;
            scholGrid.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            window.ShowPage(new FrontPage(window));
        }

        private void HideYoWife()
        {
            undergradGrid.Visibility = Visibility.Hidden;
            postgradGrid.Visibility = Visibility.Hidden;
            accommodationGrid.Visibility = Visibility.Hidden;
            scholGrid.Visibility = Visibility.Hidden;

            undergradButton.BorderBrush = transBrush;
            postgradButton.BorderBrush = transBrush;
            accommodationButton.BorderBrush = transBrush;
            scholButton.BorderBrush = transBrush;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            HideYoWife();
            undergradGrid.Visibility = Visibility.Visible;
            undergradButton.BorderBrush = whiteBrush;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            HideYoWife();
            postgradGrid.Visibility = Visibility.Visible;
            postgradButton.BorderBrush = whiteBrush;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            HideYoWife();
            accommodationGrid.Visibility = Visibility.Visible;
            accommodationButton.BorderBrush = whiteBrush;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            HideYoWife();
            scholGrid.Visibility = Visibility.Visible;
            scholButton.BorderBrush = whiteBrush;
        }
    }
}