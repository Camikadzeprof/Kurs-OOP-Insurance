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

namespace InsuranceComp.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindowAgent.xaml
    /// </summary>
    public partial class MainWindowAgent : Window
    {
        public MainWindowAgent()
        {
            InitializeComponent();
        }
        public string Username { get; set; }
        public string name { get; set; }
        public string Surname { get; set; }
        public int Role { get; set; }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }

        private void NotVisible()
        {
            ClientsGrid.Visibility = Visibility.Collapsed;
            InsTypesGrid.Visibility = Visibility.Collapsed;
            InsuranceGrid.Visibility = Visibility.Collapsed;
            IncidentsGrid.Visibility = Visibility.Collapsed;
            PayoutsGrid.Visibility = Visibility.Collapsed;
        }

        private void Clients_Selected(object sender, RoutedEventArgs e)
        {
            NotVisible();
            ClientsGrid.Visibility = Visibility.Visible;
            UpdateClientsDG();
        }

        private void ClientsDG_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            UpdateClientsDG();
        }

        private void UpdateClient()
        {
            try
            {
                var dbContext = new BaseDbContext();
                UnitOfWork unitOfWork = new UnitOfWork(dbContext);

                int row = ClientsDG.SelectedIndex;

                if (row != -1)
                {
                    var ci = new DataGridCellInfo(ClientsDG.Items[row], ClientsDG.Columns[0]);
                    var crow = ci.Column.GetCellContent(ci.Item) as TextBlock;
                    var vrow = crow.Text;

                    ClientWindow clientsWindow = new ClientWindow();
                    clientsWindow.Username = vrow;
                    clientsWindow.ShowDialog();
                    UpdateClientsDG();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateClientsDG()
        {
            var dbContext = new BaseDbContext();
            var unitOfWork = new UnitOfWork(dbContext);

            var clients = unitOfWork.UserRepository.Entities
                .Where(n => n.Role == 1)
                .ToList();

            ClientsDG.ItemsSource = clients;

        }

        private void Insurances_Selected(object sender, RoutedEventArgs e)
        {
            NotVisible();
            InsuranceGrid.Visibility = Visibility.Visible;
            UpdateInsuranceDG();
        }

        private void InsuranceDG_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UpdateInsurance();
        }

        private void UpdInsBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateInsurance();
        }

        private void UpdateInsurance()
        {
            try
            {
                var dbContext = new BaseDbContext();
                UnitOfWork unitOfWork = new UnitOfWork(dbContext);

                int row = InsTypeDG.SelectedIndex;

                if (row != -1)
                {
                    var ci = new DataGridCellInfo(InsTypeDG.Items[row], InsTypeDG.Columns[0]);
                    var crow = ci.Column.GetCellContent(ci.Item) as TextBlock;
                    var vrow = crow.Text;

                    InsuranceWindow insuranceWindow = new InsuranceWindow();
                    insuranceWindow.Ins = Convert.ToInt32(vrow);
                    insuranceWindow.ShowDialog();
                    UpdateInsuranceDG();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DelInsBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dbContext = new BaseDbContext();
                UnitOfWork unitOfWork = new UnitOfWork(dbContext);

                int row = InsuranceDG.SelectedIndex;

                if (row != -1)
                {
                    var ci = new DataGridCellInfo(InsuranceDG.Items[row], InsuranceDG.Columns[0]);
                    var crow = ci.Column.GetCellContent(ci.Item) as TextBlock;
                    string vrow = crow.Text;

                    var insurance = unitOfWork.InsuranceRepository.Entities
                            .FirstOrDefault(p => p.Num == Convert.ToInt32(vrow));
                    unitOfWork.InsuranceRepository.Remove(insurance);
                    unitOfWork.Commit();
                    MessageBox.Show("Страховка успешно удалена");
                    UpdateInsuranceDG();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateInsuranceDG()
        {
            var dbContext = new BaseDbContext();
            var unitOfWork = new UnitOfWork(dbContext);
            var ins = unitOfWork.InsuranceRepository.Entities
                        .ToList();

            InsuranceDG.ItemsSource = ins;
        }

        private void InsTypes_Selected(object sender, RoutedEventArgs e)
        {
            NotVisible();
            InsTypesGrid.Visibility = Visibility.Visible;
            UpdateInsTypeDG();
        }

        private void InsTypeDG_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UpdateInsTypeDG();
        }

        private void UpdateInsTypeDG()
        {
            var dbContext = new BaseDbContext();
            var unitOfWork = new UnitOfWork(dbContext);
            var instypes = unitOfWork.InsTypeRepository.Entities
                        .ToList();

            InsTypeDG.ItemsSource = instypes;
        }

        private void Incidents_Selected(object sender, RoutedEventArgs e)
        {
            NotVisible();
            IncidentsGrid.Visibility = Visibility.Visible;
            UpdateIncidentDG();
        }

        private void IncidentsDG_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UpdateIncident();
        }

        private void UpdIncBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateIncident();
        }

        private void UpdateIncident()
        {
            try
            {
                var dbContext = new BaseDbContext();
                UnitOfWork unitOfWork = new UnitOfWork(dbContext);

                int row = IncidentsDG.SelectedIndex;

                if (row != -1)
                {
                    var ci = new DataGridCellInfo(IncidentsDG.Items[row], IncidentsDG.Columns[0]);
                    var crow = ci.Column.GetCellContent(ci.Item) as TextBlock;
                    var vrow = crow.Text;

                    IncidentWindowAgent incidentWindow = new IncidentWindowAgent();
                    incidentWindow.Inc = Convert.ToInt32(vrow);
                    incidentWindow.ShowDialog();
                    UpdateIncidentDG();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DelIncBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dbContext = new BaseDbContext();
                UnitOfWork unitOfWork = new UnitOfWork(dbContext);

                int row = IncidentsDG.SelectedIndex;

                if (row != -1)
                {
                    var ci = new DataGridCellInfo(IncidentsDG.Items[row], IncidentsDG.Columns[0]);
                    var crow = ci.Column.GetCellContent(ci.Item) as TextBlock;
                    string vrow = crow.Text;

                    var incident = unitOfWork.IncidentRepository.Entities
                            .FirstOrDefault(p => p.IdIncident == Convert.ToInt32(vrow));
                    unitOfWork.IncidentRepository.Remove(incident);
                    unitOfWork.Commit();
                    MessageBox.Show("Страховой случай удален");
                    UpdateIncidentDG();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateIncidentDG()
        {
            var dbContext = new BaseDbContext();
            var unitOfWork = new UnitOfWork(dbContext);
            var incidents = unitOfWork.IncidentRepository.Entities
                        .Where(n => n.Status == "На рассмотрении").ToList();

            IncidentsDG.ItemsSource = incidents;

        }

        private void Payouts_Selected(object sender, RoutedEventArgs e)
        {
            NotVisible();

            PayoutsGrid.Visibility = Visibility.Visible;

            UpdatePayoutDG();
        }

        private void PayoutsDG_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UpdatePayoutDG();
        }

        private void UpdatePayoutDG()
        {
            var dbContext = new BaseDbContext();
            var unitOfWork = new UnitOfWork(dbContext);
            var payouts = unitOfWork.PayoutRepository.Entities
                        .ToList();

            PayoutsDG.ItemsSource = payouts;
        }

        private void Search_Selected(object sender, RoutedEventArgs e)
        {

        }

        public void Exit_Selected(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
