using ISAuto.Model;
using ISAuto.View.Pages;
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

namespace ISAuto.View.Controls
{
    /// <summary>
    /// Логика взаимодействия для PartCatalogAdmin.xaml
    /// </summary>
    public partial class PartCatalogAdmin : UserControl
    {
        private PartsInStore _partsInStore;
        public PartCatalogAdmin(PartsInStore carPartsStor)
        {

            InitializeComponent();
            AutoPart autoPart = carPartsStor.AutoPart;
            _partsInStore = carPartsStor;

            PartName.Text = autoPart.Name;
            PartPrise.Text = carPartsStor.Price.ToString();
            PartQuantity.Text = carPartsStor.Quantity.ToString();


            if (autoPart.Image != null)
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(autoPart.Image, UriKind.Absolute);
                bitmap.EndInit();
                ImageAutoPart.Source = bitmap;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EditPart infoClientWindow = new EditPart(_partsInStore);
            infoClientWindow.ShowDialog();

        }
    }
}
