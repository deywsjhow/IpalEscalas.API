using PortalIpalEscalas.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PortalIpalEscalas.Infraestructure.Interfaces
{
    public interface IAuthService 
    {
        Task<ObjectResponse<AuthResponse>> AutheService(AuthResponse authModel);
        Task<ObjectResponse<RegisterResponse>> UserRegister(RegisterResponse request);
    }
}
