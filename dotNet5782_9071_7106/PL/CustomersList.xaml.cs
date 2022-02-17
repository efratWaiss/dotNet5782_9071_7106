using BlApi;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for CustomersList.xaml
    /// </summary>
    public partial class CustomersList : Window
    {
        IBL blTemp;
        public CustomersList(IBL bl)
        {
            InitializeComponent();
            blTemp = bl;
            AllCustomer.ItemsSource = blTemp.GetListCustomer();
        }

        private void AllCustomer_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Customer customer = new Customer(blTemp, (BO.CustomerToList)AllCustomer.SelectedValue);
            customer.Show();

        }

        private void AddCustomerClick(object sender, RoutedEventArgs e)
        {
            Customer customer = new Customer(blTemp);
            customer.Show();
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
