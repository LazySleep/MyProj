using BLL;
using Model;
using MyUI.Models;
using System.Web.Mvc;

namespace MyUI.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(UserDto dto)
        {
            Result result = null;
            User user;
            if ((user = UserManage.FindByAccount(dto.Account)) == null)
            {
                result = new Result { Status = false, Message = "用户不存在!" };
            }
            else
            {
                if (user.Password == dto.Password)
                {
                    Session["account"] = user.Account;
                    result = new Result { Status = true, Message = "登陆成功！" };
                }
                else
                {
                    result = new Result { Status = false, Message = "账号或密码错误" };
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}