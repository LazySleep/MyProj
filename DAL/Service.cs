using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace DAL
{
    public class Service
    {
        private const string DATABASE = "vstest";
        private static MySqlConnection connection;

        /// <summary>
        /// 连接数据库
        /// </summary>
        public static MySqlConnection Connection
        {
            get
            {
                if (connection == null)
                {
                    //连接本地数据库命令
                    string strConn = "Database=" + DATABASE + ";Data Source=127.0.0.1;User Id=root;Password=123456;pooling=false;CharSet=utf8;port=3306";
                    connection = new MySqlConnection(strConn);
                    connection.Open();
                }
                return connection;
            }
        }

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        /// <returns></returns>
        public bool CloseConnection()
        {
            try
            {
                if (connection != null)
                {
                    connection.Close();
                }
                return true;
            }
            catch (MySqlException ex)
            {
                Console.Write(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="commandText">sql语句</param>
        /// <param name="commandType"></param>
        /// <param name="para">存储相应参数的容器</param>
        /// <returns>受影响的行数</returns>
        public static int ExecuteCommand(string commandText, CommandType commandType, MySqlParameter[] para)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = Connection;
            cmd.CommandText = commandText;
            try
            {
                if (para != null)
                {
                    cmd.Parameters.AddRange(para);
                }
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                connection.Close();
                cmd.Dispose();
            }
        }
    }
}