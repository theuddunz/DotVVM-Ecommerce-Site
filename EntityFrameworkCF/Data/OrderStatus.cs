using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntityFrameworkCF.ViewModels
{
    public enum OrderStatus
    {
        Processing=0,
        Sent=1,
        Delivered=2,
        Complete=3
    }
}