using ErrorOr;

namespace DomeGym.Section5.Domain.Common;

public class EventualConsistencyException(Error eventualConsistencyError, List<Error>? underlyingErrors = null)
    : Exception(eventualConsistencyError.Description)
{
    public Error EventualConsistencyError { get; } = eventualConsistencyError;

    public IReadOnlyList<Error> UnderlyingErrors { get; } = underlyingErrors ?? [];
}
