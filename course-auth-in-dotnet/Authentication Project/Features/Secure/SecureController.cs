using Microsoft.AspNetCore.Mvc;

namespace Authentication_Project.Features.Secure;

// URL: /Secure/Index
public class SecureController : Controller
{
    public IActionResult Index()
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return View();
        }

        return Challenge();
    }
}