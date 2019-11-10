using AutoMapper;

namespace Projekt.Service.Service.Mappings
{
    class AutoMapperMap
    {
        private static IMapper mapper;

        public static IMapper GetMapper()
        {
            return mapper;
        }

        public static void ConfigureMapping()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            mapper = config.CreateMapper();
        }
    }
}
