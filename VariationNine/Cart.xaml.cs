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
    public partial class Cart : Page
    {
        Users use = new Users();
        string numOrder = null;
        public Cart(Users user)
        {
            InitializeComponent();
            use = user;

            Orders userList = AppConnect.entities.Orders.FirstOrDefault(x => x.order_user_id == use.id_user && x.order_status != 2);

            if (userList != null)
            {
                numOrder = userList.order_number;
                listCart.ItemsSource = FillCart();
            }
            else
            {
                //OrderMake.IsEnabled = false;
            }
        }

        Orders[] FillCart()
        {
            var productsInCart = AppConnect.entities.Orders.ToList();

            if (numOrder != null)
            {
                //
                //var num = AppConnect.entities.Orders.FirstOrDefault(x => x.directory_order_number == numOrder).id_directory;
                //productsInCart = AppConnect.entities.Orders.Where(x => x.order_id_directory == num).ToList();

                int CountGood = 0;
                int WholeSaleSum = 0;
                int RetailSaleSum = 0;

                for (int i = 0; i < productsInCart.Count; i++)
                {
                    //int quan = productsInCart[i].order_quantity;
                   // int toy_id = productsInCart[i].order_id_toy;
                    //Toys_ToyStore pro = AppConnect.model1db.Toys_ToyStore.FirstOrDefault(x => x.id_toy == toy_id);
                    //int whol = pro.toy_wholesalePrice;
                    //int ret = pro.toy_retailPrice;

                    //CountGood += quan;
                    //WholeSaleSum += quan * whol;
                    //RetailSaleSum += quan * ret;
                }

                WholeSale.Content = "Общая цена: " + WholeSaleSum + " руб.";

                if (productsInCart.Count > 0)
                {
                    Stat.Content = $"В вашей корзине {productsInCart.Count} товаров. Ваш номер: {numOrder}";
                    //OrderMake.IsEnabled = true;
                }
                else
                {
                    Stat.Content = "Ваша корзина пуста.";
                }
            }

            return productsInCart.ToArray();
        }
    }
}
