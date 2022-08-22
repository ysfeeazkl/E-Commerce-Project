using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Entities.Concrete
{
    public class BrandPicture
    {
        public int BrandID { get; set; }
        public Brand Brand { get; set; }
        public byte[] File { get; set; }
    }
}
