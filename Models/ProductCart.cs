using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrainboxWebApi.Models
{
  public class ProductCart
  {
   
    public string CartProductGroupID { get; set; }
    [Key]
    public string CartRowID { get; set; } = Guid.NewGuid().ToString().Substring(0, 10);
    public string ProductID { get; set; }
    public Nullable<int> ProductQty { get; set; } = 0;
    public Nullable<bool> CheckedOut { get; set; } = false;
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public Nullable<decimal> Amount { get; private set; } 
    public Nullable<decimal> ProductPrice { get; set; } = 0;

  }
}