using System;
using System.Windows;
using WpfAccess185_2.Models;
using WpfAccess185_2.Pages;

namespace WpfAccess185_2
{
    public partial class MainWindow : Window
    {
        string role = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        // Вызов конструктора формы
        public MainWindow(string userrole)
        {
            InitializeComponent();
            role = userrole;
            // Открытие начальной страницы во фрейме 
            MainFrame.Navigate(new StartPage());
            Manager.MainFrame = MainFrame;
            RenderButtons();
        }

        private void WindowClosed(object sender, EventArgs e)
        {
            // После закрытия формы отбражается форма авторизации
            Owner.Show();
        }

        private void BtnUserClick(object sender, RoutedEventArgs e)
        {
            // Переход на страницу каталогов 
            MainFrame.Navigate(new CatalogOfGoodsPage());
        }

        private void BtnAdminClick(object sender, RoutedEventArgs e)
        {
            // Переход на страницу товаров 
            MainFrame.Navigate(new GoodsPage());
        }

        private void BtnCoordinatorClick(object sender, RoutedEventArgs e)
        {
            // Переход на страницу продаж 
            MainFrame.Navigate(new SellGoodsPage());
        }

        private void BtnBackClick(object sender, RoutedEventArgs e)
        {
            // Переход назад 
            MainFrame.GoBack();
        }

        private void MainFrameContentRendered(object sender, EventArgs e)
        {
            RenderButtons();
        }

        // Обработчик события для кнопки COG
        private void BtnCOGClick(object sender, RoutedEventArgs e)
        {
            // Переход на страницу CatalogOfGoodsPage
            MainFrame.Navigate(new CatalogOfGoodsPage());
        }

        // Отображение кнопок в зависимости от роли пользователя
        void RenderButtons()
        {
            // Если авторизовался пользователь
            if (role == "user")
            {
                // Скрываем кнопки товара и продаж 
                BtnAdmin.Visibility = Visibility.Collapsed;
                BtnCoordinator.Visibility = Visibility.Collapsed;
                if (MainFrame.CanGoBack)
                {
                    // Перешли на страницу каталога 
                    // Отображаем кнопку назад 
                    BtnBack.Visibility = Visibility.Visible;
                    // Скрываем кнопку каталога 
                    BtnUser.Visibility = Visibility.Collapsed;
                }
                else
                {
                    // Скрываем кнопку назад 
                    BtnBack.Visibility = Visibility.Collapsed;
                    // Отображаем кнопку каталога 
                    BtnUser.Visibility = Visibility.Visible;
                }
            }
            // Если авторизовался админ
            if (role == "admin")
            {
                // Скрываем кнопки каталога и продаж 
                BtnUser.Visibility = Visibility.Collapsed;
                BtnCoordinator.Visibility = Visibility.Collapsed;
                if (MainFrame.CanGoBack)
                {
                    // Перешли на страницу товаров 
                    // Отображаем кнопку назад 
                    BtnBack.Visibility = Visibility.Visible;
                    // Скрываем кнопку товары 
                    BtnAdmin.Visibility = Visibility.Collapsed;
                }
                else
                {
                    // Скрываем кнопку назад 
                    BtnBack.Visibility = Visibility.Collapsed;
                    // Отображаем кнопку товары 
                    BtnAdmin.Visibility = Visibility.Visible;
                }
            }
            // Если авторизовался координатор
            if (role == "coord")
            {
                // Скрываем кнопки каталога и товаров 
                BtnAdmin.Visibility = Visibility.Collapsed;
                BtnUser.Visibility = Visibility.Collapsed;
                if (MainFrame.CanGoBack)
                {
                    // Отображаем кнопку назад 
                    BtnBack.Visibility = Visibility.Visible;
                    // Скрываем кнопку продажи 
                    BtnCoordinator.Visibility = Visibility.Collapsed;
                }
                else
                {
                    // Скрываем кнопку назад 
                    BtnBack.Visibility = Visibility.Collapsed;
                    // Отображаем кнопку продажи 
                    BtnCoordinator.Visibility = Visibility.Visible;
                }
            }
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult x = MessageBox.Show("Вы действительно хотите выйти?",
            "Выйти", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (x == MessageBoxResult.Cancel)
                e.Cancel = true;
        }
    }
}
