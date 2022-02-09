
using BlApi;
using BO;
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
    /// <summary>
    /// Interaction logic for Drone.xaml
    /// </summary>
    public partial class Drone : Window
    {
        IBL bLTemp;
        public Drone(IBL bl)
        {
            InitializeComponent();
            bLTemp = bl;
            GridAdd.Visibility = Visibility.Visible;
            weightSelectorA.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
            IdStationA.ItemsSource = bLTemp.GetListStation().Select(x => x.Id);

        }

        public Drone(IBL bl, DroneToList d)
        {
            InitializeComponent();
            GridUpdate.Visibility = Visibility.Visible;
            bLTemp = bl;
            Id.Text = Convert.ToString(d.Id);
            Model.Text = d.Model;
            weight.Text = d.MaxWeight.ToString();
            status.Text = d.Status.ToString();
            IdStation.Text = d.ParcelDelivered.ToString();
            battery.Text = Convert.ToString(d.Battery);
            location.Text = Convert.ToString(d.LocationNow);
            if (Convert.ToInt32(battery.Text) != 100 && (BO.DroneStatuses)d.Status == DroneStatuses.Vacant)
            {
                SendDroneToCharging.Visibility = Visibility.Visible;
                SendDroneToCharging.Visibility = Visibility.Visible;

            }
            else if ((BO.DroneStatuses)d.Status == DroneStatuses.Maintenance)
            {
                FreeDrone.Visibility = Visibility.Visible;
            }
            else
            {
                CollectionParcelByDrone.Visibility = Visibility.Visible;
                SupplyParcel.Visibility = Visibility.Visible;
            }



        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                bLTemp.AddDrone(Convert.ToInt32(IdA.Text),
           Convert.ToString(ModelA.Text),
               (BO.WeightCategories)weightSelectorA.SelectedItem,
               Convert.ToInt32(IdStationA.SelectedItem));

                MessageBox.Show("The drone was successfully added");
                this.Close();
            }
            catch (BO.IdException ex)
            {
                MessageBox.Show(ex.Message);
            }

            //    }
            //    else
            //    {

            //        MessageBox.Show("The drone's ID is not Positive");
            //        IdA.Text = null;

            //    }


            {
                //}
                //else
                //{
                //    MessageBox.Show("The drone's ID is Invalid");
                //    IdA.Text = null;
                //}

            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            GridAdd.Visibility = Visibility.Collapsed;
            this.Close();

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            GridUpdate.Visibility = Visibility.Visible;
            this.Close();

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                bLTemp.UpdateNameDrone(Convert.ToInt32(Id.Text), Convert.ToString(Model.Text));
            }
            catch (BO.IdException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            try
            {
                bLTemp.SendDroneToStation(Convert.ToInt32(Id.Text));
            }
            catch (BO.IdException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void FreeDrone_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                /* bLTemp.FreeDrone(Convert.ToInt32(Id.Text),)*///TODO: ךהכניס זמן טעינה בתחנה
            }
            catch (BO.IdException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void SendDroneToShipping_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bLTemp.UpdateParcelToDrone(Convert.ToInt32(Id.Text));
            }
            catch (BO.IdException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void CollectionParcelByDrone_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bLTemp.PackageCollectionByDrone(Convert.ToInt32(Id.Text));

            }
            catch (BO.IdException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SupplyParcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bLTemp.DeliveryOfAParcelByDrone(Convert.ToInt32(Id.Text));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void IdA_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Id.Background = Brushes.Transparent;
            Regex regex = new Regex("[*0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
