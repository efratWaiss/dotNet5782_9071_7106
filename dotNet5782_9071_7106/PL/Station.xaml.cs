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
    public partial class Station : Window
    {
        IBL blTemp;
        BO.StationToList s;
        public Station(IBL bl)
        {
            InitializeComponent();
            blTemp = bl;
        }

        public Station(IBL bl, BO.StationToList s)
        {
            InitializeComponent();
            GridUpdate2.DataContext = s;
            blTemp = bl;
            this.s = s;
            GridUpdate2.Visibility = Visibility.Visible;
            GridAdd.Visibility = Visibility.Collapsed;
            AllDrones.ItemsSource = bl.GetStation(s.Id).DronesInCharging;
        }

        private void SaveAndAdd(object sender, RoutedEventArgs e)
        {
            try
            {
                blTemp.AddStation(Convert.ToInt32(Id.Text),
                    Convert.ToString(name.Text),
                    new BO.Location(Convert.ToDouble(longitude.Text), Convert.ToDouble(latitude.Text)),
                    Convert.ToInt32(AvailableChargeSlots));
                MessageBox.Show("The station was successfully added");
                this.Close();
            }
            catch (BO.IdException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateStationDetailsU(object sender, RoutedEventArgs e)
        {
            blTemp.UpdateStationDetails(Convert.ToInt32(IdU.Text), Convert.ToString(NameU.Text), 
                Convert.ToDouble(ChargeSlotsU.Text));
        }
        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void ListDrones(object sender, RoutedEventArgs e)
        {
            try
            {
                //AllDrones.ItemsSource = bl.GetStation(s.Id).DronesInCharging;
                blTemp.PackageCollectionByDrone(bl.GetParcel(parcelId).DroneId);
            }
            catch (BO.IdException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //public AllDrones2_MouseDoubleClick()
        //{

        //}
    }
}
    

