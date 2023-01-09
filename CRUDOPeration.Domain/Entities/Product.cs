using CRUDOPerations.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDOPeration.Domain.Entities
{
    public class Product : Base
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
