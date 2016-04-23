using FreeAD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreeAD.Infrastructure
{
    public interface ICurrentUser
    {
        ApplicationUser User { get; }
        DateTime NewZealandTime { get; }
        DateTime Start(DateTime input);
        DateTime End(DateTime input);
    }
}