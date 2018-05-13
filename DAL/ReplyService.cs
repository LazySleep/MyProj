using Dapper;
using Model;
using System.Collections.Generic;

namespace DAL
{
    public class ReplyService : Service
    {
        public const string REPLY_TABLE_NAME = "reply";
        public const string ID = "Id";
        public const string COMMENT_ID = "CommentId";
        public const string FROM_UID = "FromUId";
        public const string TO_UID = "ToUId";
        public const string CONTENT = "Content";
        public const string TIME = "Time";

        /// <summary>
        /// 插入一条回复
        /// </summary>
        /// <param name="reply">Reply</param>
        /// <returns></returns>
        public bool Add(Reply reply)
        {
            string sql = "insert into " + REPLY_TABLE_NAME + "(" + COMMENT_ID + "," + FROM_UID + "," + TO_UID + "," + CONTENT +
                ") values(@CommentId,@FromUId,@ToUId,@Content)";//sql语句字符串
            if (reply.FromUId == null || reply.Content == null || reply.ToUId == null || reply.CommentId == -1)
            {
                return false;
            }
            int count = Connection.Execute(sql, reply);//调用执行sql语句函数
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
        public bool DeleteById(int replyId)
        {
            string sql = "delet from " + REPLY_TABLE_NAME + " where " + ID + "=@Id";//sql语句字符串
            int count = Connection.Execute(sql, new { Id = replyId });//调用执行sql语句函数
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
        public List<Reply> SearchByCommentId(int commentId, bool reserse = false)
        {
            string sql = "select * from " + REPLY_TABLE_NAME + " where " + COMMENT_ID + "=" + commentId;//sql语句字符串
            if (reserse)
            {
                sql += " order by " + ID + " desc";
            }
            var query = Connection.Query<Reply>(sql);
            List<Reply> replys = query.AsList();
            if (replys.Count == 0)
            {
                return null;
            }
            else
            {
                return replys;
            }
        }
    }
}