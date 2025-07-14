using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace DevXpert.Store.Core.Business.Extensions;

public static class ClaimExtensions
{
    public static bool PossuiRole(this IEnumerable<Claim> claims, string role) 
        => claims?.Any(c => c.Type == ClaimTypes.Role && c.Value == role) == true;
}
