using ISAuto.Model;
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
    /// Логика взаимодействия для PaymentPage.xaml
    /// </summary>
    public partial class PaymentPage : Page
    {
        public PaymentPage(Order order)
        {
            InitializeComponent();
            Title.Text += order.Id.ToString();
            Cost.Text = order.Cost.ToString() + " ₽";
            Number.Text = order.Id.ToString();
        }

        private void PaySBP_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new QRCodePage());
        }

        private void PayCart_Click(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            Window main = new ClientWindow();
            main.Show();
            window.Hide();
        }
    }
}
