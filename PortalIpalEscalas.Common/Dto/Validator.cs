using PortalIpalEscalas.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PortalIpalEscalas.Common.Dto
{
    public class Validator
    {
        public static ObjectResponse<RegisterResponse> ValidRegister(RegisterResponse valid) {
            var obj = new ObjectResponse<RegisterResponse>();

            if (valid == null)
                return new ObjectResponse<RegisterResponse> { Success = false, Result = null, Errors = { new InternalError (eMessage.MSG_ERROR_REGISTER, "Objeto Vazio")}};

            if (string.IsNullOrEmpty(valid.user)     || 
                string.IsNullOrEmpty(valid.password) ||
                string.IsNullOrEmpty(valid.name)     ||
                string.IsNullOrEmpty(valid.email)    ||
                string.IsNullOrEmpty(valid.attribuation))
                    return new ObjectResponse<RegisterResponse> { Success = false, Result = null, Errors = { new InternalError(eMessage.MSG_ERROR_REGISTERVALUES, "Propriedade não nulla vazia") } };

            obj.Result = valid;
            obj.Success = true;

            return obj;
        }


        public static ObjectResponse<AuthResponse> ValidAuth (AuthResponse valid)
        {
            var obj = new ObjectResponse<AuthResponse>();

            if(string.IsNullOrEmpty(valid.user)     ||
               string.IsNullOrEmpty(valid.password) ||
               string.IsNullOrEmpty(valid.loginType.ToString()))
                return new ObjectResponse<AuthResponse> { Success = false, Result = null, Errors = { new InternalError(eMessage.MSG_ERROR_LOGIN, "Propriedade não nulla vazia") } };

            obj.Result = valid;
            obj.Success = true;

            return obj;
        }
    }
}
