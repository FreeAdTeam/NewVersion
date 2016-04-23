using System.Web;
using StructureMap;

namespace FreeAD.Infrastructure
{
    public static class ContainerPerRequestExtensions
    {
        public static IContainer GetContainer(this HttpContextBase context)
        {
            return (IContainer)HttpContext.Current.Items["_Container"] ?? ObjectFactory.Container;
        }
        public static int GetLanguageId(this HttpContextBase context)
        {
            return (int)HttpContext.Current.Items["_LanguageId"];
        }
    }
}