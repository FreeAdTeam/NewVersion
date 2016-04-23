using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreeAD.Infrastructure.ModelMetadata.Filters
{
    public class TextAreaByNameFilter : IModelMetadataFilter
    {
        private static readonly HashSet<string> TextAreaFieldName = new HashSet<string> { "body", "comments","remark", "shortdescription" };

        public void TransformMetadata(System.Web.Mvc.ModelMetadata metadata, IEnumerable<Attribute> attributes)
        {
            if(!string.IsNullOrEmpty(metadata.PropertyName) && string.IsNullOrEmpty(metadata.DataTypeName) &&
                TextAreaFieldName.Contains(metadata.PropertyName.ToLower()))
            {
                metadata.DataTypeName = "MultilineText";
                metadata.Watermark = metadata.DisplayName + "...";
            }
        }
    }
}