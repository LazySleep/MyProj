using BLL;
using Model;
using MyUI.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
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

        public JsonResult CommentSection(int articleId, int pageNo, int rowCount)
        {
            List<Comment> comments = CommentManage.GetCommentByArticleId(articleId, pageNo, rowCount);
            if (comments == null)
                return Json(new Result { Status = false, Message = "NoData" }, JsonRequestBehavior.AllowGet);
            List<object> list = new List<object>();
            for (int i = 0; i < comments.Count; i++)
            {
                List<Reply> replys = ReplyManage.GetReplyByCommentId(comments[i].Id);
                list.Add(
                    new
                    {
                        comments[i].Id,
                        comments[i].ArticleId,
                        comments[i].FromUId,
                        comments[i].Content,
                        comments[i].Time,
                        Reply = replys == null ? "[]" : JsonConvert.SerializeObject(replys)
                    }
            );
            }
            Result result = new Result { Status = true, Message = "OK", Obj = JsonConvert.SerializeObject(list) };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReplySection(int commentId, string toUId, string content)
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