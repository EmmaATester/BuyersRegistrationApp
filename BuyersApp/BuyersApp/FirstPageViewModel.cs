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

        private string _firstName;
        public string SelectedFirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                NotifyPropertyChanged("SelectedFirstName");
                SummaryInView.FirstName = _firstName;
            }
        }

        private string _lastName;
        public string SelectedLastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                NotifyPropertyChanged("SelectedLastName");
                SummaryInView.LastName = _lastName;
            }
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


        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                NotifyPropertyChanged("SelectedTitle");
                SummaryInView.Title = _title;
            }
        }
    }
}
