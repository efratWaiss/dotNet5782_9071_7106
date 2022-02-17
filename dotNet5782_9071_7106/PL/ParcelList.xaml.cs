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
            GroupTarget.Visibility = Visibility.Collapsed;
            GroupSender.Visibility = Visibility.Collapsed;

        }

        private void AllDrone_Click(object sender, RoutedEventArgs e)
        {
            AllParcel.ItemsSource = blTemp.GetListParcel();
            AllParcel.Visibility = Visibility.Visible;
            GroupTarget.Visibility = Visibility.Collapsed;
            GroupSender.Visibility = Visibility.Collapsed;
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
        //private void GroupSender_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    Parcel parcel = new Parcel(blTemp, ((BO.ParcelToList)GroupSender).Id);
        //    parcel.Show();
        //}
        //private void GroupTarget_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    Parcel parcel = new Parcel(blTemp, ((BO.ParcelToList)GroupTarget).Id);
        //    parcel.Show();
        //}

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                AllParcel.Visibility = Visibility.Collapsed;
                GroupTarget.Visibility = Visibility.Collapsed;
                GroupSender.Visibility = Visibility.Collapsed;

                if (StatusSelector.SelectedItem != null)
                {
                    //AllParcel.ItemsSource = blTemp.
                }
            }
            catch (BO.IdException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_ClickAddParcel(object sender, RoutedEventArgs e)
        {
            Parcel parcel = new Parcel(blTemp);
            parcel.Show();
        }

        private void Button_ClickGroupSender(object sender, RoutedEventArgs e)
        {
            AllParcel.Visibility = Visibility.Collapsed;
            GroupSender.Visibility = Visibility.Visible;
            GroupTarget.Visibility = Visibility.Collapsed;
            GroupSender.ItemsSource = blTemp.GetListParcelBySenderGroup();
        }

        private void Button_ClickGroupTarget(object sender, RoutedEventArgs e)
        {
            AllParcel.Visibility = Visibility.Collapsed;
            GroupSender.Visibility = Visibility.Collapsed;
            GroupTarget.Visibility = Visibility.Visible;
            GroupTarget.ItemsSource = blTemp.GetListParcelByTargetGroup();
        }

        private void Button_ClickFilter(object sender, RoutedEventArgs e)
        {
            AllParcel.Visibility = Visibility.Visible;
            GroupTarget.Visibility = Visibility.Collapsed;
            GroupSender.Visibility = Visibility.Collapsed;
            AllParcel.ItemsSource = blTemp.GetListParcelByDate(DateSelctorFrom.SelectedDate.Value.Date, DateSelctorUntil.SelectedDate.Value.Date);
         
    }

     
    }
}
