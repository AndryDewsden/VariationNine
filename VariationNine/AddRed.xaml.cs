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

            categoryComboBox.Items.Add("-выбрать-");
            ediComboBox.Items.Add("--");
            makerComboBox.Items.Add("--");
            giverComboBox.Items.Add("--");

            //заполнение списка категорий
            for (int i = 0; i < AppConnect.entities.Categories.ToList().Count; i++)
            {
                categoryComboBox.Items.Add(AppConnect.entities.Categories.ToList()[i]);
            }
            for (int i = 0; i < AppConnect.entities.Edis.ToList().Count; i++)
            {
                ediComboBox.Items.Add(AppConnect.entities.Edis.ToList()[i]);
            }
            for (int i = 0; i < AppConnect.entities.Makers.ToList().Count; i++)
            {
                makerComboBox.Items.Add(AppConnect.entities.Makers.ToList()[i]);
            }
            for (int i = 0; i < AppConnect.entities.Givers.ToList().Count; i++)
            {
                giverComboBox.Items.Add(AppConnect.entities.Givers.ToList()[i]);
            }

            if (aget != null)
            {
                _curAgent = aget;
                Title = "Редактирование товара";
                Red.IsEnabled = true;

                check(nameBox, nameBoxPlaceHolder);

            }
            else
            {
                Title = "Добавление товара";
                Red.IsEnabled = false;
                Red.Visibility = Visibility.Collapsed;
            }

            DataContext = _curAgent;
        }

        //к магазину
        private void list_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new Show(use));
        }

        //добавить товар
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (articleBox.Text != "" && nameBox.Text != "" && descriptionBox.Text != "" && priceBox.Text != "" && actSkidBox.Text != "" && maxSkidBox.Text != "" && kolvoBox.Text != "" && categoryComboBox.SelectedIndex != 0 && ediComboBox.SelectedIndex != 0 && makerComboBox.SelectedIndex != 0 && giverComboBox.SelectedIndex != 0)
            {
                var res = MessageBox.Show("Вы действительно хотите добавить этот товар?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Information);

                if (res == MessageBoxResult.Yes)
                {
                    try
                    {
                        _curAgent.good_article = articleBox.Text;
                        _curAgent.price = Convert.ToInt32(priceBox.Text);
                        _curAgent.act_skid = Convert.ToInt32(actSkidBox.Text);
                        _curAgent.max_skid = Convert.ToInt32(maxSkidBox.Text);
                        _curAgent.good_name = nameBox.Text;
                        _curAgent.discription = descriptionBox.Text;
                        _curAgent.image = imageBox.Text;
                        _curAgent.category_id = AppConnect.entities.Categories.FirstOrDefault(x => x.category_name == categoryComboBox.Text).id_category;
                        _curAgent.edi_id = AppConnect.entities.Edis.FirstOrDefault(x => x.edi_name == ediComboBox.Text).id_edi;
                        _curAgent.maker_id = AppConnect.entities.Makers.FirstOrDefault(x => x.maker_name == makerComboBox.Text).id_maker;
                        _curAgent.giver_id = AppConnect.entities.Givers.FirstOrDefault(x => x.giver_name == giverComboBox.Text).id_giver;
                        _curAgent.kolvo = Convert.ToInt32(kolvoBox.Text);


                        AppConnect.entities.Goods.Add(_curAgent);
                        AppConnect.entities.SaveChanges();
                        
                        MessageBox.Show("Товар успешно добавлен", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                        AppFrame.frameMain.Navigate(new Show(use));
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Заполните все необходимые поля!", "Уведомление", MessageBoxButton.OK);
            }
        }

        //редактировать товар
        private void Red_Click(object sender, RoutedEventArgs e)
        {
            if (articleBox.Text != "" && nameBox.Text != "" && descriptionBox.Text != "" && priceBox.Text != "" && actSkidBox.Text != "" && maxSkidBox.Text != "" && kolvoBox.Text != "" && categoryComboBox.SelectedIndex != 0 && ediComboBox.SelectedIndex != 0 && makerComboBox.SelectedIndex != 0 && giverComboBox.SelectedIndex != 0)
            {
                var res = MessageBox.Show("Вы действительно хотите редактировать этот товар?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Information);

                if (res == MessageBoxResult.Yes)
                {
                    try
                    {
                        _curAgent.good_article = articleBox.Text;
                        _curAgent.price = Convert.ToInt32(priceBox.Text);
                        _curAgent.act_skid = Convert.ToInt32(actSkidBox.Text);
                        _curAgent.max_skid = Convert.ToInt32(maxSkidBox.Text);
                        _curAgent.good_name = nameBox.Text;
                        _curAgent.discription = descriptionBox.Text;
                        _curAgent.image = imageBox.Text;
                        _curAgent.category_id = AppConnect.entities.Categories.FirstOrDefault(x => x.category_name == categoryComboBox.Text).id_category;
                        _curAgent.edi_id = AppConnect.entities.Edis.FirstOrDefault(x => x.edi_name == ediComboBox.Text).id_edi;
                        _curAgent.maker_id = AppConnect.entities.Makers.FirstOrDefault(x => x.maker_name == makerComboBox.Text).id_maker;
                        _curAgent.giver_id = AppConnect.entities.Givers.FirstOrDefault(x => x.giver_name == giverComboBox.Text).id_giver;
                        _curAgent.kolvo = Convert.ToInt32(kolvoBox.Text);
                        
                        AppConnect.entities.SaveChanges();

                        MessageBox.Show("Товар успешно редактирован", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                        AppFrame.frameMain.Navigate(new Show(use));
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Заполните все необходимые поля!", "Уведомление", MessageBoxButton.OK);
            }
        }

        private void articleBox_LostFocus(object sender, RoutedEventArgs e)
        {
            check(articleBox, actSkidBoxPlaceHolder);
            placeHolder(articleBox, articleBoxPlaceHolder);
        }
        private void articleBoxPlaceHolder_GotFocus(object sender, RoutedEventArgs e)
        {
            Original(articleBox, articleBoxPlaceHolder);
            articleBox.Focus();
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

        private void imageBox_LostFocus(object sender, RoutedEventArgs e)
        {
            check(imageBox, imageBoxPlaceHolder);
            placeHolder(imageBox, imageBoxPlaceHolder);
        }
        private void imageBoxPlaceHolder_GotFocus(object sender, RoutedEventArgs e)
        {
            Original(imageBox, imageBoxPlaceHolder);
            imageBox.Focus();
        }

        private void descriptionBox_LostFocus(object sender, RoutedEventArgs e)
        {
            check(descriptionBox, descriptionBoxPlaceHolder);
            placeHolder(descriptionBox, descriptionBoxPlaceHolder);
        }
        private void descriptionBoxPlaceHolder_GotFocus(object sender, RoutedEventArgs e)
        {
            Original(descriptionBox, descriptionBoxPlaceHolder);
            descriptionBox.Focus();
        }

        private void priceBox_LostFocus(object sender, RoutedEventArgs e)
        {
            check(priceBox, priceBoxPlaceHolder);
            placeHolder(priceBox, priceBoxPlaceHolder);
        }
        private void priceBoxPlaceHolder_GotFocus(object sender, RoutedEventArgs e)
        {
            Original(priceBox, priceBoxPlaceHolder);
            priceBox.Focus();
        }

        private void actSkidBox_LostFocus(object sender, RoutedEventArgs e)
        {
            check(actSkidBox, actSkidBoxPlaceHolder);
            placeHolder(actSkidBox, actSkidBoxPlaceHolder);
        }
        private void actSkidBoxPlaceHolder_GotFocus(object sender, RoutedEventArgs e)
        {
            Original(actSkidBox, actSkidBoxPlaceHolder);
            actSkidBox.Focus();
        }

        private void maxSkidBox_LostFocus(object sender, RoutedEventArgs e)
        {
            check(maxSkidBox, maxSkidBoxPlaceHolder);
            placeHolder(maxSkidBox, maxSkidBoxPlaceHolder);
        }
        private void maxSkidBoxPlaceHolder_GotFocus(object sender, RoutedEventArgs e)
        {
            Original(maxSkidBox, maxSkidBoxPlaceHolder);
            maxSkidBox.Focus();
        }

        private void kolvoBox_LostFocus(object sender, RoutedEventArgs e)
        {
            check(kolvoBox, kolvoBoxPlaceHolder);
            placeHolder(kolvoBox, kolvoBoxPlaceHolder);
        }
        private void kolvoBoxPlaceHolder_GotFocus(object sender, RoutedEventArgs e)
        {
            Original(kolvoBox, kolvoBoxPlaceHolder);
            kolvoBox.Focus();
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
