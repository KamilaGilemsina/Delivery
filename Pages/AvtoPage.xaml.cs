using Restaurant.DB;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Restaurant.Pages
{
    /// <summary>
    /// Логика взаимодействия для AvtoPage.xaml
    /// </summary>
    public partial class AvtoPage : Page
    {
        public AvtoPage()
        {
            InitializeComponent();
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string phone = PhoneBox.Text.Trim();
            string password = PasswordBox.Password;

            if (string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(password))
            {
                ErrorText.Text = "Заполните все поля!";
                return;
            }

            var user = DBClass.connect.Users
                .FirstOrDefault(u => u.Phone == phone && u.Password == password);

            if (user == null)
            {
                ErrorText.Text = "Неверный телефон или пароль!";
                return;
            }

            MessageBox.Show($"Добро пожаловать, {user.UserName}!");
            //NavigationService.Navigate(new MainPage(user));
        }

        private void ToRegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegPage());
        }

        private void LogGuestButton_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new MainPage());
        }
    }
}
