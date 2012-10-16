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
        Dictionary<String, List<BitmapImage> > pictureList;

        private void loadData()
        {
            infoList = new Dictionary<String, String>();
            pictureList = new Dictionary<String, List<BitmapImage> >();

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

                        List<BitmapImage> imgList = new List<BitmapImage>();

                        String imgString;
                        reader.MoveToAttribute("images");
                        imgString = reader.Value;

                        if (imgString.Length > 0)
                        {

                            foreach (String s in imgString.Split(';'))
                            {
                                BitmapImage img = new BitmapImage(new System.Uri("Resources/" + s, UriKind.Relative));

                                imgList.Add(img);
                            }

                        }

                        

                        
                        System.Console.WriteLine(content);


                        infoList.Add(name, content);
                        pictureList.Add(name, imgList);

                        AddButton(name);
                    }
                }
            }
        }


        private void addImage(String fileName, int height, int width)
        {
            Rectangle imageHolder = new Rectangle();
            imageHolder.Height = height;
            imageHolder.Width = width;

            BitmapImage image = new BitmapImage(new System.Uri("Resources/" + fileName, UriKind.Relative));

            imageHolder.Fill = new ImageBrush(image);

            Carousel.Children.Add(imageHolder);
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

            // Code to prepare the carousel.
            switch (dp.Identifier)
            {
                case "SE":
                    addImage("SE_0.png", 290, 230);
                    addImage("SE_2.png", 250, 230);
                    addImage("SE_1.png", 260, 240);
                    addImage("SE_3.png", 250, 290);
                    addImage("SE_4.jpg", 270, 190);
                    addImage("SE_7.png", 250, 600);
                    addImage("SE_5.jpg", 250, 170);
                    addImage("SE_6.png", 200, 150);
                    addImage("SE_8.jpg", 280, 400);
                    break;

                case "EEE":
                    addImage("EEE_1.png", 250, 350);
                    addImage("EEE_2.png", 300, 300);
                    addImage("EEE_3.jpg", 250, 300);
                    addImage("EEE_6.png", 300, 240);
                    addImage("EEE_4.jpg", 200, 250);
                    addImage("EEE_5.png", 270, 200);
                    // at the moment repeated, need more pictures
                    addImage("EEE_1.png", 250, 350);
                    addImage("EEE_2.png", 300, 300);
                    addImage("EEE_3.jpg", 250, 300);
                    addImage("EEE_4.jpg", 200, 250);
                    addImage("EEE_5.png", 270, 200);
                    break;

                case "CSE":
                    addImage("8.png", 250, 500);
                    addImage("10.png", 350, 800);
                    addImage("11.jpg", 250, 300);
                    addImage("12.png", 280, 500);
                    addImage("seal.png", 300, 400);
                    break;
            }
            
            Paragraph p = new Paragraph(new Run("General"));
            p.FontSize = 36;
            p.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            Paragraph content = new Paragraph(new Run(infoList["General"]));
            content.FontSize = 24;
            content.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            overviewContent.Blocks.Add(p);
            overviewContent.Blocks.Add(content);
             
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            title = ((SurfaceButton)e.OriginalSource).Content.ToString();

            if (title != previousTitle)
            {
                Storyboard hideInfo = (Storyboard)Resources["FadeOut"];
                hideInfo.Completed += hideInfo_Completed;
                hideInfo.Begin(generalView);
                previousTitle = title;
            }

        }

        private void hideInfo_Completed(object sender, EventArgs e)
        {
            Storyboard showInfo = (Storyboard)Resources["FadeIn"];

            Paragraph p = new Paragraph(new Run(title));
            p.FontSize = 36;
            p.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            Paragraph content = new Paragraph(new Run(infoList[title]));
            content.FontSize = 24;
            content.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            overviewContent.Blocks.Clear();
            overviewContent.Blocks.Add(p);
            overviewContent.Blocks.Add(content);

            foreach (BitmapImage img in pictureList[title])
            {
                Rectangle rect = new Rectangle();
                rect.Height = 200;
                rect.Width = 200;
                rect.Margin = new Thickness(2);

                rect.Fill = new ImageBrush(img);
                pictureViewer.Children.Add(rect);
            }

            showInfo.Begin(generalView);
        }
    }
}
