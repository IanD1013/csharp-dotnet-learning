namespace Minimal.Api;

public record Person(string FullName);

public class PeopleService
{
    private readonly List<Person> _people =
    [
        new("Nick Chapsas"),
        new("John Chapsas"),
        new("Tim Cook")
    ];

    public IEnumerable<Person> Search(string searchTerm)
    {
        return _people.Where(x =>
            x.FullName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
    }
}