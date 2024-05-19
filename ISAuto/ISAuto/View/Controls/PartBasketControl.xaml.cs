using ISAuto.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UserControl = System.Windows.Controls.UserControl;

namespace ISAuto.View.Controls
{
    /// <summary>
    /// Логика взаимодействия для PartBasketControl.xaml
    /// </summary>
    public partial class PartBasketControl : UserControl
    {
        private PartsInStore _partsStore;

        public PartsInStore PartsStore { get => _partsStore; set => _partsStore = value; }
        public decimal QuantityCost { get; set; }


        public event Action<object> RemoveItem;


        public PartBasketControl(PartsInStore partsStore)
        {
            InitializeComponent();
            PartsStore = partsStore;

            Picer.QuantityPrise += ActualPrise;
            Picer.QuantityInStore = partsStore.Quantity;
            QuantityCost = PartsStore.Price;

            AutoPart autoPart = partsStore.AutoPart;

            PartName.Text = autoPart.Name;
            PartPrise.Text = PartsStore.Price.ToString();


            if (autoPart.Image != null)
            {

                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(autoPart.Image, UriKind.Absolute);
                bitmap.EndInit();
                ImageAutoPart.Source = bitmap;
            }

        }


        public PartsInOeder GetPartsInOeder()
        {
            PartsInOeder partsInOeder = new PartsInOeder() {
                PartsInStoreId = PartsStore.Id,
                Quantity = Picer.Quantity
            };
            return partsInOeder;
        }

        private void ActualPrise(object sender)
        {
            if(sender is int quantity)
            {
                QuantityCost = PartsStore.Price * quantity;
                PartPrise.Text = Convert.ToString(PartsStore.Price * quantity);
            }
        }

        private void DeleteBut_Click(object sender, RoutedEventArgs e)
        {
            DialogResult result = System.Windows.Forms.MessageBox.Show(
                "Окрасить кнопку в красный цвет?",
                "Сообщение",
                MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
                RemoveItem?.Invoke(this);
        }
    }
}
