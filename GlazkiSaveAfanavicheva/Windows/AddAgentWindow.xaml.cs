using GlazkiSaveAfanavicheva.Data;
using Microsoft.Win32;
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
using System.Windows.Shapes;
using static GlazkiSaveAfanavicheva.Data.Context;

namespace GlazkiSaveAfanavicheva.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddAgentWindow.xaml
    /// </summary>
    public partial class AddAgentWindow : Window
    {
        private bool isNewClient = true;
        Agent agentEdit;

        private string filePath;
        public AddAgentWindow()
        {
            InitializeComponent();
            cmbTypeAgent.ItemsSource = context.AgentType.ToList();
            cmbTypeAgent.DisplayMemberPath = "Title";
            cmbTypeAgent.SelectedItem = 0;

        }
        public AddAgentWindow(Agent agent)
        {
            InitializeComponent();
            txbTitle.Text = "Изменить агента";
            agentEdit = context.Agent.Find(agent.ID);
            isNewClient = false;
            txbName.Text = agent.Title;
            txbEmail.Text = agent.Email;
            txbPriotity.Text = agent.Priority.ToString();
            txbNameDir.Text = agent.DirectorName;
            txbAddress.Text = agent.Address;
            txbPhone.Text = agent.Phone;
            txbINN.Text = agent.INN;
            txbKPP.Text = agent.KPP;
            cmbTypeAgent.ItemsSource = context.AgentType.ToList();
            cmbTypeAgent.DisplayMemberPath = "Title";
            cmbTypeAgent.SelectedItem = agent.AgentType;
            var uriImageSource = new Uri("\\Resources" + agent.Logo.ToString(), UriKind.RelativeOrAbsolute);
            img.Source = new BitmapImage(uriImageSource);
        }

        private void cmbTypeAgent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            if (isNewClient)
                addAgent();
            else
                editAgent();
        }

        private void editAgent()
        {
            agentEdit.Address = txbAddress.Text;
            agentEdit.Email = txbEmail.Text;
            agentEdit.DirectorName = txbNameDir.Text;
            agentEdit.INN = txbINN.Text;
            agentEdit.KPP = txbKPP.Text;
            agentEdit.Phone = txbPhone.Text;
            agentEdit.Priority = Convert.ToInt32(txbPriotity.Text);
            agentEdit.Title = txbName.Text;
            agentEdit.AgentType = cmbTypeAgent.SelectedItem as AgentType;

            context.SaveChanges();
            this.Close();
        }

        private void addAgent()
        {
            Agent newAgent = new Agent();
            newAgent.Address = txbAddress.Text;
            newAgent.Email = txbEmail.Text;
            newAgent.DirectorName = txbNameDir.Text;
            newAgent.INN = txbINN.Text;
            newAgent.KPP = txbKPP.Text;
            newAgent.Phone = txbPhone.Text;
            newAgent.Priority = Convert.ToInt32(txbPriotity.Text);
            newAgent.Title = txbName.Text;
            newAgent.AgentType = cmbTypeAgent.SelectedItem as AgentType;
            context.Agent.Add(newAgent);
            context.SaveChanges();
            this.Close();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void imgClientAdd_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image files (*.BMP, *.JPG, *.GIF, *.TIF, *.PNG, *.ICO, *.EMF, *.WMF)|*.bmp;*.jpg;*.gif; *.tif; *.png; *.ico; *.emf; *.wmf";
            if (fileDialog.ShowDialog() == true)
            {
                string fileName = fileDialog.FileName;
                filePath = fileName;
                img.Source = new BitmapImage(new Uri(fileName));
            }
        }
    }
}