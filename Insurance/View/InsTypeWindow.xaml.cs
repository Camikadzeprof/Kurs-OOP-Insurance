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
    /// Логика взаимодействия для InsTypeWindow.xaml
    /// </summary>
    public partial class InsTypeWindow : Window
    {
        public InsTypeWindow()
        {
            InitializeComponent();
        }

        public string InsTyp { get; set; }

        private static BaseDbContext dbContext = new BaseDbContext();
        private UnitOfWork unitOfWork = new UnitOfWork(dbContext);

        private void InsTypeWindow_Loaded(object sender, RoutedEventArgs e)
        {
            bool flag = IsInsTypeAlreadyExist(InsTyp);
            if (flag)
            {
                var instype = unitOfWork.InsTypeRepository.Entities
                            .FirstOrDefault(n => n.Type == InsTyp);
                TypeTextBox.Text = instype.Type;
                FeeTextBox.Text = instype.Fee.ToString();
                MaxPayoutTextBox.Text = instype.MaxPayout.ToString();
            }
        }

        private void SaveInsTypebtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool flag = IsInsTypeAlreadyExist(InsTyp);
                if (flag)
                {

                    var type = unitOfWork.InsTypeRepository.Entities
                            .FirstOrDefault(n => n.Type == InsTyp);

                    type.Type = TypeTextBox.Text;
                    type.Fee = Convert.ToInt32(FeeTextBox.Text);
                    type.MaxPayout = Convert.ToInt32(MaxPayoutTextBox.Text);
                    unitOfWork.Commit();
                }
                else
                {
                    if (TypeTextBox.Text != "" && FeeTextBox.Text != "" && MaxPayoutTextBox.Text != "")
                    {

                        var newtype = new InsType();
                        newtype.Type = TypeTextBox.Text;
                        newtype.Fee = Convert.ToInt32(FeeTextBox.Text);
                        newtype.MaxPayout = Convert.ToInt32(MaxPayoutTextBox.Text);
                        unitOfWork.InsTypeRepository.Add(newtype);
                        unitOfWork.Commit();
                        MessageBox.Show("Добавлен новый тип страховки");
                    }
                    else MessageBox.Show("Все поля должны быть заполнены");
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка добавления нового контакта\n" + ex.Message);
            }
        }

        private void CancelInsTypebtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool IsInsTypeAlreadyExist(string instype)
        {
            bool flag = false;
            var reminder = unitOfWork.InsTypeRepository.Entities
                    .FirstOrDefault(n => n.Type == instype);

            if (reminder != null)
                return flag = true;
            return flag;
        }
    }
}
