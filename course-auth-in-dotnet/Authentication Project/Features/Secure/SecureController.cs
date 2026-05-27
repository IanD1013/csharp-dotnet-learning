using Microsoft.AspNetCore.Mvc;

namespace Authentication_Project.Features.Secure;

// URL: /Secure/Index
public class SecureController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}