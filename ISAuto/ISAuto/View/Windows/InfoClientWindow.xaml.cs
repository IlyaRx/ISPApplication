using ISAuto.Model;
using System.Windows;
using System.Windows.Media.Imaging;
using System.IO;
namespace ISAuto.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для InfoWindow.xaml
    /// </summary>
    public partial class InfoClientWindow : Window
    {
        public InfoClientWindow(PartsInStore partStore)
        {
            InitializeComponent();

            AutoPart autoPart = partStore.AutoPart;

            if (autoPart.Image != null)
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(Path.GetFullPath(autoPart.Image), UriKind.Absolute);
                bitmap.EndInit();
                ImageIcon.Source = bitmap;
            }

            Name.Text = autoPart.Name;
            Manufacturer.Text = autoPart.Manufacturer.Name;
            Prise.Text = partStore.Price.ToString();
            Quantity.Text = partStore.Quantity.ToString();
            Description.Text = autoPart.Description;
        }
    }
}
