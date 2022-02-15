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
            GroupingStations.ItemsSource=blTemp.GetListSationByGroup();
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

            if (StationListViewBy.SelectedValue is StationToList)
            {
                Station station = new BO.Station(blTemp, (StationToList)StationListViewBy.SelectedValue);
                MessageBox.Show("show the station");
                Station.Show();

            }
        }
    }

}
