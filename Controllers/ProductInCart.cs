using System;

namespace BrainboxWebApi.Controllers
{
  public class ProductInCart
  {
    public string ProductName { get; set; }
    public string ProductID { get; set; }
    public Nullable<int> CartQty { get; set; }
    public string CartID { get; set; }
    public string RowID { get; set; }
    public Nullable<bool> CheckedOut { get; set; } = false;
    public Nullable<decimal> ProductPrice { get; set; } = 0;
    public Nullable<decimal> Amount { get; set; } = 0;
  }
}