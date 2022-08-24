using AutoMapper;
using E_Commerce.Data.Concrete.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.Utilities
{
    public class ManagerBase
    {
        public CommerceContext DbContext { get; }
        public IMapper Mapper{ get; }

        public ManagerBase(IMapper mapper)
        {
            Mapper = mapper;
        }

        public ManagerBase(IMapper mapper, CommerceContext context)
        {
            Mapper = mapper;
            DbContext = context;
        }
        public ManagerBase(CommerceContext context)
        {
            DbContext = context;
        }
    }
}
