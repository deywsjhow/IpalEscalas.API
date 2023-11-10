using Microsoft.Extensions.Configuration;
using PortalIpalEscalas.Infraestructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using PortalIpalEscalas.Common.Models;
using Dapper;
using Newtonsoft.Json.Linq;

namespace PortalIpalEscalas.Repository
{
    public class ScaleContext : IScaleContext
    {
        private readonly string _connectionString;
        private const string ProcRegisterScale = "IPALSP_RegistraEscala";
        private const string ProcSelectScaleForUser = "IPALSP_SelecionaEscalaUsuario";
        private const string ProcSelectScaleForAnyDate = "IPALSP_SelecionaEscalaQualquerData";

        public ScaleContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection Connection()
        {
            return new SqlConnection(_connectionString);
        }


        public async Task<ObjectResponse<RegisterScaleResponse>> ScaleRegister(RegisterScaleResponse scale)
        {
            var result = new ObjectResponse<RegisterScaleResponse>();

            using (var connectionDB = this.Connection())
            {
                try
                {
                    var p = new DynamicParameters();
                    p.Add("Nom_Dirigente", scale.managerName);
                    p.Add("Nom_PrimeiroBack", scale.firstBack);
                    p.Add("Nom_SegundoBack", scale.secondBack);
                    p.Add("Nom_TerceiroBack", scale.thirdBack);
                    p.Add("Nom_MusicoViolao", scale.guitarMusician);
                    p.Add("Nom_MusicoGuitarra", scale.guitarristMusician);
                    p.Add("Nom_MusicoBateria", scale.drumMusician);
                    p.Add("Nom_MusicoBaixo", scale.bassMusician);
                    p.Add("Nom_MusicoTeclado", scale.keyboardMusician);
                    p.Add("Dat_DiaDaEscala", Convert.ToDateTime(scale.dateScale));
                    p.Add("Cod_Erro", null, dbType: DbType.Int32, direction: ParameterDirection.Output, 50);
                    p.Add("Msg_Erro", null, dbType: DbType.String, direction: ParameterDirection.Output, 50);

                    connectionDB.Open();

                    var ret = await connectionDB.QueryAsync<RegisterScaleResponse>(ProcRegisterScale, p, commandType: CommandType.StoredProcedure);

                    var Cod_Erro = p.Get<Int32>("Cod_Erro");
                    var Msg_Erro = p.Get<String>("Msg_Erro");


                    if (Cod_Erro != 0)
                    {
                        return new ObjectResponse<RegisterScaleResponse> { Success = false, Result = null, Errors = { new InternalError(eMessage.MSG_ERROR_REGISTERVALUES, Msg_Erro) } };
                    }
                    else
                    {
                        result.Success = true;
                        result.Result = null;
                        result.Errors = null;

                    }
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
            }
            return result;

        }

        public async Task<ObjectListResponse<RegisterScaleResponse>> SelectScaleForUser(SelectScalerForUserRequest scale)
        {
            var result = new ObjectListResponse<RegisterScaleResponse>();

            using (var connectionDB = this.Connection())
            {
                try
                {
                    var p = new DynamicParameters();
                    p.Add("Nom_Usuario", scale.user);
                    p.Add("Dat_DiaDaEscala_Ini", scale.dateScaleInit);
                    p.Add("Dat_DiaDaEscala_Fim", scale.dateScaleFinish);
                    p.Add("Cod_Erro", null, dbType: DbType.Int32, direction: ParameterDirection.Output, 50);
                    p.Add("Msg_Erro", null, dbType: DbType.String, direction: ParameterDirection.Output, 50);

                    connectionDB.Open();

                    var ret = await connectionDB.QueryAsync<SelectScalesDB>(ProcSelectScaleForUser, p, commandType: CommandType.StoredProcedure);

                    var Cod_Erro = p.Get<Int32>("Cod_Erro");
                    var Msg_Erro = p.Get<String>("Msg_Erro");


                    if (Cod_Erro != 0)
                    {
                        return new ObjectListResponse<RegisterScaleResponse> { Success = false, ResultList = null, Errors = { new InternalError(eMessage.MSG_ERROR_REGISTERVALUES, Msg_Erro) } };
                    }
                    else
                    {
                        List<RegisterScaleResponse> list = new List<RegisterScaleResponse>();
                        int count = 0;
                     
                        foreach (var item in ret)
                        {

                            list.Add(new RegisterScaleResponse());
                            list[count].scaleId = item.Seql_Escala;
                            list[count].managerName = item.Nom_Dirigente;
                            list[count].firstBack = item.Nom_PrimeiroBack;
                            list[count].secondBack = item.Nom_SegundoBack;
                            list[count].thirdBack = item.Nom_TerceiroBack;
                            list[count].guitarMusician = item.Nom_MusicoViolao;
                            list[count].guitarristMusician = item.Nom_MusicoGuitarra;
                            list[count].drumMusician = item.Nom_MusicoBateria;
                            list[count].bassMusician = item.Nom_MusicoBaixo;
                            list[count].keyboardMusician = item.Nom_MusicoTeclado;
                            list[count].dateScale = item.Dat_DiaDaEscala.ToString("dd/MM/yyyy");

                            count++;
                        }


                        result.ResultList = list;
                        result.Success = true;
                        result.Errors = null;

                    }
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
            }
            return result;

        }



        public async Task<ObjectListResponse<RegisterScaleResponse>> SelectScaleAnyDate(SelectScalerForAnyDate scale)
        {
            var result = new ObjectListResponse<RegisterScaleResponse>();

            using (var connectionDB = this.Connection())
            {
                try
                {
                    var p = new DynamicParameters();
                    p.Add("Dat_DiaDaEscala_Ini", scale.dateScaleInit);
                    p.Add("Dat_DiaDaEscala_Fim", scale.dateScaleFinish);
                    p.Add("Cod_Erro", null, dbType: DbType.Int32, direction: ParameterDirection.Output, 50);
                    p.Add("Msg_Erro", null, dbType: DbType.String, direction: ParameterDirection.Output, 50);

                    connectionDB.Open();

                    var ret = await connectionDB.QueryAsync<SelectScalesDB>(ProcSelectScaleForAnyDate, p, commandType: CommandType.StoredProcedure);

                    var Cod_Erro = p.Get<Int32>("Cod_Erro");
                    var Msg_Erro = p.Get<String>("Msg_Erro");


                    if (Cod_Erro != 0)
                    {
                        return new ObjectListResponse<RegisterScaleResponse> { Success = false, ResultList = null, Errors = { new InternalError(eMessage.MSG_ERROR_REGISTERVALUES, Msg_Erro) } };
                    }
                    else
                    {
                        List<RegisterScaleResponse> list = new List<RegisterScaleResponse>();
                        int count = 0;

                     
                        foreach (var item in ret)
                        {

                            list.Add(new RegisterScaleResponse());
                            list[count].scaleId = item.Seql_Escala;
                            list[count].managerName = item.Nom_Dirigente;
                            list[count].firstBack = item.Nom_PrimeiroBack;
                            list[count].secondBack = item.Nom_SegundoBack;
                            list[count].thirdBack = item.Nom_TerceiroBack;
                            list[count].guitarMusician = item.Nom_MusicoViolao;
                            list[count].guitarristMusician = item.Nom_MusicoGuitarra;
                            list[count].drumMusician = item.Nom_MusicoBateria;
                            list[count].bassMusician = item.Nom_MusicoBaixo;
                            list[count].keyboardMusician = item.Nom_MusicoTeclado;
                            list[count].dateScale = item.Dat_DiaDaEscala.ToString("dd/MM/yyyy");

                            count++;
                        }


                        result.ResultList = list;
                        result.Success = true;
                        result.Errors = null;

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