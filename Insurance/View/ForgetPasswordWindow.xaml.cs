using System;
using System.Collections.Generic;
using System.Security.Cryptography;
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
    /// Логика взаимодействия для ForgetPasswordWindow.xaml
    /// </summary>
    public partial class ForgetPasswordWindow : Window
    {
        public ForgetPasswordWindow()
        {
            InitializeComponent();
        }

        private static BaseDbContext dbContext = new BaseDbContext();
        private UnitOfWork unitOfWork = new UnitOfWork(dbContext);

        private void ForgetSW_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Обратитесь к администратору");
        }

        private void ForgetPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ForgetPasswordLoginTextBox.Text != "" && ForgetPasswordFloatingPasswordBox1.Password != "" && ForgetPasswordSecretWordTextBox.Text != "")
                {
                    if (ForgetPasswordFloatingPasswordBox1.Password.ToString() == ForgetPasswordFloatingPasswordBox2.Password.ToString())
                    {

                        var user = unitOfWork.UserRepository.Entities
                            .FirstOrDefault(n => (n.Username == ForgetPasswordLoginTextBox.Text) && (n.SecretWord == ForgetPasswordSecretWordTextBox.Text));

                        if (user != null)
                        {
                            user.Password = ForgetPasswordFloatingPasswordBox1.Password;
                            unitOfWork.Commit();
                            MessageBox.Show("Пароль обновлен успешно");

                            LoginWindow loginWindow = new LoginWindow();
                            loginWindow.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Секретное слово или логин были введены неверно");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Поля паролей не совпадают");
                    }
                }
                else
                {
                    MessageBox.Show("Все поля должны быть заполнены", "Пустые поля", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
