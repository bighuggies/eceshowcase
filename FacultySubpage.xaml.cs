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
    /// <summary>
    /// Interaction logic for FacultySubpage.xaml
    /// </summary>
    public partial class FacultySubpage : Page
    {
        DetailPage detailPage;

        Dictionary<string, string[]> FacultyItems;

        public FacultySubpage(DetailPage dp)
        {
            InitializeComponent();
            FacultyItems = new Dictionary<string, string[]>();

            detailPage = dp;
            LoadFacultyData();
            GenerateButtons();
        }

        private void LoadFacultyData()
        {
            string ProgramName = detailPage.Identifier; // TO BE SET WHEN PAGE IS INITIALISED
            bool ReadingCorrectProgram = false;
            XmlTextReader reader = new XmlTextReader("Resources/Faculty.xml");
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
                        string StaffName;
                        string StaffType;
                        string StaffPosition;
                        string StaffRoom;
                        string StaffEmail;
                        string StaffPhone;
                        if (reader.Name == "staff") // everything stored as attributes :(
                        {
                            reader.MoveToAttribute("name");
                            StaffName = reader.Value; // Shown on button
                            reader.MoveToAttribute("type");
                            StaffType = reader.Value; // Used in switch
                            reader.MoveToAttribute("position");
                            StaffPosition = reader.Value;
                            reader.MoveToAttribute("room");
                            StaffRoom = reader.Value;
                            reader.MoveToAttribute("email");
                            StaffEmail = reader.Value;
                            reader.MoveToAttribute("phone");
                            StaffPhone = reader.Value;

                            // Everything in theory stored but needs permanent storage
                            FacultyItems.Add(StaffName, new string[] { StaffType, StaffPosition, StaffRoom, StaffEmail, StaffPhone });

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
            foreach (var pair in FacultyItems)
            {
                Button myButton = new Button();

                myButton.Content = pair.Key; //name
                myButton.Click += FacultyButton_Click;
                myButton.Tag = pair.Value[0];
                myButton.Style = (Style)Resources["Tile"];

                myButton.Background = new SolidColorBrush(Color.FromArgb(0xff, 0x00, 0xa2, 0xb6));
                myButton.Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0xff, 0xff, 0xff));
                myButton.BorderThickness = new Thickness(0);
                myButton.FontSize = 24;
                myButton.Margin = new Thickness(2);
                myButton.Padding = new Thickness(16);
                myButton.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left;
                myButton.VerticalContentAlignment = System.Windows.VerticalAlignment.Bottom;

                switch (pair.Value[0])
                {
                    case "HoD":
                        HoDPanel.Children.Add(myButton);
                        break;

                    case "Deputy HoD":
                        DeputyPanel.Children.Add(myButton);
                        break;

                    case "Manager":
                        ManagerPanel.Children.Add(myButton);
                        break;

                    case "Course Adviser":
                        AdviserPanel.Children.Add(myButton);
                        break;

                    case "Lecturer":
                        LecturerPanel.Children.Add(myButton);
                        break;

                    case "Technician":
                        TechnicianPanel.Children.Add(myButton);
                        break;

                }
            }


        }

        private void FacultyButton_Click(object sender, RoutedEventArgs e)
        {
            //var record = ((Button)e.OriginalSource).Tag;
            var ActiveButton = ((Button)e.OriginalSource);

            //MessageBox.Show(record.ToString());
            //DisplayText.Text = record.ToString();
            string FacultyKey = ActiveButton.Content.ToString();

            // Text labels
            DisplayStaffName.Text = FacultyKey;
            DisplayStaffPosition.Text = FacultyItems[FacultyKey][1];
            ContactDetails.Text = "\nContact Details";
            DisplayStaffDetails.Text = "Room " + FacultyItems[FacultyKey][2] + "\nCity Campus\n38 Princes Street"
                + "\nAuckland\n\nPhone: +64 9 373 7599 ext " + FacultyItems[FacultyKey][4] + "\nEmail: " +
                FacultyItems[FacultyKey][3];
        }
    }
}
