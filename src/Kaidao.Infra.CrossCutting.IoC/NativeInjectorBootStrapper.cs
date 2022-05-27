﻿using Kaidao.Application.AppServices;
using Kaidao.Application.AppServices.Interfaces;
using Kaidao.Application.Services;
using Kaidao.Domain.CommandHandlers;
using Kaidao.Domain.Commands.Author;
using Kaidao.Domain.Commands.Book;
using Kaidao.Domain.Commands.Category;
using Kaidao.Domain.Commands.Chapter;
using Kaidao.Domain.Commands.Role;
using Kaidao.Domain.Core.Bus;
using Kaidao.Domain.Core.Events;
using Kaidao.Domain.Core.Notifications;
using Kaidao.Domain.IdentityEntity;
using Kaidao.Domain.Interfaces;
using Kaidao.Infra.CrossCutting.Bus;
using Kaidao.Infra.CrossCutting.Identity.Context;
using Kaidao.Infra.CrossCutting.Identity.Models;
using Kaidao.Infra.CrossCutting.Identity.Repository;
using Kaidao.Infra.Data.Context;
using Kaidao.Infra.Data.EventSourcing;
using Kaidao.Infra.Data.Repository;
using Kaidao.Infra.Data.Repository.EventSourcing;
using Kaidao.Infra.Data.UoW;
using MediatR;
using Microsoft.AspNetCore.Identity;
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
        services.AddScoped<ICategoryAppService, CategoryAppService>();
        services.AddScoped<IRoleAppService, RoleAppService>();

        services.AddScoped<IStorageService, FileStorageService>();

        // Domain - Events
        services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

        // Domain - Commands
        services.AddScoped<IRequestHandler<RegisterNewAuthorCommand, bool>, AuthorCommandHandler>();

        services.AddScoped<IRequestHandler<RegisterNewCategoryCommand, bool>, CategoryCommandHandler>();

        services.AddScoped<IRequestHandler<RegisterNewBookCommand, bool>, BookCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateBookCommand, bool>, BookCommandHandler>();
        services.AddScoped<IRequestHandler<RemoveBookCommand, bool>, BookCommandHandler>();

        services.AddScoped<IRequestHandler<RegisterNewChapterCommand, bool>, ChapterCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateChapterCommand, bool>, ChapterCommandHandler>();
        services.AddScoped<IRequestHandler<RemoveChapterCommand, bool>, ChapterCommandHandler>();

        services.AddScoped<IRequestHandler<RegisterNewRoleCommand, bool>, RoleCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateRoleCommand, bool>, RoleCommandHandler>();
        services.AddScoped<IRequestHandler<RemoveRoleCommand, bool>, RoleCommandHandler>();

        // Infra - Data
        services.AddScoped<AppDbContext>();
        services.AddScoped<AuthDbContext>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IUserPermissionRepository, UserPermissionRepository>();
        services.AddScoped<IFunctionRepository, FunctionRepository>();
        services.AddScoped<ICommandInFunctionRepository, CommandInFunctionRepository>();

        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IChapterRepository, ChapterRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();

        // Infra - Data EventSourcing
        services.AddScoped<IEventStoreRepository, EventStoreSqlRepository>();
        services.AddScoped<IEventStore, SqlEventStore>();
        services.AddScoped<EventStoreSqlContext>();

        // Infra - Identity
        services.AddScoped<IUser, AspNetUser>();
    }
}