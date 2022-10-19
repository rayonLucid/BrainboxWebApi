using System;
using System.ComponentModel.DataAnnotations;

namespace BrainboxWebApi.Models
{
  public class ProductInformation
  {
  public string ProductID { get; set; } = Guid.NewGuid().ToString().Substring(0, 10);
    [Key]
    public string ProductName { get; set; }
    public string Category { get; set; }
    public string Color { get; set; }
    public string Size { get; set; }
    public Nullable<decimal> Price { get; set; } = 0;
    public Nullable<int> QuantityInStock { get; set; } = 0;
  }
}