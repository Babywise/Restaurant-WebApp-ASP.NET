using Meal_Ordering_Class_Library.Entities;
using Meal_Ordering_WebApp.DataAccess;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
/*
namespace Meal_Ordering_API.Controllers
{
    public class StoreManagementController : Controller
    {
        private MealOrderingAPIContext _dbContext;

        /// <summary>
        /// Home controller handles all home related functions with the home page.
        /// </summary>
        /// <param name="dbContext"></param>
        public StoreManagementController(MealOrderingAPIContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// <summary>
        /// Adds a product to the store. Takes the ApiKey and a product
        /// Type : POST
        /// </summary>
        /// <param name="product"></param>
        /// <param name="ApiKey"></param>
        /// <returns></returns>
        [HttpPost("/API/V1/StoreManagement/Add")]
        public string Add([FromBody]Product product, [FromHeader] Guid ApiKey)
        {
            bool check = true;
            string message = "";
            if (product != null && ApiKey != Guid.Empty && product.Name != null && product.Cost > 0 && product.CategoryId != null)
            {
                List<Account> accounts = _dbContext.Account.Where(a => a.ApiKey == ApiKey).ToList();
                List<Product> products = _dbContext.Product.ToList().OrderByDescending(a => a.Id).ToList();
                if (accounts.Count > 0) // ensure api key is valid
                {
                    // ---- Validation ------
                    // validate account type
                    Account account = accounts[0];
                    switch (account.AccountType) // ensure it is the restaurant adding an item to their inventory
                    {
                        case "Restaurant":
                            break;
                        default:
                            check = false;
                            message = "Invalid Account Type";
                            break;
                    }

                    //set product ID
                    product.StoreId = account.Id; // set store it belongs to
                    if (products.Count > 0) // set id
                    {
                        product.Id = products[0].Id + 1;
                    }
                    else
                    {
                        product.Id = 0;
                    }
                    product.Available = true;

                    //validate category
                    bool exists = _dbContext.Category.Where(d => d.Id == product.Id).Count() > 0;
                    if (!exists)
                    {
                        message = "Product Category does not exist";
                        check = false;
                    }
                    if(product.Name.Length<4 || product.Name.Length > 20)
                    {
                        message = "Name is too short or too long";
                        check = false;
                    }

                    if(check) // product is validated 
                    {
                        _dbContext.Product.Add(product);
                        _dbContext.SaveChanges();
                        message = "Product added to inventory";
                    }

                    // ---- Validation ------


                    // Set Headers
                    Response.Headers.UserAgent = "API";
                    Response.Headers["Message"] = message;


                    //return
                    return "";
                }
                else // bad api key
                {
                    Response.Headers.UserAgent = "API";
                    Response.Headers["Message"] = "Invalid ApiKey";


                    //return
                    return "";
                }
            }
            else // objects are null
            {
                Response.Headers.UserAgent = "API";
                Response.Headers["Message"] = "Product or ApiKey or name or categoryId is null or cost is lower then 0";
                //return
                return "";
            }
        }



        /// <summary>
        /// Removes an item from the store.
        /// Type : POST
        /// </summary>
        /// <param name="product"></param>
        /// <param name="ApiKey"></param>
        /// <returns></returns>
        [HttpPost("/API/V1/StoreManagement/Remove")]
        public string Remove([FromBody] Product product, [FromHeader] Guid ApiKey)
        {
            bool check = true;
            string message = "";
            if (product != null && ApiKey != Guid.Empty && product.Name != null && product.Cost > 0 && product.CategoryId != null)
            {
                List<Account> accounts = _dbContext.Account.Where(a => a.ApiKey == ApiKey).ToList();
                if (accounts.Count > 0) // ensure api key is valid
                {
                    try
                    {
                        _dbContext.Product.Remove(product);
                        _dbContext.SaveChanges();
                        message = "Product removed to inventory";
                    }
                    catch(Exception ex)
                    {
                        TextWriterTraceListener logListener = new TextWriterTraceListener("./Log.txt", "Logs");
                        Trace.Listeners.Add(logListener);
                        Trace.WriteLine(ex.Message);
                        Trace.Close();
                        check = false;
                        message = "500 Internal Error";
                    }

                


                    // Set Headers
                    Response.Headers.UserAgent = "API";
                    Response.Headers["Message"] = message;


                    //return
                    return "";
                }
                else // bad api key
                {
                    Response.Headers.UserAgent = "API";
                    Response.Headers["Message"] = "Invalid ApiKey";


                    //return
                    return "";
                }
            }
            else // objects are null
            {
                Response.Headers.UserAgent = "API";
                Response.Headers["Message"] = "Product or ApiKey or name or categoryId is null or cost is lower then 0";
                //return
                return "";
            }
        }


        /// <summary>
        /// Edits a product for the store.
        /// Type: POST
        /// </summary>
        /// <param name="product"></param>
        /// <param name="ApiKey"></param>
        /// <returns></returns>
        [HttpPost("/API/V1/StoreManagement/Edit")]
        public string Edit([FromBody] Product product, [FromHeader] Guid ApiKey)
        {
            bool check = true;
            string message = "";
            if (product != null || ApiKey != Guid.Empty || product.Name != null || product.Cost > 0 || product.CategoryId != null )
            {
                List<Account> accounts = _dbContext.Account.Where(a => a.ApiKey == ApiKey).ToList();
                if (accounts.Count > 0) // ensure api key is valid
                {
                    // ---- Validation ------
                    // validate account type
                    Account account = accounts[0];
                    switch (account.AccountType) // ensure it is the restaurant adding an item to their inventory
                    {
                        case "Restaurant":
                            break;
                        default:
                            check = false;
                            message = "Invalid Account Type";
                            break;
                    }


                    //validate category
                    bool exists = _dbContext.Category.Where(d => d.Id == product.Id).Count() > 0;
                    if (!exists)
                    {
                        message = "Product Category does not exist";
                        check = false;
                    }
                    if (product.Name.Length < 4 || product.Name.Length > 20)
                    {
                        message = "Name is too short or too long";
                        check = false;
                    }

                    //set product ID
                    List<Product> products = _dbContext.Product.Where(a => a.Id == product.Id).ToList();
                    if (products.Count > 0)
                    {
                        products[0].Name = product.Name;
                        products[0].Cost = product.Cost;
                        products[0].Inventory= product.Inventory;
                        products[0].Available = product.Available;
                        products[0].CategoryId = product.CategoryId;
                        products[0].OrderId = product.OrderId;
                        products[0].Quantity = product.Quantity;
                        products[0].StoreId = product.StoreId;
                        products[0].Status = product.Status;
                        try
                        {
                            _dbContext.SaveChanges();
                        }
                        catch(Exception ex) {
                            TextWriterTraceListener logListener = new TextWriterTraceListener("./Log.txt", "Logs");
                            Trace.Listeners.Add(logListener);
                            Trace.WriteLine(ex.Message);
                            Trace.Close();
                            check = false;
                            message = "500 Internal Error";
                        }   
                    }

    

                    // Set Headers
                    Response.Headers.UserAgent = "API";
                    Response.Headers["Message"] = message;


                    //return
                    return "";
                }
                else // bad api key
                {
                    Response.Headers.UserAgent = "API";
                    Response.Headers["Message"] = "Invalid ApiKey";


                    //return
                    return "";
                }
            }
            else // objects are null
            {
                Response.Headers.UserAgent = "API";
                Response.Headers["Message"] = "Product or ApiKey or name or categoryId is null or cost is lower then 0";
                //return
                return "";
            }
        }


        /// <summary>
        /// Adds a category from a string to the given store that links to the ApiKey
        /// Type: POST
        /// </summary>
        /// <param name="category"></param>
        /// <param name="ApiKey"></param>
        /// <returns></returns>
        [HttpPost("/API/V1/StoreManagement/AddCategory")]
        public string AddCategory([FromQuery] string category, [FromHeader] Guid ApiKey)
        {
            bool check = true;
            string message = "";
            Category categorySave= new Category();
            categorySave.Name = category;
            List<Account> accounts = _dbContext.Account.Where(a => a.ApiKey == ApiKey).ToList();
            if (ApiKey != Guid.Empty || category.Length <= 0 || category == null) // check for null items
            {
                //check ApiKey
                if (accounts.Count > 0) // ensure api key is valid
            {
                    //validate category
                    List<Category> categoriesNames = _dbContext.Category.Where(b => b.Name==category).ToList();
                    if(categoriesNames.Count > 0)
                    {
                        check = false;
                        message = "Category already exists";
                    }
                    //assign ID
                    List<Category> categoriesIds = _dbContext.Category.OrderByDescending(b => b.Id).ToList();
                    if (categoriesIds.Count > 0)
                    {
                        categorySave.Id = categoriesIds[0].Id+1;
                    }
                    else
                    {
                        categorySave.Id = 0;
                    }

                    //add
                    if(check)
                    {
                        try
                        {
                            _dbContext.Category.Add(categorySave);
                            _dbContext.SaveChanges();
                            message = "Category added";
                        }
                        catch (Exception ex)
                        {
                            TextWriterTraceListener logListener = new TextWriterTraceListener("./Log.txt", "Logs");
                            Trace.Listeners.Add(logListener);
                            Trace.WriteLine(ex.Message);
                            Trace.Close();
                            check = false;
                            message = "500 Internal Error";
                        }
                    }

                    Response.Headers.UserAgent = "API";
                    Response.Headers["Message"] = message;
                    return "";
            }
            // bad api key
            else
            {
                Response.Headers.UserAgent = "API";
                Response.Headers["Message"] = "Invalid ApiKey";


                //return
                return "";
            }
            
            }
            else // objects are null
            {
                Response.Headers.UserAgent = "API";
                Response.Headers["Message"] = "Category is null or less then 1 char or ApiKey is not set";
                //return
                return "";
            }
        }


        /// <summary>
        /// Removes a category from the store.
        /// Type: POST
        /// </summary>
        /// <param name="category"></param>
        /// <param name="ApiKey"></param>
        /// <returns></returns>
        [HttpPost("/API/V1/StoreManagement/RemoveCategory")]
        public string RemoveCategory([FromBody] Category category, [FromHeader] Guid ApiKey)
        {
            bool check = true;
            string message = "";
            if (category != null && ApiKey != Guid.Empty && category.Name != null)
            {
                List<Account> accounts = _dbContext.Account.Where(a => a.ApiKey == ApiKey).ToList();
                if (accounts.Count > 0) // ensure api key is valid
                {
                    try
                    {
                        List<Category> categories = _dbContext.Category.Where(d => d.Name == category.Name).ToList();
                        if(categories.Count > 0)
                        {
                            category= categories[0];
                            _dbContext.Entry(accounts[0]).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                            _dbContext.Category.Remove(category);
                            _dbContext.SaveChanges();
                            message = "Category removed";
                        }
                        else
                        {
                            message = "Category Not Found";
                        }
                    }
                    catch (Exception ex)
                    {
                        TextWriterTraceListener logListener = new TextWriterTraceListener("./Log.txt", "Logs");
                        Trace.Listeners.Add(logListener);
                        Trace.WriteLine(ex.Message);
                        Trace.Close();
                        check = false;
                        message = "500 Internal Error";
                    }




                    // Set Headers
                    Response.Headers.UserAgent = "API";
                    Response.Headers["Message"] = message;


                    //return
                    return "";
                }
                else // bad api key
                {
                    Response.Headers.UserAgent = "API";
                    Response.Headers["Message"] = "Invalid ApiKey";


                    //return
                    return "";
                }
            }
            else // objects are null
            {
                Response.Headers.UserAgent = "API";
                Response.Headers["Message"] = "Category, ApiKey, or Category name is null";
                //return
                return "";
            }
        }


        /// <summary>
        /// Edits a category for the given store.
        /// Type: PUT
        /// </summary>
        /// <param name="category"></param>
        /// <param name="ApiKey"></param>
        /// <returns></returns>
        [HttpPut("/API/V1/StoreManagement/EditCategory")]
        public string EditCategory([FromBody] Category category, [FromHeader] Guid ApiKey)
        {
            bool check = true;
            string message = "";
            if (category != null && ApiKey != Guid.Empty && category.Name != null)
            {
                List<Account> accounts = _dbContext.Account.Where(a => a.ApiKey == ApiKey).ToList();
                if (accounts.Count > 0) // ensure api key is valid
                {
                    try
                    {
                        List<Category> categories = _dbContext.Category.Where(d => d.Id == category.Id).ToList();
                        if (categories.Count > 0)
                        {
                            categories[0].Name = category.Name;
                            _dbContext.SaveChanges();
                            message = "Category Updated";
                        }
                        else
                        {
                            message = "Edited Category";
                        }
                    }
                    catch (Exception ex)
                    {
                        TextWriterTraceListener logListener = new TextWriterTraceListener("./Log.txt", "Logs");
                        Trace.Listeners.Add(logListener);
                        Trace.WriteLine(ex.Message);
                        Trace.Close();
                        check = false;
                        message = "500 Internal Error";
                    }




                    // Set Headers
                    Response.Headers.UserAgent = "API";
                    Response.Headers["Message"] = message;


                    //return
                    return "";
                }
                else // bad api key
                {
                    Response.Headers.UserAgent = "API";
                    Response.Headers["Message"] = "Invalid ApiKey";


                    //return
                    return "";
                }
            }
            else // objects are null
            {
                Response.Headers.UserAgent = "API";
                Response.Headers["Message"] = "Category, ApiKey, or Category name is null";
                //return
                return "";
            }
        }


        /// <summary>
        /// Gets all categories from the store.
        /// Type : GET
        /// </summary>
        /// <param name="ApiKey"></param>
        /// <returns></returns>
        [HttpGet("/API/V1/StoreManagement/getAllCategories")]
        public string GetAllCategories()
        {
            Response.Headers.UserAgent = "API";
            Response.Headers["Message"] = "Got all Categories";
            List<Category> categories =_dbContext.Category.OrderByDescending(b =>b.Name).ToList();
            return JsonSerializer.Serialize(categories);
        }
    }
}
*/