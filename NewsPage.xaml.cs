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
using System.Xml;
using System.Windows.Media.Animation;


namespace eceshowcase
{
    /// <summary>
    /// Interaction logic for NewsPage.xaml
    /// </summary>
    public partial class NewsPage : Page
    {
        private ShowcaseWindow window;

        public NewsPage(ShowcaseWindow w)
        {
            InitializeComponent();
            window = w;

            window.hidePage = (window.Resources["SlideAndFadeLeftOut"] as Storyboard).Clone();
            window.showPage = (window.Resources["SlideAndFadeLeftIn"] as Storyboard).Clone();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            window.ShowPage(new FrontPage(window));
        }
    }
}