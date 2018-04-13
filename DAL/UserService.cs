using Model;
using MySql.Data.MySqlClient;
using System.Data;

namespace DAL
{
    public class UserService : Service
    {
        private const string USERS_TABLE_NAME = "users";
        private const string ACCOUNT = "account";
        private const string PASSWORD = "password";

        /// <summary>
        /// 插入一条用户数据
        /// </summary>
        /// <param name="user">User对象</param>
        /// <returns>是否成功</returns>
        public static bool AddUser(User user)
        {
            string sql = "insert into " + USERS_TABLE_NAME + "(" + ACCOUNT + "," + PASSWORD + ") values(@account,@password)";//sql语句字符串
            if (user.Account == null || user.Password == null)
            {
                return false;
            }
            MySqlParameter[] para = new MySqlParameter[]//存储相应参数的容器
            {
                new MySqlParameter("@account",user.Account),
                new MySqlParameter("@password",user.Password),
            };
            int count = ExecuteCommand(sql, CommandType.Text, para);//调用执行sql语句函数
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 根据account删除一条数据
        /// </summary>
        /// <param name="account">账号</param>
        /// <returns>是否成功</returns>
        public static bool DeleteUserByAccount(string account)
        {
            string sql = "delet from " + USERS_TABLE_NAME + " where " + ACCOUNT + "=@account";//sql语句字符串
            if (account == null)
            {
                return false;
            }
            MySqlParameter[] para = new MySqlParameter[]//存储相应参数的容器
            {
                new MySqlParameter("@account",account),
            };
            int count = ExecuteCommand(sql, CommandType.Text, para);//调用执行sql语句函数
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="user">User对象</param>
        /// <returns>返回是否成功</returns>
        public static bool UpdataUserPassword(User user)
        {
            string sql = "update " + USERS_TABLE_NAME + " set " + PASSWORD + "=@password where " + ACCOUNT + "=@account";//sql语句字符串
            if (user.Account == null || user.Password == null)
            {
                return false;
            }
            MySqlParameter[] para = new MySqlParameter[]//存储相应参数的容器
            {
                new MySqlParameter("@account",user.Account),
                new MySqlParameter("@password",user.Password),
            };
            int count = ExecuteCommand(sql, CommandType.Text, para);//调用执行sql语句函数
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 根据account查询
        /// </summary>
        /// <param name="account">账号</param>
        /// <returns>User对象，找不到时返回null</returns>
        public static User SearchUserByAccount(string account)
        {
            string sql = "select * from " + USERS_TABLE_NAME + " where " + ACCOUNT + "=" + account;//sql语句字符串
            MySqlDataAdapter msda = new MySqlDataAdapter(sql, Connection);

            DataTable dt = new DataTable();
            msda.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                User user = new User();
                user.Account = dt.Rows[0][ACCOUNT].ToString();
                user.Password = dt.Rows[0][PASSWORD].ToString();
                return user;
            }
        }

        /// <summary>
        /// 获取整张表
        /// </summary>
        /// <returns>返回DataTable，没有数据时返回null</returns>
        public static DataTable GetUsersDataTable()
        {
            string sql = "select * from " + USERS_TABLE_NAME;//sql语句字符串
            MySqlDataAdapter msda = new MySqlDataAdapter(sql, Connection);

            DataTable dt = new DataTable();
            msda.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                return dt;
            }
        }
    }
}