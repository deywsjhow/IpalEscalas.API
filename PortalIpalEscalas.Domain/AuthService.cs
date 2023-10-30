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
        private readonly IToken token;
        public AuthService(IAccountContext _accountContext, IToken token)
        {
            this.accountContext = _accountContext;
            this.token = token;
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


        public async Task<ObjectResponse<AuthResponse>> AutheService(Login authModel)
        {            
            var getValues = new ObjectResponse<Login>();

            getValues = Validator.ValidAuth(authModel);

            if (!getValues.Success)
                return new ObjectResponse<AuthResponse> { Success = getValues.Success, Errors = getValues.Errors, Result = null};

            var result = await accountContext.UserLogin(getValues.Result);
            if (!result.Success)
                return result;

            result.Result.accessToken = token.AddToken(result.Result, authModel.user, authModel.password);


            return result;
        }

        public async Task<ObjectResponse<ChangePass>> ChangePassword(ChangePass changePass)
        {
            var getValues = new ObjectResponse<ChangePass>();

            getValues = Validator.ChangePassValid(changePass);

            if (!getValues.Success)
                return new ObjectResponse<ChangePass> { Success = getValues.Success, Errors = getValues.Errors, Result = null };


            var result = await accountContext.ChangePassword(getValues.Result);
            if (!result.Success)
                return result;

            return result;


        }
    }
}
