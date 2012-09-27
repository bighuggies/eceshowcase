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

namespace ChangePage.Pages
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : UserControl, ISwitchable
    {

        //LoginWindowForm loginForm;
        //RegisterForm registerForm;

		public MainMenu()
		{
            
			InitializeComponent();

            //loginForm = new LoginWindowForm();
            //registerForm = new RegisterForm();

            //loginForm.SubmitClicked += new EventHandler(loginWindowForm_SubmitClicked);
            //registerForm.SubmitClicked += new EventHandler(registerForm_SubmitClicked);
		}

        //private void ShowMessageBox(string title, string message, MessageBoxIcon icon)
        //{
            //MessageBoxChildWindow messageWindow =
            //    new MessageBoxChildWindow(title, message, MessageBoxButtons.Ok, icon);

            //messageWindow.Show();
        //}

        #region ISwitchable Members
        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        private void softwareTextBlock_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
        	Switcher.Switch(new Software());
        }

        private void electricalTextBlock_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
        	Switcher.Switch(new Electrical());
        }

        private void computerTextBlock_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Switcher.Switch(new Computer());
        }
        #endregion
    }
}
