using PortalIpalEscalas.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PortalIpalEscalas.Infraestructure.Interfaces
{
    public interface IToken
    {
        string AddToken(AuthResponse authResponse, string user, string pass);
    }
}
