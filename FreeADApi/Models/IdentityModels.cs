using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Linq.Expressions;
using System;
using System.Data.Entity;
using FreeADPortable.Domain;
using System.Data.SqlClient;
using FreeADPortable.Models;
using System.Collections.Generic;
using System.Dynamic;
using FreeADPortable.Helpers;
using System.Reflection;

namespace FreeADApi.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("FreeADConnection", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<UserImage> UserImages { get; set; }
        public DbSet<LanguageField> LanguageFields { get; set; }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        #region customer part
        public virtual ConditionViewModel<T> GetSearchResult<T>(ConditionViewModel<T> Condition, string Sort = "id", string Includes = null, string Fields = null) where T : class
        {
            var result = new ConditionViewModel<T>();
            IQueryable<T> group;
            if (Condition.Func == null)
            {
                Condition.Func = d => true;
            }
            int total = Set<T>().Count(Condition.Func);
            var totalPages = total / Condition.PerPageSize + (total % Condition.PerPageSize > 0 ? 1 : 0);
            if (Condition.CurrentPage > totalPages)
            {
                Condition.CurrentPage = 1;
            }

            group =Set<T>().Where(Condition.Func).ApplySort(Sort).Skip((Condition.CurrentPage - 1) * Condition.PerPageSize)
                    .Take(Condition.PerPageSize);

            List<string> includeFields = new List<string>();

            // contains "expenses", or "others", …
            if (Includes != null)
            {
                includeFields = Includes.ToLower().Split(',').ToList();
            }

            if (includeFields != null)
            {
                foreach (var item in includeFields)
                {
                    group = group.Include(item);
                }
            }
            var temp = group.ToList();
            result.ApiData = group.ToList().Select(d => ShapedObject(d, Fields));
            result.TotalPages = totalPages == 0 ? 1 : totalPages;
            result.PerPageSize = Condition.PerPageSize;
            result.CurrentPage = Condition.CurrentPage;
            result.Search = Condition.Search;
            result.SearchTwo = Condition.SearchTwo;
            return result;
        }
        public IQueryable<T> Get<T>(Expression<Func<T, bool>> criteria) where T : class
        {
            return Set<T>().Where(criteria);
        }
        public object ShapedObject<T>(T expenseGroup, string fields) where T:class
        {
            List<string> lstOfFields = new List<string>();
            if (fields != null)
            {
                lstOfFields = fields.ToLower().Split(',').ToList();
            }
            // work with a new instance, as we'll manipulate this list in this method
            if (!lstOfFields.Any())
            {
                return expenseGroup;
            }
            else
            {
                // create a new ExpandoObject & dynamically create the properties for this object
                // if we have an expense
                ExpandoObject objectToReturn = new ExpandoObject();
                foreach (var field in lstOfFields)
                {
                    // need to include public and instance, b/c specifying a binding flag overwrites the
                    // already-existing binding flags.
                    var fieldValue = expenseGroup.GetType()
                        .GetProperty(field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                        .GetValue(expenseGroup, null);
                    // add the field to the ExpandoObject
                    ((IDictionary<String, Object>)objectToReturn).Add(field, fieldValue);
                }
                return objectToReturn;
            }
        }
        #endregion
    }

}