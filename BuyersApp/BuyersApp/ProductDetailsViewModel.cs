using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using BuyersApp.Annotations;

namespace BuyersApp
{
    class ProductDetailsViewModel : INotifyPropertyChanged
    {
        private DateTime _selectedDate;


        public event PropertyChangedEventHandler PropertyChanged;

        public DateTime selectedDate
        {
            get { return _selectedDate; }
            set { _selectedDate = value; }
        }

       // int     YearComboBox.SelectedValue = date.Value.Year;
   //    int     DayComboBox.SelectedValue = date.Value.Day;
      // int     MonthComboBox.SelectedValue = date.Value.Month;

        public IEnumerable<string> Categories { get { return new[] { "Music", "book", "China" }; } }
        public string SelectedCategory { get; set; }

        public IEnumerable<int> Years
        {
            get { return Enumerable.Range(1930, 81).ToList(); }
        }
    
    
   

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }



    }
}
