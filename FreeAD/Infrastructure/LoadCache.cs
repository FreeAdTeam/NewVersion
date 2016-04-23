using FreeAD.Infrastructure;
using FreeAD.Infrastructure.Tasks;

namespace FreeAD.Infrastructure
{
    public class LoadCache : IRunAtStartup
    {
        public void Execute()
        {
            //CacheHelper.Clear(CacheType.Guider);
            //CacheHelper.GetCache<TourGuide>(CacheType.Guider);
        }
    }
}