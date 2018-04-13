using Model;
using MySql.Data.MySqlClient;
using System.Data;

namespace DAL
{
    public class ReplyService : Service
    {
        public const string REPLY_TABLE_NAME = "reply";
        public const string ID = "id";
        public const string COMMENT_ID = "comment_id";
        public const string FROM_UID = "from_uid";
        public const string TO_UID = "to_uid";
        public const string CONTENT = "content";
        public const string TIME = "time";

        /// <summary>
        /// 插入一条回复
        /// </summary>
        /// <param name="reply">Reply</param>
        /// <returns></returns>
        public static bool Add(Reply reply)
        {
            string sql = "insert into " + REPLY_TABLE_NAME + "(" + COMMENT_ID + "," + FROM_UID + "," + TO_UID + "," + CONTENT +
                ") values(@a,@b,@c,@d)";//sql语句字符串
            if (reply.FromUId == null || reply.Content == null || reply.ToUId == null || reply.CommentId == -1)
            {
                return false;
            }
            MySqlParameter[] para = new MySqlParameter[]//存储相应参数的容器
            {
                new MySqlParameter("@a",reply.CommentId),
                new MySqlParameter("@b",reply.FromUId),
                new MySqlParameter("@c",reply.ToUId),
                new MySqlParameter("@d",reply.Content),
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
        /// 根据回复的ID删除一条数据
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        public static bool DeleteById(int replyId)
        {
            string sql = "delet from " + REPLY_TABLE_NAME + " where " + ID + "=@a";//sql语句字符串
            MySqlParameter[] para = new MySqlParameter[]//存储相应参数的容器
            {
                new MySqlParameter("@a",replyId),
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
        /// 根据评论ID得到所有回复
        /// </summary>
        /// <param name="commentId">评论id</param>
        /// <returns>DataSet</returns>
        public static DataTable SearchByCommentId(int commentId, bool reserse = false)
        {
            string sql = "select * from " + REPLY_TABLE_NAME + " where " + COMMENT_ID + "=" + commentId;//sql语句字符串
            if (reserse)
            {
                sql += " order by " + ID + " desc";
            }
            MySqlDataAdapter msda = new MySqlDataAdapter(sql, Connection);

            DataTable dt = new DataTable();
            msda.Fill(dt);
            return dt;
        }
    }
}