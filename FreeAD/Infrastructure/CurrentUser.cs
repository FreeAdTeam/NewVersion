using FreeAD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using Microsoft.AspNet.Identity;

namespace FreeAD.Infrastructure
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IIdentity _identity;
        private readonly ApplicationDbContext _context;

        private ApplicationUser _user;

        public CurrentUser(IIdentity identity, ApplicationDbContext context)
        {
            _identity = identity;
            _context = context;
        }

        public ApplicationUser User
        {
            get
            {
                return _user ?? (_user = _context.Users.Find(_identity.GetUserId()));
            }
        }
        public DateTime NewZealandTime
        {
            get
            {
                DateTime serverTime = DateTime.Now;
                TimeZoneInfo timeZone1 = TimeZoneInfo.FindSystemTimeZoneById("New Zealand Standard Time");
                return TimeZoneInfo.ConvertTime(serverTime, timeZone1);
            }
        }
        public DateTime Start(DateTime input)
        {
            DateTime dt = Convert.ToDateTime(input.ToShortDateString()+" 00:00:00.000");
            return dt;
        }
        public DateTime End(DateTime input)
        {
            DateTime dt = Convert.ToDateTime(input.ToShortDateString() + " 23:59:59.999");
            return dt;
        }
    }
}