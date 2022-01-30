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
    /// Interaction logic for DronesList.xaml
    /// </summary>
    public partial class DronesList : Window
    {
        IBL.BL bLTemp;
        public DronesList(IBL.BL bl)
        {
            InitializeComponent();
            bLTemp = bl;
            StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
            weightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            AllDrone.Visibility = Visibility.Visible;
            AllDrone.ItemsSource = bLTemp.GetListDrone();


        }


        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AllDrone.Visibility = Visibility.Collapsed;
            DronesListViewBy.Visibility = Visibility.Visible;
            if (StatusSelector.SelectedItem != null)
                DronesListViewBy.ItemsSource = bLTemp.GetListByStatus((DroneStatuses)StatusSelector.SelectedItem);
        }



        private void weightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AllDrone.Visibility = Visibility.Collapsed;
            DronesListViewBy.Visibility = Visibility.Visible;
            if (weightSelector.SelectedItem != null)
                DronesListViewBy.ItemsSource = bLTemp.GetListByWeight((WeightCategories)weightSelector.SelectedItem);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Drone Drone = new Drone(bLTemp);
            MessageBox.Show("add drone");
            Drone.Show();
           
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DronesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            if (DronesListViewBy.SelectedValue is DroneToList)
            {
                Drone Drone = new Drone(bLTemp, (DroneToList)DronesListViewBy.SelectedValue);
                MessageBox.Show("show the drone");
                Drone.Show();
               
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            AllDrone.ItemsSource = bLTemp.GetListDrone();
            DronesListViewBy.Visibility = Visibility.Collapsed;
            AllDrone.Visibility = Visibility.Visible;
            weightSelector.SelectedItem = null;
            StatusSelector.SelectedItem = null;
        }

        private void AllDrone_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (AllDrone.SelectedValue is DroneToList)
            {
                Drone Drone = new Drone(bLTemp, (DroneToList)AllDrone.SelectedValue);
                MessageBox.Show("show the drone");
                Drone.Show();
               

            }
        }

        private void AllDrone_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }

}
