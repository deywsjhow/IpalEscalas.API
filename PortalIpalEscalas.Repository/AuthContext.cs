using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using PortalIpalEscalas.Common.Models;
using PortalIpalEscalas.Common.Models.Utils;
using PortalIpalEscalas.Infraestructure.Interfaces;

namespace PortalIpalEscalas.Repository
{
    public class AuthContext : IAuthtContext
    {
        private readonly string _connectionString;
        private const string ProcUserRegister = "IPALSP_User";
        private const string ProcUserLogin = "IPALSP_UserLogin";
        private const string ProcChangePassword = "IPALSP_AtualizaSenha";
        private const string ProcGetUsers = "IPALSP_UserSelect";
        public AuthContext(IConfiguration configuration) {
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
                    p.Add("Atribuicao", user.attribuation);
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



        public async Task<ObjectResponse<AuthResponse>> UserLogin(Login userLogin)
        {
            var result = new ObjectResponse<AuthResponse>();
            using (var connectionDB = this.Connection())
            {
                try
                {
                    var p = new DynamicParameters();
                    p.Add("Nom_Login", userLogin.user);
                    p.Add("Nom_Senha", userLogin.password);
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
                            userId = ret.First().id,
                            user = ret.First().Nom_Login,
                            email = ret.First().Nom_Email,
                            attribuation = ret.First().Atribuicao,
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


        public async Task<ObjectResponse<ChangePass>> ChangePassword(ChangePass user)
        {
            var result = new ObjectResponse<ChangePass>();

            try
            {
                using (var connectionDB = this.Connection())
                {
                    var p = new DynamicParameters();
                    p.Add("Seql_Usuario", user.seql_User);
                    p.Add("Nom_Senha", user.password);
                    p.Add("Nom_SenhaOld", user.oldPassword);
                    p.Add("Cod_Erro", null, dbType: DbType.Int32, direction: ParameterDirection.Output, 50);
                    p.Add("Msg_Erro", null, dbType: DbType.String, direction: ParameterDirection.Output, 50);

                    connectionDB.Open();

                    var ret = await connectionDB.QueryAsync<ChangePass>(ProcChangePassword, p, commandType: CommandType.StoredProcedure);

                    var Cod_Erro = p.Get<Int32>("Cod_Erro");
                    var Msg_Erro = p.Get<String>("Msg_Erro");

                    if (Cod_Erro != 0)
                    {
                        return new ObjectResponse<ChangePass> { Success = false, Result = null, Errors = { new InternalError(eMessage.MSG_ERROR_REGISTER, Msg_Erro) } };
                    }
                    else
                    {
                        return new ObjectResponse<ChangePass> { Success = true, Result = null, Errors = null };

                    }
                }

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return result;
        }

        public async Task<ObjectListResponse<User>> GetUsers()
        {
            var users = new ObjectListResponse<User>();

            try
            {
                using (var connectionDB = this.Connection())
                {    
                  

                    connectionDB.Open();

                    var ret = await connectionDB.QueryAsync<User>(ProcGetUsers, commandType: CommandType.StoredProcedure);


                    if (ret == null)
                    {
                        return new ObjectListResponse<User> { Success = false, ResultList = null, Errors = { new InternalError(eMessage.MSG_ERROR_REGISTER, "Houve algum problema na ca=hamada") } };
                    }
                    else
                    {
                        List<User> list = new List<User>();
                        int count = 0;


                        foreach (var item in ret)
                        {

                            list.Add(new User());
                            list[count].Nom_Login = item.Nom_Login;
                            count++;
                        }

                        users.ResultList = list;
                        users.Success = true;
                        users.Errors = null;
                    }                
                        
                }

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return users;
        }

    }
}
