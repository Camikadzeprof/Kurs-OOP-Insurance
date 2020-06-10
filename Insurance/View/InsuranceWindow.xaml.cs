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
    /// Логика взаимодействия для InsuranceWindow.xaml
    /// </summary>
    public partial class InsuranceWindow : Window
    {
        public InsuranceWindow()
        {
            InitializeComponent();
        }

        public int Ins { get; set; }
        public string Client { get; set; }

        private static BaseDbContext dbContext = new BaseDbContext();
        private UnitOfWork unitOfWork = new UnitOfWork(dbContext);

        private void InsuranceWindow_Loaded(object sender, RoutedEventArgs e)
        {
            bool flag = IsInsuranceAlreadyExist(Ins);
            if (flag)
            {
                var instype = unitOfWork.InsuranceRepository.Entities
                            .FirstOrDefault(n => n.Num == Ins);

                TypeTextBox.Text = instype.Type;
            }
        }

        private void SaveInsurancebtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool flag = IsInsuranceAlreadyExist(Ins);
                if (flag)
                {
                    var insurance = unitOfWork.InsuranceRepository.Entities
                            .FirstOrDefault(n => n.Num == Ins);
                    
                    insurance.Type = TypeTextBox.Text;
                    unitOfWork.Commit();
                }
                else if(TypeTextBox.Text != "")
                {
                    var ins = new Insurance();
                    ins.Type = TypeTextBox.Text;
                    ins.Username = Client;
                    unitOfWork.InsuranceRepository.Add(ins);
                    unitOfWork.Commit();
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка добавления новой страховки\n" + ex.StackTrace);
            }
        }

        private void CancelInsurancebtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool IsInsuranceAlreadyExist(int ins)
        {
            bool flag = false;
            var reminder = unitOfWork.InsuranceRepository.Entities
                    .FirstOrDefault(n => n.Num == ins);

            if (reminder != null)
                return flag = true;
            return flag;
        }
    }
}
