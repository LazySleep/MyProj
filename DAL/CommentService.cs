using Model;
using MySql.Data.MySqlClient;
using System.Data;

namespace DAL
{
    public class CommentService : Service
    {
        public const string COMMENT_TABLE_NAME = "comment";
        public const string ID = "id";
        public const string ARTICLE_ID = "article_id";
        public const string FROM_UID = "from_uid";
        public const string CONTENT = "content";
        public const string TIME = "time";

        /// <summary>
        /// 插入一条评论
        /// </summary>
        /// <param name="cmt">Comment对象</param>
        /// <returns></returns>
        public static bool Add(Comment cmt)
        {
            string sql = "insert into " + COMMENT_TABLE_NAME + "(" + ARTICLE_ID + "," + CONTENT + "," + FROM_UID + ") values(@a,@b,@c)";//sql语句字符串
            if (cmt.FromUId == null || cmt.Content == null || cmt.ArticleId == null)
            {
                return false;
            }
            MySqlParameter[] para = new MySqlParameter[]//存储相应参数的容器
            {
                new MySqlParameter("@a",cmt.ArticleId),
                new MySqlParameter("@b",cmt.Content),
                new MySqlParameter("@c",cmt.FromUId),
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
        /// 根据评论的ID删除一条数据
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        public static bool DeleteById(int commentId)
        {
            string sql = "delet from " + COMMENT_TABLE_NAME + " where " + ID + "=@a";//sql语句字符串
            MySqlParameter[] para = new MySqlParameter[]//存储相应参数的容器
            {
                new MySqlParameter("@a",commentId),
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
        /// 更新一条评论数据
        /// </summary>
        /// <param name="cmt">Comment对象</param>
        /// <returns>返回是否成功</returns>
        public static bool UpdataContent(Comment cmt)
        {
            string sql = "update " + COMMENT_TABLE_NAME + " set " + CONTENT + "=@content where " + ARTICLE_ID + "=@articleId and " + FROM_UID + "=@fromUId";//sql语句字符串
            if (cmt.FromUId == null || cmt.Content == null || cmt.ArticleId == null)
            {
                return false;
            }
            MySqlParameter[] para = new MySqlParameter[]//存储相应参数的容器
            {
                new MySqlParameter("@content",cmt.Content),
                new MySqlParameter("@articleId",cmt.ArticleId),
                new MySqlParameter("@fromUId",cmt.FromUId),
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
        /// 根据文章ID得到所有评论
        /// </summary>
        /// <param name="articleId">文章id</param>
        /// <returns>DataSet</returns>
        public static DataTable SearchByArticleId(int articleId, bool reserse = false)
        {
            string sql = "select * from " + COMMENT_TABLE_NAME + " where " + ARTICLE_ID + "=" + articleId;//sql语句字符串
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