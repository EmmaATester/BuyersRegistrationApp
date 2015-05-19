using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BuyersApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
      private readonly List<Page> mPages = new List<Page> { new FirstPage(), new ProductDetails(), new SummaryDetails() };

        public MainWindow()
        {
            InitializeComponent();
            BackButton.IsEnabled = false;
            PageFrame.Navigate(mPages.Single(p => p.ToString() == "FirstPage"));
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            Page fromPage = (Page)PageFrame.NavigationService.Content;
            if (fromPage.ToString() == "SummaryDetails")
                Close();

            Page toPage = mPages[mPages.IndexOf((Page)PageFrame.NavigationService.Content) + 1];
            PageFrame.Navigate(toPage);
              BackButton.IsEnabled = true;
            PassSummary(fromPage, toPage);

            if (toPage.ToString() != "SummaryDetails")
                return;

            NextButton.Content = "Close";

            CancelButton.Width = 1;
            CancelButton.Visibility = Visibility.Hidden;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (mPages.IndexOf((Page)PageFrame.NavigationService.Content) == 1)
              BackButton.IsEnabled = false;
            Page fromPage = (Page)PageFrame.NavigationService.Content;
            Page toPage = mPages[mPages.IndexOf((Page)PageFrame.NavigationService.Content) - 1];
            PageFrame.Navigate(toPage);
            PassSummary(fromPage,toPage);

            if (fromPage.ToString() != "SummaryDetails")
                return;
            
            NextButton.Content = "Next";
            
            CancelButton.Width = 75;
            CancelButton.Visibility = Visibility.Visible;
        }


      private void PassSummary(Page fromPage, Page toPage)
      {
          Summary summary; 
          switch (fromPage.ToString())
          {
              case "FirstPage":
              FirstPage firstPage = (FirstPage)fromPage;
              summary = (Summary)firstPage.SummaryDataBound.DataContext;
              break;
              case "ProductDetails":
              ProductDetails productDetails = (ProductDetails)fromPage;
              summary = (Summary)productDetails.SummaryDataBound.DataContext;
              break;
              case "SummaryDetails":
              SummaryDetails summaryDetails = (SummaryDetails)fromPage;
              summary = (Summary)summaryDetails.SummaryDataBound.DataContext;
              break;
              default:
              throw  new Exception("Gone wrong in the page horrible logic");
          }

          switch (toPage.ToString())
          {
            case "FirstPage":
              FirstPage firstPage = (FirstPage)toPage;
              Summary firstPageSummary = (Summary)firstPage.SummaryDataBound.DataContext;
              firstPageSummary.Copy(summary);
              firstPage.SummaryDataBound.DataContext = firstPageSummary;
              break;
            case "ProductDetails":
              ProductDetails productDetails = (ProductDetails)toPage;
              Summary productDetailsSummary = (Summary)productDetails.SummaryDataBound.DataContext;
              productDetailsSummary.Copy(summary);
              productDetails.SummaryDataBound.DataContext = productDetailsSummary;
              break;
            case "SummaryDetails":
              SummaryDetails summaryDetails = (SummaryDetails)toPage;
              Summary summaryDetailsSummary = (Summary)summaryDetails.SummaryDataBound.DataContext;
              summaryDetailsSummary.Copy(summary);
              summaryDetails.SummaryDataBound.DataContext = summaryDetailsSummary;
              break;
            default:
              throw new Exception("Gone wrong in the page horrible logic");
          }
      }

    }
}
