using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BuyersApp.Properties;

namespace BuyersApp
{
  public class FirstPageViewModel : ViewModelEntity
    {
        private string[] mCountries;
        public List<string> Titles { get { return new List<string> { "Mr.", "Miss", "Mrs", "Ms.", "Dr.", "Sir", "Lord" }; } }
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

        private string _address1;
        public string Address1
        {
          get { return _address1; }
          set
          {
            _address1 = value;
            NotifyPropertyChanged("Address1");
            SummaryInView.Address1 = _address1;
          }
        }

        private string _town;
        public string Town
        {
          get { return _town; }
          set
          {
            _town = value;
            NotifyPropertyChanged("Town");
            SummaryInView.Town = _town;
          }
        }

        private string _county;
        public string County
        {
          get { return _county; }
          set
          {
            _county = value;
            NotifyPropertyChanged("County");
            SummaryInView.County = _county;
          }
        }

        private string _postCodePart1;
        public string PostCodePart1
        {
          get { return _postCodePart1; }
          set
          {
            _postCodePart1 = value;
            NotifyPropertyChanged("PostCodePart1");
            SummaryInView.PostCodePart1 = _postCodePart1;
          }
        }

        private string _postCodePart2;
        public string PostCodePart2
        {
          get { return _postCodePart2; }
          set
          {
            _postCodePart2 = value;
            NotifyPropertyChanged("PostCodePart2");
            SummaryInView.PostCodePart2 = _postCodePart2;
          }
        }

        private string _email;
        public string Email
        {
          get { return _email; }
          set
          {
            _email = value;
            NotifyPropertyChanged("Email");
            SummaryInView.Email = _email;
          }
        }

        private string _phone;
        public string Phone
        {
          get { return _phone; }
          set
          {
            _phone = value;
            NotifyPropertyChanged("Phone");
            SummaryInView.Phone = _phone;
          }
        }

        private string _mobile;
        public string Mobile
        {
          get { return _mobile; }
          set
          {
            _mobile = value;
            NotifyPropertyChanged("Mobile");
            SummaryInView.Mobile = _mobile;
          }
        }


    }
}
