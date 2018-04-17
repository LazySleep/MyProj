using DAL;
using Model;
using Newtonsoft.Json;
using System.Data;

namespace BLL
{
    public class ReplyManage
    {
        public const string EXIST_DATA = "数据已存在";
        public const string EXIST_NOT_DATA = "数据不存在";
        public const string UPDATA_SUCCESS = "数据修改成功";
        public const string UPDATA_FAIL = "数据修改失败";
        public const string ADD_SUCCESS = "数据增加成功";
        public const string ADD_FAIL = "数据增加失败";

        /// <summary>
        /// 增加一条Reply数据
        /// </summary>
        /// <param name="reply"></param>
        /// <param name="msg"></param>
        /// <returns>是否成功</returns>
        public static bool AddReply(Reply reply, out string msg)
        {
            if (ReplyService.Add(reply))
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
        /// 根据回复ID删除数据
        /// </summary>
        /// <param name="replyId"></param>
        /// <returns></returns>
        public static bool DeleteReplyById(int replyId)
        {
            return ReplyService.DeleteById(replyId);
        }

        /// <summary>
        /// 根据评论ID返回Json字符串
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        public static string GetReplyByCommentId(int commentId)
        {
            DataTable dt = ReplyService.SearchByCommentId(commentId);
            string jsonString = "";
            jsonString = JsonConvert.SerializeObject(dt);
            return jsonString;
        }
    }
}