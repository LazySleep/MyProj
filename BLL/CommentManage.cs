using DAL;
using Model;
using Newtonsoft.Json;
using System.Data;

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
            if (CommentService.Add(cmt))
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
            return CommentService.DeleteById(commentId);
        }

        /// <summary>
        /// 修改评论Content数据
        /// </summary>
        /// <param name="cmt"></param>
        /// <returns></returns>
        public static bool Modify(Comment cmt)
        {
            return CommentService.UpdataContent(cmt);
        }

        /// <summary>
        /// 根据文章ID返回Json字符串
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public static string GetCommentByArticleId(int articleId)
        {
            DataTable dt = CommentService.SearchByArticleId(articleId, true);
            string jsonString = "";
            jsonString = JsonConvert.SerializeObject(dt);
            return jsonString;
        }
    }
}