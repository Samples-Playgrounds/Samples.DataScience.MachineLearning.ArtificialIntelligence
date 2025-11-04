using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Linq;

public class Person
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("age")]
    public int Age { get; set; }
    
    [JsonPropertyName("hobbies")]
    public List<string> Hobbies { get; set; }
}

public class SystemTextJsonExample
{
    public static void Main()
    {
        var person = new Person
        {
            Name = "Emma Wilson",
            Age = 27,
            Hobbies = new List<string> { "Photography", "Baking" }
        };

        // Serialize to JSON
        var options = new JsonSerializerOptions 
        { 
            WriteIndented = true 
        };
        
        string json = JsonSerializer.Serialize(person, options);
        Console.WriteLine("JSON Serialization:");
        Console.WriteLine(json);

        // Deserialize from JSON
        var deserializedPerson = JsonSerializer.Deserialize<Person>(json);
        Console.WriteLine($"\nDeserialized: {deserializedPerson.Name}, Age: {deserializedPerson.Age}");

        // Manual JSON to XML conversion
        var jsonElement = JsonSerializer.Deserialize<JsonElement>(json);
        var xml = JsonToXml(jsonElement, "Person");
        Console.WriteLine("\nManual XML Conversion:");
        Console.WriteLine(xml.ToString());
    }

    static XElement JsonToXml(JsonElement element, string name)
    {
        var xElement = new XElement(name);
        
        if (element.ValueKind == JsonValueKind.Object)
        {
            foreach (var property in element.EnumerateObject())
            {
                xElement.Add(JsonToXml(property.Value, property.Name));
            }
        }
        else if (element.ValueKind == JsonValueKind.Array)
        {
            foreach (var item in element.EnumerateArray())
            {
                xElement.Add(JsonToXml(item, "item"));
            }
        }
        else
        {
            xElement.Value = element.ToString();
        }
        
        return xElement;
    }
}