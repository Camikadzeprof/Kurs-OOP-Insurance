using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        public ClientWindow()
        {
            InitializeComponent();
        }
        public string Username { get; set; }
        private void ClientWindow_Loaded(object sender, RoutedEventArgs e)
        {
            bool p = IsUserAlreadyExist(Username);
            if (p)
            {
                var dbContext = new BaseDbContext();
                var unitOfWork = new UnitOfWork(dbContext);

                var client = unitOfWork.UserRepository.Entities
                        .FirstOrDefault(n => n.Username == Username);

                UsernameTextBox.Text = client.Username;
                NameTextBox.Text = client.Name;
                SurnameTextBox.Text = client.Surname;
                SWTextBox.Text = client.SecretWord;
            }
        }

        private void SaveClientbtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string pattern1 = @"[a-zA-Z1-9\-\._]+@[a-z1-9]+(.[a-z1-9]+){1,}";
                if (!Regex.IsMatch(UsernameTextBox.Text, pattern1) && UsernameTextBox.Text != "")
                {
                    MessageBox.Show("Некорректный email");
                }
                else
                {
                    bool k = IsUserAlreadyExist(Username);
                    if (k)
                    {
                        var dbContext = new BaseDbContext();
                        var unitOfWork = new UnitOfWork(dbContext);

                        var client = unitOfWork.UserRepository.Entities
                                .FirstOrDefault(n => n.Username == Username);

                        client.Username = UsernameTextBox.Text;
                        client.Name = NameTextBox.Text;
                        client.Surname = SurnameTextBox.Text;
                        client.SecretWord = SWTextBox.Text;
                        unitOfWork.Commit();
                    }
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка изменения пользователя\n" + ex.Message);
            }
        }

        private void CancelClientbtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool IsUserAlreadyExist(string username)
        {
            bool flag = false;

            var dbContext = new BaseDbContext();
            var unitOfWork = new UnitOfWork(dbContext);
            var contact = unitOfWork.UserRepository.Entities
                    .FirstOrDefault(n => (n.Username == username));

            if (contact != null)
                return flag = true;
            return flag;
        }
    }
}
