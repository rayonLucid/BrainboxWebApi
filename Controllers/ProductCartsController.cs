using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using BrainboxWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BrainboxWebApi.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    [ApiController]
    public class ProductCartsController : ControllerBase
  {
    DbConnection db;
    public ProductCartsController(DbConnection _db)
    {
      db = _db;
    }

    [System.Web.Http.HttpGet]
    // Users should be able to view products in their cart
    [Microsoft.AspNetCore.Mvc.Route("GetAllProductsInCart")]
    public IEnumerable<ProductInCart> GetAllProductsInCart()
    {
      var query = (from cart in db.Cart.Where(x =>x.CheckedOut==false).Distinct().ToArray()
                   join prod in db.Product.Distinct().ToArray()
                   on cart.ProductID.Trim() equals prod.ProductID.Trim()
                   select new ProductInCart
                   {
                     ProductName = prod.ProductName,
                     ProductID = prod.ProductID,
                     CartQty = cart.ProductQty,
                     ProductPrice =prod.Price,
                     CartID = cart.CartProductGroupID,
                     RowID =cart.CartRowID,
                     CheckedOut =cart.CheckedOut,
                     Amount =cart.Amount
                   }).ToArray();
                   
     
      return  query;
    }


    [System.Web.Http.HttpGet]
    public string CartProductPriceTotalSum(string cartgroupID)
    {
      var query =  db.Cart.Where(x => x.CartProductGroupID == cartgroupID).ToList();
      var Fquery = query.Sum(x => x.Amount);
      return Fquery.ToString();
    }


    [System.Web.Http.HttpPost]
    public string NewProductInCart(ProductCart productcart)
    {
      string Result = "Product Added Successfully";
      var query = db.Cart.Where(x => x.CartRowID == productcart.CartRowID && x.ProductID == productcart.ProductID).FirstOrDefault();
     //This Query prevents  Users should From adding a product to cart multiple times
      try
      {

        if (query == null)
        {
          if (ModelState.IsValid)
          {
            db.Entry(productcart).State = EntityState.Added;
          }
          db.SaveChanges();
        }else{
          Result = "Product Already Exist on Cart";
         
        }
    
      }
      catch (Exception ex)
      {
        Result= ex.Message;
      }




      return Result;
    }
  }
}