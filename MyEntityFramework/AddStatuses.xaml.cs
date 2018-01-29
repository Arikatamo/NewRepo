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
using System.Windows.Shapes;

namespace MyEntityFramework
{
    /// <summary>
    /// Interaction logic for AddStatuses.xaml
    /// </summary>
    public partial class AddStatuses : Window
    {
        public AddStatuses()
        {
            InitializeComponent();
            try
            {
                using (EFContext context = new EFContext())
                {
                    StausesGrid.ItemsSource = context.OrderStatus.ToList();
                }
            }
            catch (Exception)
            {

               // throw;
            }

        }

        private void AddStatus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (EFContext context = new EFContext())
                {
                    var checker = context.OrderStatus.First(x => x.Name == Status.Text);
                    MessageBox.Show(Status.Text + " alredy in base");
                }
            }
            catch (Exception)
            {
                using (EFContext context = new EFContext())
                {
                    context.OrderStatus.Add(new OrderStatus() { Name = Status.Text });
                    context.SaveChanges();
                    StausesGrid.ItemsSource = context.OrderStatus.ToList();

                }

            }
        }

        private void DelleStatuses_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                using (EFContext context = new EFContext())
                {
                    var a = (StausesGrid.SelectedItem as OrderStatus).Name;
                    var checker = context.OrderStatus.First(x=> x.Name == a);
                    context.OrderStatus.Remove(checker);
                    context.SaveChanges();
                    StausesGrid.ItemsSource = context.OrderStatus.ToList();

                }
            }
            catch (Exception)
            {
                using (EFContext context = new EFContext())
                {
                    MessageBox.Show("No selected Value");

                }

            }
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                using (EFContext context = new EFContext())
                {
                    var text = (StausesGrid.SelectedItem as OrderStatus).Name;
                    var a = context.OrderStatus.First(x => x.Name == text).Name = Status.Text;
                    context.SaveChanges();
                    StausesGrid.ItemsSource = context.OrderStatus.ToList();
                }
            }
            catch (Exception)
            {
             

            }
        }

        private void StausesGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                Status.Text = (StausesGrid.SelectedItem as OrderStatus).Name;
            }
            catch (Exception)
            {

            }
        
        }
    }
}
