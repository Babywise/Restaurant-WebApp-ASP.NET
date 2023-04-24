using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meal_Ordering_Class_Library.RequestEntitiesShared
{
    public class AccountLoginRequest
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? CurrentPassword { get; set; }
    }
}
