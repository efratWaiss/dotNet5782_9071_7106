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
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for ParcelList.xaml
    /// </summary>
    public partial class ParcelList : Window
    {
        IBL blTemp;
        public ParcelList(IBL bl)
        {
            InitializeComponent();
            blTemp = bl;
            AllParcel.ItemsSource = blTemp.GetListParcel();
            StatusSelector.ItemsSource = Enum.GetValues(typeof(ParcelStatsus));
        }

        private void AllDrone_Click(object sender, RoutedEventArgs e)
        {
            AllParcel.ItemsSource = blTemp.GetListParcel();
            AllParcel.Visibility = Visibility.Visible;
            GroupingParcel.Visibility = Visibility.Collapsed;
        }

        private void Button_ClickClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AllDrone_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Parcel parcel = new Parcel(blTemp, ((BO.ParcelToList)AllParcel.SelectedItem).Id);
            parcel.Show();
        }

        private void Button_Click_Group(object sender, RoutedEventArgs e)
        {
            AllParcel.Visibility = Visibility.Collapsed;
            GroupingParcel.Visibility = Visibility.Visible;
            //GroupingParcel.ItemsSource = blTemp.GetListParceleByGroup();
        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //try
            //{
            //    AllParcel.Visibility = Visibility.Collapsed;
            //    GroupingParcel.Visibility = Visibility.Collapsed;
                
            //    if (StatusSelector.SelectedItem != null)
            //    {
            //        //AllParcel.ItemsSource=blTemp
            //    }
            //}
            //catch (BO.IdException ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void Button_ClickAddParcel(object sender, RoutedEventArgs e)
        {
            Parcel parcel = new Parcel(blTemp);
            parcel.Show();
        }
    }
}
