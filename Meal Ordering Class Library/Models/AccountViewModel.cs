using Meal_Ordering_Class_Library.RequestEntitiesShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meal_Ordering_Class_Library.Models
{
    public class AccountViewModel
    {
        public AccountLoginRequest? AccountLoginRequest { get; set; }
        public AccountEditRequest? AccountEditRequest { get; set; }
        public AccountRegisterRequest? AccountRegisterRequest { get; set; }
    }
}
