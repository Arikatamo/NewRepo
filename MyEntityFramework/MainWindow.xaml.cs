using ExampleCodeFirst.Entities;
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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            using (EFContext context = new EFContext())
            {

                foreach (var item in context.OrderStatus.ToList())
                {
                    OrderStatuses.Items.Add(item.Name);
                }
               // OrderStatuses.ItemsSource = temp as List<OrderStatus>;
      
             
            }
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
        }


        }

        private void Statuses_Click(object sender, RoutedEventArgs e)
        {
            AddStatuses addStatuses = new AddStatuses();
            addStatuses.ShowDialog();
        }
    }
}
