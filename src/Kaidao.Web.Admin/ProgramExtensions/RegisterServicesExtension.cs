using Kaidao.Infra.CrossCutting.IoC;

namespace Kaidao.Web.Admin.ProgramExtensions;

public static class RegisterServicesExtension
{
    internal static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
    {
        if (builder == null) throw new ArgumentNullException(nameof(builder));

        // Adding dependencies from another layers (isolated from Presentation)
        NativeInjectorBootStrapper.RegisterServices(builder.Services);

        return builder;
    }
}