using BLL;
using Model;
using MyUI.Models;
using System.Web.Mvc;

namespace MyUI.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(UserDto dto)
        {
            User user = new User();
            user.Account = dto.Account;
            user.Password = dto.Password;
            string msg;
            Result result = new Result { Status = UserManage.Add(user, out msg), Message = msg };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}