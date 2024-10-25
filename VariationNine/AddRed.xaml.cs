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
    public partial class AddRed : Page
    {
        private Goods _curAgent = new Goods();
        Users use = new Users();
        public AddRed(Goods aget, Users user)
        {
            InitializeComponent();
            use = user;

            Combo_names.Items.Add("-выбрать-");

            //заполнение списка категорий
            for (int i = 0; i < AppConnect.entities.Categories.ToList().Count; i++)
            {
                Combo_names.Items.Add(AppConnect.entities.Categories.ToList()[i]);
            }

            if (aget != null)
            {
                _curAgent = aget;
                Title = "Редактирование агента";
                Red.IsEnabled = true;

                check(nameBox, nameBoxPlaceHolder);
                check(priorityBox, priorityBoxPlaceHolder);
                check(logoBox, logoBoxPlaceHolder);
                check(addressBox, addressBoxPlaceHolder);
                check(innBox, innBoxPlaceHolder);
                check(kppBox, kppBoxPlaceHolder);
                check(directorNameBox, directorNameBoxPlaceHolder);
                check(phoneBox, phoneBoxPlaceHolder);
                check(emailBox, emailBoxPlaceHolder);

            }
            else
            {
                Title = "Добавление агента";
                Red.IsEnabled = false;
                Red.Visibility = Visibility.Collapsed;

                check(priorityBox, priorityBoxPlaceHolder);

            }

            DataContext = _curAgent;
        }

        //вернуться
        private void goBack_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.GoBack();
        }

        //к магазину
        private void list_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new Goods());
        }

        //добавить товар
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (Combo_names.SelectedIndex != 0 && nameBox.Text != "" && priorityBox.Text != "" && addressBox.Text != "" && innBox.Text != "" && kppBox.Text != "" && directorNameBox.Text != "" && phoneBox.Text != "" && emailBox.Text != "" && logoBox.Text != "")
            {
                var res = MessageBox.Show("Вы действительно хотите добавить этот товар?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Information);

                if (res == MessageBoxResult.Yes)
                {
                    try
                    {
                        //AppConnect.entities.Goods.Add(_curAgent);
                        AppConnect.entities.SaveChanges();
                        MessageBox.Show("Данные успешно добавленны", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                        AppFrame.frameMain.Navigate(new Goods());
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Заполните все необходимые поля!", "Уведомлениее", MessageBoxButton.OK);
            }
        }

        //редактировать товар
        private void Red_Click(object sender, RoutedEventArgs e)
        {
            if (Combo_names.SelectedIndex != 0)
            {
                var res = MessageBox.Show("Вы действительно хотите редактировать этот товар?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Information);

                if (res == MessageBoxResult.Yes)
                {
                    try
                    {

                        AppConnect.entities.SaveChanges();
                        MessageBox.Show("Данные успешно редактированы", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                        AppFrame.frameMain.Navigate(new Goods());
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Заполните все необходимые поля!", "Уведомлениее", MessageBoxButton.OK);
            }
        }

        private void nameBox_LostFocus(object sender, RoutedEventArgs e)
        {
            check(nameBox, nameBoxPlaceHolder);
            placeHolder(nameBox, nameBoxPlaceHolder);
        }

        private void nameBoxPlaceHolder_GotFocus(object sender, RoutedEventArgs e)
        {
            Original(nameBox, nameBoxPlaceHolder);
            nameBox.Focus();
        }

        private void priorityBox_LostFocus(object sender, RoutedEventArgs e)
        {
            check(priorityBox, priorityBoxPlaceHolder);
            placeHolder(priorityBox, priorityBoxPlaceHolder);
        }

        private void priorityBoxPlaceHolder_GotFocus(object sender, RoutedEventArgs e)
        {
            Original(priorityBox, priorityBoxPlaceHolder);
            priorityBox.Focus();
        }

        private void logoBox_LostFocus(object sender, RoutedEventArgs e)
        {
            check(logoBox, logoBoxPlaceHolder);
            placeHolder(logoBox, logoBoxPlaceHolder);
        }

        private void logoBoxPlaceHolder_GotFocus(object sender, RoutedEventArgs e)
        {
            Original(logoBox, logoBoxPlaceHolder);
            logoBox.Focus();
        }

        private void addressBox_LostFocus(object sender, RoutedEventArgs e)
        {
            check(addressBox, addressBoxPlaceHolder);
            placeHolder(addressBox, addressBoxPlaceHolder);
        }

        private void addressBoxPlaceHolder_GotFocus(object sender, RoutedEventArgs e)
        {
            Original(addressBox, addressBoxPlaceHolder);
            addressBox.Focus();
        }

        private void innBox_LostFocus(object sender, RoutedEventArgs e)
        {
            check(innBox, innBoxPlaceHolder);
            placeHolder(innBox, innBoxPlaceHolder);
        }

        private void innBoxPlaceHolder_GotFocus(object sender, RoutedEventArgs e)
        {
            Original(innBox, innBoxPlaceHolder);
            innBox.Focus();
        }

        private void kppBox_LostFocus(object sender, RoutedEventArgs e)
        {
            check(kppBox, kppBoxPlaceHolder);
            placeHolder(kppBox, kppBoxPlaceHolder);
        }

        private void kppBoxPlaceHolder_GotFocus(object sender, RoutedEventArgs e)
        {
            Original(kppBox, kppBoxPlaceHolder);
            kppBox.Focus();
        }

        private void directorNameBox_LostFocus(object sender, RoutedEventArgs e)
        {
            check(directorNameBox, directorNameBoxPlaceHolder);
            placeHolder(directorNameBox, directorNameBoxPlaceHolder);
        }

        private void directorNameBoxPlaceHolder_GotFocus(object sender, RoutedEventArgs e)
        {
            Original(directorNameBox, directorNameBoxPlaceHolder);
            directorNameBox.Focus();
        }

        private void phoneBox_LostFocus(object sender, RoutedEventArgs e)
        {
            check(phoneBox, phoneBoxPlaceHolder);
            placeHolder(phoneBox, phoneBoxPlaceHolder);
        }

        private void phoneBoxPlaceHolder_GotFocus(object sender, RoutedEventArgs e)
        {
            Original(phoneBox, phoneBoxPlaceHolder);
            phoneBox.Focus();
        }

        private void emailBox_LostFocus(object sender, RoutedEventArgs e)
        {
            check(emailBox, emailBoxPlaceHolder);
            placeHolder(emailBox, emailBoxPlaceHolder);
        }

        private void emailBoxPlaceHolder_GotFocus(object sender, RoutedEventArgs e)
        {
            Original(emailBox, emailBoxPlaceHolder);
            emailBox.Focus();
        }
        private void Original(TextBox org, TextBox place)
        {
            place.Visibility = Visibility.Collapsed;
            org.Visibility = Visibility.Visible;
        }

        private void placeHolder(TextBox org, TextBox place)
        {
            if (string.IsNullOrEmpty(org.Text))
            {
                org.Visibility = Visibility.Collapsed;
                place.Visibility = Visibility.Visible;
            }
        }

        private void check(TextBox org, TextBox place)
        {
            if (org.Text == null)
            {
                placeHolder(org, place);
            }
            else
            {
                Original(org, place);
            }
        }
    }
}
