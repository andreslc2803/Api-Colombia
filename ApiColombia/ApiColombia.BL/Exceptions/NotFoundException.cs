using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiColombia.BL.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string message)
            : base(message, 404)
        {
        }
    }
}
