using PortalIpalEscalas.Common.Models;
using PortalIpalEscalas.Common.Models.Utils;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace PortalIpalEscalas.Infraestructure.Interfaces
{
    public interface IToken
    {
        string AddToken(AuthResponse authResponse, string user, string pass);
        ClaimCrypt DecryptToken(ClaimsIdentity identity);
    }
}
