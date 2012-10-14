using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Input;

namespace eceshowcase
{
    /// <summary>
    /// Interaction logic for ShowcaseWindow.xaml
    /// </summary>
    public partial class ShowcaseWindow : Window
    {
        private readonly Stack<Page> pages;
        public Storyboard hidePage;
        public Storyboard showPage;

        public ShowcaseWindow()
        {
            pages = new Stack<Page>();
            InitializeComponent();

            display.Content = new FrontPage(this);
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
            hidePage.Completed += hidePage_Completed;
            hidePage.Begin(display);
        }

        private void newPage_loaded(object sender, RoutedEventArgs e)
        {
            showPage.Begin(display);
        }

        private void hidePage_Completed(object sender, EventArgs e)
        {
            display.Content = null;
            ShowNextPage();
        }
    }
}
