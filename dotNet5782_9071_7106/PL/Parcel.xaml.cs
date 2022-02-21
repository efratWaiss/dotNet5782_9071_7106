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
    /// Interaction logic for Parcel.xaml
    /// </summary>
    public partial class Parcel : Window
    {
        IBL bl;
        int parcelId;
        public Parcel(IBL blTemp)
        {
            InitializeComponent();
            bl = blTemp;
            WeightSelector.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
            PrioritiesSelector.ItemsSource = Enum.GetValues(typeof(BO.Priorities));
            AddParcel.Visibility = Visibility.Visible;
            GridParcel.Visibility = Visibility.Collapsed;
        }
        public Parcel(IBL blTemp, int parcelId)
        {
            InitializeComponent();
            bl = blTemp;
            this.parcelId = parcelId;
            GridParcel.DataContext = bl.GetParcel(parcelId);
            GridParcel.Visibility = Visibility.Visible;
            AddParcel.Visibility = Visibility.Collapsed;
            WeightSelectorU.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
            PrioritiesSelectorU.ItemsSource = Enum.GetValues(typeof(BO.Priorities));
            if (bl.GetParcel(parcelId).DroneId != 0)
            {
                DeleateParcel.IsEnabled = true;
            }
            if (!bl.GetParcel(parcelId).scheduled.Equals(default) && bl.GetParcel(parcelId).PickedUp.Equals(default))
            {
                GetDrone.IsEnabled = true;
                PickedAndDeliver.IsEnabled = true;
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_ClickSaveAdd(object sender, RoutedEventArgs e)
        {
            try
            {

                bl.AddParcel(Convert.ToInt32(SenderIdA.Text)
                    , Convert.ToInt32(TargetIdA.Text)
                    , (BO.WeightCategories)WeightSelector.SelectedItem
                    , (BO.Priorities)PrioritiesSelector.SelectedItem);
                MessageBox.Show("The Parcel was successfully added");
                this.Close();
            }
            catch (BO.IdException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Button_ClickDeleateParcel(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.DeleteParcel(parcelId);
            }
            catch (BO.IdException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_UpdateParcelToDrone(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.UpdateParcelToDrone(parcelId);
                MessageBox.Show("The parcel was successfully updated");
            }
            catch (BO.IdException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BO.NotExistException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_Target(object sender, RoutedEventArgs e)
        {
            Customer customer = new Customer(bl, bl.GetParcel(parcelId).Target);
            customer.Show();
        }

        private void Button_Click_Sender(object sender, RoutedEventArgs e)
        {
            Customer customer = new Customer(bl,bl.GetParcel(parcelId).Sender);
            customer.Show();
        }

        private void Button_Click_Drone(object sender, RoutedEventArgs e)
        {
            Drone drone = new Drone(bl, bl.GetParcel(parcelId).DroneInParcel);
            drone.Show();
           
    }

        private void Button_Click_PickedAndDeliver(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.PackageCollectionByDrone(bl.GetParcel(parcelId).DroneId);
            }
            catch (BO.IdException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void PreviewTextInput_SenderIdA(object sender, TextCompositionEventArgs e)
        {
            SenderIdA.Background = Brushes.Transparent;
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void PreviewTextInput_TargetIdA(object sender, TextCompositionEventArgs e)
        {
            TargetIdA.Background = Brushes.Transparent;
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_UpdateParcelDetailes(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.UpdateParcelDetails(parcelId, (BO.WeightCategories)WeightSelectorU.SelectedItem, (BO.Priorities)PrioritiesSelectorU.SelectedItem);
            }
            catch (BO.IdException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
