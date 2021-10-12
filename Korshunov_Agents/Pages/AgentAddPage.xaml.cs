using Korshunov_Agents.Windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Korshunov_Agents.Pages
{
    /// <summary>
    /// Логика взаимодействия для AgentAddPage.xaml
    /// </summary>
    public partial class AgentAddPage : Page
    {
        Agent agent;
        ImageSources imageSources;

        public AgentAddPage(Agent agent)
        {
            InitializeComponent();

            this.agent = agent;
            CheckAgent();
        }

        private void CheckAgent()
        {
            if (agent.ID == 0)
            {
                string correctSource = @"..\..\Agents\picture.png";
                imgLogo.Source = new BitmapImage(new Uri(correctSource, 
                    UriKind.Relative));
                imageSources = new ImageSources(imgLogo, correctSource);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainVariables.GoBackFMain();
        }

        private void btnChangeImage_Click(object sender, RoutedEventArgs e)
        {
            SelectImageWindow selectImageWindow = 
                new SelectImageWindow(imageSources);
            if (selectImageWindow.ShowDialog() == true)
            {
                imageSources = selectImageWindow.mainImageSources;
                imgLogo.Source = new BitmapImage(new Uri(imageSources.correctSource, UriKind.Relative));
            }
        }
    }
}
