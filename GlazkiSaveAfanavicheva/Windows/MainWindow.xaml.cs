using GlazkiSaveAfanavicheva.Data;
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
using static GlazkiSaveAfanavicheva.Data.Context; 

namespace GlazkiSaveAfanavicheva.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int numPage;
        public MainWindow()
        {
            InitializeComponent();
            lvAgent.ItemsSource = context.Agent.ToList().Skip(10 * (numPage)).Take(10).ToList();
            cmbSort.ItemsSource = new List<String>
            { "Наименование(возр)",
               "Наименование(убыв)",
               "Скидка(возр)",
               "Скидка(убыв)",
               "Приоритет(возр)",
               "Приоритет(убыв)"
            };
            List<AgentType> agentTypes = context.AgentType.ToList();
            agentTypes.Insert(0, new AgentType() { Title = "Все типы" });
            cmbFiltr.ItemsSource = agentTypes;
            cmbFiltr.DisplayMemberPath = "Title";
            numPage = 1;
        }

        public void Filter()
        {
            var list = context.Agent.Where(i => i.Title.Contains(txtBxSearch.Text) || i.Phone.Contains(txtBxSearch.Text) || i.Email.Contains(txtBxSearch.Text)).ToList();

            switch (cmbSort.SelectedIndex)
            {
                case 0:
                    list = list.OrderBy(i => i.Title).ToList();
                    break;
                case 1:
                    list = list.OrderByDescending(i => i.Title).ToList();
                    break;
                case 2:
                    list = list.OrderBy(i => i.Percent).ToList();
                    break;
                case 3:
                    list = list.OrderByDescending(i => i.Percent).ToList();
                    break;
                case 4:
                    list = list.OrderBy(i => i.Priority).ToList();
                    break;
                case 5:
                    list = list.OrderByDescending(i => i.Priority).ToList();
                    break;
            }

            if (cmbFiltr.SelectedIndex == 0)
            {

            }
            else
            {
                var agentType = cmbFiltr.SelectedItem as AgentType;
                if (agentType != null)
                {
                    list = list.Where(i => i.AgentTypeID == agentType.ID).ToList();
                    lvAgent.ItemsSource = list;
                }
            }

            list = list.Skip(10 * (numPage - 1)).Take(10).ToList();
            lvAgent.ItemsSource = list;

        }

        private void txtBxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void cmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void cmbFiltr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Agent agent = lvAgent.SelectedItem as Agent;
            if (agent != null)
            {
                AddAgentWindow addAgentWindow = new AddAgentWindow(agent);
                addAgentWindow.ShowDialog();
                lvAgent.ItemsSource = context.Agent.ToList();
            }
            else
            {
                MessageBox.Show("Агент не выбран!", "Уведомляю вас", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddAgentWindow addAgentWindow = new AddAgentWindow();
            addAgentWindow.ShowDialog();
            Filter();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lvAgent.SelectedItem is Agent agent)
            {
                var result = MessageBox.Show("Вы действительно хотите удалить запись?",
                    "Удаление клиента", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    context.Agent.Remove(context.Agent.Find(agent.ID));
                    context.SaveChanges();
                    MessageBox.Show("Запись успешно удалена!", "Удаление агента",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    lvAgent.ItemsSource = context.Agent.ToList();
                }
            }
            else
            {
                MessageBox.Show("Выберете агента из списка!", "Удаление агента", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (numPage > 1)
            {
                numPage--;
            }
            Filter();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (lvAgent.Items.Count >= 10)
            {
                numPage++;
            }
            Filter();
        }
    }
}
