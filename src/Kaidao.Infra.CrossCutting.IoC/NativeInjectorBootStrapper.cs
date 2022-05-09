using Kaidao.Application.AppServices;
using Kaidao.Application.AppServices.Interfaces;
using Kaidao.Domain.CommandHandlers;
using Kaidao.Domain.Commands.Author;
using Kaidao.Domain.Commands.Book;
using Kaidao.Domain.Commands.Category;
using Kaidao.Domain.Commands.Chapter;
using Kaidao.Domain.Core.Bus;
using Kaidao.Domain.Core.Events;
using Kaidao.Domain.Core.Notifications;
using Kaidao.Domain.Interfaces;
using Kaidao.Infra.CrossCutting.Bus;
using Kaidao.Infra.CrossCutting.Identity.Models;
using Kaidao.Infra.CrossCutting.Identity.Repository;
using Kaidao.Infra.Data.EventSourcing;
using Kaidao.Infra.Data.Repository;
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

        // Application
        services.AddScoped<IUserAppService, UserAppService>();
        services.AddScoped<IBookAppService, BookAppService>();
        services.AddScoped<IChapterAppService, ChapterAppService>();

        // Domain - Events
        services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

        // Domain - Commands
        services.AddScoped<IRequestHandler<RegisterNewAuthorCommand, bool>, AuthorCommandHandler>();

        services.AddScoped<IRequestHandler<RegisterNewCategoryCommand, bool>, CategoryCommandHandler>();

        services.AddScoped<IRequestHandler<RegisterNewBookCommand, bool>, BookCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateBookCommand, bool>, BookCommandHandler>();

        services.AddScoped<IRequestHandler<RegisterNewChapterCommand, bool>, ChapterCommandHandler>();

        // Infra - Data
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IUserPermissionRepository, UserPermissionRepository>();

        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IChapterRepository, ChapterRepository>();

        // Infra - Data EventSourcing
        services.AddScoped<IEventStoreRepository, EventStoreSqlRepository>();
        services.AddScoped<IEventStore, SqlEventStore>();

        // Infra - Identity
        services.AddScoped<IUser, AspNetUser>();
    }
}