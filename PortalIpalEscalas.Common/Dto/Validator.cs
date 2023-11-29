﻿using PortalIpalEscalas.Common.Models;
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
               string.IsNullOrEmpty(valid.password))
                return new ObjectResponse<Login> { Success = false, Result = null, Errors = { new InternalError(eMessage.MSG_ERROR_LOGIN, "Propriedade não nulla vazia") } };

            obj.Result = valid;
            obj.Success = true;

            return obj;
        }

        public static ObjectResponse<ChangePass> ValidChangePass(ChangePass valid)
        {
            var obj = new ObjectResponse<ChangePass>();

            if (string.IsNullOrEmpty(valid.seql_User.ToString()) ||
               string.IsNullOrEmpty(valid.password) ||
               string.IsNullOrEmpty(valid.oldPassword))
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

        //public static ObjectListResponse<SelectScalerForUserRequest> ValidScaleForUser(SelectScalerForUserRequest valid)
        //{
        //    var obj = new ObjectListResponse<SelectScalerForUserRequest>();
        //    List<SelectScalerForUserRequest> list = new List<SelectScalerForUserRequest>();

        //    if (string.IsNullOrEmpty(valid.user) || string.IsNullOrEmpty(valid.dateScaleInit.ToString()) || string.IsNullOrEmpty(valid.dateScaleFinish.ToString()))
        //        return new ObjectListResponse<SelectScalerForUserRequest> { Success = false, ResultList = null, Errors = { new InternalError(eMessage.MSG_ERROR_REGISTERVALUES, "Propriedade não nulla vazia") } };

        //    list.Add(valid);
        //    foreach(var item in  valid)

        //    obj.ResultList.Add(valid);
        //    obj.Success = true;

        //    return obj;

        //}


        public static ObjectResponse<SendMessageWpp> ValidMessageWpp(SendMessageWpp valid)
        {
            var obj = new ObjectResponse<SendMessageWpp>();

            if (string.IsNullOrEmpty(valid.phoneNumber.ToString()) ||
               string.IsNullOrEmpty(valid.user) ||
               string.IsNullOrEmpty(valid.message))
                return new ObjectResponse<SendMessageWpp> { Success = false, Result = null, Errors = { new InternalError(eMessage.MSG_ERROR_LOGIN, "Propriedade não nulla vazia") } };

            obj.Result = valid;
            obj.Success = true;

            return obj;

        }


    }
}
