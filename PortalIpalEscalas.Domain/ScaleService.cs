using PortalIpalEscalas.API.Sender;
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
    public class ScaleService : IScaleService
    {
        private readonly IScaleContext scaleContext;
        private readonly IAuthtContext authContext;
        private readonly IToken token;
        private readonly SendMessage send;
        public ScaleService(IScaleContext _scaleContext, IToken token, IAuthtContext _authContext, SendMessage _send)
        {
            this.scaleContext = _scaleContext;
            this.authContext = _authContext;
            this.send = _send;
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

            var users = await authContext.GetUsers();



            if (users.Success)
            {
                List<SendMessageWpp> x = new List<SendMessageWpp>();
                var values = new SendMessageWpp();
                int count = 0;
                
                foreach (var item in users.ResultList)
                {
                    x.Add(new SendMessageWpp());

                    if (scale.managerName == item.Nom_Login)
                    {
                        x[count].user = item.Nom_Login;
                        x[count].phoneNumber = item.Telefone;
                    }
                        

                    if (scale.firstBack == item.Nom_Login)
                    {
                        if (item.Nom_Login == x[count].user) continue;
                        x[count].user = item.Nom_Login;
                        x[count].phoneNumber = item.Telefone;
                    }

                    if (scale.secondBack == item.Nom_Login)
                    {
                        if (item.Nom_Login == x[count].user) continue;
                        x[count].user = item.Nom_Login;
                        x[count].phoneNumber = item.Telefone;
                    }

                    if (scale.thirdBack == item.Nom_Login)
                    {
                        if (item.Nom_Login == x[count].user) continue;
                        x[count].user = item.Nom_Login;
                        x[count].phoneNumber = item.Telefone;
                    }

                    if (scale.guitarMusician == item.Nom_Login)
                    {
                        if (item.Nom_Login == x[count].user) continue;
                        x[count].user = item.Nom_Login;
                        x[count].phoneNumber = item.Telefone;
                    }

                    if (scale.guitarristMusician == item.Nom_Login)
                    {
                        if (item.Nom_Login == x[count].user) continue;
                        x[count].user = item.Nom_Login;
                        x[count].phoneNumber = item.Telefone;
                    }

                    if (scale.drumMusician == item.Nom_Login)
                    {
                        if (item.Nom_Login == x[count].user) continue;
                        x[count].user = item.Nom_Login;
                        x[count].phoneNumber = item.Telefone;
                    }

                    if (scale.bassMusician == item.Nom_Login)
                    {
                        if (item.Nom_Login == x[count].user) continue;
                        x[count].user = item.Nom_Login;
                        x[count].phoneNumber = item.Telefone;
                    }

                    if (scale.keyboardMusician == item.Nom_Login)
                    {
                        if (item.Nom_Login == x[count].user) continue;
                        x[count].user = item.Nom_Login;
                        x[count].phoneNumber = item.Telefone;
                    }

                    count++;
                }
               

                foreach (var item in x) {
                    values.user = item.user;
                    values.message = $"Você está na escala do dia {Convert.ToDateTime(scale.dateScale).ToString("dd/MM/yyyy")}";
                    values.phoneNumber = item.phoneNumber;

                    if(values.phoneNumber != null || values.phoneNumber == "")
                    {
                        try
                        {
                            await send.SendMessageWhatsApp(values);
                        }
                        catch (Exception)
                        {
                            continue;
                        }

                    }
                    else
                    {
                        continue;
                    }

                }
                    
            }

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

        public async Task<ObjectListResponse<RegisterScaleResponse>> SelectScaleForAnyDate(SelectScalerForAnyDate scale)
        {
            if (string.IsNullOrEmpty(scale.dateScaleInit.ToString()) || string.IsNullOrEmpty(scale.dateScaleFinish.ToString()))
                return new ObjectListResponse<RegisterScaleResponse> { Success = false, ResultList = null, Errors = { new InternalError(eMessage.MSG_ERROR_REGISTERVALUES, "Propriedade não nulla vazia") } };

            var result = await scaleContext.SelectScaleAnyDate(scale);

            if (!result.Success)
                return result;          

            return result;
        }
    }
}
