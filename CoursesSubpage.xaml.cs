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
using Microsoft.Surface.Presentation.Controls;

namespace eceshowcase
{
    /// <summary>
    /// Interaction logic for CoursesSubpage.xaml
    /// </summary>
    public partial class CoursesSubpage : Page
    {
        DetailPage detailPage;

        Dictionary<string, string[]> CourseItems;

        public CoursesSubpage(DetailPage dp)
        {
            InitializeComponent();
            CourseItems = new Dictionary<string, string[]>();

            detailPage = dp;
            LoadCourseData();
            GenerateButtons();
        }

        private void LoadCourseData()
        {
            string ProgramName = detailPage.Identifier; // TO BE SET WHEN PAGE IS INITIALISED
            bool ReadingCorrectProgram = false;
            XmlTextReader reader = new XmlTextReader("Resources/Courses.xml");
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

                }
                else // we are in the right program
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
                            CourseItems.Add(CourseCode, new string[] { CourseName, YearValue, CourseInfo, CourseType, CoursePrereq });

                        }
                    }

                    //Console.WriteLine(reader.Name);
                }
            }

        }

        private void GenerateButtons()
        {

            // use tag on button to know which year it belongs to OR combine the maps with an extra field
            // OR change XML to have year as a attribute
            //foreach (string course in firstyear)
            foreach (var pair in CourseItems)
            {
                SurfaceButton myButton = new SurfaceButton();

                myButton.Content = pair.Key; //name
                myButton.Click += CourseButton_Click;
                myButton.Tag = pair.Value[1];
                myButton.Style = (Style)Resources["Tile"];

                myButton.Background = new SolidColorBrush(Color.FromArgb(0xff, 0x00, 0xa2, 0xb6));
                myButton.Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0xff, 0xff, 0xff));
                myButton.BorderThickness = new Thickness(0);
                myButton.FontSize = 24;
                myButton.Margin = new Thickness(2);
                myButton.Padding = new Thickness(16);
                myButton.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left;
                myButton.VerticalContentAlignment = System.Windows.VerticalAlignment.Bottom;

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
            var ActiveButton = ((SurfaceButton)e.OriginalSource);

            //MessageBox.Show(record.ToString());
            //DisplayText.Text = record.ToString();
            string CourseKey = ActiveButton.Content.ToString();
            string Prereq = CourseItems[CourseKey][4];

            if (Prereq == "") {
                Prereq = "None";
            }

            // Text labels
            DisplayCourseCode.Text = CourseKey;
            DisplayCourseName.Text = CourseItems[CourseKey][0];
            DisplayCourseInfo.Text = CourseItems[CourseKey][3] + "\n\n" + CourseItems[CourseKey][2] +
                "\n\nPrerequisite: " + Prereq;
        }
    }
}