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
using Microsoft.Extensions.Configuration;

namespace BrainboxWebApi.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
				DbConnection db = new DbConnection();
			
				public ProductsController(DbConnection _db)
				{
			//			var configuration = new HttpConfiguration();
			//			Request = new HttpRequestMessage();
			//Request.SetConfiguration(configuration);
						db = _db;
					
				}


				[System.Web.Http.HttpGet]

				// Retrieves All Products
				[Microsoft.AspNetCore.Mvc.Route("GetAllProducts")]
				public IEnumerable<ProductInformation> GetAllProducts()
				{
					
						var query = db.Product.Distinct().ToArray();
						if (query.Count() ==0) {
							return Enumerable.Empty<ProductInformation>();
							//	return Request.CreateResponse(HttpStatusCode.NotFound, "Products Not Available");
						}
						//	return Request.CreateResponse(HttpStatusCode.OK, query);
						return query;
				}
				[System.Web.Http.HttpGet]
				//User should be able to search for product by name or category
				[Microsoft.AspNetCore.Mvc.Route("GetProductsByNameOrCat")]
				public IEnumerable<ProductInformation> GetProductsByNameOrCat(string value)
				{
						var query = db.Product.Where(x=>x.ProductName == value || x.Category==value).Distinct().ToArray();
						if (query.Count() == 0)
						{
								return Enumerable.Empty<ProductInformation>();
								//	return Request.CreateResponse(HttpStatusCode.NotFound, "Products Not Found");
						}
						return  query;
				}
				[System.Web.Http.HttpPost]
				[Microsoft.AspNetCore.Mvc.Route("NewProduct_Update")]
				public string NewProduct_Update(ProductInformation product)
				{
			string	Result =string.Empty;
						var query = db.Product.Where(x => x.ProductID == product.ProductID && x.ProductName == product.ProductName).FirstOrDefault();
						// this search will prevent		Users from  adding products with same / similar name to the db
						try {

								if (query == null)
								{
										if (ModelState.IsValid)
										{
												db.Product.Add(product);
												db.SaveChanges();
												Result="Product Successfully Created";
										}else{
										Result ="Model is not Valid Please check you input parameters";
										}
										
								}
								else
								{
										// update product information
										query.Price = product.Price;
										query.Color = product.Color;
										query.Category = product.Category;
										query.Size = product.Size;
										query.QuantityInStock = product.QuantityInStock;
										//		db.Product.Update(query);
										db.Entry(query).State = EntityState.Modified;
										db.SaveChanges();
	           Result="Product Successfully Updated";
								}

						}
						catch (Exception ex) {
							
						Result =ex.Message;
						}




						return Result;
				}

				[System.Web.Http.HttpPost]
				//User should be able to remove product from cart
				[Microsoft.AspNetCore.Mvc.Route("Product_Delete")]
				public HttpResponseMessage Product_Delete(string productID)
				{
						var query = db.Product.Where(x => x.ProductID == productID).FirstOrDefault();
				
						try
						{

								if (query == null)
								{

										//string result = string.Format("Product with the ID {0} Was not Found", productID);
									
										return new HttpResponseMessage(HttpStatusCode.NotFound);
								}
								else
								{
										//	db.Product.Remove(query);
										db.Entry(query).State = EntityState.Deleted;
										db.SaveChanges();

								}

						}
						catch (Exception ex)
						{
							return new HttpResponseMessage(HttpStatusCode.ExpectationFailed);
						}




						return new HttpResponseMessage(HttpStatusCode.OK);
				}
		}
}