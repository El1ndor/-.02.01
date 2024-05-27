using System.Collections.Generic;
using System.Windows;
using System.Data.OleDb;
using System.Configuration;
using WpfAccess185_2.Models;
using System.Linq;
using System;

namespace WpfAccess185_2
{
    public partial class LoginWindow : Window
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["AccessCS"].ConnectionString;
        List<User> users = new List<User>();

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void BtnOkClick(object sender, RoutedEventArgs e)
        {
            string username = TbLogin.Text;
            string password = TbPass.Password;
            string sqlExpression = "SELECT * FROM [User]";

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand(sqlExpression, connection);
                    OleDbDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            User u = new User(reader.GetValue(0).ToString(), reader.GetValue(1).ToString(), reader.GetValue(2).ToString());
                            users.Add(u);
                        }
                    }
                    reader.Close();
                }

                User x = users.FirstOrDefault(p => p.Username == username && p.Password == password);

                if (x != null)
                {
                    MainWindow secondWindow = new MainWindow(x.UserRole);
                    secondWindow.Owner = this;
                    this.Hide();
                    secondWindow.Show();
                }
                else
                {
                    MessageBox.Show("Не верный логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnCancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult x = MessageBox.Show("Вы действительно хотите закрыть приложение?", "Выйти", MessageBoxButton.OKCancel, MessageBoxImage.Question);

            if (x == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
        }
    }
}
