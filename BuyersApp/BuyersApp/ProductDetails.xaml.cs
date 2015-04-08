using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BuyersApp
{
    /// <summary>
    /// Interaction logic for ProductDetails.xaml
    /// </summary>
    public partial class ProductDetails : Page
    {

        
        public ProductDetails()
        {
            InitializeComponent();
        }

        private void DatePicker_OnSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
           

            // ... Get DatePicker reference.
            var picker = sender as DatePicker;

            // ... Get nullable DateTime from SelectedDate.
            DateTime? date = picker.SelectedDate;
            if (date == null)
            {
                // ... A null object.
                MessageBox.Show("A handled exception just occurred: " , "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                // ... No need to display the time.
                this.Title = date.Value.ToShortDateString();
            }
            int index = 0;
            foreach (var item in YearComboBox.Items.Cast<int>())
            {
                if (item.Equals(date.Value.Year))
                {
                    index = YearComboBox.Items.IndexOf(item);
                }
            }
            YearComboBox.SelectedIndex = index;


            //       DayComboBox.SelectedValue = date.Value.Day;
            //   MonthComboBox.SelectedValue = date.Value.Month;


        }
    }
}
