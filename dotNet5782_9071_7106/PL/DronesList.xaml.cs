using BlApi;
using BO;
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
        IBL bLTemp;
        public DronesList(IBL bl)
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
            try
            {
                AllDrone.Visibility = Visibility.Collapsed;
                DronesListViewBy.Visibility = Visibility.Visible;
                if (StatusSelector.SelectedItem != null)
                    DronesListViewBy.ItemsSource = bLTemp.GetListByStatus((DroneStatuses)StatusSelector.SelectedItem);
            }
            catch (BO.IdException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void weightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                AllDrone.Visibility = Visibility.Collapsed;
                DronesListViewBy.Visibility = Visibility.Visible;
                if (weightSelector.SelectedItem != null)
                    DronesListViewBy.ItemsSource = bLTemp.GetListByWeight((WeightCategories)weightSelector.SelectedItem);

            }
            catch (BO.IdException ex)
            {
                MessageBox.Show(ex.Message);
            }
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


        private void AllDrone_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               
                weightSelector.SelectedItem = null;
                StatusSelector.SelectedItem = null; 
                AllDrone.ItemsSource = bLTemp.GetListDrone();
                DronesListViewBy.Visibility = Visibility.Collapsed;
                GroupingDrones.Visibility = Visibility.Collapsed;
                AllDrone.Visibility = Visibility.Visible;
                
            }
            catch (BO.IdException ex)
            {
                MessageBox.Show(ex.Message);
            }


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

        //למה יש כפילות בפונקציות?
        private void DronesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            if (DronesListViewBy.SelectedValue is DroneToList)
            {
                Drone Drone = new Drone(bLTemp, (DroneToList)DronesListViewBy.SelectedValue);
                MessageBox.Show("show the drone");
                Drone.Show();

            }
        }

        private void Button_Click_Group(object sender, RoutedEventArgs e)
        {
            DronesListViewBy.Visibility = Visibility.Collapsed;
            AllDrone.Visibility = Visibility.Collapsed;
            GroupingDrones.Visibility = Visibility.Visible;
            GroupingDrones.ItemsSource = bLTemp.GetListDroneByGroup();

        }
    }

}
