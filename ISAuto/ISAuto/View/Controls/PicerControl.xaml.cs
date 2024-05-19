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

namespace ISAuto.View.Controls
{
    /// <summary>
    /// Логика взаимодействия для PicerControl.xaml
    /// </summary>
    public partial class PicerControl : UserControl
    {
        private int _quantity = 1;
        private int _quantityInStore = 10000;
        public int Quantity { get => _quantity; set => _quantity = value; }
        public int QuantityInStore { get => _quantityInStore; set => _quantityInStore = value; }

        public event Action<object> QuantityPrise;

        public PicerControl()
        {
            InitializeComponent();
            TextQuant.Text = _quantity.ToString();
            Minus.IsEnabled = false;
        }


        private void Minus_Click(object sender, RoutedEventArgs e)
        {
            if (Quantity <= 1)
                return;
            
            Quantity--;

            if (Quantity <= 1)
                Minus.IsEnabled = false;

            Plas.IsEnabled = true;
            TextQuant.Text = _quantity.ToString();
            QuantityPrise?.Invoke(Quantity);
        }

        private void Plas_Click(object sender, RoutedEventArgs e)
        {
            if (Quantity >= _quantityInStore)
                return;

            Quantity++;

            if (Quantity >= _quantityInStore)
                Plas.IsEnabled = false;

            Minus.IsEnabled = true;
            TextQuant.Text = _quantity.ToString();
            QuantityPrise?.Invoke(Quantity);
            

        }
    }
}
