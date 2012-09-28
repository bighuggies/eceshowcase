using System.Windows.Controls;

namespace eceshowcase
{
    /// <summary>
    /// Static class to navigate to other user controls/pages
    /// </summary>
    public static class Switcher
    {
        public static PageSwitcher pageSwitcher;

        public static void Switch(UserControl newPage)
        {
            pageSwitcher.ShowPage(newPage);
        }
    }
}
