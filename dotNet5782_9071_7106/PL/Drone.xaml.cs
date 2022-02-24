
using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        DroneToList d;
        DroneInParcel d1;
        DroneInCharging d2;
        BackgroundWorker worker; //field
        public Drone(IBL bl)
        {
            InitializeComponent();
            bLTemp = bl;
            GridUpdate.Visibility = Visibility.Collapsed;
            GridAdd.Visibility = Visibility.Visible;
            GetDroneinParcel.Visibility = Visibility.Collapsed;
            weightSelectorA.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
            IdStationA.ItemsSource = bLTemp.GetListStation().Select(x => x.Id);

        }

        public Drone(IBL bl, DroneToList d)
        {
            InitializeComponent();
            this.d = d;
            bLTemp = bl;
            GridUpdate.Visibility = Visibility.Visible;
            GetDroneinParcel.Visibility = Visibility.Collapsed;
            GridAdd.Visibility = Visibility.Collapsed;
            GetDroneinCharging.Visibility = Visibility.Collapsed;
            GridUpdate.DataContext = d;
            if (d.Battery != 100 && (BO.DroneStatuses)d.Status == DroneStatuses.Vacant)
            {
                SendDroneToCharging.IsEnabled = true;
                SendDroneToShipping.IsEnabled = true;



            }
            else if ((BO.DroneStatuses)d.Status == DroneStatuses.Maintenance)
            {
                FreeDrone.IsEnabled = true;
               
            }

            else
            {
                DeliveryParcel.IsEnabled = true;
                CollectionParcelByDrone.IsEnabled = true;
            }



            if (d.ParcelDelivered != 0)
            {
                DronesParcle.IsEnabled = true;
            }

        }
        public Drone(IBL bl, DroneInParcel d)
        {
            InitializeComponent();
            this.d1 = d;
            bLTemp = bl;
            GridUpdate.Visibility = Visibility.Collapsed;
            GetDroneinParcel.Visibility = Visibility.Visible;
            GridAdd.Visibility = Visibility.Collapsed;
            GetDroneinCharging.Visibility = Visibility.Collapsed;

            GetDroneinParcel.DataContext = d1;


        }
        public Drone(IBL bl, DroneInCharging d)
        {
            InitializeComponent();
            bLTemp = bl;
            d2 = d;
            GridUpdate.Visibility = Visibility.Collapsed;
            GetDroneinParcel.Visibility = Visibility.Collapsed;
            GridAdd.Visibility = Visibility.Collapsed;
            GetDroneinCharging.Visibility = Visibility.Visible;

        }
        private void Button_ClickSave(object sender, RoutedEventArgs e)
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

            {

            }
        }
        private void Button_Click_Close2(object sender, RoutedEventArgs e)
        {


            this.Close();

        }

        private void Button_Click_close1(object sender, RoutedEventArgs e)
        {

            this.Close();

        }

        private void UpdateDronesModel_Click(object sender, RoutedEventArgs e)
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

        private void SendDroneToCharging_Click(object sender, RoutedEventArgs e)
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
                bLTemp.FreeDrone(Convert.ToInt32(Id.Text), 0);
                MessageBox.Show("The drone רelease from the charging station");
            }
            catch (BO.IdException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BO.NotExistException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BO.NotImplementedException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void SendDroneToShipping_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bLTemp.DeliveryOfAParcelByDrone(Convert.ToInt32(Id.Text));
            }
            catch (BO.NotImplementedException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void CollectionParcelByDrone_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bLTemp.CollectionAParcelByDroen(Convert.ToInt32(Id.Text));
                MessageBox.Show("The drone collection the parcel");
            }
            catch (BO.IdException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeliveryParcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bLTemp.DeliveryOfAParcelByDrone(Convert.ToInt32(Id.Text));
                MessageBox.Show("The delivery the parcel");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void IdA_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Id.Background = Brushes.Transparent;
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void GetParcleDroen_Click(object sender, RoutedEventArgs e)
        {

            Parcel Parcel = new Parcel(bLTemp, d.ParcelDelivered);
            MessageBox.Show("show the drone");
            Parcel.Show();

        }

        private void Button_Click_Close3(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public void Simulator_Click(object sender, RoutedEventArgs e)
        {
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.WorkerReportsProgress = true;
            worker.RunWorkerAsync();
            //bLTemp.Simulator();
        }
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            bLTemp.Simulator(d.Id,() => { },() => e.Cancel);
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        }
        void StopSimulator()
        {
            worker.CancelAsync();
        }
    }
}
