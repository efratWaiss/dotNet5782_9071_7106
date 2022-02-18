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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        IBL bl;
        public MainWindow()
        {
           
            InitializeComponent();
            bl = BLFactory.GetBL();
        }
      

        private void Button_ClickDrone(object sender, RoutedEventArgs e)
        {

            DronesList DronesList = new DronesList(bl);
            MessageBox.Show("show the drone list");
            DronesList.Show();
        }

       
        private void Button_ClickStation(object sender, RoutedEventArgs e)
        {
            StationList StationList = new StationList(bl);
            MessageBox.Show("show the station list");
            StationList.Show();
        }

        
        private void Button_ClickCustomer(object sender, RoutedEventArgs e)
        {
            CustomersList CustomersList = new CustomersList(bl);
            MessageBox.Show("show the Customer list");
            CustomersList.Show();
        }

        private void Button_ClickParcel(object sender, RoutedEventArgs e)
        {
            ParcelList parcelList = new ParcelList(bl);
            MessageBox.Show("show the Parcel list");
            parcelList.Show();  
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
