﻿using Kaidao.Domain.IdentityEntity;
using Kaidao.Domain.Interfaces;
using Kaidao.Infra.CrossCutting.Identity.Context;
using Microsoft.EntityFrameworkCore;

namespace Kaidao.Infra.CrossCutting.Identity.Repository
{
    public class UserPermissionRepository : Repository<UserPermission>, IUserPermissionRepository
    {
        public UserPermissionRepository(AuthDbContext context)
            : base(context)
        {
        }

        public IQueryable<UserPermission> GetByAllow(bool isAllow)
        {
            return DbSet.AsNoTracking().Where(up => up.Allow == isAllow);
        }
    }
}