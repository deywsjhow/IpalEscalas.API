using PortalIpalEscalas.Common.Models;
using PortalIpalEscalas.Common.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PortalIpalEscalas.Infraestructure.Interfaces
{
    public interface IAuthtContext
    {
        Task<ObjectResponse<RegisterResponse>> UserRegister(RegisterResponse user);
        Task<ObjectResponse<AuthResponse>> UserLogin(Login userLogin);
        Task<ObjectResponse<ChangePass>> ChangePassword(ChangePass user);
        Task<ObjectListResponse<UserLogin>> GetUsersLogins();
        Task<ObjectListResponse<Users>> GetUsers();
    }
}
