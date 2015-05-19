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
    }
  }
}
