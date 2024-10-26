using System;
using System.Collections.Concurrent;
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
using VariationNine.ApplicationData;

namespace VariationNine
{
    public partial class Show : Page
    {
        Users use = new Users();
        public Show(Users user)
        {
            InitializeComponent();

            if (user != null)
            {
                use = user;

                switch (user.user_role_id)
                {
                    case 1:
                        //пользователь
                        addConMenu.IsEnabled = false;
                        editConMenu.IsEnabled = false;
                        delConMenu.IsEnabled = false;
                        addConMenu.Visibility = Visibility.Collapsed;
                        editConMenu.Visibility = Visibility.Collapsed;
                        delConMenu.Visibility = Visibility.Collapsed;

                        addButton.IsEnabled = false;
                        editButton.IsEnabled = false;
                        delButton.IsEnabled = false;
                        addButton.Visibility = Visibility.Collapsed;
                        editButton.Visibility = Visibility.Collapsed;
                        delButton.Visibility = Visibility.Collapsed;
                        break;
                    case 2:
                        //менеджер
                        addConMenu.IsEnabled = true;
                        editConMenu.IsEnabled = true;
                        delConMenu.IsEnabled = true;

                        addButton.IsEnabled = true;
                        editButton.IsEnabled = true;
                        delButton.IsEnabled = true;
                        break;
                    case 3:
                        //админ
                        addConMenu.IsEnabled = true;
                        editConMenu.IsEnabled = true;
                        delConMenu.IsEnabled = true;

                        addButton.IsEnabled = true;
                        editButton.IsEnabled = true;
                        delButton.IsEnabled = true;
                        break;
                    default:
                        MessageBox.Show("Произошла какая-то ошибка с данными пользователя. Вас перекинут на страницу авторизации.", "О-оу", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                }
            }
            else
            {
                addConMenu.IsEnabled = false;
                editConMenu.IsEnabled = false;
                delConMenu.IsEnabled = false;
                addConMenu.Visibility = Visibility.Collapsed;
                editConMenu.Visibility = Visibility.Collapsed;
                delConMenu.Visibility = Visibility.Collapsed;

                addButton.IsEnabled = false;
                editButton.IsEnabled = false;
                delButton.IsEnabled = false;
                addButton.Visibility = Visibility.Collapsed;
                editButton.Visibility = Visibility.Collapsed;
                delButton.Visibility = Visibility.Collapsed;

                buyConMenu.IsEnabled = false;
                buyConMenu.Visibility = Visibility.Collapsed;

                addCart.IsEnabled = false;
                addCart.Visibility = Visibility.Collapsed;
            }

            //сортировщик
            Sorter.ItemsSource = new Sort[]
            {
                new Sort { Name_sorter = "нет" },
                new Sort { Name_sorter = "по увеличению" },
                new Sort { Name_sorter = "по уменьшению" }
            };
            Sorter.SelectedIndex = 0;

            //фильтр
            Filter.ItemsSource = new Filt[]
            {
                new Filt { Name_filter = "нет" },
                new Filt { Name_filter = "до 9.99%" },
                new Filt { Name_filter = "от 10% до 14.99%" },
                new Filt { Name_filter = "от 15%" }
            };
            Filter.SelectedIndex = 0;

            //список
            listProducts.ItemsSource = FindProduct();
        }

        public class Sort
        {
            public string Name_sorter { get; set; } = "";
            public override string ToString() => $"{Name_sorter}";
        }

        public class Filt
        {
            public string Name_filter { get; set; } = "";
            public override string ToString() => $"{Name_filter}";
        }

        //составление списка
        Goods[] FindProduct()
        {
            var products = AppConnect.entities.Goods.ToList();
            var producttall = products;

            if (Searcher.Text != null)
            {
                products = products.Where(x => x.good_name.ToLower().Contains(Searcher.Text.ToLower()) || x.good_article.ToLower().Contains(Searcher.Text.ToLower())).ToList();
            }

            if (Filter.SelectedIndex > 0)
            {
                switch (Filter.SelectedIndex)
                {
                    case 1:
                        products = products.Where(x => x.act_skid > 0 && x.act_skid < 10).ToList();
                        break;
                    case 2:
                        products = products.Where(x => x.act_skid >= 10 && x.act_skid < 15).ToList();
                        break;
                    case 3:
                        products = products.Where(x => x.act_skid >= 15).ToList();
                        break;
                }
            }

            if (Sorter.SelectedIndex > 0)
            {
                switch (Sorter.SelectedIndex)
                {
                    case 1:
                        products = products.OrderBy(x => x.price).ToList();
                        break;
                    case 2:
                        products = products.OrderByDescending(x => x.price).ToList();
                        break;
                }
            }

            if (products.Count > 0)
            {
                Counter.Content = $"Найдено {products.Count} из {producttall.Count} товаров.";
            }
            else
            {
                Counter.Content = "Информация о товарах не найдена. Возможно у вас проблемы с сетью.";
            }

            return products.ToArray();
        }

        //обновление страницы
        private void Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listProducts.ItemsSource = FindProduct();
        }

        //обновление страницы
        private void Sorter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listProducts.ItemsSource = FindProduct();
        }

        //обновление страницы
        private void Searcher_TextChanged(object sender, TextChangedEventArgs e)
        {
            listProducts.ItemsSource = FindProduct();
        }

        //добавление товара через кнопку
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            addFun();
        }

        //редактирование товара через кнопку
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            editFun();
        }

        //удаление товара через кнопку
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            delFun();
        }

        //обновление контента на странице
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                AppConnect.entities.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                listProducts.ItemsSource = AppConnect.entities.Goods.ToList();
            }
        }

        //перевод товара в корзину через контекстное меню
        private void Buy_Click(object sender, RoutedEventArgs e)
        {
            AddToCart();
        }

        //перейти в корзину
        private void goCart_Click(object sender, RoutedEventArgs e)
        {
            //AppFrame.frameMain.Navigate(new TeamBuild(use));
        }

        //добавить товар через админа
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            addFun();
        }

        //редактировать товар через админа
        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            if ((Goods)listProducts.SelectedItem != null)
            {
                editFun();
            }
            else
            {
                MessageBox.Show("Выберите сотрудника", "Уведомление", MessageBoxButton.OK);
            }
        }

        //удаление товара через админа
        private void delButton_Click(object sender, RoutedEventArgs e)
        {
            if ((Goods)listProducts.SelectedItem != null)
            {
                delFun();
            }
            else
            {
                MessageBox.Show("Выберите сотрудника", "Уведомление", MessageBoxButton.OK);
            }
        }

        //Переход на добавление товара через контекстное меню через админа
        private void addFun()
        {
            AppFrame.frameMain.Navigate(new AddRed(null, use));
        }

        //Переход на редактирование товара через контекстное меню через админа
        private void editFun()
        {
            Goods red = (Goods)listProducts.SelectedItem;
            AppFrame.frameMain.Navigate(new AddRed(red, use));
        }

        //Удаление товара через контекстное меню через админа
        private void delFun()
        {
            if ((Goods)listProducts.SelectedItem != null)
            {
                var del = (Goods)listProducts.SelectedItem;
                var res = MessageBox.Show($"Вы действительно хотите удалить этого товар?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Information);

                if (res == MessageBoxResult.Yes)
                {
                    try
                    {
                        AppConnect.entities.Goods.Remove(del);
                        AppConnect.entities.SaveChanges();
                        listProducts.ItemsSource = FindProduct();
                        MessageBox.Show("Товар успешно удален", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        //кнопка на товаре
        private void Add_toCart_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"", "Тестирование", MessageBoxButton.OK);
            AddToCart();
        }

        //Добавление товара в заказ
        private void AddToCart()
        {
            //string numOrder;

            //Orders userList = AppConnect.entities.Orders.FirstOrDefault(x => x.order_user_id == use.id_user && x.order_status != 2);

            ////присваивание сотрудника к номеру проекта
            //if (userList == null)
            //{
            //    //генератор номера проекта                
            //    Random r = new Random();
            //    numOrder = "";

            //    while (AppConnect.entities.Orders.Where(x => x.order_number == numOrder).Count() > 0 || numOrder == "")
            //    {
            //        numOrder = "UN0";
            //        for (int i = 0; i < 10; i++)
            //        {
            //            int t = r.Next(0, 2);
            //            switch (t)
            //            {
            //                case 0:
            //                    numOrder += Convert.ToChar(r.Next(65, 90));
            //                    break;
            //                case 1:
            //                    numOrder += r.Next(0, 10).ToString();
            //                    break;
            //            }
            //        }
            //        MessageBox.Show("Номер создан", "", MessageBoxButton.OK);

            //        if (AppConnect.entities.Orders.Where(x => x.order_number == numOrder).Count() > 0)
            //        {
            //            MessageBox.Show("Такой номер уже есть.", "lol", MessageBoxButton.OK);
            //        }
            //    }

            //    try
            //    {
            //        Orders userDir = new Orders()
            //        {
            //            order_number = numOrder,
            //            order_good_id = 
            //        };
            //        AppConnect.entities.Orders.Add(userDir);
            //        AppConnect.entities.SaveChanges();
            //        MessageBox.Show($"Новый номер сгенерирован: {numOrder}", "Тестирование", MessageBoxButton.OK);
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show($"Ошибка при внедрении данных на сервер!\n{ex.Message}", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Товару успешно присвоен номер", "Тестирование", MessageBoxButton.OK);
            //    numOrder = userList.project_number.ToString();
            //}

            ////Добавление сотрудника в корзину проекта
            //var ord = (Employees_Cursach)listProducts.SelectedItem;

            ////ищем наш проект
            //var num = AppConnect.model1db.Projects_Cursach.FirstOrDefault(x => x.project_number == numOrder && (x.project_status != 2 || x.project_status != 3));
            ////ищем нашего сотрудника
            //var goodOrder = AppConnect.model1db.Groups_Cursach.FirstOrDefault(x => x.group_id_employee == ord.id_employee && x.project_id == num.id_project);

            ////если сотрудника ещё нет в корзине
            //if (ord != null && goodOrder == null)
            //{
            //    try
            //    {
            //        Groups_Cursach userOrder = new Groups_Cursach()
            //        {
            //            project_id = num.id_project,
            //            group_id_employee = ord.id_employee
            //        };
            //        AppConnect.model1db.Groups_Cursach.Add(userOrder);
            //        AppConnect.model1db.SaveChanges();
            //        MessageBox.Show("Пользователь успешно подсоединён к проекту.", "Тестовое уведомление", MessageBoxButton.OK);
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show($"Ошибка при внедрении данных пользователя!\n{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //    }
            //}

            ////если сотрудник уже есть в корзине 
            //else
            //{
            //    MessageBox.Show("Этот сотрудник уже состоит в проекте!", "Уведомление", MessageBoxButton.OK);
            //}
        }

        private void Add_toCart_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            AddToCart();
        }

        private void Searcher_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Searcher.Text))
            {
                Searcher.Visibility = Visibility.Collapsed;
                SearcherPlaceHolder.Visibility = Visibility.Visible;
            }
        }

        private void SearcherPlaceHolder_GotFocus(object sender, RoutedEventArgs e)
        {
            SearcherPlaceHolder.Visibility = Visibility.Collapsed;
            Searcher.Visibility = Visibility.Visible;
            Searcher.Focus();
        }
    }
}
