using System;

namespace Kaidao.Application.AutoMapper
{
    public class AutoMapperConfig
    {
        public static Type[] RegisterMappings()
        {
            return new Type[]
            {
                typeof(DomainToViewModelMappingProfile),
                typeof(ViewModelToDomainMappingProfile),
                typeof(DomainToResponseMappingProfile)
            };
        }
    }
}
