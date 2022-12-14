using E_Commerce.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Entities.Concrete
{
    public class Report:EntityBase<int>,IEntity
    {
        public string Content { get; set; }
        public int CustomerID { get; set; }
        public Customer Customer{ get; set; }
        public int CommentID { get; set; }
        public Comment? Comment { get; set; }
        public int ProductID { get; set; }
        public Product? Product { get; set; }
        public int SellerID { get; set; }
        public Seller? Seller{ get; set; }
        public int BrandID { get; set; }
        public Brand? Brand{ get; set; }
        public int ReportPictureID { get; set; }
        public ReportPicture? ReportPicture { get; set; }
    }
}
