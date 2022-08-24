using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Entities.Concrete
{
    public class CustomerAndOperationClaim
    {
        public int CustomerID { get; set; }
        public Customer Customer { get; set; }
        public int OperationClaimID { get; set; }
        public OperationClaim OperationClaim { get; set; }
    }
}
