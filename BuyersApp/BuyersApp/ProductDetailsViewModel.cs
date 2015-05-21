using System;
using System.Collections.Generic;
using System.Linq;

namespace BuyersApp
{
    class ProductDetailsViewModel : ViewModelEntity
    {
        private const string cDefaultCurrency = "United States Dollar";
        private const string cDefaultCurrencyCode = "USD";
        public IEnumerable<Catagories> Categories { get { return LocalDatabaseInteraction.GetCategories(); } }
        public List<string> Currencies { get { return YahooFinanceCurrencyConverter.GetCurrencyCodeDictionary().Values.ToList(); } }

        private Catagories mSelectedCategory;
        public Catagories SelectedCategory 
        {
            get { return mSelectedCategory; }
            set
            {
                mSelectedCategory = value;
                Products = LocalDatabaseInteraction.GetProductsByCategoryId(SelectedCategory.Id);
                NotifyPropertyChanged("SelectedCategory");
                SummaryInView.Catagory = mSelectedCategory; 
            }
        }

        private List<Products> _Products;
        public List<Products> Products
        {
            get { return _Products; }
            set
            {
                _Products = value;
                NotifyPropertyChanged("Products");
            }
        }

        private Products _selectedProduct;
        public Products SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                NotifyPropertyChanged("SelectedProduct");
                SummaryInView.Product = _selectedProduct; 
            }
        }

        private string _pricePaid;
        public string PricePaid
        {
          get { return _pricePaid; }
          set
          {
            _pricePaid = value;
            NotifyPropertyChanged("PricePaid");
            CalculatePricePaidVsRelativeRetailPrice();
            SummaryInView.PricePaid = _pricePaid; 
          }
        }

        private string _selectedCurrency;
        public string SelectedCurrency
        {
          get { return _selectedCurrency; }
          set
          {
            _selectedCurrency = value;
            NotifyPropertyChanged("SelectedCurrency");
            CalculatePricePaidVsRelativeRetailPrice();
            SummaryInView.Currency = _selectedCurrency;
          }
        }

        private string _comparedToRRP;
        public string ComparedToRRP
        {
          get { return _comparedToRRP; }
          set
          {
            _comparedToRRP = value;
            NotifyPropertyChanged("ComparedToRRP");
            SummaryInView.ComparedToRelativeRetailPrice = _comparedToRRP;
          }
        }

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                NotifyPropertyChanged("SelectedDate");
            }
        }

        private int _selectedYear;
        public int SelectedYear
        {
          get { return _selectedYear; }
          set
          {
            _selectedYear = value;
            NotifyPropertyChanged("SelectedYear");
            SummaryInView.SelectedYear = _selectedYear;
          }
        }

        private int _selectedMonth;
        public int SelectedMonth
        {
          get { return _selectedMonth; }
          set
          {
            _selectedMonth = value;
            NotifyPropertyChanged("SelectedMonth");
            SummaryInView.SelectedMonth = _selectedMonth;
          }
        }

        private int _selectedDay;
        public int SelectedDay
        {
          get { return _selectedDay; }
          set
          {
            _selectedDay = value;
            NotifyPropertyChanged("SelectedDay");
            SummaryInView.SelectedDay = _selectedDay;
          }
        }

        public IEnumerable<int> Years
        {
            get { return Enumerable.Range(1930, 100).ToList(); }
        }
        public IEnumerable<int> Months
        {
            get { return Enumerable.Range(1, 12).ToList(); }
        }
        public IEnumerable<int> Days
        {
            get { return Enumerable.Range(1, 31).ToList(); }
        }

        public ProductDetailsViewModel()
        {
            SummaryInView = new Summary();        
        }

        private void CalculatePricePaidVsRelativeRetailPrice()
        {
            if (null == SelectedProduct || null == SelectedCurrency || null == PricePaid)
                return;

            decimal price;
            if (!decimal.TryParse(PricePaid, out price))
            {
                ComparedToRRP = "Cannot parse price as decimal";
                return;
            }
          
            if(null == SelectedProduct.RRP)
            {
                ComparedToRRP = "Product has no RRP!";
                return;
            }

            string threeDidigtCodeForSelectedCurrency = GetThreeDidigtCodeForSelectedCurrency(SelectedCurrency);
            try
            {
                var pricePaidAsDefaultCurrency = cDefaultCurrency != SelectedCurrency
                  ? ConvertPricePaidToDefaultCurrency(cDefaultCurrencyCode, threeDidigtCodeForSelectedCurrency, price)
                  : price;
                ComparedToRRP = pricePaidAsDefaultCurrency > SelectedProduct.RRP
                  ? String.Format("You paid over the RRP by {0} {1}", pricePaidAsDefaultCurrency - SelectedProduct.RRP, cDefaultCurrencyCode)
                  : String.Format("You paid under the RRP by {0} {1}", SelectedProduct.RRP - pricePaidAsDefaultCurrency, cDefaultCurrencyCode);
            }
            catch (Exception)
            {
              ComparedToRRP = "Failed to contact Yahoo Finance";
            }
            
          }

        private string GetThreeDidigtCodeForSelectedCurrency(string selectedCurrency)
        {
            if(!YahooFinanceCurrencyConverter.GetCurrencyCodeDictionary().ContainsValue(selectedCurrency))
                throw new Exception("Can't find selected currency in dictionary");

            return YahooFinanceCurrencyConverter.GetCurrencyCodeDictionary().Single(c => c.Value == selectedCurrency).Key;
        }

        private decimal ConvertPricePaidToDefaultCurrency(string defaultCurrencycode, string threeDidigtCodeForCurrency, decimal toConvert)
        {
            YahooFinanceCurrencyConverter yahooFinanceCurrencyConverter = new YahooFinanceCurrencyConverter();
            return (decimal)yahooFinanceCurrencyConverter.Convert(threeDidigtCodeForCurrency, defaultCurrencycode, (float)toConvert);
        }
    }
}
