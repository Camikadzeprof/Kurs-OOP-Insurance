using System;
using Microsoft.Win32;
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
    /// Логика взаимодействия для IncidentWindow.xaml
    /// </summary>
    public partial class IncidentWindowClient : Window
    {
        public int UserRole { get; set; }
        public int Inc { get; set; }

        public IncidentWindowClient()
        {
            InitializeComponent();
        }

        private void IncidentWindowClient_Loaded(object sender, RoutedEventArgs e)
        {
            var dbContext = new BaseDbContext();
            var unitOfWork = new UnitOfWork(dbContext);
            var insnums = unitOfWork.InsuranceRepository.Entities
                .ToList();
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
                    if (inc.Status != "Отказано" || inc.Status != "Одобрено")
                    {
                        inc.Num = Convert.ToInt32(NumTextBox.Text);
                        inc.Explain = ExplainTextBox.Text;
                        if (imgLocation == "")
                        {
                            inc.File = "/Images/noimagefound.jpg";
                        }
                        else
                        {
                            inc.File = imgLocation;
                        }
                        unitOfWork.Commit();
                    }
                    else
                    {
                        MessageBox.Show("Изменение недоступно");
                        this.Close();
                    }
                }
                else
                {
                    var dbContext = new BaseDbContext();
                    var unitOfWork = new UnitOfWork(dbContext);

                    var inc = new Incident();
                    inc.Num = Convert.ToInt32(NumTextBox.Text);
                    inc.Explain = ExplainTextBox.Text;
                    inc.Status = "На рассмотрении";
                    if (imgLocation == "")
                    {
                        inc.File = "/Images/noimagefound.jpg";
                    }
                    else
                    {
                        inc.File = imgLocation;
                    }

                    unitOfWork.IncidentRepository.Add(inc);
                    unitOfWork.Commit();
                    MessageBox.Show("Добавлен новый случай");
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка добавления нового случая\n" + ex.StackTrace);
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        string imgLocation = "";
        BitmapImage bitmap1;
        private void photoBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                bitmap1 = new BitmapImage(new Uri(op.FileName));
                Photo.Source = bitmap1;
                imgLocation = op.FileName.ToString();
            }
        }

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
