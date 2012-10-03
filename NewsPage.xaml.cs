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
            

            data.Add(new newsItem("Hello :D", "lolwyut", "adsfadFA"));

            XmlNodeList n = xmlDoc.SelectNodes("//channel/item");
            for ( int i=0; i < n.Count; i++ )
            {
                data.Add(new newsItem(n[i]["title"].InnerText, n[i]["description"].InnerText, n[i]["pubDate"].InnerText));
            }

            
        }
    }

    /// <summary>
    /// Interaction logic for DetailPage.xaml
    /// </summary>
    public partial class DetailPage : Page
    {
        // These commented out bits should be reincluded when this is merged back into master.
        //private ShowcaseWindow window;

        

        public DetailPage(/*ShowcaseWindow w*/)
        {
            InitializeComponent();
            //window = w;
            //child.Content = new OverviewSubpage(this);
            rssFeed lolwut = new rssFeed();

            Label t = new Label();
            foreach (newsItem item in lolwut.data)
            {
                t.Content += item.title + "\n";
            }
            contentPanel.Children.Add(t);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //window.ShowPage(new FrontPage(window));
        }
    }
}
