﻿using System.Security.Claims;

namespace Kaidao.Domain.Interfaces
{
    public interface IUser
    {
        string Name { get; }

        bool IsAuthenticated();

        IEnumerable<Claim> GetClaimsIdentity();
    }
}