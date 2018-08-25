using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediatorApi.Responses
{
    public class GetCustomerResponse
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Lastname { set; get; }
        public string FavoriteDrink { set; get; }
        public int Age { set; get; }
        public string Company { set; get; }
    }
}
