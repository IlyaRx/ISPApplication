using ISAuto.Model;
using ISAuto.View.Pages;
using System.Windows.Controls;

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
        }

        private void MainButt_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Type?.Invoke(_typePart.Id);
        }
    }
}
