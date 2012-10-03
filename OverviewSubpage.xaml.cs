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

namespace eceshowcase
{
    /// <summary>
    /// Interaction logic for OverviewSubpage.xaml
    /// </summary>
    public partial class OverviewSubpage : Page
    {
        DetailPage detailPage;

        public OverviewSubpage(DetailPage dp)
        {
            InitializeComponent();
            detailPage = dp;
        }

        private void HideYoKids()
        {
            foreach (var c in views.Children)
            {
                ((FrameworkElement)c).Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HideYoKids();
            generalView.Visibility = System.Windows.Visibility.Visible;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            HideYoKids();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            HideYoKids();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            HideYoKids();
        }
    }
}