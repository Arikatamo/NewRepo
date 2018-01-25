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
        }

        private void AddStatus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (EFContext context = new EFContext())
                {
                    context.OrderStatus.Add(new OrderStatus() { Name = Status.Text});
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
