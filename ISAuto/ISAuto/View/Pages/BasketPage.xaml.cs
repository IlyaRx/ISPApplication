using ISAuto.Model;
using ISAuto.View.Controls;
using ISAuto.View.Windows;
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

namespace ISAuto.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для BasketPage.xaml
    /// </summary>
    public partial class BasketPage : Page
    {
        private List<PartBasketControl> _partBasketControls = new List<PartBasketControl>();
        private Frame _frame;

        public BasketPage(Frame frame)
        {
            InitializeComponent();
            _frame = frame;
            Sum();
        }

        public void Add(PartsInStore partsInStore)
        {
            if (_partBasketControls.FirstOrDefault(p => p.PartsStore.Id == partsInStore.Id) != null)
                return;
            PartBasketControl partBasketControl = new PartBasketControl(partsInStore);
            partBasketControl.RemoveItem += UserControl_RemoveItem;
            partBasketControl.Picer.QuantityPrise += Sum;
            _partBasketControls.Add(partBasketControl);

            FullingList();
        }

        private void FullingList()
        {
            MainList.Children.Clear();
            foreach (var part in _partBasketControls)
            {
                MainList.Children.Add(part);
            }
            Sum();
        }

        private void UserControl_RemoveItem(object sender)
        {
            if (sender is PartBasketControl partBasketControl)
                _partBasketControls.Remove(partBasketControl);
            FullingList();


        }

        private void Sum(object obj = null)
        {
            decimal sum = 0;
            foreach (var part in _partBasketControls)
            {
                sum += part.QuantityCost;
            }

            SumPrise.Text = sum.ToString();
        }

        private void BuyBut_Click(object sender, RoutedEventArgs e)
        {
            Order order = new Order()
            {
                Cost = int.Parse(SumPrise.Text),
                DateTimePurchase = DateTime.Now,
            };

            DatabaseAutoContext.GetContext().Orders.Add(order);

            foreach (var part in _partBasketControls)
            {
                PartsInOeder partsInOeder = new PartsInOeder()
                {
                    PartsInStoreId = part.PartsStore.Id,
                    Order = order,
                    Quantity = part.Picer.Quantity,
                };

                DatabaseAutoContext.GetContext().PartsInStores.First(p => p.Id == part.PartsStore.Id).Quantity -= part.Picer.Quantity;

                DatabaseAutoContext.GetContext().PartsInOeders.Add(partsInOeder);

            }
            DatabaseAutoContext.GetContext().SaveChanges();

            Window window = Window.GetWindow(this);
            Window main = new ClientWindow();
            main.Show();
            window.Hide();

        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e) => Sum();

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            _frame.NavigationService.GoBack();
        }
    }

}
