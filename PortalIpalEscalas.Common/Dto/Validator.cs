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


        public static ObjectResponse<Login> ValidAuth (Login valid)
        {
            var obj = new ObjectResponse<Login>();

            if(string.IsNullOrEmpty(valid.user)     ||
               string.IsNullOrEmpty(valid.password) ||
               string.IsNullOrEmpty(valid.loginType.ToString()))
                return new ObjectResponse<Login> { Success = false, Result = null, Errors = { new InternalError(eMessage.MSG_ERROR_LOGIN, "Propriedade não nulla vazia") } };

            obj.Result = valid;
            obj.Success = true;

            return obj;
        }

        public static ObjectResponse<ChangePass> ValidChangePass(ChangePass valid)
        {
            var obj = new ObjectResponse<ChangePass>();

            if (string.IsNullOrEmpty(valid.Seql_Usuario.ToString()) ||
               string.IsNullOrEmpty(valid.Nom_Senha) ||
               string.IsNullOrEmpty(valid.Nom_SenhaOld))
                return new ObjectResponse<ChangePass> { Success = false, Result = null, Errors = { new InternalError(eMessage.MSG_ERROR_LOGIN, "Propriedade não nulla vazia") } };

            obj.Result = valid;
            obj.Success = true;

            return obj;

        }

        public static ObjectResponse<RegisterScaleResponse> ValidRegisterScale(RegisterScaleResponse valid)
        {
            var obj = new ObjectResponse<RegisterScaleResponse>();

            if (string.IsNullOrEmpty(valid.managerName) || string.IsNullOrEmpty(valid.dateScale.ToString()))
                    return new ObjectResponse<RegisterScaleResponse> { Success = false, Result = null, Errors = { new InternalError(eMessage.MSG_ERROR_REGISTERVALUES, "Propriedade não nulla vazia") } };

            obj.Result = valid;
            obj.Success = true;

            return obj;

        }

    }
}
