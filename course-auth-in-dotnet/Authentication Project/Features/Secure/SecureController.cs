using Microsoft.AspNetCore.Mvc;

namespace Authentication_Project.Features.Secure;

// URL: /Secure/Index
public class SecureController : Controller
{
    public IActionResult Index()
    {
        // return Forbid();
        
        if (User.Identity?.IsAuthenticated == true)
        {
            return View();
        }

        return Challenge();
    }
}