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

namespace eceshowcase
{
    public class NewsItem
    {
        public string title { get; set; }
        public string content { get; set; }
        public string date { get; set; }

        public NewsItem(string _title, string _content, string _date)
        {
            title = _title;
            content = _content;
            date = _date;
        }
    }

    public class RSSFeed
    {
        public List<NewsItem> newsList { get; set; }

        public RSSFeed()
        {
            XmlTextReader rssReader = new XmlTextReader("http://www.engineering.auckland.ac.nz/uoa/home/template/news_feed.rss");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(rssReader);

            newsList = new List<NewsItem>();

            XmlNodeList n = xmlDoc.SelectNodes("//channel/item");
            foreach (XmlNode node in n)
            {
                newsList.Add(new NewsItem(node["title"].InnerText, node["description"].InnerText, node["pubDate"].InnerText));
            }
        }
    }

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

            RSSFeed feed = new RSSFeed();

            SolidColorBrush whiteBrush = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            foreach (NewsItem item in feed.newsList)
            {
                Label title = new Label();
                title.Foreground = whiteBrush;
                title.Content = item.title;
                title.FontSize = 26;
                contentPanel.Children.Add(title);

                Label date = new Label();
                date.Foreground = whiteBrush;
                date.Content = item.date;
                date.FontSize = 26;
                contentPanel.Children.Add(date);

                TextBlock content = new TextBlock();
                content.FontSize = 20;
                content.Text = item.content;
                content.Foreground = whiteBrush;
                content.Width = 800;
                content.TextWrapping = TextWrapping.Wrap;
                content.TextAlignment = TextAlignment.Left;
                content.HorizontalAlignment = HorizontalAlignment.Left;
                content.Margin = new Thickness(20, 0, 20, 0);
                contentPanel.Children.Add(content);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            window.ShowPage(new FrontPage(window));
        }
    }
}