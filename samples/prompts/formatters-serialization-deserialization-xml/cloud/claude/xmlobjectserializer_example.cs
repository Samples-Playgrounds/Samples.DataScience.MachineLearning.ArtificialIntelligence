using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}

// Custom serializer inheriting from XmlObjectSerializer
public class CustomPersonSerializer : XmlObjectSerializer
{
    public override bool IsStartObject(XmlDictionaryReader reader)
    {
        return reader.IsStartElement("CustomPerson");
    }

    public override object ReadObject(XmlDictionaryReader reader, bool verifyObjectName)
    {
        var person = new Person();
        
        reader.ReadStartElement("CustomPerson");
        person.Name = reader.ReadElementString("FullName");
        person.Age = reader.ReadElementContentAsInt("Years", "");
        reader.ReadEndElement();
        
        return person;
    }

    public override void WriteEndObject(XmlDictionaryWriter writer)
    {
        writer.WriteEndElement();
    }

    public override void WriteObjectContent(XmlDictionaryWriter writer, object graph)
    {
        var person = (Person)graph;
        writer.WriteElementString("FullName", person.Name);
        writer.WriteElementString("Years", person.Age.ToString());
    }

    public override void WriteStartObject(XmlDictionaryWriter writer, object graph)
    {
        writer.WriteStartElement("CustomPerson");
    }
}

public class XmlObjectSerializerExample
{
    public static void Main()
    {
        var person = new Person
        {
            Name = "Frank Miller",
            Age = 45
        };

        var serializer = new CustomPersonSerializer();
        
        // Serialization
        using (var ms = new MemoryStream())
        {
            using (var writer = XmlDictionaryWriter.CreateTextWriter(ms))
            {
                serializer.WriteObject(writer, person);
                writer.Flush();
            }
            
            string xml = System.Text.Encoding.UTF8.GetString(ms.ToArray());
            Console.WriteLine("Serialized XML:");
            Console.WriteLine(xml);
            
            // Deserialization
            ms.Position = 0;
            using (var reader = XmlDictionaryReader.CreateTextReader(ms, new XmlDictionaryReaderQuotas()))
            {
                var deserializedPerson = (Person)serializer.ReadObject(reader);
                Console.WriteLine($"\nDeserialized: {deserializedPerson.Name}, Age: {deserializedPerson.Age}");
            }
        }
    }
}