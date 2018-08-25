using System;
using System.Collections.Generic;
using System.Text;

namespace Modular.Domain
{
    public class Customer
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Lastname { set; get; }
        public string FavoriteDrink { set; get; }
        public int Age { set; get; }
        public string Company { set; get; }
    }
}
