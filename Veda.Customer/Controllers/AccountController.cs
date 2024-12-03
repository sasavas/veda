using Microsoft.AspNetCore.Mvc;

namespace Veda.Customer.Controllers;

public class AccountController : Controller
{
    public ActionResult SignIn()
    {
        return View();
    }

    public ActionResult SignUp()
    {
        return View();
    }
}
