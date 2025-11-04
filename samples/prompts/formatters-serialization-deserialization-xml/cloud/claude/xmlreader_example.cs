using System;
using System.IO;
using System.Xml;

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public List<string> Hobbies { get; set; }
}

public class XmlReaderWriterExample
{
    public static void Main()
    {
        var person = new Person
        {
            Name = "Carol Davis",
            Age = 29,
            Hobbies = new List<string> { "Running", "Music" }
        };

        string xml;

        // Serialization using XmlWriter
        using (var sw = new StringWriter())
        using (var writer = XmlWriter.Create(sw, new XmlWriterSettings { Indent = true }))
        {
            writer.WriteStartElement("Person");
            writer.WriteElementString("Name", person.Name);
            writer.WriteElementString("Age", person.Age.ToString());
            
            writer.WriteStartElement("Hobbies");
            foreach (var hobby in person.Hobbies)
            {
                writer.WriteElementString("Hobby", hobby);
            }
            writer.WriteEndElement(); // Hobbies
            
            writer.WriteEndElement(); // Person
            writer.Flush();
            
            xml = sw.ToString();
            Console.WriteLine("Serialized XML:");
            Console.WriteLine(xml);
        }

        // Deserialization using XmlReader
        var deserializedPerson = new Person { Hobbies = new List<string>() };
        
        using (var sr = new StringReader(xml))
        using (var reader = XmlReader.Create(sr))
        {
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case "Name":
                            deserializedPerson.Name = reader.ReadElementContentAsString();
                            break;
                        case "Age":
                            deserializedPerson.Age = reader.ReadElementContentAsInt();
                            break;
                        case "Hobby":
                            deserializedPerson.Hobbies.Add(reader.ReadElementContentAsString());
                            break;
                    }
                }
            }
        }

        Console.WriteLine($"\nDeserialized: {deserializedPerson.Name}, Age: {deserializedPerson.Age}");
    }
}