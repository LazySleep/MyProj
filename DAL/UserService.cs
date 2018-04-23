using Dapper;
using Model;
using System.Collections.Generic;

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
        public bool AddUser(User user)
        {
            string sql = "insert into " + USERS_TABLE_NAME + "(" + ACCOUNT + "," + PASSWORD + ") values(@Account,@Password)";//sql语句字符串
            int count = Connection.Execute(sql, user);
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
        public bool DeleteUserByAccount(string account)
        {
            string sql = "delet from " + USERS_TABLE_NAME + " where " + ACCOUNT + "=@Account";//sql语句字符串
            if (account == null)
            {
                return false;
            }
            int count = Connection.Execute(sql, new { Account = account });//调用执行sql语句函数
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
        public bool UpdataUserPassword(User user)
        {
            string sql = "update " + USERS_TABLE_NAME + " set " + PASSWORD + "=@Password where " + ACCOUNT + "=@Account";//sql语句字符串
            if (user.Account == null || user.Password == null)
            {
                return false;
            }

            int count = Connection.Execute(sql, user);//调用执行sql语句函数
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
        public User SearchUserByAccount(string account)
        {
            string sql = "select * from " + USERS_TABLE_NAME + " where " + ACCOUNT + " = " + account;//sql语句字符串
            var query = Connection.Query<User>(sql);                          // 查询
            if (query.AsList().Count == 0)
            {
                return null;
            }
            else
            {
                return query.AsList()[0];
            }
        }

        /// <summary>
        /// 获取整张表
        /// </summary>
        /// <returns>返回DataTable，没有数据时返回null</returns>
        public List<User> GetUsersDataTable()
        {
            string sql = "select * from " + USERS_TABLE_NAME;//sql语句字符串
            var query = Connection.Query<User>(sql);                          // 查询
            List<User> users = query.AsList();
            if (users.Count == 0)
            {
                return null;
            }
            else
            {
                return users;
            }
        }
    }
}