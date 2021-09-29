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
            lbAgents.ItemsSource = DB.db.Agent.Where(x => x.Title.StartsWith(tbFinder.Text)).ToList();
        }


        private void FilterAgents()
        {
            if (cbFilter.SelectedIndex > 0)
            {
                string agentType = cbFilter.SelectedItem.ToString();
                lbAgents.ItemsSource = DB.db.Agent.Where(x => x.AgentType.Title == agentType).ToList();
            }
        }

        private void SortAgents()
        {
            switch (cbSort.SelectedIndex)
            {
                case 1: lbAgents.ItemsSource = DB.db.Agent.OrderBy(x => x.Title).ToList(); break;
                case 2: lbAgents.ItemsSource = DB.db.Agent.OrderByDescending(x => x.Title).ToList(); break;
            }
        }

        public AgentsPage()
        {
            InitializeComponent();

            FindAgents();

            cbFilter.Items.Add("Сортировка");
            cbFilter.Items.Add("От А до Я");
            cbFilter.Items.Add("От Я до А");
            cbFilter.SelectedIndex = 0;

            cbSort.Items.Add("Фильтрация");
            foreach (var agentType in DB.db.AgentType)
            {
                cbSort.Items.Add(agentType.Title);
            }
            cbSort.SelectedIndex = 0;
        }

        private void tbFinder_TextChanged(object sender, TextChangedEventArgs e)
        {
            FindAgents();
            FilterAgents();
            SortAgents();
        }

        private void cbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FindAgents();
            FilterAgents();
            SortAgents();
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FindAgents();
            FilterAgents();
            SortAgents();
        }
    }
}
