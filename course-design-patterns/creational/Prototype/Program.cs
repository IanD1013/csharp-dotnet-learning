using Dumpify;

var person = new Person("Ian", ["hobby1"]);

var shallowCopy = person.ShallowClone();
var deepCopy = person.DeepClone();

shallowCopy.Hobbies.Add("hobby2");
deepCopy.Hobbies.Add("hobby3");

person.Dump();

internal record Person(string Name, List<string> Hobbies)
{
    public Person ShallowClone()
    {
        return this with { };
    }

    public Person DeepClone()
    {
        return new Person(Name, [.. Hobbies]);
    }
}









