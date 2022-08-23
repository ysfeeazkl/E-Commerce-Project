﻿using E_Commerce.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Entities.Concrete
{
    public class CustomerPicture : EntityBase<int>, IEntity
    {
        public string? FileName { get; set; }
        public string? FilePath { get; set; }
        public int CustomerID { get; set; }
        public Customer Customer{ get; set; }
    }
}
