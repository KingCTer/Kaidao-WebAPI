using Kaidao.Domain.Constants;
using Kaidao.Domain.IdentityEntity;
using Microsoft.EntityFrameworkCore;

namespace Kaidao.Infra.CrossCutting.Identity.Seeds;

public static class SeedFactory
{
    public static void SeedData(this ModelBuilder builder)
    {
        // Seed Command
        builder.Entity<Command>().HasData(
            new Command() { Id = CommandCode.CREATE.ToString(), Name = "Thêm" },
            new Command() { Id = CommandCode.READ.ToString(), Name = "Xem" },
            new Command() { Id = CommandCode.UPDATE.ToString(), Name = "Sửa" },
            new Command() { Id = CommandCode.DELETE.ToString(), Name = "Xoá" },
            new Command() { Id = CommandCode.APPROVE.ToString(), Name = "Duyệt" }
        );

        //Seed Function
        builder.Entity<Function>().HasData(
            new Function { Id = FunctionCode.SYSTEM.ToString(), Name = "Hệ thống", ParentId = null }
        );

        //Seed CommandInFunction
        builder.Entity<CommandInFunction>().HasData(
            new CommandInFunction()
            {
                FunctionId = FunctionCode.SYSTEM.ToString(),
                CommandId = CommandCode.CREATE.ToString(),
                Description = "Thêm"
            },
            new CommandInFunction()
            {
                FunctionId = FunctionCode.SYSTEM.ToString(),
                CommandId = CommandCode.READ.ToString(),
                Description = "Xem"
            },
            new CommandInFunction()
            {
                FunctionId = FunctionCode.SYSTEM.ToString(),
                CommandId = CommandCode.UPDATE.ToString(),
                Description = "Cập nhật"
            },
            new CommandInFunction()
            {
                FunctionId = FunctionCode.SYSTEM.ToString(),
                CommandId = CommandCode.DELETE.ToString(),
                Description = "Xoá"
            },
            new CommandInFunction()
            {
                FunctionId = FunctionCode.SYSTEM.ToString(),
                CommandId = CommandCode.APPROVE.ToString(),
                Description = "Duyệt"
            }
        );

        // Seed IdentityRole
        builder.Entity<AppRole>().HasData(
            new AppRole
            {
                Id = IdentityConstant.Roles.Admin,
                Name = IdentityConstant.Roles.Admin,
                IsSystemRole = true,
                NormalizedName = IdentityConstant.Roles.Admin.ToUpper()
            },
            new AppRole
            {
                Id = IdentityConstant.Roles.User,
                Name = IdentityConstant.Roles.User,
                IsSystemRole = true,
                NormalizedName = IdentityConstant.Roles.User.ToUpper()
            }
        );

        // Seed Permission
        builder.Entity<Permission>().HasData(
            // Admin Permission
            new Permission() { RoleId = IdentityConstant.Roles.Admin, FunctionId = FunctionCode.SYSTEM.ToString(), CommandId = CommandCode.CREATE.ToString() },
            new Permission() { RoleId = IdentityConstant.Roles.Admin, FunctionId = FunctionCode.SYSTEM.ToString(), CommandId = CommandCode.READ.ToString() },
            new Permission() { RoleId = IdentityConstant.Roles.Admin, FunctionId = FunctionCode.SYSTEM.ToString(), CommandId = CommandCode.UPDATE.ToString() },
            new Permission() { RoleId = IdentityConstant.Roles.Admin, FunctionId = FunctionCode.SYSTEM.ToString(), CommandId = CommandCode.DELETE.ToString() },

            // User Permission
            new Permission() { RoleId = IdentityConstant.Roles.User, FunctionId = FunctionCode.SYSTEM.ToString(), CommandId = CommandCode.READ.ToString() }

        );
    }
}