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
    /// Логика взаимодействия для IncidentWindowAgent.xaml
    /// </summary>
    public partial class IncidentWindowAgent : Window
    {
        public int Inc { get; set; }
        public IncidentWindowAgent()
        {
            InitializeComponent();
        }

        private void IncidentWindowAgent_Loaded(object sender, RoutedEventArgs e)
        {
            bool flag = IsIncidentAlreadyExist(Inc);
            if (flag)
            {
                var dbContext = new BaseDbContext();
                var unitOfWork = new UnitOfWork(dbContext);
                var inc = unitOfWork.IncidentRepository.Entities
                            .FirstOrDefault(n => n.IdIncident == Inc);

                ExplainTextBlock.Text = inc.Explain;
                NumTextBlock.Text = inc.Num.ToString();
                bitmap1 = new BitmapImage(new Uri(inc.File));
                Photo.Source = bitmap1;
            }
        }

        private void SaveIncbtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool flag = IsIncidentAlreadyExist(Inc);
                if (flag)
                {
                    var dbContext = new BaseDbContext();
                    var unitOfWork = new UnitOfWork(dbContext);

                    var inc = unitOfWork.IncidentRepository.Entities
                            .FirstOrDefault(n => n.IdIncident == Inc);

                    inc.Status = StatusComboBox.Text;
                    unitOfWork.Commit();
                    if (inc.Status == "Одобрено")
                    {
                        PayoutWindow payoutWindow = new PayoutWindow();
                        payoutWindow.IdInc = Inc;
                        payoutWindow.ShowDialog();
                    }
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при изменении записи\n" + ex);
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        BitmapImage bitmap1;

        private bool IsIncidentAlreadyExist(int inc)
        {
            bool flag = false;
            var dbContext = new BaseDbContext();
            var unitOfWork = new UnitOfWork(dbContext);
            var reminder = unitOfWork.IncidentRepository.Entities
                    .FirstOrDefault(n => n.IdIncident == inc);

            if (reminder != null)
                return flag = true;
            return flag;
        }
    }
}
