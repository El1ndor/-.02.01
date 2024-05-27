using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfAccess185_2.Models;

namespace WpfAccess185_2.Pages
{
    public partial class CatalogOfGoodsPage : Page
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["AccessCS"].ConnectionString;

        struct BuyItem
        {
            public int Count { get; set; }
            public double Total { get; set; }
        }

        Dictionary<Good, BuyItem> buyGoods = new Dictionary<Good, BuyItem>();

        public CatalogOfGoodsPage()
        {
            InitializeComponent();
            LoadDataFromTable();
        }

        void LoadDataFromTable()
        {
            ComboType.ItemsSource = null;
            LViewGoods.ItemsSource = null;
            ComboType.ItemsSource = GetGategories();
            ComboType.SelectedIndex = 0;
            LViewGoods.ItemsSource = GetGoods();
            TbCount.Text = "В корзине 0 товаров";
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var x = (sender as Button).DataContext as Good;
            if (buyGoods.ContainsKey(x))
            {
                int k = buyGoods[x].Count + 1;
                double p = x.Price * k;
                buyGoods[x] = new BuyItem { Count = k, Total = p };
            }
            else
            {
                int k = 1;
                double p = x.Price * k;
                buyGoods[x] = new BuyItem { Count = k, Total = p };
            }
            MessageBox.Show("Запись добавлена в корзину " + buyGoods.Count.ToString());
            TbCount.Text = $"В корзине {buyGoods.Count} товаров";
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateData();
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        List<Category> GetGategories()
        {
            List<Category> categories = new List<Category>();
            string sqlExpression = "SELECT * FROM Category";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand(sqlExpression, connection);
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Category u = new Category(reader.GetInt32(0), reader.GetString(1));
                        categories.Add(u);
                    }
                }
                reader.Close();
            }
            categories.Insert(0, new Category(-1, "Все типы"));
            return categories;
        }

        List<Good> GetGoods()
        {
            List<Good> goods = new List<Good>();
            string sqlExpression = "SELECT * FROM Good";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand(sqlExpression, connection);
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
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
                reader.Close();
            }
            return goods;
        }

        private void UpdateData()
        {
            var currentGoods = GetGoods();
            if (ComboType.SelectedIndex > 0)
                currentGoods = currentGoods.Where(p => p.CategoryId == (ComboType.SelectedItem as Category).CategoryId).ToList();
            currentGoods = currentGoods.Where(p => p.GoodName.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            if (ComboSort.SelectedIndex >= 0)
            {
                if (ComboSort.SelectedIndex == 0)
                    currentGoods = currentGoods.OrderBy(p => p.GoodName).ToList();
                if (ComboSort.SelectedIndex == 1)
                    currentGoods = currentGoods.OrderBy(p => p.CategoryId).ToList();
                if (ComboSort.SelectedIndex == 2)
                    currentGoods = currentGoods.OrderBy(p => p.Price).ToList();
                if (ComboSort.SelectedIndex == 3)
                    currentGoods = currentGoods.OrderByDescending(p => p.Price).ToList();
            }

            LViewGoods.ItemsSource = currentGoods;
        }

        private void ComboSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        private void BtnBuyClick(object sender, RoutedEventArgs e)
        {
            LbBuy.ItemsSource = null;
            LbBuy.ItemsSource = buyGoods;
            BuysWindow.IsOpen = true;
        }

        private void BtnOkClick(object sender, RoutedEventArgs e)
        {
            BuysWindow.IsOpen = false;
        }

        private void BtnDeleteClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show($"Удалить товар из корзины ??? ", "Удаление", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.OK)
            {
                if (LbBuy.SelectedIndex >= 0)
                {
                    var x = (LbBuy.SelectedValue as Good);
                    buyGoods.Remove(x);
                    LbBuy.ItemsSource = null;
                    LbBuy.ItemsSource = buyGoods;
                    TbCount.Text = $"В корзине {buyGoods.Count} товаров";
                }
            }
        }

        private void BtnBuyItemClick(object sender, RoutedEventArgs e)
        {
            if (buyGoods.Count <= 0) return;

            MessageBoxResult messageBoxResult = MessageBox.Show($"Купить товар(ы)???", "Покупка", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.OK)
            {
                try
                {
                    using (OleDbConnection connection = new OleDbConnection(connectionString))
                    {
                        int m = 0;
                        connection.Open();
                        OleDbCommand GetCount = new OleDbCommand("Select Count(*) from Sell", connection);
                        if (Convert.ToInt32(GetCount.ExecuteScalar()) == 0)
                        {
                            m = 1;
                        }
                        else
                        {
                            OleDbCommand MaxID = new OleDbCommand("SELECT MAX(SellId) FROM Sell", connection);
                            object result = MaxID.ExecuteScalar();
                            if (result != null)
                            {
                                m = Convert.ToInt32(result);
                            }
                        }

                        foreach (var x in buyGoods)
                        {
                            OleDbCommand commandInsert = new OleDbCommand("INSERT INTO [Sell] " +
                            "VALUES(@ID, @GoodId, @DateSell,@SellCount)", connection);
                            commandInsert.Parameters.AddWithValue("@ID", m + 1);
                            commandInsert.Parameters.AddWithValue("@GoodId", x.Key.GoodId);
                            commandInsert.Parameters.AddWithValue("@DateSell", DateTime.Now.ToString("g"));
                            commandInsert.Parameters.AddWithValue("@SellCount", x.Value.Count);
                            commandInsert.ExecuteNonQuery();
                            m++;
                        }

                        buyGoods.Clear();
                        LbBuy.ItemsSource = buyGoods;
                        TbCount.Text = $"В корзине {buyGoods.Count} товаров";
                        BuysWindow.IsOpen = false;
                        MessageBox.Show("СПАСИБО ЗА ПОКУПКУ");
                    }
                }
                catch (OleDbException exception)
                {
                    MessageBox.Show(exception.ToString());
                }
            }
        }
    }
}
