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
    /// Interaction logic for StationList.xaml
    /// </summary>
    public partial class StationList : Window
    {
        
        IBL blTemp;
        public StationList(IBL bl)
        {
            InitializeComponent();
            blTemp = bl;
            All_Stations.ItemsSource = blTemp.GetListStation();
            All_Stations.Visibility = Visibility.Visible;
         

        }
      
        private void GroupStationsByAvailable(object sender, RoutedEventArgs e)
        {
            All_Stations.Visibility = Visibility.Collapsed;        
            GroupingStations.ItemsSource=blTemp.GetListStationByGroup();
            GroupingStations.Visibility= Visibility.Visible; 
        }

        private void ViewAllStations(object sender, RoutedEventArgs e)
        {
            GroupingStations.Visibility=Visibility.Collapsed;
            All_Stations.Visibility = Visibility.Visible;
        }
        private void Add_Station(object sender, RoutedEventArgs e)
        {
            Station Station  = new Station(blTemp);
            MessageBox.Show("add Station");
            Station.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        private void StationListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            Station station = new Station(blTemp, (StationToList)All_Stations.SelectedValue);
            MessageBox.Show("show the station");
            station.Show();


        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

}
