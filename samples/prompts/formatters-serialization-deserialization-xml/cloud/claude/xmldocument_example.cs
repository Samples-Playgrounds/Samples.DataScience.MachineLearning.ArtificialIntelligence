using System;
using System.Xml;

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public List<string> Hobbies { get; set; }
}

public class XmlDocumentExample
{
    public static void Main()
    {
        var person = new Person
        {
            Name = "Bob Wilson",
            Age = 42,
            Hobbies = new List<string> { "Fishing", "Woodworking" }
        };

        // Serialization (manual)
        var doc = new XmlDocument();
        var root = doc.CreateElement("Person");
        doc.AppendChild(root);

        var nameElement = doc.CreateElement("Name");
        nameElement.InnerText = person.Name;
        root.AppendChild(nameElement);

        var ageElement = doc.CreateElement("Age");
        ageElement.InnerText = person.Age.ToString();
        root.AppendChild(ageElement);

        var hobbiesElement = doc.CreateElement("Hobbies");
        foreach (var hobby in person.Hobbies)
        {
            var hobbyElement = doc.CreateElement("Hobby");
            hobbyElement.InnerText = hobby;
            hobbiesElement.AppendChild(hobbyElement);
        }
        root.AppendChild(hobbiesElement);

        Console.WriteLine("Serialized XML:");
        Console.WriteLine(doc.OuterXml);

        // Deserialization (manual)
        var deserializedPerson = new Person
        {
            Name = doc.SelectSingleNode("//Person/Name")?.InnerText,
            Age = int.Parse(doc.SelectSingleNode("//Person/Age")?.InnerText ?? "0"),
            Hobbies = new List<string>()
        };

        var hobbyNodes = doc.SelectNodes("//Person/Hobbies/Hobby");
        foreach (XmlNode hobbyNode in hobbyNodes)
        {
            deserializedPerson.Hobbies.Add(hobbyNode.InnerText);
        }

        Console.WriteLine($"\nDeserialized: {deserializedPerson.Name}, Age: {deserializedPerson.Age}");
    }
}