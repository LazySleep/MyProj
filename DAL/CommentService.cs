using Dapper;
using Model;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;

namespace DAL
{
    public class CommentService : Service
    {
        public const string COMMENT_TABLE_NAME = "comment";
        public const string ID = "Id";
        public const string ARTICLE_ID = "ArticleId";
        public const string FROM_UID = "FromUId";
        public const string CONTENT = "Content";
        public const string TIME = "Time";

        /// <summary>
        /// 插入一条评论
        /// </summary>
        /// <param name="cmt">Comment对象</param>
        /// <returns></returns>
        public bool Add(Comment cmt)
        {
            string sql = "insert into " + COMMENT_TABLE_NAME + "(" + ARTICLE_ID + "," + CONTENT + "," + FROM_UID + ") values(@ArticleId,@Content,@FromUId)";//sql语句字符串
            if (cmt.FromUId == null || cmt.Content == null || cmt.ArticleId == null)
            {
                return false;
            }
            int count = Connection.Execute(sql, cmt);//调用执行sql语句函数
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
        public bool DeleteById(int commentId)
        {
            string sql = "delet from " + COMMENT_TABLE_NAME + " where " + ID + "=@CommentId";//sql语句字符串
            int count = Connection.Execute(sql, new { CommentId = commentId });//调用执行sql语句函数
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
        public bool UpdataContent(Comment cmt)
        {
            string sql = "update " + COMMENT_TABLE_NAME + " set " + CONTENT + "=@Content where " + ARTICLE_ID + "=@ArticleId and " + FROM_UID + "=@FromUId";//sql语句字符串
            if (cmt.FromUId == null || cmt.Content == null || cmt.ArticleId == null)
            {
                return false;
            }
            int count = Connection.Execute(sql, cmt);//调用执行sql语句函数
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
        /// 获取文章对应的评论
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <param name="reserse">默认为false</param>
        /// <param name="pageNo">起始行</param>
        /// <param name="rowCount">自其实行开始多少行</param>
        /// <returns>DataTable</returns>
        public List<Comment> SearchByArticleId(int articleId, bool reserse = false, int pageNo = -1, int rowCount = -1)
        {
            string sql = "select * from " + COMMENT_TABLE_NAME + " where " + ARTICLE_ID + "=" + articleId;//sql语句字符串
            if (reserse)
            {
                sql += " order by " + ID + " desc";
            }
            if (pageNo != -1 && rowCount != -1)
            {
                sql += " limit " + pageNo + "," + rowCount;
            }
            var query = Connection.Query<Comment>(sql);
            List<Comment> comments = query.AsList();
            if(comments.Count == 0)
            {
                return null;
            }
            else
            {
                return comments;
            }
        }
    }
}