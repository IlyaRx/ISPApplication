using ISAuto.Model;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
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

        private void LogInGuest_Click(object sender, RoutedEventArgs e) => LogGuest();
        private void LogGuest()
        {
            try
            {
                ClientWindow client = new ClientWindow();
                client.Show();
                this.Hide();

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Возникли проблемы с соединением", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private void Enter_Click(object sender, RoutedEventArgs e) => LogAdmin();

        private void LogAdmin()
        {
            try
            {
                Employee employee = DatabaseAutoContext.GetContext().Employees.FirstOrDefault(e => e.Login == Login.Text && e.Password == PasswordPlant.Password);
                if (employee == null)
                {
                    System.Windows.Forms.MessageBox.Show("Введён не верный логин или пароль!");
                    PasswordPlant.Password = "";
                    Login.Text = "";
                    LablePassword.Visibility = Visibility.Visible;
                    return;
                }
                else
                {
                    AdminWindow adminWindow = new AdminWindow();
                    adminWindow.Show();
                    this.Hide();
                    return;
                }

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Возникли проблемы с соединением", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void PasswordPlant_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if(PasswordPlant.Password.Length != 0)
                LablePassword.Visibility = Visibility.Collapsed;
            else { LablePassword.Visibility = Visibility.Visible; }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (Login.Text.Length != 0)
                    LogAdmin();
                else
                    LogGuest();
            }
        }
    }
}