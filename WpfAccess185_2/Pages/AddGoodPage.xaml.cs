using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Data.OleDb;
using System.Configuration;
using WpfAccess185_2.Models;
using Microsoft.Win32;
using System.IO;
using System.Security.Cryptography;
namespace WpfAccess185_2.Pages
{
    /// <summary> 
    /// Логика взаимодействия для AddGoodPage.xaml 
    /// </summary> 
    public partial class AddGoodPage : Page
    {
        // текущий товар 
        private Good _currentGood = new Good();
        // путь к файлу изображения
       private string _filePath = null; string message = "Товар добавлен"; 
        // путь к текущей папке, в которой размещен проект
        private static string _currentDirectory = 
        Directory.GetCurrentDirectory() + @"\Images\"; 
// строка подключения 
static string connectionString =
ConfigurationManager.ConnectionStrings["AccessCS"].ConnectionString; 
//  список категорий товаров 
// конструктор формы, принимает товар в виде аргумента
public AddGoodPage(Good selectedGoods) 
{ 
InitializeComponent(); 
this.Title = "Добавление товара"; // если товар был передан
                                  if (selectedGoods != null) 
{ 
_currentGood = selectedGoods; this.Title = "Редактирование товара"; message = "Данные о товаре изменены"; 
}
    // привязка контекста к текущему товару 
    DataContext = _currentGood; 
CmbCategory.ItemsSource = GetCategories();
}
/// <summary> 
/// Метод получения списка категорий 
/// </summary> 
/// <returns>возвращает List of Category</returns> 
List<Category> GetCategories()
{
    List<Category> categories = new List<Category>(); // формируем запрос на выборку данных о категориях
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
                Category u = new Category(reader.GetInt32(0), reader.GetString(1)
                ); categories.Add(u);
            }
        }
        reader.Close();
    }
    return categories;
}
// проверка полей 
private StringBuilder CheckFields()
{
    StringBuilder s = new StringBuilder();
    if (string.IsNullOrWhiteSpace(_currentGood.GoodName))
        s.AppendLine("Поле название пустое");
    if (string.IsNullOrWhiteSpace(_currentGood.Description))
        s.AppendLine("Поле описание пустое");
    if (_currentGood.Price < 0)
        s.AppendLine("Поле цена пустое");
    if (CmbCategory.SelectedIndex == -1)
        s.AppendLine("Категория не выбрана");
    if (string.IsNullOrWhiteSpace(TbPicture.Text))
        s.AppendLine("фото не выбрано пустое");
    return s;
}
private void BtnSaveClick(object sender, RoutedEventArgs e)
{
    //проверка полей 
    StringBuilder _error = CheckFields();
    // если ошибки есть, выводим сообщение и завершаем метод
    if (_error.Length > 0) 
    {
        MessageBox.Show(_error.ToString()); return;
    }
    if (_currentGood.GoodId == 0)
    {
        // товар добавляется в таблицу
        string photo = ChangePhotoName(); string dest = _currentDirectory + photo; 
        File.Copy(_filePath, dest);
        _currentGood.Picture = photo;
        // вызов метода сохранения 
        SaveNewItem();
        // переход на форму товаров 
        Manager.MainFrame.GoBack();
    }
    else
    {
        if (_filePath != null)
        {
            string photo = ChangePhotoName(); string dest = _currentDirectory + photo;
            File.Copy(_filePath, dest);
            _currentGood.Picture = photo;
        }
        // вызов метода обновления 
        ChangeItem();
        // переход на форму товаров 
        Manager.MainFrame.GoBack();
    }
}
// добавление товара в таблицу
void SaveNewItem() 
{
    try
    {
        using (OleDbConnection connection = new OleDbConnection(connectionString))
        {
            // открываем подключение
            connection.Open(); int m = 0; 
            // получаем количество записей 
            OleDbCommand GetCount = new OleDbCommand("Select Count(*) from Good", connection); if (Convert.ToInt32(GetCount.ExecuteScalar()) == 0)
            { m = 1; }
            else
            {
                OleDbCommand MaxID = new OleDbCommand("Select Max(GoodId) from Good", connection); if (MaxID.ExecuteScalar() != null) m = Convert.ToInt32(MaxID.ExecuteScalar());
            }
            // формируем запрос на добавление данных о товарах 
            OleDbCommand commandInsert = new OleDbCommand("INSERT INTO [Good] " +
            "VALUES(@ID, @GoodName, @Price,@Picture,@Description,@CategoryId)", connection);
            // передача параметров в запрос
            commandInsert.Parameters.AddWithValue("@ID", m + 1); 
            commandInsert.Parameters.AddWithValue("@GoodName", _currentGood.GoodName); commandInsert.Parameters.AddWithValue("@Price", _currentGood.Price);
            commandInsert.Parameters.AddWithValue("@Picture", _currentGood.Picture); commandInsert.Parameters.AddWithValue("@Description", _currentGood.Description);
            commandInsert.Parameters.AddWithValue("@CategoryId", _currentGood.CategoryId);
            // выполнение запроса 
            commandInsert.ExecuteNonQuery(); MessageBox.Show(message);
        }
    }
    catch (OleDbException exception)
    {
        MessageBox.Show(exception.ToString());
    }
}
// изменение товаров в таблице Access
void ChangeItem() 
{
    try
    {
        using (OleDbConnection connection = new OleDbConnection(connectionString))
        {
            // открываем подключение
            connection.Open(); 
            // формируем запрос на изменение данных о товарах 
            OleDbCommand commandUpdate = new OleDbCommand("UPDATE Good SET " +
            " GoodName=@GoodName, " +
            " Price=@Price,  " +
            " Picture=@Picture," +
            " Description=@Description," +
            " CategoryId=@CategoryId" +
            " WHERE GoodId=@ID", connection);
            // передача параметров в запрос
            commandUpdate.Parameters.AddWithValue("@GoodName", _currentGood.GoodName); commandUpdate.Parameters.AddWithValue("@Price", _currentGood.Price); commandUpdate.Parameters.AddWithValue("@Picture", _currentGood.Picture); commandUpdate.Parameters.AddWithValue("@Description", _currentGood.Description); commandUpdate.Parameters.AddWithValue("@CategoryId", _currentGood.CategoryId); commandUpdate.Parameters.AddWithValue("@ID", _currentGood.GoodId);
                    // выполнение запроса 
                    int v = commandUpdate.ExecuteNonQuery();
            MessageBox.Show(message);
        }
    }
    catch (OleDbException exception)
    {
        MessageBox.Show(exception.ToString());
    }
}
// загрузка фотографии из компа в Image 
private void BtnLoadClick(object sender, RoutedEventArgs e)
{
    try
    {
        OpenFileDialog op = new OpenFileDialog(); op.Title = "Select a picture";
        op.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|" +
        "JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif"; if (op.ShowDialog() == true)
        {
            ImgPhoto.Source = new BitmapImage(new Uri(op.FileName));
            TbPicture.Text = op.SafeFileName;
            _filePath = op.FileName;
        }
    }
    catch
    {
        MessageBox.Show("Нет файла");
        _filePath = null;
    }
}
//подбор имени файла
string ChangePhotoName() 
{
    string x = _currentDirectory + TbPicture.Text; string photoname = TbPicture.Text; int i = 0; if (File.Exists(x))
    {
        while (File.Exists(x))
        {
            i++;
            x = _currentDirectory + i.ToString() + photoname;
        }
        photoname = i.ToString() + photoname;
    }
    return photoname;
} 
} 
}
