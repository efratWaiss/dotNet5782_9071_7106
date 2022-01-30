
using IBL.BO;
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
    /// Interaction logic for Drone.xaml
    /// </summary>
    public partial class Drone : Window
    {
        IBL.BL bLTemp;
        public Drone(IBL.BL bl)
        {
            InitializeComponent();
            bLTemp = bl;
            GridAdd.Visibility = Visibility.Visible;
            weightSelectorA.ItemsSource = Enum.GetValues(typeof(IBL.BO.WeightCategories));
            IdStationA.ItemsSource = bLTemp.GetListStation().Select(x => x.Id);

        }

        public Drone(IBL.BL bl, DroneToList d)
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
            if (Convert.ToInt32(battery.Text) != 100 && (IBL.BO.DroneStatuses)d.Status == DroneStatuses.Vacant)
            {
                SendDroneToCharging.Visibility = Visibility.Visible;
                SendDroneToCharging.Visibility = Visibility.Visible;

            }
            else if ((IBL.BO.DroneStatuses)d.Status == DroneStatuses.Maintenance)
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
                //int g;
                //if (int.TryParse(Id.Text, out g) == true)
                //{
                //    if (Convert.ToInt32(Id.Text) > 0)
                //    {
                bLTemp.AddDrone(Convert.ToInt32(IdA.Text),
           Convert.ToString(ModelA.Text),
               (IBL.BO.WeightCategories)weightSelectorA.SelectedItem,
               Convert.ToInt32(IdStationA.SelectedItem));

                MessageBox.Show("The drone was successfully added");
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
            //    }
            //    else
            //    {

            //        MessageBox.Show("The drone's ID is not Positive");
            //        IdA.Text = null;

            //    }


            //}
            //else
            //{
            //    MessageBox.Show("The drone's ID is Invalid");
            //    IdA.Text = null;
            //}
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try { 
                GridAdd.Visibility = Visibility.Collapsed;
            this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            GridUpdate.Visibility = Visibility.Visible;
            this.Close();

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try {
            bLTemp.UpdateNameDrone(Convert.ToInt32(Id.Text), Convert.ToString(Model.Text));
 }
            catch (Exception ex)
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
            catch(Exception ex)
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
            catch(Exception ex)
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
            catch(Exception ex)
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
            catch(Exception ex)
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
