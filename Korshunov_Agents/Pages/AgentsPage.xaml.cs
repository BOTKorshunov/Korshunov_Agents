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
        PageSwitcher pageSwitcher;
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

            pageSwitcher = new PageSwitcher(lbAgents, agents);
            spPageSwitcher.Children.Clear();
            spPageSwitcher.Children.Add(pageSwitcher.spPageSwitcher);
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

    public class PageSwitcher
    {
        private int currentPage = 1;
        private int countPages = 1;
        private const int countElementsOnPage = 10;

        private ListBox listBox = new ListBox();
        private List<Agent> agents = new List<Agent>();

        public StackPanel spPageSwitcher = new StackPanel();

        public PageSwitcher(ListBox listBox, List<Agent> agents)
        {
            this.agents = agents;
            this.listBox = listBox;

            for (int i = 0; i < this.agents.Count; i++)
            {
                if (i % 10 == 0 && i != 0)
                {
                    countPages++;
                }
            }

            spPageSwitcher.Orientation = Orientation.Horizontal;
            CreatePages();
        }

        private void CreatePages()
        {
            spPageSwitcher.Children.Clear();
            Label lbBack = new Label();
            lbBack.Content = "<";
            lbBack.MouseLeftButtonDown += LbBack_MouseLeftButtonDown;

            Label lbNext = new Label();
            lbNext.Content = ">";
            lbNext.MouseLeftButtonDown += LbNext_MouseLeftButtonDown;

            spPageSwitcher.Children.Add(lbBack);

            if (currentPage < countPages - 5)
            {
                for (int i = currentPage; i < currentPage + 5; i++)
                {
                    Label lbPage = new Label();
                    lbPage.Content = i;
                    lbPage.MouseLeftButtonDown += LbPage_MouseLeftButtonDown;

                    if (i == currentPage)
                    {
                        lbPage.Foreground = Brushes.Red;
                    }
                    spPageSwitcher.Children.Add(lbPage);
                }
            }
            else
            {
                for (int i = countPages - 5; i < countPages; i++)
                {
                    Label lbPage = new Label();
                    lbPage.Content = i;
                    lbPage.MouseLeftButtonDown += LbPage_MouseLeftButtonDown;

                    if (i == currentPage)
                    {
                        lbPage.Foreground = Brushes.Red;
                    }
                    spPageSwitcher.Children.Add(lbPage);
                }
            }

            spPageSwitcher.Children.Add(lbNext);
        }

        private void ShowAgentsOnPage()
        {
            List<Agent> newAgents = new List<Agent>();
            for (int i = currentPage * countElementsOnPage - countElementsOnPage;
                i < currentPage * countElementsOnPage; i++)
            {
                newAgents.Add(agents[i]);
            }
            listBox.ItemsSource = newAgents;
        }

        private void LbPage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Label label = (Label)sender;
            currentPage = int.Parse(label.Content.ToString());
            ShowAgentsOnPage();
            CreatePages();
        }

        private void LbNext_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (currentPage < countPages)
            {
                currentPage++;
            }
            ShowAgentsOnPage();
            CreatePages();
        }

        private void LbBack_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
            }
            ShowAgentsOnPage();
            CreatePages();
        }
    }
}
