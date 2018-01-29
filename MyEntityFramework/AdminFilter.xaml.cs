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
    /// Interaction logic for AdminFilter.xaml
    /// </summary>
    public partial class AdminFilter : Window
    {
        public AdminFilter()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (EFContext context = new EFContext())
            {
                try
                {

                    var a = context.FilterName.First(x => x.Name == FilterName.Text);
                    MessageBox.Show("Alredy in base"); 

                }
                catch (Exception)
                {
                    if (!string.IsNullOrEmpty(FilterName.Text))
                    {
                        context.FilterName.Add(new FilterName() { Name = FilterName.Text });
                        context.SaveChanges();
                    }


                }
            }
        }

        private void AddValue_Click(object sender, RoutedEventArgs e)
        {
            using (EFContext context = new EFContext())
            {
                try
                {
                    var a = context.FilterValue.First(x => x.Name == FilerValueName.Text);
                        MessageBox.Show("Alredy in base");

                }
                catch (Exception)
                {
                    if (!string.IsNullOrEmpty(FilerValueName.Text))
                    {
                        context.FilterValue.Add(new FilterValue() { Name = FilerValueName.Text });
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
