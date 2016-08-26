using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;

namespace Mvc_Qdis.Helper
{

    public class SqlHelper
    {

        public static readonly string connstr = "server=10.233.15.36;database =Qdis;uid =sa;pwd=123";
        public static DataTable ExecuteDataTable(string connectionstring, CommandType cmdtype, string commandText, params SqlParameter[] cmdParms)
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                System.Data.SqlClient.SqlCommand command = new SqlCommand();
                command.CommandText = commandText;
                command.CommandType = cmdtype;
                command.Connection = con;
                command.CommandTimeout = 1000 * 60 * 10;
                if (cmdParms != null)
                {
                    command.Parameters.AddRange(cmdParms);
                }
                adapter.SelectCommand = command;
                adapter.Fill(ds, "tb");
            }
            return ds.Tables["tb"];

        }
        public static DataTable ExecuteDataTable(string connectionstring, CommandType cmdtype, string commandText, int timeout, params SqlParameter[] cmdParms)
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                System.Data.SqlClient.SqlCommand command = new SqlCommand();
                command.CommandText = commandText;
                command.CommandType = cmdtype;
                command.Connection = con;
                command.CommandTimeout = timeout;
                if (cmdParms != null)
                {
                    command.Parameters.AddRange(cmdParms);
                }
                adapter.SelectCommand = command;
                adapter.Fill(ds, "tb");
            }

            return ds.Tables["tb"];

        }
        public static DataTable ExecuteM_LogDataTable(string connectionstring, CommandType cmdtype, string commandText)
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                System.Data.SqlClient.SqlCommand command = new SqlCommand();
                command.CommandText = commandText;
                command.CommandType = cmdtype;
                command.Connection = con;
                adapter.SelectCommand = command;
                adapter.Fill(ds, "tb");
            }
            return ds.Tables["tb"];

        }
        public static int InsertAndReturnID(string connectionstring, CommandType cmdtype, string commandText, params SqlParameter[] cmdParms)
        {
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                SqlCommand myCommand = new SqlCommand();
                conn.Open();
                SqlTransaction myTrans = conn.BeginTransaction();
                myCommand.Transaction = myTrans;
                try
                {
                    PrepareCommand(myCommand, conn, null, cmdtype, commandText + ";select scope_identity();", cmdParms);
                    Object o = myCommand.ExecuteScalar();
                    myCommand.Parameters.Clear();
                    myTrans.Commit();
                    return Convert.ToInt32(o);
                }
                catch (Exception e)
                {
                    try
                    {
                        myTrans.Rollback();
                        return 0;
                    }
                    catch (SqlException ex)
                    {
                        return 0;
                    }
                }
                finally
                {
                    conn.Close();
                }

            }
        }
        public static int ExecuteUpdate(string connectionstring, CommandType cmdtype, string cmdText, params SqlParameter[] cmdParms)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                try
                {
                    SqlCommand command = new SqlCommand();
                    PrepareCommand(command, con, null, cmdtype, cmdText, cmdParms);
                    int i = command.ExecuteNonQuery();
                    command.Parameters.Clear();
                    return i;

                }
                catch
                {
                    return 0;
                }
                finally
                {
                    con.Close();
                }
            }

        }
        public static int ExecuteUpdateorInsertorDelete(SqlConnection con, CommandType cmdtype, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand command = new SqlCommand();
            PrepareCommand(command, con, null, cmdtype, cmdText, cmdParms);
            int i = command.ExecuteNonQuery();
            command.Parameters.Clear();
            con.Close();
            return i;
        }
        public static int ExecuteUpdateorInsertorDelete(SqlTransaction trans, CommandType cmdtype, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand command = new SqlCommand();
            PrepareCommand(command, trans.Connection, trans, cmdtype, cmdText, cmdParms);
            int i = command.ExecuteNonQuery();
            command.Parameters.Clear();
            trans.Connection.Close();
            return i;
        }
        public static Object ExecuteScalar(string connectionstring, CommandType cmdtype, string cmdText, params SqlParameter[] cmdParms)
        {
            using (SqlConnection con = new SqlConnection(connstr))
            {
                SqlCommand command = new SqlCommand();
                PrepareCommand(command, con, null, cmdtype, cmdText, cmdParms);
                Object o = command.ExecuteScalar();
                command.Parameters.Clear();
                return o;
            }

        }
        public static Object ExecuteScalar(SqlConnection con, CommandType cmdtype, string cmdText, params SqlParameter[] cmdParms)
        {

            SqlCommand command = new SqlCommand();
            PrepareCommand(command, con, null, cmdtype, cmdText, cmdParms);
            Object o = command.ExecuteScalar();
            command.Parameters.Clear();
            return o;


        }
        public static int ExecuteScalar(SqlTransaction trans, CommandType cmdtype, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand command = new SqlCommand();
            PrepareCommand(command, trans.Connection, trans, cmdtype, cmdText, cmdParms);
            int i = command.ExecuteNonQuery();
            command.Parameters.Clear();
            return i;
        }
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
            if (trans != null)
            {
                cmd.Transaction = trans;
            }
            if (cmdParms != null)
            {
                foreach (SqlParameter sp in cmdParms)
                {
                    if (sp.Value == null)
                        sp.Value = DBNull.Value;
                }
                cmd.Parameters.AddRange(cmdParms);
            }
        }
        public static SqlDataReader ExecuteReader(string connectionstring, CommandType cmdtype, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(connectionstring);
            try
            {
                PrepareCommand(cmd, conn, null, cmdtype, cmdText, cmdParms);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }

        }
        public static DataSet ExecuteDataSet(string connectionstring, CommandType cmdtype, string commandText)
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                System.Data.SqlClient.SqlCommand command = new SqlCommand();
                command.CommandText = commandText;
                command.CommandType = cmdtype;
                command.Connection = con;
                adapter.SelectCommand = command;
                adapter.Fill(ds);
            }
            return ds;
        }

        #region 带参数的DataSet查询
        /// <summary>  
        /// 执行存储过程  
        /// </summary>  
        /// <param name="storedProcName">存储过程名</param>  
        /// <param name="parameters">存储过程参数</param>  
        /// <param name="tableName">DataSet结果中的表名</param>  
        /// <returns>DataSet</returns>  
        public static DataSet RunProcedure(string connectionString, string storedProcName, IDataParameter[] parameters, string tableName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet dataSet = new DataSet();
                connection.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                sqlDA.Fill(dataSet, tableName);
                connection.Close();
                return dataSet;
            }
        }
        /// <summary>  
        /// 构建 SqlCommand 对象(用来返回一个结果集，而不是一个整数值)  
        /// </summary>  
        /// <param name="connection">数据库连接</param>  
        /// <param name="storedProcName">存储过程名</param>  
        /// <param name="parameters">存储过程参数</param>  
        /// <returns>SqlCommand</returns>  
        private static SqlCommand BuildQueryCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(storedProcName, connection);
            command.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter parameter in parameters)
            {
                if (parameter != null)
                {
                    // 检查未分配值的输出参数,将其分配以DBNull.Value.  
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    command.Parameters.Add(parameter);
                }
            }

            return command;
        }
        #endregion
    }
}