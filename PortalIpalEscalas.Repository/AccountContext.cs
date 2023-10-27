using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using PortalIpalEscalas.Common.Models;
using PortalIpalEscalas.Infraestructure.Interfaces;

namespace PortalIpalEscalas.Repository
{
    public class AccountContext : IAccountContext
    {
        private readonly string _connectionString;
        private const string ProcUserRegister = "IPALSP_User";
        private const string ProcUserLogin = "IPALSP_UserLogin";
        public AccountContext(IConfiguration configuration) {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection Connection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<ObjectResponse<RegisterResponse>> UserRegister(RegisterResponse user)
        {
            var result = new ObjectResponse<RegisterResponse>();

            try
            {
                using(var connectionDB = this.Connection())
                {
                    var p = new DynamicParameters();
                    p.Add("Nom_Login", user.user);
                    p.Add("Nom_Senha", user.password);
                    p.Add("Nom_Email", user.email);
                    p.Add("Nome", user.name);
                    p.Add("Atribuicao", user.attribuation);
                    p.Add("TipoVoz", user.voiceType);
                    p.Add("Instrumento", user.Instrument);
                    p.Add("SegundoInstrumento", user.secondInstrument);
                    p.Add("Num_TipoLogin", 2);
                    p.Add("Cod_Erro", null, dbType: DbType.Int32, direction: ParameterDirection.Output, 50);
                    p.Add("Msg_Erro", null, dbType: DbType.String, direction: ParameterDirection.Output, 50);

                    connectionDB.Open();

                    var ret = await connectionDB.QueryAsync<RegisterResponse>(ProcUserRegister, p, commandType: CommandType.StoredProcedure);

                    var Cod_Erro = p.Get<Int32>("Cod_Erro");
                    var Msg_Erro = p.Get<String>("Msg_Erro");

                    if (Cod_Erro != 0)
                    {
                        return new ObjectResponse<RegisterResponse> { Success = false, Result = null, Errors = { new InternalError(eMessage.MSG_ERROR_REGISTER, Msg_Erro) } };
                    }
                    else
                    {
                        return new ObjectResponse<RegisterResponse> { Success = true, Result = null};

                    }
                }

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return result;
        }



        public async Task<ObjectResponse<AuthResponse>> UserLogin(AuthResponse userLogin)
        {
            var result = new ObjectResponse<AuthResponse>();
            using (var connectionDB = this.Connection())
            {
                try
                {
                    var p = new DynamicParameters();
                    p.Add("Nom_Login", userLogin.user);
                    p.Add("Nom_Senha", userLogin.password);
                    p.Add("Num_TipoLogin", userLogin.loginType);
                    p.Add("Cod_Erro", null, dbType: DbType.Int32, direction: ParameterDirection.Output, 50);
                    p.Add("Msg_Erro", null, dbType: DbType.String, direction: ParameterDirection.Output, 50);

                    connectionDB.Open();

                    var ret = await connectionDB.QueryAsync<AuthDB>(ProcUserLogin, p, commandType: CommandType.StoredProcedure);

                    var Cod_Erro = p.Get<Int32>("Cod_Erro");
                    var Msg_Erro = p.Get<String>("Msg_Erro");


                    if (Cod_Erro != 0)
                    {
                        return new ObjectResponse<AuthResponse> { Success = false, Result = null, Errors = { new InternalError(eMessage.MSG_ERROR_LOGIN, Msg_Erro) } };
                    }
                    else
                    {
                        result.Success = true;
                        result.Result = new AuthResponse
                        {
                            user = ret.First().Nom_Login,
                            name = ret.First().Nome,
                            email = ret.First().Nom_Email,
                            attribuation = ret.First().Atribuicao,
                            voiceType = ret.First().TipoVoz,
                            Instrument = ret.First().Instrumento,
                            secondInstrument = ret.First().SegundoInstrumento,
                            loginType = ret.First().Seql_Tipo,
                            nameType = ret.First().Nom_Tipo
                        };

                    }

                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
            }
            return result;
        }

    }
}
