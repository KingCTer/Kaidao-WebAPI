﻿// <auto-generated />
using System;
using Kaidao.Infra.CrossCutting.Identity.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Kaidao.Infra.CrossCutting.Identity.Migrations
{
    [DbContext(typeof(AuthDbContext))]
    [Migration("20220502103610_FixKeyAndSeedData")]
    partial class FixKeyAndSeedData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Kaidao.Domain.IdentityEntity.AppRole", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsSystemRole")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AppRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "Admin",
                            ConcurrencyStamp = "e1892331-9e44-40a2-8de5-c172062ee2a1",
                            IsSystemRole = true,
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "User",
                            ConcurrencyStamp = "f886c046-1e53-4da3-9cbd-eab6c34cc697",
                            IsSystemRole = true,
                            Name = "User",
                            NormalizedName = "USER"
                        });
                });

            modelBuilder.Entity("Kaidao.Domain.IdentityEntity.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AppUsers", (string)null);
                });

            modelBuilder.Entity("Kaidao.Domain.IdentityEntity.Command", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.ToTable("Commands", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "CREATE",
                            Name = "Thêm"
                        },
                        new
                        {
                            Id = "READ",
                            Name = "Xem"
                        },
                        new
                        {
                            Id = "UPDATE",
                            Name = "Sửa"
                        },
                        new
                        {
                            Id = "DELETE",
                            Name = "Xoá"
                        },
                        new
                        {
                            Id = "APPROVE",
                            Name = "Duyệt"
                        });
                });

            modelBuilder.Entity("Kaidao.Domain.IdentityEntity.CommandInFunction", b =>
                {
                    b.Property<string>("CommandId")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("FunctionId")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(128)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("CommandId", "FunctionId");

                    b.HasIndex("FunctionId");

                    b.ToTable("CommandInFunctions", (string)null);

                    b.HasData(
                        new
                        {
                            CommandId = "CREATE",
                            FunctionId = "SYSTEM",
                            Description = "Thêm"
                        },
                        new
                        {
                            CommandId = "READ",
                            FunctionId = "SYSTEM",
                            Description = "Xem"
                        },
                        new
                        {
                            CommandId = "UPDATE",
                            FunctionId = "SYSTEM",
                            Description = "Cập nhật"
                        },
                        new
                        {
                            CommandId = "DELETE",
                            FunctionId = "SYSTEM",
                            Description = "Xoá"
                        },
                        new
                        {
                            CommandId = "APPROVE",
                            FunctionId = "SYSTEM",
                            Description = "Duyệt"
                        });
                });

            modelBuilder.Entity("Kaidao.Domain.IdentityEntity.Function", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ParentId")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Functions", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "SYSTEM",
                            Name = "Hệ thống"
                        });
                });

            modelBuilder.Entity("Kaidao.Domain.IdentityEntity.Permission", b =>
                {
                    b.Property<string>("RoleId")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("FunctionId")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("CommandId")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("RoleId", "FunctionId", "CommandId");

                    b.HasIndex("CommandId");

                    b.HasIndex("FunctionId");

                    b.ToTable("Permissions", (string)null);

                    b.HasData(
                        new
                        {
                            RoleId = "Admin",
                            FunctionId = "SYSTEM",
                            CommandId = "CREATE"
                        },
                        new
                        {
                            RoleId = "Admin",
                            FunctionId = "SYSTEM",
                            CommandId = "READ"
                        },
                        new
                        {
                            RoleId = "Admin",
                            FunctionId = "SYSTEM",
                            CommandId = "UPDATE"
                        },
                        new
                        {
                            RoleId = "Admin",
                            FunctionId = "SYSTEM",
                            CommandId = "DELETE"
                        },
                        new
                        {
                            RoleId = "User",
                            FunctionId = "SYSTEM",
                            CommandId = "READ"
                        });
                });

            modelBuilder.Entity("Kaidao.Domain.IdentityEntity.UserPermission", b =>
                {
                    b.Property<string>("UserId")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("FunctionId")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("CommandId")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<bool>("Allow")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.HasKey("UserId", "FunctionId", "CommandId");

                    b.HasIndex("CommandId");

                    b.HasIndex("FunctionId");

                    b.ToTable("UserPermissions", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(50)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Kaidao.Domain.IdentityEntity.CommandInFunction", b =>
                {
                    b.HasOne("Kaidao.Domain.IdentityEntity.Command", "Command")
                        .WithMany("CommandInFunctions")
                        .HasForeignKey("CommandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kaidao.Domain.IdentityEntity.Function", "Function")
                        .WithMany("CommandInFunctions")
                        .HasForeignKey("FunctionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Command");

                    b.Navigation("Function");
                });

            modelBuilder.Entity("Kaidao.Domain.IdentityEntity.Function", b =>
                {
                    b.HasOne("Kaidao.Domain.IdentityEntity.Function", "Parent")
                        .WithMany("Functions")
                        .HasForeignKey("ParentId");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Kaidao.Domain.IdentityEntity.Permission", b =>
                {
                    b.HasOne("Kaidao.Domain.IdentityEntity.Command", "Command")
                        .WithMany("Permissions")
                        .HasForeignKey("CommandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kaidao.Domain.IdentityEntity.Function", "Function")
                        .WithMany("Permissions")
                        .HasForeignKey("FunctionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kaidao.Domain.IdentityEntity.AppRole", "Role")
                        .WithMany("Permissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Command");

                    b.Navigation("Function");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Kaidao.Domain.IdentityEntity.UserPermission", b =>
                {
                    b.HasOne("Kaidao.Domain.IdentityEntity.Command", "Command")
                        .WithMany("UserPermissions")
                        .HasForeignKey("CommandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kaidao.Domain.IdentityEntity.AppUser", "User")
                        .WithMany("UserPermissions")
                        .HasForeignKey("FunctionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kaidao.Domain.IdentityEntity.Function", "Function")
                        .WithMany("UserPermissions")
                        .HasForeignKey("FunctionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Command");

                    b.Navigation("Function");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Kaidao.Domain.IdentityEntity.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Kaidao.Domain.IdentityEntity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Kaidao.Domain.IdentityEntity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Kaidao.Domain.IdentityEntity.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kaidao.Domain.IdentityEntity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Kaidao.Domain.IdentityEntity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Kaidao.Domain.IdentityEntity.AppRole", b =>
                {
                    b.Navigation("Permissions");
                });

            modelBuilder.Entity("Kaidao.Domain.IdentityEntity.AppUser", b =>
                {
                    b.Navigation("UserPermissions");
                });

            modelBuilder.Entity("Kaidao.Domain.IdentityEntity.Command", b =>
                {
                    b.Navigation("CommandInFunctions");

                    b.Navigation("Permissions");

                    b.Navigation("UserPermissions");
                });

            modelBuilder.Entity("Kaidao.Domain.IdentityEntity.Function", b =>
                {
                    b.Navigation("CommandInFunctions");

                    b.Navigation("Functions");

                    b.Navigation("Permissions");

                    b.Navigation("UserPermissions");
                });
#pragma warning restore 612, 618
        }
    }
}
