using ISAuto.Model;
using ISAuto.View.Windows;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.IO;

namespace ISAuto.View.Controls
{
    /// <summary>
    /// Логика взаимодействия для PartCatalogAdmin.xaml
    /// </summary>
    public partial class PartCatalogAdmin : UserControl
    {
        private PartsInStore _partsInStore;
        private AutoPart _autoPart;
        public PartCatalogAdmin(PartsInStore carPartsStor)
        {

            InitializeComponent();
            _autoPart = carPartsStor.AutoPart;
            _partsInStore = carPartsStor;

            PartName.Text = _autoPart.Name;
            PartPrise.Text = carPartsStor.Price.ToString() + " ₽";
            PartQuantity.Text = carPartsStor.Quantity.ToString() + " шт.";

            

            if (_autoPart.Image != null)
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(Path.GetFullPath(_autoPart.Image), UriKind.Absolute);
                bitmap.EndInit();
                ImageAutoPart.Source = bitmap;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            var editPart= new EditPart(_partsInStore, Window.GetWindow(this) as AdminWindow);
            editPart.ShowDialog();
        }

        private void ButDelete_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show(
                "Вы точно хотите удалить товар?",
                "Сообщение",
                System.Windows.Forms.MessageBoxButtons.YesNo);

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                var partsInStore = DatabaseAutoContext.GetContext().PartsInStores.First(p => p == _partsInStore);
                var autoParts = DatabaseAutoContext.GetContext().AutoParts.First(a => a == _autoPart);

                DatabaseAutoContext.GetContext().PartsInStores.Remove(partsInStore);
                DatabaseAutoContext.GetContext().AutoParts.Remove(autoParts);

                DatabaseAutoContext.GetContext().SaveChanges();

                if (Window.GetWindow(this) is AdminWindow adminWindow)
                    adminWindow.Update();
            }
        }
    }
}
