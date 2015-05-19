using System.ComponentModel;

namespace BuyersApp
{
  public class ViewModelEntity : INotifyPropertyChanged
    {
        /// <summary>
        /// The property changed event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private Summary mSummaryInView;
        public Summary SummaryInView
        {
          get { return mSummaryInView; }
          set
          {
            mSummaryInView = value;
            NotifyPropertyChanged("Summary");
          }
        }

        /// <summary>
        /// Raises the property changed event.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        public virtual void NotifyPropertyChanged(string propertyName)
        {
            //  If the event has been subscribed to, fire it.
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
