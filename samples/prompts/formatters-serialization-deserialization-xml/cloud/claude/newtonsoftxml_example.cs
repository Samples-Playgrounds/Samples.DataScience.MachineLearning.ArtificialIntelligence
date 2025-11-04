using System;
using System.Xml;
using Newtonsoft.Json;

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public List<string> Hobbies { get; set; }
}

public class NewtonsoftXmlExample
{
    public static void Main()
    {
        var person = new Person
        {
            Name = "David Brown",
            Age = 33,
            Hobbies = new List<string> { "Chess", "Swimming" }
        };

        // Object to JSON
        string json = JsonConvert.SerializeObject(person, Newtonsoft.Json.Formatting.Indented);
        Console.WriteLine("JSON:");
        Console.WriteLine(json);

        // JSON to XML
        XmlDocument doc = JsonConvert.DeserializeXmlNode(json, "Person");
        Console.WriteLine("\nConverted to XML:");
        Console.WriteLine(doc.OuterXml);

        // XML back to JSON
        string xmlString = doc.OuterXml;
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlString);
        
        string jsonFromXml = JsonConvert.SerializeXmlNode(xmlDoc, Newtonsoft.Json.Formatting.Indented);
        Console.WriteLine("\nConverted back to JSON:");
        Console.WriteLine(jsonFromXml);

        // Deserialize back to object
        var deserializedPerson = JsonConvert.DeserializeObject<Person>(json);
        Console.WriteLine($"\nDeserialized: {deserializedPerson.Name}, Age: {deserializedPerson.Age}");
    }
}