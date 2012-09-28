using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eceshowcase
{
    /// <summary>
    /// Application logic for PageSwitcher.xaml
    /// </summary>
    public partial class PageSwitcher : Window
    {
        Stack<UserControl> pages = new Stack<UserControl>();

        public UserControl CurrentPage { get; set; }

        // Register TransitionType dependency property to PageSwitcher object. Initialised to SlideAndFade.
        public static readonly DependencyProperty TransitionTypeProperty = DependencyProperty.Register("TransitionType",
            typeof(PageTransitionType), typeof(PageSwitcher));

        public PageTransitionType TransitionType
        {
            get
            {
                return (PageTransitionType)GetValue(TransitionTypeProperty);
            }
            set
            {
                SetValue(TransitionTypeProperty, value);
            }
        }

        /// <summary>
        ///  Initialise the main program window with the MainMenu page.
        /// </summary>
        public PageSwitcher()
        {
            InitializeComponent();
            // Static class 
            Switcher.pageSwitcher = this;
            Switcher.Switch(new Pages.MainMenu());
        }

        /// <summary>
        /// Shows a new page by starting the page transition process.
        /// </summary>
        public void ShowPage(UserControl newPage)
        {
            pages.Push(newPage);

            Task.Factory.StartNew(() => ShowNewPage());
        }

        /// <summary>
        /// Unloads the old page (if any) and shows the new page.
        /// </summary>
        void ShowNewPage()
        {
            Dispatcher.Invoke((Action)delegate
            {
                if (contentPresenter.Content != null)
                {
                    UserControl oldPage = contentPresenter.Content as UserControl;

                    if (oldPage != null)
                    {
                        oldPage.Loaded -= newPage_Loaded;

                        UnloadPage(oldPage);
                    }
                }
                else
                {
                    ShowNextPage();
                }

            });
        }

        /// <summary>
        /// Pops the next page off of the stack, maps the loaded event, and then sets the content of the ContentControl to the new page.
        /// </summary>
        void ShowNextPage()
        {
            UserControl newPage = pages.Pop();

            newPage.Loaded += newPage_Loaded;

            contentPresenter.Content = newPage;
        }

        /// <summary>
        /// Starts the animation unloading the existing page.
        /// </summary>
        void UnloadPage(UserControl page)
        {
            Storyboard hidePage = (Resources[string.Format("{0}Out", TransitionType.ToString())] as Storyboard).Clone();

            hidePage.Completed += hidePage_Completed;

            hidePage.Begin(contentPresenter);
        }

        /// <summary>
        /// Once the new page is loaded into the ContentControl, the animation is started.
        /// </summary>
        void newPage_Loaded(object sender, RoutedEventArgs e)
        {
            Storyboard showNewPage = Resources[string.Format("{0}In", TransitionType.ToString())] as Storyboard;

            showNewPage.Begin(contentPresenter);

            CurrentPage = sender as UserControl;
        }

        /// <summary>
        /// Loads the next page after the unload of the previous page.
        /// </summary>
        void hidePage_Completed(object sender, EventArgs e)
        {
            contentPresenter.Content = null;

            ShowNextPage();
        }
    }

    /// <summary>
    /// Enum with the possible page transition animations. For now only SlideAndFade is used.
    /// </summary>
    public enum PageTransitionType
    {
        Fade,
        SlideAndFadeLeft,
        SlideAndFadeRight,
        Grow,
        GrowAndFade
    }
}