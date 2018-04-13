using BLL;
using Model;
using MyUI.Models;
using Newtonsoft.Json;
using System.Data;
using System.Web.Mvc;

namespace MyUI.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        ///  url:index/1
        ///  get传参
        /// </summary>
        /// <param name="id">这里只能用id，否则会出错</param>
        /// <returns></returns>
        public ActionResult Index(int id)
        {
            //id这里默认是1因为没有更多文章测试
            string commentJson = CommentManage.GetCommentByArticleId(id);
            DataTable commentDataTable = JsonConvert.DeserializeObject<DataTable>(commentJson);
            int commentId;
            string replyJson; 
            for (int i = 0; i < commentDataTable.Rows.Count; i++)
            {
                replyJson = "";
                commentId = int.Parse(commentDataTable.Rows[i]["comment_id"].ToString());
                replyJson = ReplyManage.GetReplyByCommentId(commentId);

            } 
            return View();
        }

        [HttpPost]
        public ActionResult Index(CommentDto dd)
        {
            Comment cmt = new Comment();
            cmt.FromUId = dd.FromUId;
            cmt.Content = dd.Content;
            cmt.ArticleId = dd.ArticleId;
            string msg;
            Result result = new Result { Status = CommentManage.AddComment(cmt, out msg), Message = msg };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}