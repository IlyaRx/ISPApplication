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
using System.IO;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.IO;
namespace ISAuto.View.Controls
{
    /// <summary>
    /// Логика взаимодействия для PartCatalog.xaml
    /// </summary>
    public partial class PartCatalog : UserControl
    {
        private PartsInStore _partsInStore;
        private BasketPage _basketPage;
        public PartCatalog(PartsInStore carPartsStor, BasketPage basketPage)
        {

            InitializeComponent();
            AutoPart autoPart = carPartsStor.AutoPart;
            _partsInStore = carPartsStor;
            _basketPage = basketPage;

            PartName.Text = autoPart.Name;
            PartPrise.Text = carPartsStor.Price.ToString() + " ₽";

            if (autoPart.Image != null)
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(Path.GetFullPath(autoPart.Image), UriKind.Absolute);
                bitmap.EndInit();
                ImageAutoPart.Source = bitmap;
            }
        }

        private void AddCart_Click(object sender, RoutedEventArgs e)
        {
            _basketPage.Add(_partsInStore);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InfoClientWindow infoClientWindow = new InfoClientWindow(_partsInStore);
            infoClientWindow.ShowDialog();

        }
    }
}
