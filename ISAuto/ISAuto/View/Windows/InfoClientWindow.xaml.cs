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
                bitmap.UriSource = new Uri(autoPart.Image, UriKind.Absolute);
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
