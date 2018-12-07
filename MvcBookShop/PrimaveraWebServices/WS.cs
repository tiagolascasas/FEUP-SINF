using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcBookShop.PrimaveraWebServices
{
    public abstract class WS
    {
        protected string route;
        protected string body;

        public abstract string Send();
    }
}
