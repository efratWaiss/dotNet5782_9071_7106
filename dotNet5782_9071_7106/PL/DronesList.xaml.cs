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
            bLTemp = bl;
            InitializeComponent();
            StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
            weightSelector.ItemsSource= Enum.GetValues(typeof(WeightCategories));



        }

        
        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           DronesListView.ItemsSource = bLTemp.GetListByStatus((DroneStatuses)StatusSelector.SelectedItem);
        }

        private void DronesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void weightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DronesListView.ItemsSource = bLTemp.GetListByWeight((WeightCategories)weightSelector.SelectedItem);
        }
    }

}
