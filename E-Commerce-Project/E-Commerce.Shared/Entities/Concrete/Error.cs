using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared.Entities.Concrete
{
    public class Error
    {
        public Error(string message, string propertyName)
        {
            Message = message;
            PropertyName = propertyName;
        }
        public Error()
        {

        }
        public string PropertyName { get; set; }
        public string Message { get; set; }
    }
}
