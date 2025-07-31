using GymManagement.Application.Services;
using GymManagement.Contracts.Subscriptions;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SubscriptionsController : ControllerBase
{
    private readonly ISubscriptionsWriteService _subscriptionsWriteService;

    public SubscriptionsController(ISubscriptionsWriteService subscriptionsWriteService)
    {
        _subscriptionsWriteService = subscriptionsWriteService;
    }

    [HttpPost]
    public IActionResult CreateSubscription(CreateSubscriptionRequest request)
    {
        var subscriptionId = _subscriptionsWriteService.CreateSubscription(request.SubscriptionType.ToString(), request.AdminId);
        var response = new SubscriptionResponse(subscriptionId, request.SubscriptionType);
        return Ok(response);
    }
}