using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Data.OleDb;
using System.Configuration;
using WpfAccess185_2.Models;
using System.Linq;
using System.IO;
using SolrNet.Utils;

namespace WpfAccess185_2.Pages
{
    /// <summary> 
    /// Логика взаимодействия для SellGoodsPage.xaml 
    /// </summary> 
    public partial class SellGoodsPage : Page
    {
        // строка подключения из файла app.config
        static string connectionString = ConfigurationManager.ConnectionStrings["AccessCS"].ConnectionString;

        public SellGoodsPage()
        {
            InitializeComponent();
            ComboGoods.ItemsSource = GetGoods();
            if (ComboGoods.Items.Count > 0) ComboGoods.SelectedIndex = 0;
        }

        // фильтрация продаж по товару 
        private void ComboGoodsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // если в ComboBox выбрана строка
            if (ComboGoods.SelectedIndex >= 0)
            {
                // получаем GoodID выбранного товара 
                int goodId = Convert.ToInt32(ComboGoods.SelectedValue);

                // используя linq фильтруем данные списка продаж по товару 
                // сортируем по дате продажи
                var x = GetSells().Where(p => p.GoodId == goodId).OrderBy(p => p.DateSell).ToList();

                // в качестве источника данных для DataGrid ставим отфильтрованный список 
                DataGridSells.ItemsSource = x;

                // отображаем выбранный товар 
                GridGood.DataContext = ComboGoods.SelectedItem;
            }
        }

        /// <summary> 
        /// Возвращает список продаж 
        /// </summary> 
        /// <returns>List of Sell</returns> 
        List<Sell> GetSells()
        {
            // список продаж 
            List<Sell> sells = new List<Sell>();

            // формируем запрос на выборку данных о продажах 
            string sqlExpression = "SELECT * FROM Sell ORDER BY DateSell";

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    // открываем подключение
                    connection.Open();
                    OleDbCommand command = new OleDbCommand(sqlExpression, connection);

                    // результаты запроса помещаем в reader 
                    OleDbDataReader reader = command.ExecuteReader();

                    if (reader.HasRows) // если есть данные 
                    {
                        Console.WriteLine("Данные из таблицы Sell получены."); // Для отладки

                        // каждая строка представляем собой строку из таблицы Sell 
                        // в таблице Sell четыре поля 
                        // int SellId, int GoodId, DateTime DateSell, int SellCount
                        while (reader.Read()) // построчно считываем данные 
                        {
                            if (!reader.IsDBNull(0) && !reader.IsDBNull(1) && !reader.IsDBNull(2) && !reader.IsDBNull(3))
                            {
                                Sell u = new Sell(
                                    reader.GetInt32(0),
                                    reader.GetInt32(1),
                                    reader.GetDateTime(2),
                                    reader.GetInt32(3)
                                );
                                sells.Add(u);
                            }
                            else
                            {
                                // Логика для обработки отсутствующих значений, если это необходимо
                                Console.WriteLine("Отсутствуют значения в некоторых столбцах."); // Для отладки
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Нет данных в таблице Sell."); // Для отладки
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                // Логирование или отображение ошибки
                Console.WriteLine("Ошибка при выполнении запроса: " + ex.Message);
            }

            // возвращает список продаж
            return sells;
        }

        /// <summary> 
        /// Возвращает список товаров 
        /// </summary> 
        /// <returns>List of Good</returns> 
        List<Good> GetGoods()
        {
            // список товаров 
            List<Good> goods = new List<Good>();

            // формируем запрос на выборку данных о товарах 
            string sqlExpression = "SELECT * FROM Good ORDER BY GoodName";

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    // открываем подключение
                    connection.Open();
                    OleDbCommand command = new OleDbCommand(sqlExpression, connection);

                    // результаты запроса помещаем в reader 
                    OleDbDataReader reader = command.ExecuteReader();

                    if (reader.HasRows) // если есть данные 
                    {
                        Console.WriteLine("Данные из таблицы Good получены."); // Для отладки

                        // каждая строка представляем собой строку из таблицы Good 
                        // в таблице Good шесть полей 
                        // int GoodId, string GoodName, double Price, string Picture 
                        // string Description, int CategoryId 
                        while (reader.Read()) // построчно считываем данные 
                        {
                            Good u = new Good(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetInt32(2),
                                reader.GetString(3),
                                reader.GetString(4),
                                reader.GetInt32(5)
                            );
                            goods.Add(u);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Нет данных в таблице Good."); // Для отладки
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                // Логирование или отображение ошибки
                Console.WriteLine("Ошибка при выполнении запроса: " + ex.Message);
            }

            // возвращает список товаров
            return goods;
        }
    }
}
