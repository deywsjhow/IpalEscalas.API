using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PortalIpalEscalas.Common.Models;
using RestSharp;

namespace PortalIpalEscalas.API.Sender
{
    public class SendMessage
    {
        private readonly IConfiguration configuration;

        public SendMessage(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<ObjectResponse<object>> SendMessageWhatsApp(SendMessageWpp wpp)
        {
            var result = new ObjectResponse<object>();

            try
            {       
                var number = wpp.phoneNumber.ToString();
                string message = $"Olá *{wpp.user}*. {wpp.message}";
                var url = $"https://api.ultramsg.com/{configuration.GetSection("InstanceIdWpp").Value}/messages/chat";
                var client = new RestClient(url);
                var request = new RestRequest(url, Method.Post);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");

                request.AddParameter("token", configuration.GetSection("TokenWpp").Value);
                request.AddParameter("to", number);
                request.AddParameter("body", message);


                RestResponse response = await client.ExecuteAsync(request);

                result.Success = true;
                result.Result = response.Content;

                return result;
            }
            catch (Exception)
            {
                return new ObjectResponse<object> { Success = false, Result = null, Errors = { new InternalError("Houve um erro no envio da mensagem!") } };

            }
        }

    }
}
