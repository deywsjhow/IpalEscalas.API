using PortalIpalEscalas.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PortalIpalEscalas.Infraestructure.Interfaces
{
    public interface IScaleService
    {
        Task<ObjectResponse<RegisterScaleResponse>> ScaleRegister(RegisterScaleResponse scale);
        Task<ObjectListResponse<RegisterScaleResponse>> SelectScaleForUser(SelectScalerForUserRequest scale);
        Task<ObjectListResponse<RegisterScaleResponse>> SelectScaleForAnyDate(SelectScalerForAnyDate scale);
    }
}

