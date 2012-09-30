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
using Microsoft.Surface;
using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;
using Microsoft.Surface.Presentation.Input;
using System.Xml;
using System.IO;



namespace eceshowcase
{
    /// <summary>
    /// Interaction logic for SurfaceWindow1.xaml
    /// </summary>
    /// 

    public partial class SurfaceWindow1 : SurfaceWindow
    {

        private Dictionary<string, string[]> derp = new Dictionary<string, string[]>();
        private string[] firstyear = new string[] { "CHEMMAT 121", "ELECTENG 101", "ENGGEN 115", "ENGGEN 121", "ENGGEN 131", "ENGGEN 140", "ENGSCI 111" };
        /// <summary>
        /// Default constructor.
        /// </summary>
        public SurfaceWindow1()
        {
            InitializeComponent();
            LoadCourseData();
            GenerateButtons();

            // Add handlers for window availability events
            AddWindowAvailabilityHandlers();

        }
        
        // Read in the correct course list from XML
        private void LoadCourseData()
        {
            derp.Add("CHEMMAT 121", new string[] {"Chemical Engineering", "has more women than software"});
        }

        private void GenerateButtons(){
            // create maps
            // firstyear = 
            // secondyear =
            // thirdyear = 
            // fourthyear = 
            // for course in firstyear

            //foreach (string course in firstyear)
            foreach (KeyValuePair<string, string[]> pair in derp) 
            {
                Button myButton = new Button();
                //myButton.Content = course;
                myButton.Content = pair.Key; //name
                //myButton.Tag = ("Turnips"); // TEMP SOLUTION
                myButton.Click += CourseButton_Click;
                Year1Panel.Children.Add(myButton);
            }
        }

        private void CourseButton_Click(object sender, RoutedEventArgs e)
        {
            //var record = ((Button)e.OriginalSource).Tag;
            var ActiveButton = ((Button)e.OriginalSource);
            
            //MessageBox.Show(record.ToString());
            //DisplayText.Text = record.ToString();
            string CourseKey = ActiveButton.Content.ToString();
            DisplayCourseName.Text = CourseKey;
            DisplayCourseInfo.Text = derp[CourseKey][0];

            
        }


        /// <summary>
        /// Runs when window loaded
        /// </summary>
        void Window1_Loaded(object sender, RoutedEventArgs e)
        {

        }

        void PopulateCourseButtons()
        {
            ///
        }

        /// <summary>
        /// Occurs when the window is about to close. 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            // Remove handlers for window availability events
            RemoveWindowAvailabilityHandlers();
        }

        /// <summary>
        /// Adds handlers for window availability events.
        /// </summary>
        private void AddWindowAvailabilityHandlers()
        {
            // Subscribe to surface window availability events
            ApplicationServices.WindowInteractive += OnWindowInteractive;
            ApplicationServices.WindowNoninteractive += OnWindowNoninteractive;
            ApplicationServices.WindowUnavailable += OnWindowUnavailable;
        }

        /// <summary>
        /// Removes handlers for window availability events.
        /// </summary>
        private void RemoveWindowAvailabilityHandlers()
        {
            // Unsubscribe from surface window availability events
            ApplicationServices.WindowInteractive -= OnWindowInteractive;
            ApplicationServices.WindowNoninteractive -= OnWindowNoninteractive;
            ApplicationServices.WindowUnavailable -= OnWindowUnavailable;
        }

        /// <summary>
        /// This is called when the user can interact with the application's window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowInteractive(object sender, EventArgs e)
        {
            //TODO: enable audio, animations here
        }

        /// <summary>
        /// This is called when the user can see but not interact with the application's window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowNoninteractive(object sender, EventArgs e)
        {
            //TODO: Disable audio here if it is enabled

            //TODO: optionally enable animations here
        }

        /// <summary>
        /// This is called when the application's window is not visible or interactive.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowUnavailable(object sender, EventArgs e)
        {
            //TODO: disable audio, animations here
        }



      
    }
}