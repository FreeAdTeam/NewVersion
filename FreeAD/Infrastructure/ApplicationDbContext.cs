using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Linq.Expressions;
using System;
using System.Data.SqlClient;
using FreeAD.Models;
using FreeADPortable.Domain;
using FreeADPortable.Models;

namespace FreeAD.Infrastructure
{
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
        public virtual ConditionViewModel<T> GetSearchResult<T, TOrderBy>(ConditionViewModel<T> condition, Expression<Func<T, TOrderBy>> orderBy, string[] includes = null, bool httpget = true) where T : class
        {
            var result = new ConditionViewModel<T>();
            SortOrder sortOrder = SortOrder.Descending;
            if (httpget)
            {
                if (condition.CurrentPage == 0 || condition.PerPageSize == 0)
                {
                    condition.CurrentPage = 1;
                    condition.PerPageSize = 10;
                }
            }
            else
            {
                if (condition.CurrentPage == 0) //search
                {
                    condition.PerPageSize = 10;
                    condition.CurrentPage = 1;
                }
            }
            if (condition.ChangeOrderDirection)
            {
                if (condition.OrderDirection == "desc")
                {
                    sortOrder = SortOrder.Descending;
                    condition.OrderDirection = "asc";
                }
                else
                {
                    sortOrder = SortOrder.Ascending;
                    condition.OrderDirection = "desc";
                }
            }
            else
            {
                if (condition.OrderDirection == "desc")
                {
                    sortOrder = SortOrder.Descending;
                }
                else
                {
                    sortOrder = SortOrder.Ascending;
                }
            }

            IQueryable<T> group;
            if (condition.Func == null)
            {
                condition.Func = d => true;
            }
            int total = Set<T>().Count(condition.Func);
            var totalPages = total / condition.PerPageSize + (total % condition.PerPageSize > 0 ? 1 : 0);
            if (condition.CurrentPage > totalPages)
            {
                condition.CurrentPage = 1;
            }
            if (sortOrder == SortOrder.Ascending)
            {
                group =
                    Set<T>()
                        .Where(condition.Func)
                        .OrderBy(orderBy)
                        .Skip((condition.CurrentPage - 1) * condition.PerPageSize)
                        .Take(condition.PerPageSize);
            }
            else
            {
                group =
                    Set<T>()
                        .Where(condition.Func)
                        .OrderByDescending(orderBy)
                        .Skip((condition.CurrentPage - 1) * condition.PerPageSize)
                        .Take(condition.PerPageSize);
            }
            if (includes != null)
            {
                foreach (var item in includes)
                {
                    group = group.Include(item);
                }
            }
            result.Data = group.ToList();
            result.TotalPages = totalPages == 0 ? 1 : totalPages;
            result.PerPageSize = condition.PerPageSize;
            result.CurrentPage = condition.CurrentPage;
            result.OrderDirection = condition.OrderDirection;
            result.Search = condition.Search;
            result.SearchTwo = condition.SearchTwo;
            result.LanguageId = condition.LanguageId;
            return result;
        }
        public IQueryable<T> Get<T>(Expression<Func<T, bool>> criteria) where T : class
        {
            return Set<T>().Where(criteria);
        }
        #endregion
    }
}