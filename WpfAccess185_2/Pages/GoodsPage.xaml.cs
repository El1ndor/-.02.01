using System.Collections.Generic;
using System.Windows.Controls;
using System.Data.OleDb;
using System.Configuration;
using System.Windows;
using System.Linq;
using System;
using WpfAccess185_2.Models;
namespace WpfAccess185_2.Pages
{
    /// <summary>
    /// Логика взаимодействия для GoodsPage.xaml
    /// </summary>
    public partial class GoodsPage : Page
    {
        // строка подключения
        static string connectionString =
        ConfigurationManager.ConnectionStrings["AccessCS"].ConnectionString;
        //список товаров
        List<Good> goods = new List<Good>();
        //список категорий товаров
        List<Category> categories = new List<Category>();
        public GoodsPage()
        {
            InitializeComponent();
            // подгружаем данные в DataGrid
            LoadDataFromTable();
        }
        // загрузка данных из таблиц
        void LoadDataFromTable()
        {
            colCategories.ItemsSource = null;
            DgGood.ItemsSource = null;
            categories.Clear();
            goods.Clear();
            
        // формируем запрос на выборку данных о категориях
string sqlExpression = "SELECT * FROM Category";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                // открываем подключение
                connection.Open();
                OleDbCommand command = new OleDbCommand(sqlExpression, connection);
                // результаты запроса помещаем в reader
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.HasRows) // если есть данные
                {
                    //каждая строка представляем собой строку из таблицы Category
                    // в таблице Category два поля
                    //IdCategory - целое число, CategoryName - строка
                    while (reader.Read()) // построчно считываем данные
                    {
                        Category u = new Category(reader.GetInt32(0),
                        reader.GetString(1)
                        );
                        categories.Add(u);
                    }
                }
                reader.Close();
            }
            // Для ComboBox указываем в качестве
            // источника данных список категорий categories
            colCategories.ItemsSource = categories;
            // формируем запрос на выборку данных о товарах
            sqlExpression = "SELECT * FROM Good";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                // открываем подключение
                connection.Open();
                OleDbCommand command = new OleDbCommand(sqlExpression, connection);
                // результаты запроса помещаем в reader
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.HasRows) // если есть данные
                {
                    //каждая строка представляем собой строку из таблицы Good
                    // в таблице Good шесть полей
                    //int GoodId, string GoodName
                    //double Price, string Picture
                    //string Description, int CategoryId
                    while (reader.Read()) // построчно считываем данные
                    {
                        Good u = new Good(reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetInt32(2),
                        reader.GetString(3),
                        reader.GetString(4),
                        reader.GetInt32(5));
                        goods.Add(u);
                    }
                }
                reader.Close();
            }
            // Для DataGrid указываем в качестве
            // источника данных список товаров goods
            DgGood.ItemsSource = goods;
        } 
// кнопка добавить
private void BtnAddClick(object sender, System.Windows.RoutedEventArgs e)
        {
            // открытие AddGoodPage для добавления новой записи
            Manager.MainFrame.Navigate(new AddGoodPage(null));
        }
        // кнопка редактировать
        private void BtnEditClick(object sender, System.Windows.RoutedEventArgs e)
        {
            // открытие редактирования товара
            // передача выбранного товара в AddGoodPage
            Manager.MainFrame.Navigate(
            new AddGoodPage((sender as Button).DataContext as Good));
        }
        //событие отображения данного Page
        private void PageIsVisibleChanged(object sender,
        System.Windows.DependencyPropertyChangedEventArgs e)
        {
            // обновляем данные каждый раз когда активируется этот Page
            if (Visibility == Visibility.Visible)
            {
                LoadDataFromTable();
            }
        }
        // кнопка удаления
        private void BtnDeleteClick(object sender, System.Windows.RoutedEventArgs e)
        {
            //если зв DataGride не выбрана строка,
            //ничего не удаляем
            if (DgGood.SelectedIndex == -1) return;
            // получаем выбранный товар
            var selectedGoods = (DgGood.SelectedItem) as Good;
            // вывод сообщения с вопросом Удалить запись?
            MessageBoxResult messageBoxResult = MessageBox.Show($"Удалить 1 запись???",
            "Удаление", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            //если пользователь нажал ОК пытаемся удалить запись
            if (messageBoxResult == MessageBoxResult.OK)
            {
                try
                {
                    using (OleDbConnection connection = new OleDbConnection(connectionString))
                    {
                        // открываем соединение
                        connection.Open();
                        //формируем запрос на удаление
                        OleDbCommand commandDelete = new OleDbCommand("Delete From Good" +
                        " where GoodId = @ID", connection);
                        commandDelete.Parameters.AddWithValue("@ID", selectedGoods.GoodId);
                        // выпполняем запрос на удаление
                        commandDelete.ExecuteNonQuery();
                    }
                    MessageBox.Show("Запись удалена");
                    //обновляем данный в DataGrid
                    LoadDataFromTable();
                }
                catch
                {
                    MessageBox.Show("Ошибка удаления, есть связанные записи", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        } 
// отображение номеров строк в DataGrid
private void DgGoodLoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
    }
}
