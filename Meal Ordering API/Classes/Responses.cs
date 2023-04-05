using System.Net;
using System.Text.Json;
using Meal_Ordering_API.Entities;
using Meal_Ordering_API.Models;

namespace Meal_Ordering_API.Classes
{
    /// <summary>
    /// This class deals with HttpWebResponses. It converts responses into various objects
    /// V1
    ///  - Account
    ///  - Order
    ///  - List of Orders
    ///  - Product
    ///  - List of products
    ///  - Categories
    ///  - List of categories
    ///  - Value from a key ( from the headers )
    /// </summary>
    public class Responses
    {
            /// <summary>
            /// Converts a string Json into a Account object
            /// </summary>
            /// <param name="resp"></param>
            /// <returns></returns>
            public static Account getAccountFromResponse(HttpWebResponse resp)
            {
                Stream responseStream = resp.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                string result = reader.ReadToEnd();

                Account acc = JsonSerializer.Deserialize<Account>(result);
                return acc;
            }
        /// <summary>
        /// Converts a response to a loginResponseEntity
        /// </summary>
        /// <param name="resp"></param>
        /// <returns></returns>
        public static LoginResponse getLoginResponseFromResponse(HttpWebResponse resp)
        {
            Stream responseStream = resp.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            string result = reader.ReadToEnd();

            LoginResponse response = JsonSerializer.Deserialize<LoginResponse>(result);
            return response;
        }

        /// <summary>
        /// Pulls the specififed key from the response provided.. IE : getKeyFromResponse(Response, "User-Agent")
        /// </summary>
        /// <param name="response"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static string getKeyFromResponse(HttpWebResponse response, string Key)
            {
                string returnVal = response.Headers[Key]; // set ApiKey for easy reading
                return returnVal;
            }

        /// <summary>
        /// Converts a string Json into a Order Object
        /// </summary>
        /// <param name="resp"></param>
        /// <returns></returns>
        public static Order getOrderFromResponse(HttpWebResponse resp)
        {
            Stream responseStream = resp.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            string result = reader.ReadToEnd();

            Order order = JsonSerializer.Deserialize<Order>(result);
            return order;
        }


        /// <summary>
        /// Converts a response into a category
        /// </summary>
        /// <param name="resp"></param>
        /// <returns></returns>
        public static Category getCategoryFromResponse(HttpWebResponse resp)
        {
            Stream responseStream = resp.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            string result = reader.ReadToEnd();

            Category cat = JsonSerializer.Deserialize<Category>(result);
            return cat;
        }

        /// <summary>
        /// Converts a response into a product
        /// </summary>
        /// <param name="resp"></param>
        /// <returns></returns>
        public static Product getProductFromResponse(HttpWebResponse resp)
        {
            Stream responseStream = resp.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            string result = reader.ReadToEnd();

            Product prod = JsonSerializer.Deserialize<Product>(result);
            return prod;
        }


        /// <summary>
        /// Converts a response into a list of products
        /// </summary>
        /// <param name="resp"></param>
        /// <returns></returns>
        public static List<Product> getProductsFromResponse(HttpWebResponse resp)
        {
            Stream responseStream = resp.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            string result = reader.ReadToEnd();

            List<Product> prod = JsonSerializer.Deserialize<List<Product>>(result);
            return prod;
        }


        /// <summary>
        /// Converts a response into a list of orders
        /// </summary>
        /// <param name="resp"></param>
        /// <returns></returns>
        public static List<Order> getOrdersFromResponse(HttpWebResponse resp)
        {
            Stream responseStream = resp.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            string result = reader.ReadToEnd();

            List<Order> orders = JsonSerializer.Deserialize<List<Order>>(result);
            return orders;
        }

        /// <summary>
        /// Converts a response into a list of Categories
        /// </summary>
        /// <param name="resp"></param>
        /// <returns></returns>
        public static List<Category> getCategoriesFromResponse(HttpWebResponse resp)
        {
            Stream responseStream = resp.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            string result = reader.ReadToEnd();

            List<Category> categories = JsonSerializer.Deserialize<List<Category>>(result);
            return categories;
        }
    }
}
