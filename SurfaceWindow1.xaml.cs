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



namespace eceshowcase
{
    /// <summary>
    /// Interaction logic for SurfaceWindow1.xaml
    /// </summary>
    /// 

    public partial class SurfaceWindow1 : SurfaceWindow
    {
        // Store the course data from XML
        private Dictionary<string, string[]> CourseItems = new Dictionary<string, string[]>();
        //private Dictionary<string, string[]> secondyear = new Dictionary<string, string[]>();
        //private Dictionary<string, string[]> thirdyear = new Dictionary<string, string[]>();
        //private Dictionary<string, string[]> fourthyear = new Dictionary<string, string[]>();
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
        // CHECK THAT THE ALGORITHM DOES NOT CAUSE A MEASURABLE DELAY IN VIEWING
        // XML IS IN BIN/DEBUG
        // 
        private void LoadCourseData()
        {
            string ProgramName = "SE"; // TO BE SET WHEN PAGE IS INITIALISED
            bool ReadingCorrectProgram = false;
            XmlTextReader reader = new XmlTextReader ("D:\\Programming\\repos\\eceshowcase\\Resources\\courses.xml");
            while (reader.Read()) // each new line
            {
                if (ReadingCorrectProgram == false) // assume not in the right place so we must look for it
                {
                    // This code will look for an element called Program with a value of common or the desired program
                    // Once found it will set a boolean to true
                    if (reader.NodeType == XmlNodeType.Element) // a tag
                    {
                        // looking for the program element
                        if (reader.Name == "program")
                        {
                            // read program value
                            while (reader.MoveToNextAttribute())
                            {
                                if (reader.Value == "Common" || reader.Value == ProgramName) // Must be a firstyear or correct department
                                {
                                    Console.WriteLine(" " + reader.Name + "='" + reader.Value + "'"); //temp
                                    ReadingCorrectProgram = true; // in the right program data
                                }
                            }
                           
                        }
                    }
                    
                 } else // we are in the right program
                        {
                            if (reader.NodeType == XmlNodeType.EndElement) // it will always be set to false when leaving a program
                            {
                                if (reader.Name == "program")
                                {
                                    ReadingCorrectProgram = false;
                                    continue;
                                }
                            }
                            else if (reader.NodeType == XmlNodeType.Element)
                            {
                                string CourseCode;
                                string YearValue;
                                string CourseName;
                                string CourseType;
                                string CoursePrereq;
                                string CourseInfo;
                                if (reader.Name == "course") // everything stored as attributes :(
                                { 
                                    reader.MoveToAttribute("code"); 
                                    CourseCode = reader.Value; // Shown on button
                                    reader.MoveToAttribute("year"); 
                                    YearValue = reader.Value; // Used in switch
                                    reader.MoveToAttribute("name"); 
                                    CourseName = reader.Value; 
                                    reader.MoveToAttribute("type"); 
                                    CourseType = reader.Value;
                                    reader.MoveToAttribute("prereq");
                                    CoursePrereq = reader.Value;
                                    reader.MoveToAttribute("info");
                                    CourseInfo = reader.Value; 

                                    // Everything in theory stored but needs permanent storage
                                    CourseItems.Add(CourseCode, new string[] { CourseName, YearValue, CourseInfo });
                                    
                                }      
                            }

                            //Console.WriteLine(reader.Name);                               
                        }
            }

        }

        private void GenerateButtons(){

            // use tag on button to know which year it belongs to OR combine the maps with an extra field
            // OR change XML to have year as a attribute
            //foreach (string course in firstyear)
            foreach (KeyValuePair<string, string[]> pair in CourseItems) 
            {
                Button myButton = new Button();
                myButton.Content = pair.Key; //name
                myButton.Click += CourseButton_Click;
                myButton.Tag = pair.Value[1];
                switch (pair.Value[1])
                {
                    case "1":
                        Year1Panel.Children.Add(myButton);
                        break;

                    case "2":
                        Year2Panel.Children.Add(myButton);
                        break;

                    case "3":
                        Year3Panel.Children.Add(myButton);
                        break;

                    case "4":
                        Year4Panel.Children.Add(myButton);
                        break;
                
                }
            }
            

        }

        private void CourseButton_Click(object sender, RoutedEventArgs e)
        {
            //var record = ((Button)e.OriginalSource).Tag;
            var ActiveButton = ((Button)e.OriginalSource);
            
            //MessageBox.Show(record.ToString());
            //DisplayText.Text = record.ToString();
            string CourseKey = ActiveButton.Content.ToString();
            // Text labels
            DisplayCourseName.Text = CourseKey;
            DisplayCourseInfo.Text = CourseItems[CourseKey][0];

            
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