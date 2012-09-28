﻿using System;
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
    /// Interaction logic for Electrical.xaml
    /// </summary>
    public partial class Software : UserControl
    {
        public Software()
        {
            InitializeComponent();
            Switcher.pageSwitcher.TransitionType = PageTransitionType.SlideAndFade;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Switcher.Switch(new MainMenu());
        }
    }
}