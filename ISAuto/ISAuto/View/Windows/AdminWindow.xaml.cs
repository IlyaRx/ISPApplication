using ISAuto.Model;
using ISAuto.View.Pages;
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
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {

        private bool _flag = true;
        public AdminWindow()
        {
            InitializeComponent();
            CatalogFrame.Content = new AutoPartsPage();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if(_flag)
                Application.Current.Shutdown();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Window main = new MainWindow();
            main.Show();
            _flag = false;
            this.Close();
        }

        public void Update(object obj = null) { 
            CatalogFrame.Content = new AutoPartsPage();
        } 
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EditPart infoClientWindow = new EditPart();
            infoClientWindow.update += Update;
            infoClientWindow.ShowDialog();
        }
    }
}
