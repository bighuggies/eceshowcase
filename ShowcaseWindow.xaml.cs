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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading.Tasks;
using System.Diagnostics;

namespace eceshowcase
{
    /// <summary>
    /// Interaction logic for ShowcaseWindow.xaml
    /// </summary>
    public partial class ShowcaseWindow : Window
    {
        private readonly Stack<Page> pages;

        public ShowcaseWindow()
        {
            pages = new Stack<Page>();
            InitializeComponent();

            ShowPage(new FrontPage(this));
        }

        public void ShowPage(Page page)
        {
            pages.Push(page);
            Task.Factory.StartNew(() => ShowNewPage());
        }

        private void ShowNewPage()
        {
            Dispatcher.Invoke((Action)delegate
            {
                if (display.Content != null)
                {
                    Page oldPage = display.Content as Page;
                    if (oldPage != null)
                    {
                        oldPage.Loaded -= newPage_loaded;
                        UnloadPage(oldPage);
                    }
                }
                else
                {
                    ShowNextPage();
                }
            });
        }

        private void ShowNextPage()
        {
            Page newPage = pages.Pop();
            newPage.Loaded += newPage_loaded;
            display.Content = newPage;
        }

        private void UnloadPage(Page page)
        {
            Storyboard hidePage = (Resources["GrowOut"] as Storyboard).Clone();
            hidePage.Completed += hidePage_Completed;
            hidePage.Begin(display);
        }

        private void newPage_loaded(object sender, RoutedEventArgs e)
        {
            Storyboard showPage = Resources["GrowIn"] as Storyboard;
            showPage.Begin(display);
        }

        private void hidePage_Completed(object sender, EventArgs e)
        {
            display.Content = null;
            ShowNextPage();
        }
    }
}
