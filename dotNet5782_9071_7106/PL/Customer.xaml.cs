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
    /// Interaction logic for Customer.xaml
    /// </summary>
    public partial class Customer : Window
    {
        IBL bl;
        BO.CustomerToList customer;
        BO.CustomerInParcel customer1;
        public Customer(IBL blTemp)
        {
            InitializeComponent();
            bl = blTemp;

            AddCustomer.Visibility = Visibility.Visible;
            GetCustomer.Visibility = Visibility.Collapsed;
            UpDateCustomer.Visibility = Visibility.Collapsed;
        }

        public Customer(IBL blTemp, BO.CustomerToList customer)
        {
            InitializeComponent();
            bl = blTemp;
            UpDateCustomer.Visibility = Visibility.Visible;
            AddCustomer.Visibility = Visibility.Collapsed;
            GetCustomer.Visibility = Visibility.Collapsed;
            this.customer = customer;
            UpDateCustomer.DataContext = customer;
            ParcelsGet.ItemsSource = bl.GetCustomer(customer.Identity).parcelFromCustomer;
            ParcelsSet.ItemsSource = bl.GetCustomer(customer.Identity).parcelToCustomer;
        }

        public Customer(IBL blTemp, BO.CustomerInParcel c)
        {
            InitializeComponent();
            bl = blTemp;
            UpDateCustomer.Visibility = Visibility.Collapsed;
            AddCustomer.Visibility = Visibility.Collapsed;
            GetCustomer.Visibility = Visibility.Visible;
            this.customer1 = c;
            GetCustomer.DataContext = customer1;
        }
        private void Button_ClickSave(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.AddCustomer(Convert.ToInt32(IdA.Text)
                    , NameA.Text.ToString()
                    , PhoneA.Text.ToString()
                    , Convert.ToDouble(LongitudeA.Text)
                    , Convert.ToDouble(LatitudeA.Text)
                    );
                MessageBox.Show("The customer was successfully added");
                this.Close();
            }
            catch (BO.IdException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_ClickSaveUpdate(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.UpdateCustomerDetails(Convert.ToInt32(IdU.Text), NameU.Text.ToString(), PhoneU.Text.ToString());
                MessageBox.Show("The customer was successfully update");
            }
            catch (BO.IdException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ParcelsGet.Visibility = Visibility.Visible;
        }

        private void Button_Click__ParcelSet(object sender, RoutedEventArgs e)
        {
            ParcelsSet.Visibility = Visibility.Visible;
        }

        //private void Button_Click_ParcelSet(object sender, RoutedEventArgs e)
        //{
        //    ParcelsSet.Visibility = Visibility.Visible;
        //}

        //private void Button_Click__ParcelSet(object sender, RoutedEventArgs e)
        //{

        //}

        //private void Button_ClickClose(object sender, RoutedEventArgs e)
        //{
        //    this.Close();
        //}
    }
}
