# BrainboxWebApi
The Users of this API will can do the following :
1. Creation of CRUD endpoints for Products
2. Cannot add products with same/similar name to the db
3. Be able to add product to cart
4. Users are able to view products in their cart
5. Be able to see the sum of the cost of all products in their cart
6. Will not be able to add product to cart multiple times
7. Be  able to search for product by name o
8. Be able to remove product from cart
9. Can checkout items Either Single or in Batch
10. Can Discard Cart.

# Note

- Database is created automatically when the web Api is ran for the first time.

# Product Sample Data

{
    "productID": "AVpfHrJ6ilAPnD_01",
    "productName": "Josmo Walking Shoes ",
    "category": "All Men's Shoes",
    "color": "Shady Brown",
    "size": "Size 7. 5",
    "price": 40.89,
    "quantityInStock": 10
  }

  # Cart Sample Data
  {
    "productName": "Josmo 8190 Plain Infant Walking Shoes, Navy - Wide - Size 7. 5",
    "productID": "AVpfHrJ6ilAPnD_xVXOI",
    "cartQty": 1,
    "cartID": "Menshoe",
    "rowID": "1",
    "checkedOut": false,
    "productPrice": 39.89,
    "amount": 39.89
  }
  # New Cart Sample Data
   {
    "productID": "AVpfHrJ6ilAPnD_xVXOI",
    "ProductQty": 1,
    "CartProductGroupID": "Menshoe",
    "checkedOut": false,
    "productPrice": 39.89
  }

# Api Urls Product Information
- GetAllProducts: Displays all products information
  # Sample Url
 https://localhost:44305/Products/GetAllProducts

- GetProductsByNameOrCat: this allows users to get product in formation either by product name or Its category
  # Sample Url
 https://localhost:44305/Products/GetProductsByNameOrCat?value={string}

- NewProduct_Update : Add to or Update  product information

 # Sample Url
 https://localhost:44305/Products/NewProduct_Update

 sample data:
  {
    "productID": "AVpfHrJ6ilAPnD_01",
    "productName": "Josmo Walking Shoes ",
    "category": "All Men's Shoes",
    "color": "Shady Brown",
    "size": "Size 7. 5",
    "price": 40.89,
    "quantityInStock": 10
  }
- Product_Delete: Deletes product from the Product information table
 # Sample Url
 https://localhost:44305/Products/Product_Delete?productID={string}

- -----------------------------------------------------------------------


# Api Urls CART
- GetAllProductsInCart : This displays all items in the cart
 # Sample Url
 https://localhost:44305/ProductCarts/GetAllProductsInCart
 

- GetProductsInCart : This displays all item by the CartItemGroupID
 # Sample Url
 https://localhost:44305/ProductCarts/GetProductsInCart?cartgroupID=Menshoe

- Cart_CheckOut : this checks out all items in a cart (Batch)
 # Sample Url
 https://localhost:44305/ProductCarts/Cart_CheckOut?cartgroupID=Menshoe

- CartItem_CheckOut: This checks out a single item in a cart
 # Sample Url
 https://localhost:44305/ProductCarts/CartItem_CheckOut?RowID={string}

 - CartProductPriceTotalSum : This calculates the sum of all items in a cart.
  # Sample Url
 https://localhost:44305/ProductCarts/CartProductPriceTotalSum?cartgroupID=Menshoe

- NewProductInCart: This is for adding new to product to the cart.
  # Sample Url
 https://localhost:44305/ProductCarts/NewProductInCart

  the following is a sample of data passed this url:
  {
    "productID": "AVpfHrJ6ilAPnD_xVXOI",
    "ProductQty": 1,
    "CartProductGroupID": "Menshoe",
    "checkedOut": false,
    "productPrice": 39.89
  }
- CartItem_Delete: This deletes an item in a cart
  # Sample Url
 https://localhost:44305/ProductCarts/CartItem_Delete?RowID={string}

 - DiscardCart: This deletes all item in a cart
  # Sample Url
 https://localhost:44305/ProductCarts/DiscardCart?cartgroupID={string}




 