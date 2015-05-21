using System;
using System.IO;

namespace BuyersApp
{
    /// <summary>
    /// Interaction logic for SummaryDetails.xaml
    /// </summary>
    public partial class SummaryDetails
    {
        public override string ToString()
        {
          return "SummaryDetails";
        }
        public SummaryDetails()
        {
            InitializeComponent();
        }

        private void SaveButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
          Summary summary = (Summary)SummaryDetailsInfo.DataContext;
          using (StreamWriter writer = new StreamWriter("RegistrationInfo.csv",false))
          {
              writer.WriteLine("Registration information collected on: " + DateTime.Now);
              writer.WriteLine("Name," + summary.Name);
              writer.WriteLine("Address," + summary.Address);
              writer.WriteLine("Email," + summary.Email);
              writer.WriteLine("Phone," + summary.Phone);
              writer.WriteLine("Mobile," + summary.Mobile);
              writer.WriteLine("Product," + summary.Product);
              writer.WriteLine("Category," + summary.Catagory);
              writer.WriteLine("Price," + summary.PriceWithCurrency);
              writer.WriteLine("DatePurchased" + summary.DatePurchased);
          }
        }
    }
}
