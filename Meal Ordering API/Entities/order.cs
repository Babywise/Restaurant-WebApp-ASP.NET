﻿namespace Meal_Ordering_API.Entities
{
    public class Order
    {
        public List<Product>? products { get; set; }    
        public int? StoreId { get; set; }
        public int? CustomerId { get; set; }
        public int? Id { get; set; }
        public bool? updated { get; set; } 
        public string? Status {  get; set; }
    }
}