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
    /// Interaction logic for MainMenu2.xaml
    /// </summary>
    public partial class MainMenu2 : UserControl
    {
        public MainMenu2()
        {
            Switcher.pageSwitcher.Background = this.Background;
            Switcher.pageSwitcher.TransitionType = PageTransitionType.GrowAndFade;

            InitializeComponent();
        }

        private void softwareButton_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Switcher.Switch(new Software2());
        }

        private void UI_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new MainMenu());
        }
    }
}
