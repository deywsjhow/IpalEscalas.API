using PortalIpalEscalas.Common.Models;
using PortalIpalEscalas.Common.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PortalIpalEscalas.Infraestructure.Interfaces
{
    public interface IAuthService
    {
        Task<ObjectResponse<AuthResponse>> AutheService(Login authModel);
        Task<ObjectResponse<RegisterResponse>> UserRegister(RegisterResponse request);
        Task<ObjectResponse<ChangePass>> ChangePassword(ChangePass changePass);
        Task<ObjectListResponse<UserLogin>> GetUsers();
        Task<ObjectResponse<object>> SendMessageWpp(SendMessageWpp request);
    }
}
