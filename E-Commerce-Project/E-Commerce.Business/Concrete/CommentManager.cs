using AutoMapper;
using E_Commerce.Business.Abstract;
using E_Commerce.Business.Utilities;
using E_Commerce.Data.Concrete.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.Concrete
{
    public class CommentManager : ManagerBase,ICommentService
    {
        public CommentManager(CommerceContext context, IMapper mapper) : base(mapper, context)
        {

        }
    }
}
