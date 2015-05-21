using System;

namespace BuyersApp
{
  public class Summary
  {
    public string ComparedToRelativeRetailPrice;
    public string Country { get; set; }
    public string Currency { get; set; }
    public Catagories Catagory { get; set; }
    public Products Product { get; set; }
    public string PricePaid { get; set; }
    public int SelectedYear { get; set; }
    public int SelectedMonth { get; set; }
    public int SelectedDay { get; set; }
    public string Title { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public string Email { get; set; }
    public string Address1 { get; set; }
    public string Town { get; set; }
    public string County { get; set; }
    public string PostCodePart1 { get; set; }
    public string PostCodePart2 { get; set; }
    public string Phone { get; set; }
    public string Mobile { get; set; }
    public string Address { get { return GetAddress(); } }
    public string Name { get { return GetName(); } }
    public string PriceWithCurrency { get { return GetPriceWithCurrency(); } }
    public string DatePurchased { get { return GetDatePurchased(); } }
    
    public void Copy(Summary summary)
    {
      ComparedToRelativeRetailPrice = summary.ComparedToRelativeRetailPrice;
      Country = summary.Country;
      Currency = summary.Currency;
      Catagory = summary.Catagory;
      Product = summary.Product;
      PricePaid = summary.PricePaid;
      SelectedYear = summary.SelectedYear;
      SelectedMonth = summary.SelectedMonth;
      SelectedDay = summary.SelectedDay;
      Title = summary.Title;
      FirstName = summary.FirstName;
      LastName = summary.LastName;
      Email = summary.Email;
      Address1 = summary.Address1;
      Town = summary.Town;
      County = summary.County;
      PostCodePart1 = summary.PostCodePart1;
      PostCodePart2 = summary.PostCodePart2;
      Phone = summary.Phone;
      Mobile = summary.Mobile;

    }

    private string GetAddress()
    {
      return String.Format("{0},{1},{2},{3},{4}-{5}", Address1, Town, County, Country, PostCodePart1, PostCodePart2);
    }

    private string GetName()
    {
      return String.Format("{0} {1} {2}", Title, FirstName, LastName);
    }

    private string GetPriceWithCurrency()
    {
       return String.Format("{0} {1}", PricePaid, Currency);
    }
    
    private string GetDatePurchased()
    {
      return String.Format("{0}/{1}/{2}", SelectedDay, SelectedMonth, SelectedYear);
    }
    

  }
}
