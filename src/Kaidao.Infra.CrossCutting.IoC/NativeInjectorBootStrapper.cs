using Kaidao.Domain.Core.Bus;
using Kaidao.Domain.Core.Events;
using Kaidao.Domain.Core.Notifications;
using Kaidao.Domain.Interfaces;
using Kaidao.Infra.CrossCutting.Bus;
using Kaidao.Infra.CrossCutting.Identity.Models;
using Kaidao.Infra.CrossCutting.Identity.Repository;
using Kaidao.Infra.Data.EventSourcing;
using Kaidao.Infra.Data.Repository.EventSourcing;
using Kaidao.Infra.Data.UoW;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NovelQT.Infra.Data.Repository.EventSourcing;

namespace Kaidao.Infra.CrossCutting.IoC;

public class NativeInjectorBootStrapper
{
    public static void RegisterServices(IServiceCollection services)
    {
        // ASP.NET HttpContext dependency
        services.AddHttpContextAccessor();

        // Domain Bus (Mediator)
        services.AddScoped<IMediatorHandler, InMemoryBus>();

        // Domain - Events
        services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

        // Infra - Data
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IUserPermissionRepository, UserPermissionRepository>();

        // Infra - Data EventSourcing
        services.AddScoped<IEventStoreRepository, EventStoreSqlRepository>();
        services.AddScoped<IEventStore, SqlEventStore>();

        // Infra - Identity
        services.AddScoped<IUser, AspNetUser>();
    }
}