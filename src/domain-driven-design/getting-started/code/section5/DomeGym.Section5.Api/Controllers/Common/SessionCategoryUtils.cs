using DomeGym.Section5.Domain.SessionAggregate;
using ErrorOr;

namespace DomeGym.Section5.Api.Controllers.Common;

public static class SessionCategoryUtils
{
    public static ErrorOr<List<SessionCategory>> ToDomain(List<string>? categories)
    {
        List<SessionCategory> parsedCategories = [];
        List<Error> errors = [];

        foreach (string category in categories ?? [])
        {
            if (SessionCategory.TryFromName(category, ignoreCase: true, out SessionCategory? parsedCategory))
            {
                parsedCategories.Add(parsedCategory);
            }
            else
            {
                errors.Add(Error.Validation(
                    code: "Categories.InvalidCategory",
                    description: $"Invalid category '{category}'"));
            }
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        return parsedCategories;
    }
}
