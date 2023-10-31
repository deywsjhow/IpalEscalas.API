using PortalIpalEscalas.Common.Dto;
using PortalIpalEscalas.Common.Models;
using PortalIpalEscalas.Infraestructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PortalIpalEscalas.Domain
{
    public class ScaleService : IScaleService
    {
        private readonly IScaleContext scaleContext;
        private readonly IToken token;
        public ScaleService(IScaleContext _scaleContext, IToken token)
        {
            this.scaleContext = _scaleContext;
            this.token = token;
        }


        public async Task<ObjectResponse<RegisterScaleResponse>> ScaleRegister(RegisterScaleResponse scale)
        {
            var getValues = new ObjectResponse<RegisterScaleResponse>();

            getValues = Validator.ValidRegisterScale(scale);

            if (!getValues.Success)
                return getValues;

            var result = await scaleContext.ScaleRegister(getValues.Result);

            if (!result.Success)
                return result;

            return result;
        }


        public async Task<ObjectListResponse<RegisterScaleResponse>> SelectScaleForUser(SelectScalerForUserRequest scale)
        {
            if (string.IsNullOrEmpty(scale.user) || string.IsNullOrEmpty(scale.dateScaleInit.ToString()) || string.IsNullOrEmpty(scale.dateScaleFinish.ToString()))
                return new ObjectListResponse<RegisterScaleResponse> { Success = false, ResultList = null, Errors = { new InternalError(eMessage.MSG_ERROR_REGISTERVALUES, "Propriedade não nulla vazia") } };

            var result = await scaleContext.SelectScaleForUser(scale);

            if (!result.Success)
                return result;

            return result;
        }
    }
}
