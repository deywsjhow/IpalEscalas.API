using PortalIpalEscalas.Common.Dto;
using PortalIpalEscalas.Common.Models;
using PortalIpalEscalas.Common.Models.Utils;
using PortalIpalEscalas.Infraestructure.Interfaces;
using System.Threading.Tasks;

namespace PortalIpalEscalas.Domain
{
    public class AuthService(IAuthtContext _authContext, IToken token) : IAuthService
    {
        public async Task<ObjectResponse<RegisterResponse>> UserRegister(RegisterResponse request)
        {
            ObjectResponse<RegisterResponse> getValues = Validator.ValidRegister(request);

            if (!getValues.Success) 
                return getValues;
            
            var result = await _authContext.UserRegister(getValues.Result);

            if (!result.Success)
                return result;

            return result;
        }


        public async Task<ObjectResponse<AuthResponse>> AutheService(Login authModel)
        {            
            ObjectResponse<Login> getValues = Validator.ValidAuth(authModel);

            if (!getValues.Success)
                return new ObjectResponse<AuthResponse> { Success = getValues.Success, Errors = getValues.Errors, Result = null};

            var result = await _authContext.UserLogin(getValues.Result);
            if (!result.Success)
                return result;

            result.Result.accessToken = token.AddToken(result.Result, authModel.user, authModel.password);


            return result;
        }

        public async Task<ObjectResponse<ChangePass>> ChangePassword(ChangePass changePass)
        {
            ObjectResponse<ChangePass>  getValues = Validator.ValidChangePass(changePass);

            if (!getValues.Success)
                return new ObjectResponse<ChangePass> { Success = getValues.Success, Errors = getValues.Errors, Result = null };


            var result = await _authContext.ChangePassword(getValues.Result);
            if (!result.Success)
                return result;

            return result;


        }

        public async Task<ObjectListResponse<UserLogin>> GetUsers()
        {

            var ret = await _authContext.GetUsersLogins();         
            

            return ret;
        }
    }
}
