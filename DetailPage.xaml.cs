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
    /// Interaction logic for DetailPage.xaml
    /// </summary>
    public partial class DetailPage : Page
    {
        private ShowcaseWindow window;

        public DetailPage(ShowcaseWindow w)
        {
            InitializeComponent();
            window = w;
            child.Content = new OverviewSubpage(this);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            window.ShowPage(new FrontPage(window));
        }
    }
}