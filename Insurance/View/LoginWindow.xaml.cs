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
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private static BaseDbContext dbContext = new BaseDbContext();
        private UnitOfWork unitOfWork = new UnitOfWork(dbContext);

        private void LoginEnterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var user = unitOfWork.UserRepository.Entities
                    .FirstOrDefault(n => (n.Username == LoginTextBox.Text) && (n.Password == LoginFloatingPasswordBox.Password));

                if (user != null)
                {
                    if (user.Role == 3)
                    {
                        MainWindowAdm mainWindow = new MainWindowAdm();
                        mainWindow.user = user;
                        mainWindow.Show();
                        this.Close();
                    }
                    else if(user.Role==2)
                    {
                        MainWindowAgent mainWindow = new MainWindowAgent();
                        mainWindow.user = user;
                        mainWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        MainWindowClient mainWindow = new MainWindowClient();
                        mainWindow.user = user;
                        mainWindow.Show();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Неправильно набран логин или пароль");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoginRegisterButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
            this.Close();
        }

        private void ForgetPassword_Button_Click(object sender, RoutedEventArgs e)
        {
            ForgetPasswordWindow forgetPasswordWindow = new ForgetPasswordWindow();
            forgetPasswordWindow.Show();
        }
    }
}
