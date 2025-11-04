using System;
using System.Xml.Linq;
using System.Linq;

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public List<string> Hobbies { get; set; }
}

public class XDocumentExample
{
    public static void Main()
    {
        var person = new Person
        {
            Name = "Alice Johnson",
            Age = 35,
            Hobbies = new List<string> { "Painting", "Yoga", "Traveling" }
        };

        // Serialization (manual)
        var xml = new XDocument(
            new XElement("Person",
                new XElement("Name", person.Name),
                new XElement("Age", person.Age),
                new XElement("Hobbies",
                    person.Hobbies.Select(h => new XElement("Hobby", h))
                )
            )
        );

        Console.WriteLine("Serialized XML:");
        Console.WriteLine(xml.ToString());

        // Deserialization (manual)
        var root = xml.Root;
        var deserializedPerson = new Person
        {
            Name = root.Element("Name")?.Value,
            Age = int.Parse(root.Element("Age")?.Value ?? "0"),
            Hobbies = root.Element("Hobbies")?
                .Elements("Hobby")
                .Select(h => h.Value)
                .ToList()
        };

        Console.WriteLine($"\nDeserialized: {deserializedPerson.Name}, Age: {deserializedPerson.Age}");
        Console.WriteLine($"Hobbies: {string.Join(", ", deserializedPerson.Hobbies)}");
    }
}