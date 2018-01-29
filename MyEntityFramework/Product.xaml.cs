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
    /// Interaction logic for Product.xaml
    /// </summary>
    public partial class Product : Window
    {
        public Product()
        {
            InitializeComponent();
            try
            {
                using (EFContext context = new EFContext())
                {
                    FilterName.ItemsSource = context.FilterName.Select(x => x.Name).ToList();
                    FilterValue.ItemsSource = context.FilterValue.Select(x => x.Name).ToList();
                    Category.ItemsSource = context.Categories.Select(x => x.Name).ToList();
                    SubCategory.ItemsSource = context.Categories.Select(x => x.Name).ToList();
                    ProductGrid.ItemsSource = context.Products.ToList();
                }
                
            }
            catch (Exception)
            {

              //  throw;
            }
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (EFContext context = new EFContext())
                {
                    context.Products.Add(new ExampleCodeFirst.Entities.Product() { Name = ProductName.Text, Price = int.Parse(ProductPrice.Text), Quantity = int.Parse(ProductQuantity.Text), DateCreate = new DateTime(), CategoriesID = context.Categories.First(x => x.Name == Category.SelectedValue.ToString()) });
                }

            }
            catch (Exception)
            {

                //  throw;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (EFContext context = new EFContext())
                {
                    if (SubCategory.SelectedIndex>=0)
                    {
                        var a = context.Categories.First(x => x.Name == SubCategory.SelectedValue.ToString());

                        context.Categories.Add(new Categories() { Name = CategoryName.Text, IsHead = (bool)IsHeadCheker.IsChecked, ParentID = a.Id });
                    }
                    else
                    {
                        context.Categories.Add(new Categories() { Name = CategoryName.Text, IsHead = (bool)IsHeadCheker.IsChecked});

                    }


                    context.SaveChanges();
                    Category.ItemsSource = context.Categories.Select(x => x.Name).ToList();
                    SubCategory.ItemsSource = context.Categories.Select(x => x.Name).ToList();
                }
            }
            catch (Exception)
            {

                //throw;
            }
        }
    }
}
