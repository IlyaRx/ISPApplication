using ISAuto.Model;
using System.IO;
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
            DirectoryResourse();
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
            System.Windows.Application.Current.Shutdown();
        }

        private void DirectoryResourse()
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            string sourceDirectory = System.IO.Path.GetFullPath(@"..\..\..\Resourse");

            string destinationDirectory = @"C:\Resourse";

            if (Directory.Exists(destinationDirectory))
                return;
            if (Directory.Exists(sourceDirectory))
            {
                Directory.CreateDirectory(destinationDirectory);
                CopyDirectory(sourceDirectory, destinationDirectory);
            }
        }

        private void CopyDirectory(string sourceDir, string destinationDir)
        {
            foreach (string dirPath in Directory.GetDirectories(sourceDir, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(sourceDir, destinationDir));
            }

            foreach (string newPath in Directory.GetFiles(sourceDir, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(sourceDir, destinationDir), true);
            }
        }
    }
}