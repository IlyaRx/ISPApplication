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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ISAuto.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditPart.xaml
    /// </summary>
    public partial class EditPart : Window
    {
        private PartsInStore _partsInStore;
        private AutoPart _autoPart;
        public EditPart(PartsInStore partsInStore)
        {
            InitializeComponent();
            _partsInStore = partsInStore;
            _autoPart = partsInStore.AutoPart;

            Manufacturer.ItemsSource = DatabaseAutoContext.GetContext().Manufacturers.Select(a => a.Name).ToList();


            if (_autoPart.Image != null)
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(_autoPart.Image, UriKind.Absolute);
                bitmap.EndInit();
                ImageIcon.Source = bitmap;
            }

            Manufacturer.SelectedItem = _autoPart.Manufacturer.Name;
            Name.Text = _autoPart.Name;
            Prise.Text = partsInStore.Price.ToString();
            Quantity.Text = partsInStore.Quantity.ToString();
            Description.Text = _autoPart.Description;
        }

        private void SaveBut_Click(object sender, RoutedEventArgs e)
        {
            string ManufactName = Manufacturer.SelectedItem.ToString();
            AutoPart autoPart = DatabaseAutoContext.GetContext().AutoParts.First(a => a == _autoPart);
            PartsInStore partsInStore = DatabaseAutoContext.GetContext().PartsInStores.First(a => a == _partsInStore);

            autoPart.Name = Name.Text;
            autoPart.ManufacturerId = DatabaseAutoContext.GetContext().Manufacturers.First(a => a.Name == ManufactName).Id; 
            partsInStore.Price = int.Parse(Prise.Text);
            partsInStore.Quantity = int.Parse(Quantity.Text);
            autoPart.Description = Description.Text;

            DatabaseAutoContext.GetContext().SaveChanges();

            MessageBox.Show("Вы изменили товар");
            this.Close();

        }
    }
}
