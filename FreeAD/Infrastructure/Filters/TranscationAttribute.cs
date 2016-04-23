using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreeAD.Infrastructure.Filters
{
    public class TranscationAttribute: ActionFilterAttribute
    {
        public ApplicationDbContext Context { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.HttpContext.Items["_Transaction"] =Context.Database.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
            base.OnActionExecuting(filterContext);
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var transaction = (DbContextTransaction)filterContext.HttpContext.Items["_Transaction"];
            
            if (filterContext.HttpContext.Items["Error"] != null)
            {
                transaction.Rollback();
            }
            else
            {
                transaction.Commit();
            }
        }
    }
}