using Application.Mappings;

namespace HomeControl.Extensions;

public static class MappingExtensions
{
    public static void AddMappings(this IServiceCollection services)
    {
        TinyMapperConfig.Register();
    }
}
