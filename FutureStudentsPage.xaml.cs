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

        public FutureStudentsPage(ShowcaseWindow w)
        {
            InitializeComponent();
            window = w;
            //child.Content = new OverviewSubpage(this);
            undergradGrid.Visibility = Visibility.Visible;
            postgradGrid.Visibility = Visibility.Hidden;
            accomodationGrid.Visibility = Visibility.Hidden;
            scholGrid.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            window.ShowPage(new FrontPage(window));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            undergradGrid.Visibility = Visibility.Visible;
            postgradGrid.Visibility = Visibility.Hidden;
            accomodationGrid.Visibility = Visibility.Hidden;
            scholGrid.Visibility = Visibility.Hidden;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            undergradGrid.Visibility = Visibility.Hidden;
            postgradGrid.Visibility = Visibility.Visible;
            accomodationGrid.Visibility = Visibility.Hidden;
            scholGrid.Visibility = Visibility.Hidden;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            undergradGrid.Visibility = Visibility.Hidden;
            postgradGrid.Visibility = Visibility.Hidden;
            accomodationGrid.Visibility = Visibility.Visible;
            scholGrid.Visibility = Visibility.Hidden;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            undergradGrid.Visibility = Visibility.Hidden;
            postgradGrid.Visibility = Visibility.Hidden;
            accomodationGrid.Visibility = Visibility.Hidden;
            scholGrid.Visibility = Visibility.Visible;
        }
    }
}