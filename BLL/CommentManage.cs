using DAL;
using Model;
using System.Collections.Generic;

namespace BLL
{
    public class CommentManage
    {
        public const string EXIST_DATA = "数据已存在";
        public const string EXIST_NOT_DATA = "数据不存在";
        public const string UPDATA_SUCCESS = "数据修改成功";
        public const string UPDATA_FAIL = "数据修改失败";
        public const string ADD_SUCCESS = "数据增加成功";
        public const string ADD_FAIL = "数据增加失败";

        /// <summary>
        /// 增加一条Comment数据
        /// </summary>
        /// <param name="cmt"></param>
        /// <param name="msg"></param>
        /// <returns>是否成功</returns>
        public static bool AddComment(Comment cmt, out string msg)
        {
            CommentService service = new CommentService();
            if (service.Add(cmt))
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

        /// <summary>
        /// 根据评论ID删除数据
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public static bool DeleteCommentById(int commentId)
        {
            CommentService service = new CommentService();
            return service.DeleteById(commentId);
        }

        /// <summary>
        /// 修改评论Content数据
        /// </summary>
        /// <param name="cmt"></param>
        /// <returns></returns>
        public static bool Modify(Comment cmt)
        {
            CommentService service = new CommentService();
            return service.UpdataContent(cmt);
        }

        /// <summary>
        /// 根据文章ID返回Json字符串
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public static List<Comment> GetCommentByArticleId(int articleId, int pageNo, int rowCount)
        {
            CommentService service = new CommentService();
            List<Comment> comments = service.SearchByArticleId(articleId, true, pageNo, rowCount);
            return comments;
        }
    }
}