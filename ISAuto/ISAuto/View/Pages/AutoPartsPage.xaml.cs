using ISAuto.Model;
using ISAuto.View.Controls;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ISAuto.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для AutoPartsPage.xaml
    /// </summary>
    public partial class AutoPartsPage : Page
    {
        private int _typeId;
        private BasketPage _basketPage;
        private bool _flagIsParam;
        private bool _flagInit = false;



        public AutoPartsPage(int typeId, BasketPage basketPage)
        {
            InitializeComponent();
            _flagIsParam = true;
            _typeId = typeId;
            _basketPage = basketPage;
            FullCatalog();
            _flagInit = true;
        }

        public AutoPartsPage()
        {
            InitializeComponent();
            _flagIsParam = false;
            MainList.Columns = 5;
            FullCatalog();
            _flagInit = true;

        }


        private void FullCatalog(bool orderby = false, string text = null)
        {
            MainList.Children.Clear();
            List<PartsInStore> parts = new List<PartsInStore>();

            try
            {
                if (_flagIsParam)
                {
                    DatabaseAutoContext.GetContext().PartsInStores.Include(p => p.AutoPart).Include(p => p.AutoPart.Manufacturer)
                                                       .Where(p => p.AutoPart.TypeId == _typeId && p.Quantity > 0).OrderBy(p => p.AutoPart.Name).ToList()
                                                       .ForEach(p => parts.Add(p));

                    if (orderby)
                        parts = parts.OrderByDescending(p => p.AutoPart.Name).ToList();
                }
                else
                {

                    DatabaseAutoContext.GetContext().PartsInStores.Include(p => p.AutoPart).Include(p => p.AutoPart.Manufacturer)
                                                       .OrderBy(p => p.Quantity).ToList()
                                                       .ForEach(p => parts.Add(p));

                    if (orderby)
                        parts = parts.OrderByDescending(p => p.Quantity).ToList();
                }

                if (text != null && text != "")
                    parts = parts.Where(p => p.AutoPart.Name.ToLower().Contains(text.ToLower())).ToList();

                if (_flagIsParam)
                    parts.ForEach(p => MainList.Children.Add(new PartCatalog(p, _basketPage)));
                else
                    parts.ForEach(p => MainList.Children.Add(new PartCatalogAdmin(p)));

            }catch (Exception ex)
            {
                MessageBox.Show("Ошибка)" + ex.ToString());
            }
        }

        private void TextBoxPoisk_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_flagInit)
            FullCatalog((bool)OrByDisRad.IsChecked, TextBoxPoisk.Text);
        }

        private void OrByRad_Checked(object sender, RoutedEventArgs e)
        {
            if (_flagInit)

                FullCatalog(!(bool)OrByRad.IsChecked, TextBoxPoisk.Text);
        }

        private void OrByDisRad_Checked(object sender, RoutedEventArgs e)
        {
            if (_flagInit)
                FullCatalog((bool)OrByDisRad.IsChecked, TextBoxPoisk.Text);
        }
    }
}
