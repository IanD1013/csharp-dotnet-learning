using DomeGym.Section5.Domain.AdminAggregate;

namespace DomeGym.Section5.Domain.UnitTests.TestUtils.Admins;

public static class AdminFactory
{
    public static Admin CreateAdmin(Guid? userId = null, Guid? subscriptionId = null, Guid? id = null)
    {
        return new Admin(
            userId: userId ?? Constants.User.Id,
            subscriptionId: subscriptionId,
            id: id ?? Constants.Admin.Id);
    }
}
