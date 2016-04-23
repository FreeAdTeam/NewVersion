using FreeAD.Infrastructure;
using FreeADPortable.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Web.Script.Serialization;

namespace FreeAD.Helpers
{
    public static class PagingHelper
    {
        public static MvcHtmlString TypeaheadFor<TModel, TValue>(
        this HtmlHelper<TModel> htmlHelper,
        Expression<Func<TModel, TValue>> expression,
        IEnumerable<string> source,
        int items = 8)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            if (source == null)
                throw new ArgumentNullException("source");

            var jsonString = new JavaScriptSerializer().Serialize(source);

            return htmlHelper.TextBoxFor(
                expression,
                new
                {
                    autocomplete = "off",
                    data_provide = "typeahead",
                    data_items = items,
                    data_source = jsonString,
                    @class = "form-control"
                }
            );
        }
        public static MvcHtmlString Page(this HtmlHelper html, string name, int currentPage, int numberOfPages, object htmlAttributes = null )
        {
            List<int> pages = new List<int>();
            for (int i = 1; i <= numberOfPages; i++)
            {
                pages.Add(i);
            }
            return System.Web.Mvc.Html.SelectExtensions.DropDownList(html, name, new SelectList(pages, "Key", "Value", currentPage), htmlAttributes);
        }
        public static MvcHtmlString PageFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, int numberOfPages, object htmlAttributes = null) where TModel : class
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            return htmlHelper.Page(metadata.PropertyName, (int)metadata.Model, numberOfPages, htmlAttributes);
        }


        public static MvcHtmlString SelectList_PageSize(this HtmlHelper html, string name, int selectedvalue, int pagestep = 10, object htmlattributes = null)
        {
            Dictionary<int, int> list = new Dictionary<int, int>();
            for (int i = pagestep; i < 100; i += pagestep)
            {
                list.Add(i, i);
            }
            return System.Web.Mvc.Html.SelectExtensions.DropDownList(html, name, new SelectList(list, "Key", "Value", selectedvalue), htmlattributes);
        }
        public static MvcHtmlString SelectList_Pages(this HtmlHelper html, string name, int selectedvalue, int numofpages, object htmlattributes = null)
        {
            if (numofpages < selectedvalue)
                selectedvalue = numofpages;

            Dictionary<int, int> list = new Dictionary<int, int>();
            for (int i = 0; i < numofpages; i++)
            {
                list.Add(i + 1, i + 1);
            }
            return System.Web.Mvc.Html.SelectExtensions.DropDownList(html, name, new SelectList(list, "Key", "Value", selectedvalue), htmlattributes);
        }
        public static String ShowAllErrors<T>(this HtmlHelper helper)
        {
            StringBuilder sb = new StringBuilder();
            Type myType = typeof(T);
            PropertyInfo[] propInfo = myType.GetProperties();

            foreach (PropertyInfo prop in propInfo)
            {
                foreach (var e in helper.ViewData.ModelState[prop.Name].Errors)
                {
                    TagBuilder div = new TagBuilder("div");
                    div.MergeAttribute("class", "field-validation-error");
                    div.SetInnerText(e.ErrorMessage);
                    sb.Append(div.ToString());
                }
            }
            return sb.ToString();
        }

        public static MvcHtmlString LanguageField<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, int languageId=1, object htmlAttributes = null)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);

            TagBuilder tag = new TagBuilder("label");
            if (htmlAttributes != null)
            {
                tag.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            }
            tag.InnerHtml = GetStringWithSpaces(metadata.PropertyName);
            if (languageId == 1)
            return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));


            var item = CacheHelper.GetCache<LanguageField>(CacheType.LanguageField).FirstOrDefault(d => d.EnglishName.ToLower()==metadata.PropertyName.ToLower());
            if (item == null)
            {
                return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
            }
            tag.InnerHtml = item.ShowName;
            return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
        }
        public static string LanguageFieldString(this HtmlHelper html, string word, int languageId = 1)
        {
            if (languageId == 1)
                return word;
            var item = CacheHelper.GetCache<LanguageField>(CacheType.LanguageField).FirstOrDefault(d => d.EnglishName.ToLower() == word.ToLower());
            if (item == null)
            {
                return word;
            }
            return item.ShowName;
        }
       
        private static string GetStringWithSpaces(string input)
        {
            return Regex.Replace(
                input,
                "(?<!^)" +
                "(" +
                "  [A-Z][a-z] |" +
                "  (?<=[a-z])[A-Z] |" +
                "  (?<![A-Z])[A-Z]$" +
                ")",
                " $1",
                RegexOptions.IgnorePatternWhitespace);
        }
    }
}
