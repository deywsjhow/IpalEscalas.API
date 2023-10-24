using PortalIpalEscalas.Common.Models;
using PortalIpalEscalas.Infraestructure.Interfaces;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace PortalIpalEscalas.Domain
{
    public class AuthService : IAuthService
    {
        private readonly IAccountContext accountContext;
        public AuthService(IAccountContext _accountContext)
        {
            this.accountContext = _accountContext;
        }


        public async Task<ObjectResponse<RegisterResponse>> UserRegister(RegisterResponse request)
        {
            var result = new ObjectResponse<RegisterResponse>();

            var getValues = await accountContext.UserRegister(request);


            return result;
        }


        public Task<ObjectResponse<AuthResponse>> AutheService(AuthResponse authModel)
        {            
            var result = new ObjectResponse<AuthResponse>();

            if (string.IsNullOrEmpty(authModel.user) || string.IsNullOrEmpty(authModel.password))
                 return Task.FromResult(new ObjectResponse<AuthResponse> { Success = false, Result = null, Errors = { new InternalError(eMessage.MSG_ERROR_LOGIN, "Usuário ou Senha não informado") }});


            return Task.FromResult(result);
        }
    }
}
