using E_Commerce.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Entities.Concrete
{
    public class Category:EntityBase<int>,IEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<CategoryAndProduct> CategoryAndProducts { get; set; }
    }
}
