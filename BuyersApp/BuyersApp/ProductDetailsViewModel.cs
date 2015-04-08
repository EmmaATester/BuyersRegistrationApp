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
    class ProductDetailsViewModel : INotifyPropertyChanged
    {


        public event PropertyChangedEventHandler PropertyChanged;
     

        public IEnumerable<string> Categories { get { return new[] { "Music", "book", "China" }; } }
        public string SelectedCategory { get; set; }

       

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
