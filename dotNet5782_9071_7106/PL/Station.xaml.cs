using BlApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class Station : Window
    {
        IBL blTemp;
        BO.StationToList s;
        public Station(IBL bl)
        {
            InitializeComponent();
            blTemp = bl;
            GridUpdate2.Visibility = Visibility.Collapsed;
            GridAdd.Visibility = Visibility.Visible;
            drones.Visibility = Visibility.Collapsed;

        }
        public Station(IBL bl, BO.StationToList s)
        {
            InitializeComponent();
            blTemp = bl;
            GridUpdate2.DataContext = s;
            this.s = s;
            GridUpdate2.DataContext = s;
            GridUpdate2.Visibility = Visibility.Visible;
            GridAdd.Visibility = Visibility.Collapsed;
            drones.Visibility = Visibility.Collapsed;
          



        }
        private void SaveAndAdd(object sender, RoutedEventArgs e)
        {
            try
            {
                blTemp.AddStation(Convert.ToInt32(Id.Text),
                    (name.Text).ToString(),
                    new BO.Location(Convert.ToDouble(longitude.Text), Convert.ToDouble(latitude.Text)),
                    Convert.ToInt32(AvailableChargeSlots.Text));
                MessageBox.Show("The station was successfully added");
                this.Close();//יש כאן בעיה בהרצה

            }
            catch (BO.IdException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void LongitudeA_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            longitude.Background = Brushes.Transparent;
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void latitudeA_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            latitude.Background = Brushes.Transparent;
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void UpdateStationDetails(object sender, RoutedEventArgs e)
        {
            try
            {
                blTemp.UpdateStationDetails(s.Id, NameU.Text.ToString(), Convert.ToInt32(ChargeSlotsU.Text));
                MessageBox.Show("The Station was successfully updated");

            }
            catch (BO.IdException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Button_ClickDroneInParcel(object sender, RoutedEventArgs e)
        {

            drones.Visibility = Visibility.Visible;
            drones.ItemsSource = blTemp.GetListDroneInChargingByStation(s.Id);

        }
        private void MouseDoubleClick_DroneInParcel(object sender, MouseButtonEventArgs e)
        {
            Drone drone = new Drone(blTemp, (BO.DroneInCharging)drones.SelectedValue);
            drone.Show();

        }
        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Button_Click_Close1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }

}


