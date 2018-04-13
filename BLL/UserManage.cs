using DAL;
using Model;

namespace BLL
{
    public class UserManage
    {
        public const string EXIST_USER = "用户名已存在";
        public const string EXIST_NOT_USER = "用户名不存在";
        public const string UPDATA_SUCCESS = "密码修改成功";
        public const string UPDATA_FAIL = "密码修改失败";
        public const string ADD_SUCCESS = "用户增加成功";
        public const string ADD_FAIL = "用户增加失败";

        /// <summary>
        /// 增加用户
        /// </summary>
        /// <param name="user"></param>
        /// <param name="msg">返回成功/错误信息</param>
        /// <returns>是否成功</returns>
        public static bool Add(User user, out string msg)
        {
            if (UserService.SearchUserByAccount(user.Account) != null)
            {
                msg = EXIST_USER;
                return false;
            }
            else
            {
                if (UserService.AddUser(user))
                {
                    msg = ADD_SUCCESS;
                    return true;
                }
                else
                {
                    msg = ADD_FAIL;
                    return false;
                }
            }
        }

        /// <summary>
        /// 根据Account查找User信息
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public static User FindByAccount(string account)
        {
            return UserService.SearchUserByAccount(account);
        }

        /// <summary>
        ///根据account删除User
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public static bool Delete(string account)
        {
            if (UserService.SearchUserByAccount(account) != null)
            {
                return UserService.DeleteUserByAccount(account);
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <param name="msg">返回成功/失败信息</param>
        /// <returns></returns>
        public static bool Modify(User user, out string msg)
        {
            if (UserService.SearchUserByAccount(user.Account) == null)
            {
                msg = EXIST_NOT_USER;
                return false;
            }
            else
            {
                if (UserService.UpdataUserPassword(user))
                {
                    msg = UPDATA_SUCCESS;
                    return true;
                }
                else
                {
                    msg = UPDATA_FAIL;
                    return false;
                }
            }
        }
    }
}