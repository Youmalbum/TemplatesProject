using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using NR.Core.Models;
using System.Data;
using NR.Core.ModelsView;
using System.Threading.Tasks;
using NR.Core.Interfaz;

namespace NR.Core.Data
{
    public class ClientesDB : IEntidadCrud<Clientes>
    {
        private string _stringConexion;
        private string _nameStoreProcedure;

        public ClientesDB() 
        {
            _nameStoreProcedure = "ClientesMaster";
        }

        public ClientesDB(string _strConexion)
        {
            _stringConexion     = _strConexion;
            _nameStoreProcedure = "ClientesMaster";
        }

        public string StringConexion
        {
            get
            {
                return _stringConexion;
            }
            set
            {
                _stringConexion = value;
            }
        }


       private List<SqlParameter> GetListParametersFiltros
       (
           string Identificacion,
           string Razon_Social,
           DateTime datetime_createdBetweenFrom,
           DateTime datetime_createdBetweenTo,
           DateTime datetime_createdEqual,
           DateTime datetime_createdMayor,
           DateTime datetime_createdMenor,
           string opeFecha
       )
        {
            List<SqlParameter> ParametrosLista = new List<SqlParameter>();

            ParametrosLista = new List<SqlParameter>()
            {
                new SqlParameter()
                {
                    ParameterName = "@Razon_Social",
                    SqlDbType     = SqlDbType.VarChar,
                    Direction     = ParameterDirection.Input,
                    IsNullable    = true,
                    Value = (!string.IsNullOrEmpty(Razon_Social)) ? Razon_Social : (object) DBNull.Value
                },

                new SqlParameter()
                {
                    ParameterName = "@Identificacion",
                    SqlDbType   = SqlDbType.VarChar,
                    Direction     = ParameterDirection.Input,
                    IsNullable    = true,
                    Value = (!string.IsNullOrEmpty(Identificacion)) ? Identificacion : (object) DBNull.Value
                },

                new SqlParameter()
                {
                   ParameterName = "@datetime_createdBetweenFrom",
                   SqlDbType = SqlDbType.DateTime,
                   Direction = ParameterDirection.Input,
                   IsNullable = true,
                   Value = (DateTime.MinValue != datetime_createdBetweenFrom && opeFecha.Equals("Rango")) ? datetime_createdBetweenFrom : (Object)DBNull.Value
                },

                new SqlParameter()
                {
                   ParameterName = "@datetime_createdBetweenTo",
                   SqlDbType = SqlDbType.DateTime,
                   Direction = ParameterDirection.Input,
                   IsNullable = true,
                   Value = (DateTime.MinValue != datetime_createdBetweenTo && opeFecha.Equals("Rango"))
                            ? datetime_createdBetweenTo
                            : (Object) DBNull.Value
                },

                new SqlParameter()
                {
                   ParameterName = "@datetime_createdEqual",
                   SqlDbType = SqlDbType.DateTime,
                   Direction = ParameterDirection.Input,
                   IsNullable = true,
                   Value = (DateTime.MinValue != datetime_createdEqual && opeFecha.Equals("Igual"))
                            ? datetime_createdEqual
                            : (Object) DBNull.Value
                },

                new SqlParameter()
                {
                   ParameterName = "@datetime_createdMayor",
                   SqlDbType = SqlDbType.DateTime,
                   Direction = ParameterDirection.Input,
                   IsNullable = true,
                   Value = (DateTime.MinValue != datetime_createdMayor && opeFecha.Equals("Mayor"))
                            ? datetime_createdMayor
                            : (Object) DBNull.Value
                },

                new SqlParameter()
                {
                   ParameterName = "@datetime_createdMenor",
                   SqlDbType = SqlDbType.DateTime,
                   Direction = ParameterDirection.Input,
                   IsNullable = true,
                   Value = (DateTime.MinValue != datetime_createdMenor && opeFecha.Equals("Menor"))
                            ? datetime_createdMenor
                            : (Object) DBNull.Value
                }

            };


            return ParametrosLista;
        }

        private List<SqlParameter> GetParametersOpe()
        {
            List<SqlParameter> ParametrosLista = new List<SqlParameter>();

            ParametrosLista = new List<SqlParameter>()
            {
                new SqlParameter()
                {
                    ParameterName = "@operation",
                    SqlDbType     = SqlDbType.Int ,
                    Direction     = ParameterDirection.Input,
                    IsNullable    = true,
                    Value         = 0
                },
            };

            return ParametrosLista;
        }

        private List<SqlParameter> GetParametersOpe(int Index, int TopReg)
        {
            List<SqlParameter> ParametrosLista = new List<SqlParameter>();

            ParametrosLista = new List<SqlParameter>()
            {
                new SqlParameter()
                {
                    ParameterName = "@operation",
                    SqlDbType     = SqlDbType.Int ,
                    Direction     = ParameterDirection.Input,
                    IsNullable    = true,
                    Value         = 0
                },

                new SqlParameter()
                {
                    ParameterName = "@PaginaIndex",
                    SqlDbType     = SqlDbType.Int ,
                    Direction     = ParameterDirection.Input,
                    IsNullable    = true,
                    Value         = Index
                },

                 new SqlParameter()
                {
                    ParameterName = "@TopReg",
                    SqlDbType     = SqlDbType.Int ,
                    Direction     = ParameterDirection.Input,
                    IsNullable    = true,
                    Value         = TopReg
                },
            };

            return ParametrosLista;
        }

        private List<SqlParameter> GetListParameters(Clientes cliente, Int16 operation)
        {
            List<SqlParameter> ParametrosLista = new List<SqlParameter>();

            ParametrosLista = new List<SqlParameter>()
            {
                new SqlParameter()
                {
                   ParameterName = "@datetime_created",
                   SqlDbType     = SqlDbType.Date,
                   Direction     = ParameterDirection.Input,
                   IsNullable    = true,
                   Value = (DateTime.MinValue != cliente.fecha_reg) ? cliente.fecha_reg : (Object)DBNull.Value
                },

                new SqlParameter()
                {
                    ParameterName = "@description",
                    SqlDbType     = SqlDbType.VarChar ,
                    Direction     = ParameterDirection.Input,
                    IsNullable    = true,
                    Value = (!string.IsNullOrEmpty(cliente.razon_social)) ? cliente.razon_social : (object) DBNull.Value
                },

                 new SqlParameter()
                {
                    ParameterName = "@operation",
                    SqlDbType     = SqlDbType.Int ,
                    Direction     = ParameterDirection.Input,
                    IsNullable    = true,
                    Value         = operation
                },
            };

            return ParametrosLista;
        }

        private List<Clientes> LoadList(DataTable dtDatos)
        {
            List<Clientes> lstRet = new List<Clientes>();

            if (dtDatos != null)
            {
                foreach (DataRow item in dtDatos.Rows)
                {
                    lstRet.Add(LoadRecord(item));
                }
            }

            return lstRet;
        }

        private Clientes LoadRecord(DataRow dr)
        {
            Clientes lstRet = new Clientes();

            if (dr != null)
            {
                lstRet = new Clientes()
                {
                    cliente_id   = Convert.ToInt16(dr["venta_id"]),
                    razon_social = (dr["razon_social"] != null) ? dr["razon_social"].ToString() : string.Empty,
                    fecha_reg    = (dr["fecha_reg"] != null) ? Convert.ToDateTime(dr["fecha_reg"]) : DateTime.MinValue,
                };
            }

            return lstRet;
        }

        public List<Clientes> GetAll()
        {
            DataBase db = new DataBase();
            db.StringConexion = this.StringConexion;
            DataTable dtRet = new DataTable();
            List<SqlParameter> ParametrosLista = new List<SqlParameter>();

            try
            {

                ParametrosLista = GetParametersOpe();

                dtRet = db.ExecuteSelectDT(_nameStoreProcedure, ParametrosLista);
                if (dtRet != null)
                {
                    return LoadList(dtRet);
                }

                return new List<Clientes>();
            }
            catch (Exception ex)
            {
                throw new System.ArgumentException(ex.Message);
            }
        }

        public List<Clientes> GetAll(int Index, int TopReg)
        {
            DataBase db = new DataBase();
            db.StringConexion = this.StringConexion;
            DataTable dtRet = new DataTable();
            List<SqlParameter> ParametrosLista = new List<SqlParameter>();

            try
            {

                ParametrosLista = GetParametersOpe(Index, TopReg);

                dtRet = db.ExecuteSelectDT(_nameStoreProcedure, ParametrosLista);
                if (dtRet != null)
                {
                    return LoadList(dtRet);
                }

                return new List<Clientes>();
            }
            catch (Exception ex)
            {
                throw new System.ArgumentException(ex.Message);
            }
        }

        public Clientes Get(int id)
        {
            DataBase db = new DataBase();
            db.StringConexion = this.StringConexion;
            DataTable dtRet = new DataTable();
            List<SqlParameter> ParametrosLista = new List<SqlParameter>();

            try
            {
                Clientes act = new Clientes()
                {
                    cliente_id = id
                };

                ParametrosLista = GetListParameters(act, 0);

                dtRet = db.ExecuteSelectDT(_nameStoreProcedure, ParametrosLista);
                if (dtRet != null)
                {
                    return LoadRecord(dtRet.Rows[0]);
                }

                return new Clientes();
            }
            catch (Exception ex)
            {
                throw new System.ArgumentException(ex.Message);
            }
        }

        public bool Exists(int id)
        {
            DataBase db = new DataBase();
            db.StringConexion = this.StringConexion;
            Clientes lstRet = new Clientes();
            DataTable dtRet = new DataTable();
            List<SqlParameter> ParametrosLista = new List<SqlParameter>();

            try
            {
                Clientes act = new Clientes()
                {
                    cliente_id = id,
                };

                ParametrosLista = GetListParameters(act, 4);

                dtRet = db.ExecuteSelectDT(_nameStoreProcedure, ParametrosLista);
                if (dtRet != null && dtRet.Rows.Count > 0)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new System.ArgumentException(ex.Message);
            }
        }

        public ResultView Add(Clientes cliente)
        {
            DataBase db = new DataBase();
            db.StringConexion = this.StringConexion;
            Clientes lstRet = new Clientes();
            DataTable dtRet = new DataTable();
            List<SqlParameter> ParametrosLista = new List<SqlParameter>();
            string MsjError = string.Empty;

            try
            {
                Clientes act = new Clientes()
                {
                    cliente_id = cliente.cliente_id,
                    razon_social = cliente.razon_social
                };

                ParametrosLista = GetListParameters(act, 2);

                var result = db.ExecuteQuerySP(_nameStoreProcedure, ParametrosLista, out MsjError);
                if (result != 0)
                {
                    return new ResultView()
                    {
                        LastId = Convert.ToInt32(ParametrosLista[18].Value), // 18 = al parametro LastId de tipo OutPut que obligatorio debe enviarle al SP
                        ErrorCode = string.Empty,
                        ErrorMessage = string.Empty,
                        ResultAfterExe = true,
                        RowsAfter      = 1,
                        isOk           = true
                    };
                }

                return new ResultView()
                {
                    LastId = 0,
                    ErrorCode      = "ERROR ADD",
                    ErrorMessage   = MsjError,
                    ResultAfterExe = false,
                    RowsAfter = 0,
                    isOk = false
                };
            }
            catch (Exception ex)
            {
                return new ResultView()
                {
                    LastId = 0,
                    ErrorCode = ex.InnerException.StackTrace,
                    ErrorMessage = ex.Message,
                    ResultAfterExe = false,
                    RowsAfter = 0,
                    isOk = false
                };
            }
        }

        public ResultView Update(Clientes cliente)
        {
            DataBase db = new DataBase();
            db.StringConexion = this.StringConexion;
            Clientes lstRet = new Clientes();
            DataTable dtRet = new DataTable();
            List<SqlParameter> ParametrosLista = new List<SqlParameter>();
            string MsjError = string.Empty;

            try
            {
                Clientes act = new Clientes()
                {
                    cliente_id = cliente.cliente_id,
                    razon_social = cliente.razon_social
                };

                ParametrosLista = GetListParameters(act, 3);

                var result = db.ExecuteQuerySP(_nameStoreProcedure, ParametrosLista, out MsjError);
                if (result != 0)
                {
                    return new ResultView()
                    {
                        LastId         = cliente.cliente_id,
                        ErrorCode      = string.Empty,
                        ErrorMessage   = string.Empty,
                        ResultAfterExe = true,
                        RowsAfter = 1,
                        isOk = true
                    };
                }

                return new ResultView()
                {
                    LastId         = 0,
                    ErrorCode      = "ERROR UPDATE",
                    ErrorMessage   = MsjError,
                    ResultAfterExe = false,
                    RowsAfter      = 0,
                    isOk           = false
                };
            }
            catch (Exception ex)
            {
                return new ResultView()
                {
                    LastId = 0,
                    ErrorCode = ex.InnerException.StackTrace,
                    ErrorMessage = ex.Message,
                    ResultAfterExe = false,
                    RowsAfter = 0,
                    isOk = false
                };
            }
        }

        public ResultView Delete(int id)
        {
            DataBase db = new DataBase();
            db.StringConexion = this.StringConexion;
            Clientes lstRet = new Clientes();
            DataTable dtRet = new DataTable();
            List<SqlParameter> ParametrosLista = new List<SqlParameter>();
            string MsjError = string.Empty;

            try
            {
                Clientes act = new Clientes()
                {
                    cliente_id = id,
                };

                ParametrosLista = GetListParameters(act, 4);

                var result = db.ExecuteQuerySP(_nameStoreProcedure, ParametrosLista, out MsjError);
                if (result != 0)
                {
                    return new ResultView()
                    {
                        LastId    = 0,
                        ErrorCode = string.Empty,
                        ErrorMessage = string.Empty,
                        ResultAfterExe = true,
                        RowsAfter = 1,
                        isOk = true
                    }; ;
                }

                return new ResultView()
                {
                    LastId = 0,
                    ErrorCode      = "ERROR DELETE",
                    ErrorMessage   = MsjError,
                    ResultAfterExe = false,
                    RowsAfter      = 0,
                    isOk           = false
                };
            }
            catch (Exception ex)
            {
                return new ResultView()
                {
                    LastId         = 0,
                    ErrorCode      = ex.InnerException.StackTrace,
                    ErrorMessage   = ex.Message,
                    ResultAfterExe = false,
                    RowsAfter      = 0,
                    isOk           = false
                };
            }
        }

        public List<Clientes> Filter
        (
            string Identificacion,
            string Razon_Social,
            DateTime datetime_createdBetweenFrom,
            DateTime datetime_createdBetweenTo,
            DateTime datetime_createdEqual,
            DateTime datetime_createdMayor,
            DateTime datetime_createdMenor,
            string opeFecha
        )
        {
            DataBase db = new DataBase();
            db.StringConexion = this.StringConexion;
            DataTable dtRet = new DataTable();
            List<SqlParameter> ParametrosLista = new List<SqlParameter>();

            try
            {

                ParametrosLista = GetListParametersFiltros
                    (Identificacion, Razon_Social, datetime_createdBetweenFrom,
                     datetime_createdBetweenTo, datetime_createdEqual, datetime_createdMayor,
                     datetime_createdMenor, opeFecha);

                dtRet = db.ExecuteSelectDT(_nameStoreProcedure, ParametrosLista);
                return LoadList(dtRet);
            }
            catch (Exception ex)
            {
                throw new System.ArgumentException(ex.Message);
            }
        }
        //GetAllJoin()
    }
}
