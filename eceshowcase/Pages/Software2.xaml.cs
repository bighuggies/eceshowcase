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
using System.Windows.Media.Animation;

namespace eceshowcase.Pages
{
    /// <summary>
    /// Interaction logic for Software2.xaml
    /// </summary>
    public partial class Software2 : UserControl
    {
        DoubleAnimationUsingKeyFrames coursesScaleAnimation;
        DoubleAnimationUsingKeyFrames coursesShrinkAnimation;
        ScaleTransform coursesScaleTransform;

        public Software2()
        {
            Switcher.pageSwitcher.TransitionType = PageTransitionType.SlideAndFadeLeft;

            InitializeComponent();

            // Insert code required on object creation below this point.
            coursesScaleTransform = new ScaleTransform();
            CoursesIcon.RenderTransform = coursesScaleTransform;

            coursesScaleAnimation = new DoubleAnimationUsingKeyFrames();
            coursesScaleAnimation.Duration = TimeSpan.FromSeconds(0.5);

            coursesScaleAnimation.KeyFrames.Add(new LinearDoubleKeyFrame(1.0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.0))));
            coursesScaleAnimation.KeyFrames.Add(new LinearDoubleKeyFrame(1.2, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.5))));

            coursesShrinkAnimation = new DoubleAnimationUsingKeyFrames();
            coursesShrinkAnimation.Duration = TimeSpan.FromSeconds(0.5);

            coursesShrinkAnimation.KeyFrames.Add(new LinearDoubleKeyFrame(1.2, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.0))));
            coursesShrinkAnimation.KeyFrames.Add(new LinearDoubleKeyFrame(1.0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.5))));
        }

        private void CoursesIcon_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            coursesScaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, coursesScaleAnimation);
            coursesScaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, coursesScaleAnimation);
        }

        private void CoursesIcon_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            coursesScaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, coursesShrinkAnimation);
            coursesScaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, coursesShrinkAnimation);
        }

        private void homeButton_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Switcher.Switch(new MainMenu2());
        }
    }
}
