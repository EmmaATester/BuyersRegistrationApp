using System;
using System.Collections.Generic;
using System.Linq;

namespace BuyersApp
{
    class ProductDetailsViewModel : ViewModelEntity
    {
        private const string cDefaultCurrency = "United States Dollar";
        private const string cDefaultCurrencyCode = "USD"; 

        public IEnumerable<Catagories> Categories { get { return LocalDatabaseInteraction.GetCatagories(); } }

        public List<string> Currencies { get { return YahooFinanceCurrencyConverter.GetCurrencyCodeDictionary().Values.ToList(); } }

        private Catagories _selectedCategory;
        public Catagories SelectedCategory 
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                Products = LocalDatabaseInteraction.GetProductsByCatagoryId(SelectedCategory.Id);
                NotifyPropertyChanged("SelectedCategory");
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

        // int     YearComboBox.SelectedValue = date.Value.Year;
        // int     DayComboBox.SelectedValue = date.Value.Day;
        // int     MonthComboBox.SelectedValue = date.Value.Month;

        public IEnumerable<int> Years
        {
            get { return Enumerable.Range(1930, 81).ToList(); }
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
          var pricePaidAsDefaultCurrency = cDefaultCurrency != SelectedCurrency ? ConvertPricePaidToDefaultCurrency(cDefaultCurrencyCode, threeDidigtCodeForSelectedCurrency, price) : price;

          ComparedToRRP = pricePaidAsDefaultCurrency > SelectedProduct.RRP
            ? String.Format("You paid over the RRP by {0} {1}", pricePaidAsDefaultCurrency - SelectedProduct.RRP, cDefaultCurrencyCode)
            : String.Format("You paid under the RRP by {0} {1}", SelectedProduct.RRP - pricePaidAsDefaultCurrency, cDefaultCurrencyCode);
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
