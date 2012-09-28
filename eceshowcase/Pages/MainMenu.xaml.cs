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

namespace eceshowcase.Pages
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : UserControl
    {
        public MainMenu()
        {
            Switcher.pageSwitcher.TransitionType = PageTransitionType.GrowAndFade;

            InitializeComponent();
        }

        private void softwareButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Switcher.Switch(new Software());
        }

        private void electricalButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Switcher.Switch(new Electrical());
        }

        private void computerButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Switcher.Switch(new Computer());
        }
    }
}