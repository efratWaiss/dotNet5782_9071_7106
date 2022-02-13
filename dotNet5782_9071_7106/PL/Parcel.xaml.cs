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
    /// Interaction logic for Parcel.xaml
    /// </summary>
    public partial class Parcel : Window
    {
        IBL bl;
        int parcelId;
        public Parcel(IBL blTemp)
        {
            bl = blTemp;
            WeightSelectorA.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
            PrioritiesSelectorA.ItemsSource = Enum.GetValues(typeof(BO.ParcelStatsus));
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
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_ClickSaveAdd(object sender, RoutedEventArgs e)
        {
            try
            {
                var parcel= bl.GetParcel(parcelId);
                bl.AddParcel(Convert.ToInt32(SenderIdA.Text)
                    , Convert.ToInt32(TargetIdA.Text)
                    ,(BO.WeightCategories)WeightSelectorA.SelectedItem
                    ,(BO.Priorities)PrioritiesSelectorA.SelectedItem);
                MessageBox.Show("The Parcel was successfully added");
                this.Close();
            }
            catch (BO.IdException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
