using ISAuto.Model;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using MessageBox = System.Windows.MessageBox;
using static System.Net.Mime.MediaTypeNames;
using ISAuto.View.Pages;

namespace ISAuto.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditPart.xaml
    /// </summary>
    public partial class EditPart : Window
    {
        private PartsInStore _partsInStore;
        private AutoPart _autoPart;
        private string _selectedFile = null;
        private string _targetFile = null;
        public event Action<object> update;
        private AdminWindow _adminWindow;

        public EditPart()
        {
            InitializeComponent();
            ImageIcon.MouseDown += ImageIcon_MouseDown;
            SaveBut.Content = "Добавить";
            SaveBut.Click += CriateBut_Click;
            FullCombobox();

        }
        public EditPart(PartsInStore partsInStore, AdminWindow adminWindow = null)
        {
            InitializeComponent();
            SaveBut.Click += SaveBut_Click;
            _partsInStore = partsInStore;
            _adminWindow = adminWindow;
            _autoPart = partsInStore.AutoPart;

            FullCombobox();


            if (_autoPart.Image != null)
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(Path.GetFullPath(_autoPart.Image), UriKind.Absolute);
                bitmap.EndInit();
                ImageIcon.Source = bitmap;
            }

            TypePart.SelectedItem = _autoPart.Type.Name;
            Manufacturer.SelectedItem = _autoPart.Manufacturer.Name;
            Name.Text = _autoPart.Name;
            Prise.Text = partsInStore.Price.ToString();
            PurchasePrise.Text = partsInStore.AutoPart.PurchasePrise.ToString();
            Quantity.Text = partsInStore.Quantity.ToString();
            Description.Text = _autoPart.Description;
        }

        private void FullCombobox()
        {
            Manufacturer.ItemsSource = DatabaseAutoContext.GetContext().Manufacturers.Select(a => a.Name).ToList();
            TypePart.ItemsSource = DatabaseAutoContext.GetContext().TypeParts.Select(a => a.Name).ToList();
        }
        private void SaveBut_Click(object sender, RoutedEventArgs e)
        {
            string ManufactName = Manufacturer.SelectedItem.ToString();
            string TypeName = TypePart.SelectedItem.ToString();
            AutoPart autoPart = DatabaseAutoContext.GetContext().AutoParts.First(a => a == _autoPart);
            PartsInStore partsInStore = DatabaseAutoContext.GetContext().PartsInStores.First(a => a == _partsInStore);

            autoPart.Name = Name.Text;
            autoPart.ManufacturerId = DatabaseAutoContext.GetContext().Manufacturers.First(a => a.Name == ManufactName).Id; 
            autoPart.TypeId = DatabaseAutoContext.GetContext().TypeParts.First(a => a.Name == TypeName).Id;
            autoPart.PurchasePrise = int.Parse(PurchasePrise.Text);
            partsInStore.Price = int.Parse(Prise.Text);
            partsInStore.Quantity = int.Parse(Quantity.Text);
            autoPart.Description = Description.Text;

            DatabaseAutoContext.GetContext().SaveChanges();

            MessageBox.Show("Вы изменили товар");
            update?.Invoke(this);
            if(_adminWindow != null)
                _adminWindow.Update();
            this.Close();

        }
        private void CriateBut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string ManufactName = Manufacturer.SelectedItem.ToString();
                string TypeName = TypePart.SelectedItem.ToString();
                AutoPart autoPart = new AutoPart();
                PartsInStore partsInStore = new PartsInStore();

                if (ImageIcon.Tag != null)
                {
                    SaveImage();
                    autoPart.Image = @"..\..\..\Resourse\DvigatelImage\" + ImageIcon.Tag;
                }
                autoPart.Name = Name.Text;
                autoPart.ManufacturerId = DatabaseAutoContext.GetContext().Manufacturers.First(a => a.Name == ManufactName).Id;
                autoPart.TypeId = DatabaseAutoContext.GetContext().TypeParts.First(a => a.Name == TypeName).Id;
                autoPart.PurchasePrise = int.Parse(PurchasePrise.Text);
                autoPart.Description = Description.Text;
                DatabaseAutoContext.GetContext().AutoParts.Add(autoPart);

                partsInStore.AutoPart = autoPart;
                partsInStore.Price = int.Parse(Prise.Text);
                partsInStore.Quantity = int.Parse(Quantity.Text);
                DatabaseAutoContext.GetContext().PartsInStores.Add(partsInStore);


                DatabaseAutoContext.GetContext().SaveChanges();

                MessageBox.Show("Вы создали товар");
                update?.Invoke(this);
                this.Close();
            }
            catch
            {
                MessageBox.Show("Проверьте правильность написания");
            }

        }

        private void ImageIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png) | *.png";
            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFile = openFileDialog.FileName;
                ImageIcon.Tag = Path.GetFileName(openFileDialog.FileName);
                string targetDirectory = Path.GetFullPath(@"..\..\..\Resourse\DvigatelImage"); 
                string targetFile = Path.Combine(targetDirectory, Path.GetFileName(selectedFile));

                _selectedFile = selectedFile;
                _targetFile = targetFile;
            }

            ImageIcon.Source = new BitmapImage(new Uri(Path.GetFullPath(@"..\..\..\Resourse\Icons\галочка.png")));
        }

        private void SaveImage()
        {
            try
            {
                File.Copy(_selectedFile, _targetFile, true);
                MessageBox.Show("Фотография успешно загружена в систему.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке фотографии: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
    }
}
