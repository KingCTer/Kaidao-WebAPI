using Kaidao.Infra.CrossCutting.IoC;
using Kaidao.Services.Api.ProgramExtensions;
using Microsoft.AspNetCore.Builder;
using System;

namespace Kaidao.Services.Api.ProgramExtensions;
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
