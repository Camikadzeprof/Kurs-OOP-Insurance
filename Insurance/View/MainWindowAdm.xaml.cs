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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindowAdm : Window
    {
        public MainWindowAdm()
        {
            InitializeComponent();
        }

        public virtual User user { get; set; }

        private static BaseDbContext dbContext = new BaseDbContext();
        private UnitOfWork unitOfWork = new UnitOfWork(dbContext);

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

        private void Personel_Selected(object sender, RoutedEventArgs e)
        {
            NotVisible();

            PersonelGrid.Visibility = Visibility.Visible;

            UpdatePersonelDG();
        }

        private void PersonelDG_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            UpdatePersonel();
        }

        private void UpdatePersonelBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdatePersonel();
        }

        private void UpdatePersonel()
        {
            try
            {
                int row = PersonelDG.SelectedIndex;
                if (row != -1)
                {
                    var ci = new DataGridCellInfo(PersonelDG.Items[row], PersonelDG.Columns[0]);
                    var crow = ci.Column.GetCellContent(ci.Item) as TextBlock;
                    var vrow = crow.Text;

                    ClientWindow personelWindow = new ClientWindow();
                    personelWindow.Username = vrow;
                    personelWindow.ShowDialog();
                    UpdatePersonelDG();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdatePersonelDG()
        {

            var notes = unitOfWork.UserRepository.Entities
                        .Where(n => n.Role == 2).ToList();

            PersonelDG.ItemsSource = notes;
        }

        private void NotVisible()
        {
            PersonelGrid.Visibility = Visibility.Collapsed;
            ClientsGrid.Visibility = Visibility.Collapsed;
            InsTypesGrid.Visibility = Visibility.Collapsed;
            InsuranceGrid.Visibility = Visibility.Collapsed;
            IncidentsGrid.Visibility = Visibility.Collapsed;
            PayoutsGrid.Visibility = Visibility.Collapsed;
            SearchGrid.Visibility = Visibility.Collapsed;
            SearchAgentsGrid.Visibility = Visibility.Collapsed;
            SearchClientsGrid.Visibility = Visibility.Collapsed;
            SearchInsTypesGrid.Visibility = Visibility.Collapsed;
            SearchInsGrid.Visibility = Visibility.Collapsed;
            SearchIncGrid.Visibility = Visibility.Collapsed;
            SearchPayoutGrid.Visibility = Visibility.Collapsed;
        }

        private void Clients_Selected(object sender, RoutedEventArgs e)
        {
            NotVisible();
            ClientsGrid.Visibility = Visibility.Visible;
            UpdateClientsDG();
        }

        private void UpdClientBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateClient();
        }

        private void ClientsDG_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            UpdateClient();
        }

        private void UpdateClient()
        {
            try
            {
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
                int row = InsuranceDG.SelectedIndex;

                if (row != -1)
                {
                    var ci = new DataGridCellInfo(InsuranceDG.Items[row], InsuranceDG.Columns[0]);
                    var crow = ci.Column.GetCellContent(ci.Item) as TextBlock;
                    int vrow = Convert.ToInt32(crow.Text);

                    var insurance = unitOfWork.InsuranceRepository.Entities
                            .FirstOrDefault(p => p.Num == vrow);
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
            UpdateInsTypes();
        }

        private void AddTypeBtn_Click(object sender, RoutedEventArgs e)
        {
            InsTypeWindow instypeWindow = new InsTypeWindow();
            instypeWindow.InsTyp = "";
            instypeWindow.ShowDialog();
            UpdateInsTypeDG();
        }

        private void UpdTypeBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateInsTypes();
        }

        private void UpdateInsTypes()
        {
            try
            {
                int row = InsTypeDG.SelectedIndex;

                if (row != -1)
                {
                    var ci = new DataGridCellInfo(InsTypeDG.Items[row], InsTypeDG.Columns[0]);
                    var crow = ci.Column.GetCellContent(ci.Item) as TextBlock;
                    var vrow = crow.Text;

                    InsTypeWindow instypeWindow = new InsTypeWindow();
                    instypeWindow.InsTyp = vrow;
                    instypeWindow.ShowDialog();
                    UpdateInsTypeDG();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DelTypeBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int row = InsTypeDG.SelectedIndex;

                if (row != -1)
                {
                    var ci = new DataGridCellInfo(InsTypeDG.Items[row], InsTypeDG.Columns[0]);
                    var crow = ci.Column.GetCellContent(ci.Item) as TextBlock;
                    string vrow = crow.Text;

                    var instype = unitOfWork.InsTypeRepository.Entities
                            .FirstOrDefault(p => p.Type == vrow);
                    unitOfWork.InsTypeRepository.Remove(instype);
                    unitOfWork.Commit();
                    MessageBox.Show("Тип страховки успешно удален");
                    UpdateInsTypeDG();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateInsTypeDG()
        {
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
                int row = IncidentsDG.SelectedIndex;

                if (row != -1)
                {
                    var ci = new DataGridCellInfo(IncidentsDG.Items[row], IncidentsDG.Columns[0]);
                    var crow = ci.Column.GetCellContent(ci.Item) as TextBlock;
                    int vrow = Convert.ToInt32(crow.Text);

                    var incident = unitOfWork.IncidentRepository.Entities
                            .FirstOrDefault(p => p.IdIncident == vrow);
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
            var incidents = unitOfWork.IncidentRepository.Entities
                        .ToList();

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
            var payouts = unitOfWork.PayoutRepository.Entities
                        .ToList();

            PayoutsDG.ItemsSource = payouts;
        }

        private void Search_Selected(object sender, RoutedEventArgs e)
        {
            NotVisible();
            SearchGrid.Visibility = Visibility.Visible;
        }

        private void AgentBtn_Click(object sender, RoutedEventArgs e)
        {
            NotVisible();
            SearchClientsGrid.Visibility = Visibility.Visible;
        }

        private void AgSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            NotVisible();
            PersonelGrid.Visibility = Visibility.Visible;
            if (TypeAgSearch.Text != "")
            {
                if (AgParamTextBox.Text != "")
                {
                    if (TypeAgSearch.Text == "По имени")
                    {
                        var agents = unitOfWork.UserRepository.Entities.Where(n => n.Role == 2 && n.Name == ClParamTextBox.Text).ToList();
                        PersonelDG.ItemsSource = agents;
                    }
                    if (TypeClSearch.Text == "По фамилии")
                    {
                        var agents = unitOfWork.UserRepository.Entities.Where(n => n.Role == 2 && n.Surname == ClParamTextBox.Text).ToList();
                        PersonelDG.ItemsSource = agents;
                    }
                    if (TypeClSearch.Text == "По логину")
                    {
                        var agents = unitOfWork.UserRepository.Entities.Where(n => n.Role == 2 && n.Username == ClParamTextBox.Text).ToList();
                        PersonelDG.ItemsSource = agents;
                    }
                }
                else MessageBox.Show("Введите значение параметра поиска");
            }
            else MessageBox.Show("Выберите параметр поиска");
            AgParamTextBox.Clear();
        }

        private void ClientBtn_Click(object sender, RoutedEventArgs e)
        {
            NotVisible();
            SearchClientsGrid.Visibility = Visibility.Visible;
        }

        private void ClSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            NotVisible();
            ClientsGrid.Visibility = Visibility.Visible;
            if (TypeClSearch.Text != "")
            {
                if (ClParamTextBox.Text != "")
                {
                    if (TypeClSearch.Text == "По имени")
                    {
                        var clients = unitOfWork.UserRepository.Entities.Where(n => n.Role == 1 && n.Name == ClParamTextBox.Text).ToList();
                        ClientsDG.ItemsSource = clients;
                    }
                    if (TypeClSearch.Text == "По фамилии")
                    {
                        var clients = unitOfWork.UserRepository.Entities.Where(n => n.Role == 1 && n.Surname == ClParamTextBox.Text).ToList();
                        ClientsDG.ItemsSource = clients;
                    }
                    if (TypeClSearch.Text == "По логину")
                    {
                        var clients = unitOfWork.UserRepository.Entities.Where(n => n.Role == 1 && n.Username == ClParamTextBox.Text).ToList();
                        ClientsDG.ItemsSource = clients;
                    }
                }
                else MessageBox.Show("Введите значение параметра поиска");
            }
            else MessageBox.Show("Выберите параметр поиска");
            ClParamTextBox.Clear();
        }

        private void InsBtn_Click(object sender, RoutedEventArgs e)
        {
            NotVisible();
            SearchInsGrid.Visibility = Visibility.Visible;
        }

        private void InsSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            NotVisible();
            InsuranceGrid.Visibility = Visibility.Visible;
            if (TypeInsSearch.Text != "")
            {
                if (InsParamTextBox.Text != "")
                {
                    if (TypeInsSearch.Text == "По типу")
                    {
                        var inst = unitOfWork.InsuranceRepository.Entities.Where(n => n.Type == InsParamTextBox.Text).ToList();
                        InsuranceDG.ItemsSource = inst;
                    }
                    if (TypeInsSearch.Text == "По логину клиента")
                    {
                        var inst = unitOfWork.InsuranceRepository.Entities.Where(n => n.Username == InsParamTextBox.Text).ToList();
                        InsuranceDG.ItemsSource = inst;
                    }
                }
                else MessageBox.Show("Введите значение параметрa поиска");
            }
            else MessageBox.Show("Выберите параметр поиска");
            InsParamTextBox.Clear();
        }

        private void InsTypeBtn_Click(object sender, RoutedEventArgs e)
        {
            NotVisible();
            SearchInsTypesGrid.Visibility = Visibility.Visible;
        }

        private void ItSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            NotVisible();
            InsTypesGrid.Visibility = Visibility.Visible;
            if (TypeInsTypeSearch.Text != "")
            {
                if (InsTypesParamTextBox.Text != "")
                {
                    if (TypeInsTypeSearch.Text == "По типу")
                    {
                        var inst = unitOfWork.InsTypeRepository.Entities.Where(n => n.Type == InsTypesParamTextBox.Text).ToList();
                        InsTypeDG.ItemsSource = inst;
                    }
                    if (TypeInsTypeSearch.Text == "По взносу")
                    {
                        int fee = Convert.ToInt32(InsTypesParamTextBox.Text);
                        var inst = unitOfWork.InsTypeRepository.Entities.Where(n => n.Fee == fee).ToList();
                        InsTypeDG.ItemsSource = inst;
                    }
                    if (TypeInsTypeSearch.Text == "По максимальной выплате")
                    {
                        int payout = Convert.ToInt32(InsTypesParamTextBox.Text);
                        var inst = unitOfWork.InsTypeRepository.Entities.Where(n => n.MaxPayout == payout).ToList();
                        InsTypeDG.ItemsSource = inst;
                    }
                }
                else MessageBox.Show("Введите значение параметрa поиска");
            }
            else MessageBox.Show("Выберите параметр поиска");
            InsTypesParamTextBox.Clear();
        }

        private void InсBtn_Click(object sender, RoutedEventArgs e)
        {
            NotVisible();
            SearchIncGrid.Visibility = Visibility.Visible;
        }

        private void IncSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            NotVisible();
            IncidentsGrid.Visibility = Visibility.Visible;
            if (TypeIncSearch.Text != "")
            {
                if (IncParamTextBox.Text != "")
                {
                    if (TypeIncSearch.Text == "По номеру страховки")
                    {
                        int num = Convert.ToInt32(IncParamTextBox.Text);
                        var inc = unitOfWork.IncidentRepository.Entities.Where(n => n.Num == num).ToList();
                        IncidentsDG.ItemsSource = inc;
                    }
                    if (TypeIncSearch.Text == "По логину клиента")
                    {
                        var inc = unitOfWork.IncidentRepository.Entities.Where(n => n.Ins.Username == IncParamTextBox.Text).ToList();
                        IncidentsDG.ItemsSource = inc;
                    }
                }
                else MessageBox.Show("Введите значение параметрa поиска");
            }
            else MessageBox.Show("Выберите параметр поиска");
            IncParamTextBox.Clear();
        }

        private void PayoutBtn_Click(object sender, RoutedEventArgs e)
        {
            NotVisible();
            SearchPayoutGrid.Visibility = Visibility.Visible;
        }

        private void PayoutSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            NotVisible();
            PayoutsGrid.Visibility = Visibility.Visible;
            if (TypePayoutSearch.Text != "")
            {
                if (PayoutParamTextBox.Text != "")
                {
                    if (TypePayoutSearch.Text == "Больше указанного значения")
                    {
                        int sum = Convert.ToInt32(PayoutParamTextBox.Text);
                        var pay = unitOfWork.PayoutRepository.Entities.Where(n => n.Sum > sum).ToList();
                        PayoutsDG.ItemsSource = pay;
                    }
                    if (TypePayoutSearch.Text == "Меньше указанного значения")
                    {
                        int sum = Convert.ToInt32(PayoutParamTextBox.Text);
                        var pay = unitOfWork.PayoutRepository.Entities.Where(n => n.Sum < sum).ToList();
                        PayoutsDG.ItemsSource = pay;
                    }
                    if (TypePayoutSearch.Text == "Равно указанному значению")
                    {
                        int sum = Convert.ToInt32(PayoutParamTextBox.Text);
                        var pay = unitOfWork.PayoutRepository.Entities.Where(n => n.Sum == sum).ToList();
                        PayoutsDG.ItemsSource = pay;
                    }
                }
                else MessageBox.Show("Введите значение параметрa поиска");
            }
            else MessageBox.Show("Выберите параметр поиска");
            PayoutParamTextBox.Clear();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            NotVisible();
            SearchGrid.Visibility = Visibility.Visible;
        }

        public void Exit_Selected(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
