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
            return View();
        }

        [HttpPost]
        public JsonResult Index(CommentDto dd)
        {
            if (Session["account"] == null)
            {
                return Json(new Result { Status = false, Message = "没有登陆信息，请先登陆再操作" }, JsonRequestBehavior.AllowGet);
            }
            Comment cmt = new Comment();
            cmt.FromUId = Session["account"].ToString();
            cmt.Content = dd.Content;
            cmt.ArticleId = dd.ArticleId;
            string msg;
            Result result = new Result { Status = CommentManage.AddComment(cmt, out msg), Message = msg };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CommentSection(int ArticleId, int pageNo, int rowCount)
        {
            string commentJson = CommentManage.GetCommentByArticleId(ArticleId, pageNo, rowCount);
            DataTable commentDataTable = JsonConvert.DeserializeObject<DataTable>(commentJson);
            commentDataTable.Columns.Add("reply", typeof(string));
            int commentId;
            string replyJson;
            for (int i = 0; i < commentDataTable.Rows.Count; i++)
            {
                replyJson = "";
                commentId = int.Parse(commentDataTable.Rows[i]["id"].ToString());
                replyJson = ReplyManage.GetReplyByCommentId(commentId);
                commentDataTable.Rows[i]["reply"] = replyJson;
            }
            Result result = new Result { Status = true, Message = "OK", Obj = JsonConvert.SerializeObject(commentDataTable) };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReplySection(int commentId, string toUId,string content)
        {
            if (Session["account"] == null)
            {
                return Json(new Result { Status = false, Message = "没有登陆信息，请先登陆再操作" }, JsonRequestBehavior.AllowGet);
            }
            Reply reply = new Reply();
            reply.CommentId = commentId;
            reply.FromUId = Session["account"].ToString();
            reply.ToUId = toUId;
            reply.Content = content;
            string msg;
            Result result = new Result { Status = ReplyManage.AddReply(reply, out msg), Message = msg };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}