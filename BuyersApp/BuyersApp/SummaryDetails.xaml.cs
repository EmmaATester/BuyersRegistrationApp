using System;
using System.IO;

namespace BuyersApp
{
    /// <summary>
    /// Interaction logic for SummaryDetails.xaml
    /// </summary>
    public partial class SummaryDetails
    {
        private const string cDefaultSaveFileName = "RegistrationInfo.csv";
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
            string savePath = Path.Combine(Directory.GetCurrentDirectory(), cDefaultSaveFileName);
            try
            {
                Summary summary = (Summary)SummaryDetailsInfo.DataContext;
                using (StreamWriter writer = new StreamWriter(savePath, false))
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
                this.SaveButtonLabel.Content = "Saved to '" + savePath + "'";
            }
            catch (Exception exception)
            {
                this.SaveButtonLabel.Content = "Save failed '" + savePath + "' Error:" + exception.Message;
            }
        }
    }
}
