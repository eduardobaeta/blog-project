﻿using System.Security.Claims;
using Blog.Models;

namespace Blog.Extensions;

public static class RoleClaimsExtension
{
    public static IEnumerable<Claim> GetClaims(this User user)
    {
        var results = new List<Claim>()
        {
            new(ClaimTypes.Name, user.Email)
        };
        
        results.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role.Slug)));
        return results;
    }
}