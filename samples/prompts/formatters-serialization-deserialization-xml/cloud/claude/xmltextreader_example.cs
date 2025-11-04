using System;
using System.IO;
using System.Xml;

#pragma warning disable CS0618 // Type or member is obsolete

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}

public class XmlTextReaderWriterExample
{
    public static void Main()
    {
        var person = new Person
        {
            Name = "Isabel Martinez",
            Age = 31
        };

        string xml;

        // Serialization using XmlTextWriter (obsolete but still works)
        using (var sw = new StringWriter())
        {
            using (var writer = new XmlTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;
                
                writer.WriteStartElement("Person");
                writer.WriteElementString("Name", person.Name);
                writer.WriteElementString("Age", person.Age.ToString());
                writer.WriteEndElement();
            }
            
            xml = sw.ToString();
            Console.WriteLine("Serialized XML:");
            Console.WriteLine(xml);
        }

        // Deserialization using XmlTextReader (obsolete but still works)
        var deserializedPerson = new Person();
        
        using (var sr = new StringReader(xml))
        using (var reader = new XmlTextReader(sr))
        {
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case "Name":
                            deserializedPerson.Name = reader.ReadElementString();
                            break;
                        case "Age":
                            deserializedPerson.Age = int.Parse(reader.ReadElementString());
                            break;
                    }
                }
            }
        }

        Console.WriteLine($"\nDeserialized: {deserializedPerson.Name}, Age: {deserializedPerson.Age}");
        Console.WriteLine("\nNote: XmlTextReader/Writer are obsolete. Use XmlReader/Writer instead.");
    }
}

#pragma warning restore CS0618