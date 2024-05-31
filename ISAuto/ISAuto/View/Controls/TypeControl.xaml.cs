using ISAuto.Model;
using ISAuto.View.Pages;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ISAuto.View.Controls
{
    /// <summary>
    /// Логика взаимодействия для TypeControl.xaml
    /// </summary>
    public partial class TypeControl : UserControl
    {
        private TypePart _typePart;
        public event Action<object> Type;

        public TypeControl(TypePart typePart)
        {
            InitializeComponent();
            Namecat.Text = typePart.Name;
            _typePart = typePart;

            if (typePart.Imade != null)
            {

                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(typePart.Imade, UriKind.Absolute);
                bitmap.EndInit();
                ImageTypePart.Source = bitmap;
            }

        }

        private void MainButt_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Type?.Invoke(_typePart.Id);
        }
    }
}
