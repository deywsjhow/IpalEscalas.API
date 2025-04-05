using PortalIpalEscalas.Common.Dto;
using PortalIpalEscalas.Common.Models;
using PortalIpalEscalas.Infraestructure.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Validator = PortalIpalEscalas.Common.Dto.Validator;

namespace PortalIpalEscalas.Domain
{
    public class ScaleService(IScaleContext _scaleContext) : IScaleService
    {
        public async Task<ObjectResponse<RegisterScaleResponse>> ScaleRegister(RegisterScaleResponse scale)
        {
            ObjectResponse<RegisterScaleResponse> getValues = Validator.ValidRegisterScale(scale);

            if (!getValues.Success)
                return getValues;

            var result = await _scaleContext.ScaleRegister(getValues.Result);

            if (!result.Success)
                return result;

            return result;
        }


        public async Task<ObjectListResponse<RegisterScaleResponse>> SelectScaleForUser(SelectScalerForUserRequest scale)
        {
            if (string.IsNullOrEmpty(scale.user) || string.IsNullOrEmpty(scale.dateScaleInit.ToString()) || string.IsNullOrEmpty(scale.dateScaleFinish.ToString()))
                return new ObjectListResponse<RegisterScaleResponse> { Success = false, ResultList = null, Errors = { new InternalError(eMessage.MSG_ERROR_REGISTERVALUES, "Propriedade não nulla vazia") } };

            var result = await _scaleContext.SelectScaleForUser(scale);

            if (!result.Success)
                return result;

            return result;
        }

        public async Task<ObjectListResponse<RegisterScaleResponse>> SelectScaleForAnyDate(SelectScalerForAnyDate scale)
        {
            if (string.IsNullOrEmpty(scale.dateScaleInit.ToString()) || string.IsNullOrEmpty(scale.dateScaleFinish.ToString()))
                return new ObjectListResponse<RegisterScaleResponse> { Success = false, ResultList = null, Errors = { new InternalError(eMessage.MSG_ERROR_REGISTERVALUES, "Propriedade não nulla vazia") } };

            var result = await _scaleContext.SelectScaleAnyDate(scale);

            if (!result.Success)
                return result;          

            return result;
        }
    }
}
