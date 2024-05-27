using ISAuto.Model;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ISAuto.View.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LogInGuest_Click(object sender, RoutedEventArgs e)
        {
            ClientWindow client = new ClientWindow();
            client.Show();
            this.Hide();
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow();
            adminWindow.Show();
            this.Hide();
            return;


            Employee employee = DatabaseAutoContext.GetContext().Employees.FirstOrDefault(e => e.Login == Login.Text && e.Password == PasswordPlant.Password);
            if (employee == null)
            {
                MessageBox.Show("Введён не верный логин или пароль!");
                PasswordPlant.Password = "";
                Login.Text = "";
            }
            
        }

        private void PasswordPlant_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if(PasswordPlant.Password.Length != 0)
                LablePassword.Visibility = Visibility.Collapsed;
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }
    }
}