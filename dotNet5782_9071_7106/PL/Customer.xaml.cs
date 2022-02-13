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
        public Customer(IBL blTemp)
        {
            InitializeComponent();
            bl = blTemp;

            AddCustomer.Visibility = Visibility.Visible;
        }
        //<TextBox x:Name="Nameu"  Grid.Column="2" HorizontalAlignment="Center" Margin="0,0,0,0" Grid.Row="2" TextWrapping="Wrap" VerticalAlignment="Top" Width="120" />
        //   <TextBox x:Name="PhoneU"  Grid.Column="2" HorizontalAlignment="Center" Margin="0,0,0,0" Grid.Row="3" TextWrapping="Wrap" VerticalAlignment="Top" Width="120" />
        //   <TextBox x:Name="ParcelSendAndprovidedU"  Grid.Column="2" HorizontalAlignment="Center" Margin="0,0,0,0" Grid.Row="4" TextWrapping="Wrap" VerticalAlignment="Top" Width="120" IsReadOnly="True"/>
        //   <TextBox x:Name="ParcelSendAndNotprovidedU"  Grid.Column="2" HorizontalAlignment="Center" Margin="0,0,0,0" Grid.Row="5" TextWrapping="Wrap" VerticalAlignment="Top" Width="120" IsReadOnly="True"/>
        //   <TextBox x:Name="NumberGetListParcelU"  Grid.Column="2" HorizontalAlignment="Center" Margin="0,0,0,0" Grid.Row="6" TextWrapping="Wrap" VerticalAlignment="Top" Width="120" IsReadOnly="True"/>
        //   <TextBox x:Name="NumberParcelTOCustomerU"  Grid.Column="2" HorizontalAlignment="Center" Margin="0,0,0,0" Grid.Row="7" TextWrapping="Wrap" VerticalAlignment="Top" Width="120" IsReadOnly="True"/>

        public Customer(IBL blTemp, BO.CustomerToList customer)
        {
            InitializeComponent();
            bl = blTemp;
            UpDateCustomer.Visibility = Visibility.Visible;
            AddCustomer.Visibility = Visibility.Collapsed;
            this.customer = customer;
            UpDateCustomer.DataContext = customer;
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
                bl.UpdateCustomerDetails(Convert.ToInt32(IdU.Text), NameU.Text.ToString(),PhoneU.Text.ToString());
                MessageBox.Show("The customer was successfully update");
            }
            catch (BO.IdException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //private void Button_ClickClose(object sender, RoutedEventArgs e)
        //{
        //    this.Close();
        //}
    }
}
