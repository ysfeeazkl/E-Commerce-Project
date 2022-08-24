using E_Commerce.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Entities.Concrete
{
    public class OperationClaim : EntityBase<int>, IEntity
    {
        public string Name { get; set; }
        public ICollection<CustomerAndOperationClaim> CustomerAndOperationClaims { get; set; }
    }
}
