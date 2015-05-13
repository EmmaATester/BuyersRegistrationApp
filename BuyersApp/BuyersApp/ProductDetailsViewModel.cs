using System;
using System.Collections.Generic;
using System.Linq;

namespace BuyersApp
{
    class ProductDetailsViewModel : ViewModelEntity
    {
        public IEnumerable<Catagories> Categories { get { return LocalDatabaseInteraction.GetCatagories(); } }

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
    }
}
