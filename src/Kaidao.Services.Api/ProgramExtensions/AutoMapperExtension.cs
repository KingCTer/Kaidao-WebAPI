using Kaidao.Application.AutoMapper;

namespace Kaidao.Services.Api.ProgramExtensions
{
    public static class AutoMapperExtension
    {
        public static WebApplicationBuilder AddAutoMapperConfiguration(this WebApplicationBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            builder.Services.AddAutoMapper(AutoMapperConfig.RegisterMappings());

            return builder;
        }

    }
}
