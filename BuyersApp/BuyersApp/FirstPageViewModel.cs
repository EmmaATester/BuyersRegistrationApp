using System;
using System.Collections.Generic;
using System.Linq;
using BuyersApp.Properties;

namespace BuyersApp
{
  public class FirstPageViewModel : ViewModelEntity
    {
        private string[] mCountries;
        public IEnumerable<string> Countries { get {return mCountries;} }
    
        public FirstPageViewModel()
        {
            GetCountries();
            SummaryInView = new Summary();
        }

        private string selectedCountry;
        public string SelectedCountry
        {
          get { return selectedCountry; }
          set
          {
            selectedCountry = value;
            NotifyPropertyChanged("SelectedCountry");
            SummaryInView.Country = selectedCountry;
          }
        }

        private void GetCountries()
        {
            string fileContent = Resources.Countries;
            mCountries = fileContent.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToArray();
        }
    }
}
