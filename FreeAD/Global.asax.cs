using FreeAD.Infrastructure;
using FreeAD.Infrastructure.Tasks;
using FreeAD.Infrastructure.ModelMetadata;
using StructureMap;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace FreeAD
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public IContainer Container
        {
            get { return (IContainer)HttpContext.Current.Items["_Container"]; }
            set { HttpContext.Current.Items["_Container"] = value; }
        }
        public int LanguageId
        {
            get { return (int)HttpContext.Current.Items["_LanguageId"]; }
            set { HttpContext.Current.Items["_LanguageId"] = value; }
        }
       
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DependencyResolver.SetResolver(new StructureMapDependencyResolver(() => Container ?? ObjectFactory.Container));

            ObjectFactory.Configure(cfg => {

                cfg.AddRegistry(new StandardRegistry());
                cfg.AddRegistry(new ControllerRegistry());
                cfg.AddRegistry(new ActionFilterRegistry(() => Container ?? ObjectFactory.Container));
                cfg.AddRegistry(new MvcRegistry());
                cfg.AddRegistry(new TaskRegistry());
                cfg.AddRegistry(new ModelMetadataRegistry());
            });

            using (var container = ObjectFactory.Container.GetNestedContainer())
            {
                foreach (var task in container.GetAllInstances<IRunAtInit>())
                {
                    task.Execute();
                }

                foreach (var task in container.GetAllInstances<IRunAtStartup>())
                {
                    task.Execute();
                }
            }
        }
        public void Application_BeginRequest()
        {
            Container = ObjectFactory.Container.GetNestedContainer();
            if (Request.Cookies["Language"] != null)
            {
                try
                {
                    LanguageId = int.Parse(Request.Cookies["Language"].Values["Id"].ToString().Trim());
                }
                catch (System.Exception)
                {
                    LanguageId = 1;
                }
            }
            else
            {
                LanguageId = 1;//english default
            }
        }

        public void Application_EndRequest()
        {
                Container.Dispose();
                Container = null;
        }
    }
}
