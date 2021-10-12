using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Korshunov_Agents
{
    public class MainVariables
    {
        private static Window mainWindow;
        private static Frame fMain;

        public MainVariables(Window window, Frame frame)
        {
            mainWindow = window;
            fMain = frame;
        }

        public static void NavigateFMain(Page page)
        {
            fMain.NavigationService.Navigate(page);
        }

        public static void GoBackFMain()
        {
            fMain.GoBack();
        }
    }
}
