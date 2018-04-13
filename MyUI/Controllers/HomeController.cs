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
        public ActionResult Index()
        {
            if(Session["account"] == null)
            {
                Response.Redirect("/Login/index");
            }
                return View();
        }

        [HttpPost]
        public JsonResult Index(CommentDto dd)
        {
            Comment cmt = new Comment();
            cmt.FromUId = dd.FromUId;
            cmt.Content = dd.Content;
            cmt.ArticleId = dd.ArticleId;
            string msg;
            Result result = new Result { Status = CommentManage.AddComment(cmt, out msg), Message = msg };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult GetCommentSection(int ArticleId)
        {
            string commentJson = CommentManage.GetCommentByArticleId(ArticleId);
            DataTable commentDataTable = JsonConvert.DeserializeObject<DataTable>(commentJson);
            int commentId;
            string replyJson;
            for (int i = 0; i < commentDataTable.Rows.Count; i++)
            {
                replyJson = "";
                commentId = int.Parse(commentDataTable.Rows[i]["comment_id"].ToString());
                replyJson = ReplyManage.GetReplyByCommentId(commentId);
            }
            return null;
        }
    }
}