using PortalIpalEscalas.Common.Dto;
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
            var getValues = new ObjectResponse<RegisterResponse>();

            getValues = Validator.ValidRegister(request);
            
            if(!getValues.Success) 
                return getValues;
            
            var result = await accountContext.UserRegister(getValues.Result);

            if (!result.Success)
                return result;

            return result;
        }


        public async Task<ObjectResponse<AuthResponse>> AutheService(AuthResponse authModel)
        {            
            var getValues = new ObjectResponse<AuthResponse>();

            getValues = Validator.ValidAuth(authModel);

            if (!getValues.Success)
                return getValues;

            var result = await accountContext.UserLogin(getValues.Result);
            if (!result.Success)
                return result;


            return result;
        }
    }
}
