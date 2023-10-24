using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PortalIpalEscalas.Infraestructure.Interfaces
{
    public interface IAccountContext
    {
        Task<ObjectResponse<RegisterResponse>> UserRegister(RegisterResponse user);
    }
}
