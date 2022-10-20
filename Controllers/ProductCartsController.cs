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
    [Microsoft.AspNetCore.Mvc.Route("CartProductPriceTotalSum")]
    public string CartProductPriceTotalSum(string cartgroupID)
    {
      var query =  db.Cart.Where(x => x.CartProductGroupID == cartgroupID).ToList();
      var Fquery = query.Sum(x => x.Amount);
      return Fquery.ToString();
    }


    [System.Web.Http.HttpPost]
    [Microsoft.AspNetCore.Mvc.Route("NewProductInCart")]
    public string NewProductInCart([FromUri]ProductCart productcart)
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

    [System.Web.Http.HttpDelete]
    //User should be able to Check out product from cart
    [Microsoft.AspNetCore.Mvc.Route("Cart_CheckOut")]
    public string Cart_CheckOut(string cartgroupID)
    {
      string Result = string.Empty;
      var query = db.Cart.Where(x => x.CartProductGroupID == cartgroupID).ToList();

      try
      {
        if (query != null)
        {

          foreach (var item in query)
          {
            item.CheckedOut = true;
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
          }




          Result = "Product Successfully Checked Out";
        }

      }
      catch (Exception ex)
      {
        Result = ex.Message;
      }




      return Result;
    }


    [System.Web.Http.HttpDelete]
    //User should be able to Check out product from cart
    [Microsoft.AspNetCore.Mvc.Route("CartItem_CheckOut")]
    public string CartItem_CheckOut(string RowID)
    {
      string Result = string.Empty;
      var query = db.Cart.Where(x => x.CartRowID == RowID).FirstOrDefault();

      try
      {

          query.CheckedOut = true;

          db.Entry(query).State = EntityState.Modified;
          db.SaveChanges();
          Result = "Product Successfully Checked Out";
        

      }
      catch (Exception ex)
      {
        Result = ex.Message;
      }




      return Result;
    }

    [System.Web.Http.HttpDelete]
    //User should be able to remove product from cart
    [Microsoft.AspNetCore.Mvc.Route("CartItem_Delete")]
    public string CartItem_Delete(string RowID)
    {
      string Result = string.Empty;
      var query = db.Cart.Where(x =>  x.CartRowID== RowID).FirstOrDefault();

      try
      {

        if (query == null)
        {

          string result = string.Format("Item Was not Found Or has been deleted");

          Result = result;
        }
        else
        {
          //	db.Product.Remove(query);
          db.Entry(query).State = EntityState.Deleted;
          db.SaveChanges();
          Result = "Product Successfully Deleted";
        }

      }
      catch (Exception ex)
      {
        Result = ex.Message;
      }




      return Result;
    }


    [System.Web.Http.HttpDelete]
    //User should be able to   Discard cart just in case you dont want any item in the cart and you dont want o keep deleting on item at a time
    [Microsoft.AspNetCore.Mvc.Route("DiscardCart")]
    public string DiscardCart(string cartgroupID)
    {
      string Result = string.Empty;
      var query = db.Cart.Where(x => x.CartProductGroupID == cartgroupID).FirstOrDefault();

      try
      {

        if (query == null)
        {

          string result = string.Format("Cart Item Was not Found Or has been deleted");

          Result = result;
        }
        else
        {
          //	db.Product.Remove(query);
          db.Entry(query).State = EntityState.Deleted;
          db.SaveChanges();
          Result = "Cart Successfully Discarded";
        }

      }
      catch (Exception ex)
      {
        Result = ex.Message;
      }




      return Result;
    }
  }
}