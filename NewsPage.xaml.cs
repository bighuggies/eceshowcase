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
using System.Xml;

namespace eceshowcase
{
    public class newsItem
    {
        public string title { get; set; }
        public string content { get; set; }
        public string date { get; set; }

        public newsItem(string _title, string _content, string _date)
        {
            title = _title;
            content = _content;
            date = _date;
        }
    }

    public class rssFeed
    {
        //public IEnumerable<Channel> channels;
        public List<newsItem> data { get; set; }

        public rssFeed()
        {
            XmlTextReader rssReader = new XmlTextReader("http://www.engineering.auckland.ac.nz/uoa/home/template/news_feed.rss");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(rssReader);

            data = new List<newsItem>();

            XmlNodeList n = xmlDoc.SelectNodes("//channel/item");
            for (int i = 0; i < n.Count; i++)
            {
                data.Add(new newsItem(n[i]["title"].InnerText, n[i]["description"].InnerText, n[i]["pubDate"].InnerText));
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

            rssFeed lolwut = new rssFeed();

            SolidColorBrush whiteBrush = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            foreach (newsItem item in lolwut.data)
            {
                Label temp = new Label();
                temp.Foreground = whiteBrush;
                temp.Content = item.title;
                temp.FontSize = 26;
                contentPanel.Children.Add(temp);

                TextBlock contentLabel = new TextBlock();
                contentLabel.FontSize = 20;
                contentLabel.Text = item.content;
                contentLabel.Foreground = whiteBrush;
                contentLabel.Width = 800;
                contentLabel.TextWrapping = TextWrapping.Wrap;
                contentLabel.TextAlignment = TextAlignment.Left;
                contentLabel.HorizontalAlignment = HorizontalAlignment.Left;
                contentLabel.Margin = new Thickness(20, 0, 20, 0);
                contentPanel.Children.Add(contentLabel);
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            window.ShowPage(new FrontPage(window));
        }
    }
}