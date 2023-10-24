using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using PortalIpalEscalas.Infraestructure.Interfaces;

namespace PortalIpalEscalas.Repository
{
    public class AccountContext : IAccountContext
    {
        private readonly string _connectionString;
        private const string ProcUserRegister = "IPALSP_User";
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

                    connectionDB.Open();

                    var get = await connectionDB.QueryAsync<RegisterResponse>(ProcUserRegister, p, commandType: CommandType.StoredProcedure);

                }

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return result;
        }

    }
}
