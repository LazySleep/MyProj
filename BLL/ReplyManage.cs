using DAL;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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
            ReplyService service = new ReplyService();
            try
            {
                if (service.Add(reply))
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
            catch (Exception e)
            {
                Console.WriteLine("\n异常信息:\n{0}", e.Message);
                msg = ADD_FAIL;
                return false;
            }
            finally
            {
                service.CloseConnection();
            }
        }

        /// <summary>
        /// 根据回复ID删除数据
        /// </summary>
        /// <param name="replyId"></param>
        /// <returns></returns>
        public static bool DeleteReplyById(int replyId)
        {
            ReplyService service = new ReplyService();
            try
            {
                return service.DeleteById(replyId);
            }
            catch (Exception e)
            {
                Console.WriteLine("\n异常信息:\n{0}", e.Message);
                return false;
            }
            finally
            {
                service.CloseConnection();
            }
        }

        /// <summary>
        /// 根据评论ID返回Json字符串
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        public static List<Reply> GetReplyByCommentId(int commentId)
        {
            ReplyService service = new ReplyService();
            try
            {
                List<Reply> replys = service.SearchByCommentId(commentId);
                return replys;
            }
            catch (Exception e)
            {
                Console.WriteLine("\n异常信息:\n{0}", e.Message);
                return null;
            }
            finally
            {
                service.CloseConnection();
            }
        }
    }
}