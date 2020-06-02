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
    /// Логика взаимодействия для RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dbContext = new BaseDbContext();
                var unitOfWork = new UnitOfWork(dbContext);
                if (NameTextBox.Text != "" && RegisterLoginTextBox.Text != "" && RegisterFloatingPasswordBox1.Password != "" && RegisterFloatingPasswordBox2.Password != "" && RegisterSecretWordTextBox.Text != "")
                {
                    if (RegisterFloatingPasswordBox1.Password == RegisterFloatingPasswordBox2.Password)
                    {
                        var userRepeate = unitOfWork.UserRepository.Entities
                                    .FirstOrDefault(b => b.Username == RegisterLoginTextBox.Text);
                        var secrwRepeate = unitOfWork.UserRepository.Entities
                                    .FirstOrDefault(n => n.SecretWord == RegisterSecretWordTextBox.Text);
                        if (userRepeate == null)
                        {
                            if (secrwRepeate == null)
                            {
                                User user = new User();
                                user.Name = NameTextBox.Text;
                                user.Surname = SurnameTextBox.Text;
                                user.Username = RegisterLoginTextBox.Text;
                                user.Password = RegisterFloatingPasswordBox1.Password;
                                user.SecretWord = RegisterSecretWordTextBox.Text;
                                user.Role = 1;

                                unitOfWork.UserRepository.Add(user);
                                unitOfWork.Commit();

                                MessageBox.Show("Регистрация прошла успешно");

                                LoginWindow loginWindow = new LoginWindow();
                                loginWindow.Show();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Придумайте другое секретное слово!", "Новое секретное слово", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Пользователь с таким логином уже существует!\nИспользуйте другой логин.", "Повторение логина", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пароли в полях должны совпадать");
                    }
                }
                else
                {
                    MessageBox.Show("Все поля должны быть заполнены", "Пустые поля", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }
    }
}
