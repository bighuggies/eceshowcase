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
using Microsoft.Surface.Presentation.Controls;
using System.Windows.Media.Animation;

namespace eceshowcase
{
    /// <summary>
    /// Interaction logic for OverviewSubpage.xaml
    /// </summary>
    public partial class OverviewSubpage : Page
    {
        DetailPage detailPage;
        private String title;
        private String previousTitle;

        Dictionary<String, String> infoList;

        private void loadData()
        {
            infoList = new Dictionary<String, String>();

            string programName = detailPage.Identifier;
            bool inCorrectProgram = false;
            XmlTextReader reader = new XmlTextReader("Resources/Faculty.xml");

            while ( reader.Read() )
            {
                //AddButton(reader.Name);
                if (!inCorrectProgram)
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.Name == "program")
                        {
                            while (reader.MoveToNextAttribute())
                            {
                                if (reader.Value == programName)
                                {
                                    inCorrectProgram = true;
                                }
                            }
                        }
                    }
                }
                else
                {
                    // if we are at the end of the program tag.
                    if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "program")
                    {
                        break;
                    }

                    if (reader.Name == "topic" && reader.NodeType != XmlNodeType.EndElement )
                    {
                        String content, name;
                        reader.MoveToAttribute("name");
                        name = reader.Value;
                        reader.MoveToAttribute("content");
                        content = reader.Value;
                        System.Console.WriteLine(content);

                        infoList.Add(name, content);

                        AddButton(name);
                    }
                }
            }
        }

        public void AddButton(String name)
        {
            // Create a new button for the left part of the screen.
            SurfaceButton myButton = new SurfaceButton();
            myButton.Content = name;
            myButton.Style = (Style)Resources["Tile"];

            myButton.Background = new SolidColorBrush(Color.FromArgb(0xff, 0x9a, 0x77, 0xcf));
            myButton.Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0xff, 0xff, 0xff));
            myButton.BorderThickness = new Thickness(0);
            myButton.FontSize = 24;
            myButton.Margin = new Thickness(2);
            myButton.Padding = new Thickness(16);
            myButton.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left;
            myButton.VerticalContentAlignment = System.Windows.VerticalAlignment.Bottom;

            myButton.TouchDown += Button_Click;
            myButton.Click += Button_Click;
            

            ButtonPanel.Children.Add(myButton);
           
        }

        public OverviewSubpage(DetailPage dp)
        {
            InitializeComponent();
            detailPage = dp;
            loadData();
            
            // Uncomment this when the XML file is complete to initialize with General information as default
            /*
            Paragraph p = new Paragraph(new Run("General"));
            p.FontSize = 36;
            Paragraph content = new Paragraph(new Run(infoList["General"]));
            content.FontSize = 24;

            overviewContent.Blocks.Add(p);
            overviewContent.Blocks.Add(content);
             */
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            title = ((SurfaceButton)e.OriginalSource).Content.ToString();

            if (title != previousTitle)
            {
                previousTitle = title;
                Storyboard hideInfo = (Storyboard)Resources["FadeOut"];
                hideInfo.Completed += hideInfo_Completed;
                hideInfo.Begin(generalView);
            }
        }

        private void hideInfo_Completed(object sender, EventArgs e)
        {
            Storyboard showInfo = (Storyboard)Resources["FadeIn"];

            Paragraph p = new Paragraph(new Run(title));
            p.FontSize = 36;
            Paragraph content = new Paragraph(new Run(infoList[title]));
            content.FontSize = 24;

            overviewContent.Blocks.Clear();
            overviewContent.Blocks.Add(p);
            overviewContent.Blocks.Add(content);

            showInfo.Begin(generalView);
        }
    }
}
