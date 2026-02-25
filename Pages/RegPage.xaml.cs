using Restaurant.DB;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Restaurant.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {
        public RegPage()
        {
            InitializeComponent();
        }
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string name = NameBox.Text.Trim();
            string phone = PhoneBox.Text.Trim();
            string password = PasswordBox.Password;
            string confirm = ConfirmPasswordBox.Password;

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(phone) ||
                string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirm))
            {
                ErrorText.Text = "Все поля обязательны для заполнения!";
                return;
            }

            if (password != confirm)
            {
                ErrorText.Text = "Пароли не совпадают!";
                return;
            }

            if (!IsPasswordValid(password))
            {
                ErrorText.Text = "Пароль должен содержать хотя бы одну цифру, одну букву и один из символов: ! @ # ? $";
                return;
            }

            var existingUser = DBClass.connect.Users.FirstOrDefault(u => u.Phone == phone);
            if (existingUser != null)
            {
                ErrorText.Text = "Пользователь с таким телефоном уже существует!";
                return;
            }

            int newId = 1;
            if (DBClass.connect.Users.Any())
                newId = DBClass.connect.Users.Max(u => u.UserId) + 1;

            var newUser = new Users
            {
                UserId = newId,
                UserName = name,
                Phone = phone,
                Password = password,        
                RoleId = 2                     
            };

            try
            {
                DBClass.connect.Users.Add(newUser);
                DBClass.connect.SaveChanges();

                MessageBox.Show("Регистрация прошла успешно! Теперь вы можете войти.");
                NavigationService.Navigate(new AvtoPage());
            }
            catch (Exception ex)
            {
                ErrorText.Text = "Ошибка при сохранении: " + ex.Message;
            }
        }

        private bool IsPasswordValid(string password)
        {
            bool hasDigit = Regex.IsMatch(password, @"\d");
            bool hasLetter = Regex.IsMatch(password, @"[a-zA-Zа-яА-Я]");
            bool hasSpecial = Regex.IsMatch(password, @"[!@#?$]");

            return hasDigit && hasLetter && hasSpecial;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AvtoPage());
        }
    }
}
