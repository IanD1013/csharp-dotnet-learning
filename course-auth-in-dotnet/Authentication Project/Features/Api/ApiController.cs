using Microsoft.AspNetCore.Mvc;

namespace Authentication_Project.Features.Api;

public class ApiController : Controller
{
    public IActionResult Index()
    {
        if (User?.Identity?.IsAuthenticated == false)
        {
            return Challenge();
        }

        return Ok("API Response");
    }
}