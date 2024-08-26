using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PSW24.Infrastructure.Auth
{
    public static class ClaimsPrincipalExtensions
    {
        public static int UserId(this ClaimsPrincipal user)
            => int.Parse(user.Claims.First(i => i.Type == "id").Value);
    }
}
