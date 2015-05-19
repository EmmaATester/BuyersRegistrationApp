using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BuyersApp
{
    /// <summary>
    /// Interaction logic for ProductDetails.xaml
    /// </summary>
    public partial class ProductDetails
    {
        public override string ToString()
        {
          return "ProductDetails";
        }
        public ProductDetails()
        {
            InitializeComponent();
        }

        private void DatePicker_OnSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            // ... Get DatePicker reference.
            var picker = sender as DatePicker;

            // ... Get nullable DateTime from SelectedDate.
            if (picker == null)
                return;
            DateTime? date = picker.SelectedDate;
            if (date == null)
            {
                // ... A null object.
                MessageBox.Show("A handled exception just occurred: ", "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                // ... No need to display the time.
                this.Title = date.Value.ToShortDateString();
                YearComboBox.SelectedIndex = YearComboBox.Items.Cast<int>().ToList().IndexOf(date.Value.Year);
                MonthComboBox.SelectedIndex = MonthComboBox.Items.Cast<int>().ToList().IndexOf(date.Value.Month);
                DayComboBox.SelectedIndex = DayComboBox.Items.Cast<int>().ToList().IndexOf(date.Value.Day);
            }
        }
    }
}
