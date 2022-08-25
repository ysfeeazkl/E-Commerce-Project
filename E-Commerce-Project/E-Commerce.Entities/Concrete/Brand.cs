using E_Commerce.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Entities.Concrete
{
    public class Brand : EntityBase<int>, IEntity
    {
        public string Name { get; set; }
        public string Introduc { get; set; }
        public BrandPicture? BrandPicture { get; set; }
        public int BrandPictureID { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<Report> Reports { get; set; }
    }
}
