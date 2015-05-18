using System;
using System.Collections.Generic;
using System.Linq;
using BuyersApp.Properties;

namespace BuyersApp
{
    class FirstPageViewModel : ViewModelEntity
    {
        private string[] m_Countries;

        public IEnumerable<string> Countries { get {return m_Countries;} }
        public string SelectedCountry { get; set; }

        public FirstPageViewModel()
        {
            GetCountries();
        }

        private void GetCountries()
        {
            string fileContent = Resources.Countries;
            m_Countries = fileContent.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToArray();
        }
    }
}
