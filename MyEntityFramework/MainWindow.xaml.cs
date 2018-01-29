using ExampleCodeFirst.Entities;
using MahApps.Metro.Controls;
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

namespace MyEntityFramework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (EFContext context = new EFContext())
                {
                    context.UserProfiles.Add(new UserProfile() { Name = Name.Text, Image = Image.Text, Telephone = Phone.Text });
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {

            }
        }

        private void TabController_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var tab = sender as TabControl;
            var item = tab.SelectedItem as TabItem;
            using (EFContext context = new EFContext())
            {
                if (item.Name.ToString() == "UserList")
                {
                        Data.ItemsSource = context.UserProfiles.ToList();
              
                }
                if (item.Name.ToString() == "OrderStat")
                {

                    OrderS.ItemsSource = context.OrderStatus.ToList();
                
                }
                if (item.Name.ToString() == "Filters")
                {
                    FiltersValueGrid.ItemsSource = context.FilterValue.ToList();
                    FiltersGrid.ItemsSource = context.FilterName.ToList();
                    Products.ItemsSource = context.Products.ToList();
                }
            }


        }

        private void Statuses_Click(object sender, RoutedEventArgs e)
        {
            var checker = sender as Button;
            if (checker.Name == "Statuses")
            {
                AddStatuses addStatuses = new AddStatuses();
                addStatuses.ShowDialog();
            }

            if (checker.Name == "AddSFilters")
            {
                AdminFilter filters = new AdminFilter();
                filters.ShowDialog();
            }

        }

        private void StausesGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {

        }

        private void ProductClick(object sender, RoutedEventArgs e)
        {
            Product prod = new Product();
            prod.ShowDialog();
        }
    }
}
