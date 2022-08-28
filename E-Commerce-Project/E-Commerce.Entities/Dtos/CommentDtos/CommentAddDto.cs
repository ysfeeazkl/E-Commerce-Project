using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Entities.Dtos.CommentDtos
{
    public class CommentAddDto
    {
        public string Content { get; set; }
        public int CustomerID { get; set; }
        public int ProductID { get; set; }    
        public int SellerID { get; set; }
        public int BaseCommentID { get; set; }
    
    }
}
