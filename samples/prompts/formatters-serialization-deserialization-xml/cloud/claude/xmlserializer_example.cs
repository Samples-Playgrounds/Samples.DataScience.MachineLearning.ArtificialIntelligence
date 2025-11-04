using System;
using System.IO;
using System.Xml.Serialization;

// Model class
[XmlRoot("Person")]
public class Person
{
    [XmlElement("Name")]
    public string Name { get; set; }
    
    [XmlElement("Age")]
    public int Age { get; set; }
    
    [XmlArray("Hobbies")]
    [XmlArrayItem("Hobby")]
    public List<string> Hobbies { get; set; }
}

public class XmlSerializerExample
{
    public static void Main()
    {
        // Serialization
        var person = new Person
        {
            Name = "John Doe",
            Age = 30,
            Hobbies = new List<string> { "Reading", "Gaming", "Hiking" }
        };

        var serializer = new XmlSerializer(typeof(Person));
        
        using (var writer = new StringWriter())
        {
            serializer.Serialize(writer, person);
            string xml = writer.ToString();
            Console.WriteLine("Serialized XML:");
            Console.WriteLine(xml);
            
            // Deserialization
            using (var reader = new StringReader(xml))
            {
                var deserializedPerson = (Person)serializer.Deserialize(reader);
                Console.WriteLine($"\nDeserialized: {deserializedPerson.Name}, Age: {deserializedPerson.Age}");
            }
        }
    }
}