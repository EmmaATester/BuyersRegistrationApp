using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BuyersApp.Annotations;

namespace BuyersApp
{
    class FirstPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string[] m_Countries;

        public IEnumerable<string> Countries { get {return m_Countries;} }
        public string SelectedCountry { get; set; }

        public FirstPageViewModel()
        {
            GetCountries();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void GetCountries()
        {
            string fileContent = Properties.Resources.Countries;
            m_Countries = fileContent.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToArray();

        }
    }
}
