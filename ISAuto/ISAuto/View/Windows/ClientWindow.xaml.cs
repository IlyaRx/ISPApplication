using ISAuto.Model;
using ISAuto.View.Controls;
using ISAuto.View.Pages;
using System.Windows;
using System.Windows.Controls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Window = System.Windows.Window;


namespace ISAuto.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        private BasketPage _basketPage;
        private bool _flag = true;

        public ClientWindow()
        {
            InitializeComponent();
            _basketPage = new BasketPage(FrameMain);
            _flag = true;



            DatabaseAutoContext.GetContext().TypeParts.ToList().ForEach(c =>
            {

                TypeControl typeControl = new TypeControl(c);
                typeControl.Type += FuliCatalog;
                ListTypeCatalog.Children.Add(typeControl);

            });

            if(ListTypeCatalog.Children.Count != 0)
                CatalogFrame.Content = new AutoPartsPage(1, _basketPage);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if(_flag) Application.Current.Shutdown();
        }

        private void FuliCatalog(object sender)
        {
            if(sender is int typeId)
                CatalogFrame.Content = new AutoPartsPage(typeId, _basketPage);
        }

        private void Basket_Click(object sender, RoutedEventArgs e)
        {
            FrameMain.NavigationService.Navigate(_basketPage);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Window main = new MainWindow();
            main.Show();
            _flag = false;
            this.Close();
        }
    }
}
