using Korshunov_Agents.Model;
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

            cbType.ItemsSource = DB.db.AgentType.ToList();
            CheckAgent();
        }

        private void CheckAgent()
        {
            imgLogo.Source = new BitmapImage(new Uri(agent.CorrectLogo, 
                UriKind.Relative));
            tbTitle.Text = agent.Title;
            tbAddress.Text = agent.Address;
            tbINN.Text = agent.INN;
            tbKPP.Text = agent.KPP;
            tbDirectorName.Text = agent.DirectorName;
            tbPhone.Text = agent.Phone;
            tbEmail.Text = agent.Email;
            tbPriority.Text = agent.Priority.ToString();

            if (agent.AgentType == null)
            {
                cbType.SelectedIndex = 0;
            }
            else
            {
                cbType.SelectedItem = agent.AgentType;
            }
            imageSources = new ImageSources(imgLogo, agent.CorrectLogo);
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
                FileInfo fileInfo = new FileInfo(imageSources.correctSource);
                imgLogo.Source = new BitmapImage(new Uri(fileInfo.FullName));
            }
        }

        private void btnAddAgent_Click(object sender, RoutedEventArgs e)
        {
            agent.Title = tbTitle.Text;
            agent.Logo = imageSources.correctSource;
            agent.AgentType = (AgentType)cbType.SelectedItem;
            agent.Address = tbAddress.Text;
            agent.INN = tbINN.Text;
            agent.KPP = tbKPP.Text;
            agent.DirectorName = tbDirectorName.Text;
            agent.Phone = tbPhone.Text;
            agent.Email = tbEmail.Text;
            agent.Priority = int.Parse(tbPriority.Text);
            
            if (agent.ID == 0)
            {
                DB.db.Agent.Add(agent);
            }
            DB.db.SaveChanges();

            MainVariables.GoBackFMain();
        }
    }
}
