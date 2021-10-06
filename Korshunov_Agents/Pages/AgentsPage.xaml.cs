using Korshunov_Agents.Model;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для AgentsPage.xaml
    /// </summary>
    public partial class AgentsPage : Page
    {
        private void FindAgents()
        {
            List<Agent> agents = DB.db.Agent.Where(x => x.Title.StartsWith(tbFinder.Text)).ToList();

            switch (cbSort.SelectedIndex)
            {
                case 0:; break;
                case 1: agents = agents.OrderBy(x => x.Title).ToList(); break;
                case 2: agents = agents.OrderByDescending(x => x.Title).ToList(); break;
            }

            if (cbFilter.SelectedIndex > 0)
            {
                string agentType = cbFilter.SelectedItem.ToString();
                agents = agents.Where(x => x.AgentType.Title == agentType).ToList();
            }

            lbAgents.ItemsSource = agents;
        }

        public AgentsPage()
        {
            InitializeComponent();

            cbFilter.Items.Add("Фильтрация");
            foreach (var agentType in DB.db.AgentType)
            {
                cbFilter.Items.Add(agentType.Title);
            }
            cbFilter.SelectedIndex = 0;

            cbSort.Items.Add("Сортировка");
            cbSort.Items.Add("От А до Я");
            cbSort.Items.Add("От Я до А");
            cbSort.SelectedIndex = 0;

            FindAgents();
        }

        private void tbFinder_TextChanged(object sender, TextChangedEventArgs e)
        {
            FindAgents();
        }

        private void cbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FindAgents();
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FindAgents();
        }
    }
}
