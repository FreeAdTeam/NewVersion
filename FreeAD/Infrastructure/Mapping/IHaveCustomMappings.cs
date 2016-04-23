using AutoMapper;

namespace FreeAD.Infrastructure.Mapping
{
    interface IHaveCustomMappings
    {
        void CreateMappings(IConfiguration configuration);
    }
}
