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
using VariationNine.ApplicationData;

namespace VariationNine
{
    public partial class Goods : Page
    {
        Users use = new Users();
        public Goods()
        {
            InitializeComponent();
            //use = user;

            //switch (user.user_id_role)
            //{
            //    case 3:
            //        //админ
            //        addConMenu.IsEnabled = true;
            //        editConMenu.IsEnabled = true;
            //        delConMenu.IsEnabled = true;

            //        addButton.IsEnabled = true;
            //        editButton.IsEnabled = true;
            //        delButton.IsEnabled = true;
            //        break;
            //    case 1:
            //        //пользователь
            //        addConMenu.IsEnabled = false;
            //        editConMenu.IsEnabled = false;
            //        delConMenu.IsEnabled = false;
            //        addConMenu.Visibility = Visibility.Collapsed;
            //        editConMenu.Visibility = Visibility.Collapsed;
            //        delConMenu.Visibility = Visibility.Collapsed;

            //        addButton.IsEnabled = false;
            //        editButton.IsEnabled = false;
            //        delButton.IsEnabled = false;
            //        addButton.Visibility = Visibility.Collapsed;
            //        editButton.Visibility = Visibility.Collapsed;
            //        delButton.Visibility = Visibility.Collapsed;

            //        //goCart.IsEnabled = false;
            //        //goCart.Visibility = Visibility.Collapsed;
            //        //addCart.IsEnabled = false;
            //        //addCart.Visibility = Visibility.Collapsed;
            //        break;
            //    case 2:
            //        //менеджер
            //        addConMenu.IsEnabled = true;
            //        editConMenu.IsEnabled = true;
            //        delConMenu.IsEnabled = true;

            //        addButton.IsEnabled = true;
            //        editButton.IsEnabled = true;
            //        delButton.IsEnabled = true;
            //        break;
            //    default:
            //        MessageBox.Show("Произошла какая-то ошибка с данными пользователя. Вас перекинут на страницу авторизации.", "О-оу", MessageBoxButton.OK, MessageBoxImage.Error);
            //        break;
            //}

            //сортировшик
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
                new Filt { Name_filter = "до 26 лет" },
                new Filt { Name_filter = "от 26 до 40 лет" },
                new Filt { Name_filter = "от 40 лет" }
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
            return null;
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
