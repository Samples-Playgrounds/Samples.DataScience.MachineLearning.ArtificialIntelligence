Here’s a comprehensive overview of **XML formatting (serialization/deserialization) in .NET (C#)**, including built-in and third-party libraries, with code samples for each.

---

## 1. **System.Xml.Serialization (XmlSerializer)**
**Built-in, simple, attribute-driven**

```csharp
using System;
using System.IO;
using System.Xml.Serialization;

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}

class Program
{
    static void Main()
    {
        var person = new Person { Name = "Alice", Age = 30 };

        // Serialize
        var serializer = new XmlSerializer(typeof(Person));
        using (var writer = new StringWriter())
        {
            serializer.Serialize(writer, person);
            Console.WriteLine(writer.ToString());
        }

        // Deserialize
        var xml = "<Person><Name>Bob</Name><Age>25</Age></Person>";
        using (var reader = new StringReader(xml))
        {
            var result = (Person)serializer.Deserialize(reader);
            Console.WriteLine($"{result.Name}, {result.Age}");
        }
    }
}
```

---

## 2. **System.Runtime.Serialization (DataContractSerializer)**
**Built-in, supports complex types, requires attributes**

```csharp
using System;
using System.IO;
using System.Runtime.Serialization;

[DataContract]
public class Person
{
    [DataMember]
    public string Name { get; set; }
    [DataMember]
    public int Age { get; set; }
}

class Program
{
    static void Main()
    {
        var person = new Person { Name = "Alice", Age = 30 };

        // Serialize
        var serializer = new DataContractSerializer(typeof(Person));
        using (var stream = new MemoryStream())
        {
            serializer.WriteObject(stream, person);
            stream.Position = 0;
            using (var reader = new StreamReader(stream))
            {
                Console.WriteLine(reader.ReadToEnd());
            }
        }

        // Deserialize
        var xml = "<Person xmlns=\"http://schemas.datacontract.org/2004/07/YourNamespace\" xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\"><Age>25</Age><Name>Bob</Name></Person>";
        using (var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(xml)))
        {
            var result = (Person)serializer.ReadObject(stream);
            Console.WriteLine($"{result.Name}, {result.Age}");
        }
    }
}
```

---

## 3. **System.Xml.XmlDocument / XmlReader / XmlWriter**
**Manual, full control over XML structure**

```csharp
using System;
using System.Xml;

class Program
{
    static void Main()
    {
        // Serialize
        var doc = new XmlDocument();
        var person = doc.CreateElement("Person");
        var name = doc.CreateElement("Name");
        name.InnerText = "Alice";
        var age = doc.CreateElement("Age");
        age.InnerText = "30";
        person.AppendChild(name);
        person.AppendChild(age);
        doc.AppendChild(person);
        Console.WriteLine(doc.OuterXml);

        // Deserialize
        var xml = "<Person><Name>Bob</Name><Age>25</Age></Person>";
        doc.LoadXml(xml);
        var root = doc.DocumentElement;
        var nameValue = root["Name"].InnerText;
        var ageValue = int.Parse(root["Age"].InnerText);
        Console.WriteLine($"{nameValue}, {ageValue}");
    }
}
```

---

## 4. **System.Xml.Linq (XDocument)**
**LINQ to XML, modern, concise**

```csharp
using System;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        // Serialize
        var person = new XElement("Person",
            new XElement("Name", "Alice"),
            new XElement("Age", 30)
        );
        Console.WriteLine(person.ToString());

        // Deserialize
        var xml = "<Person><Name>Bob</Name><Age>25</Age></Person>";
        var doc = XDocument.Parse(xml);
        var name = doc.Root.Element("Name").Value;
        var age = (int)doc.Root.Element("Age");
        Console.WriteLine($"{name}, {age}");
    }
}
```

---

## 5. **Newtonsoft.Json (XmlNodeConverter)**
**Third-party, supports JSON and XML**

```csharp
using System;
using System.Xml;
using Newtonsoft.Json;

class Program
{
    static void Main()
    {
        var person = new { Name = "Alice", Age = 30 };

        // Serialize
        var doc = JsonConvert.DeserializeXmlNode(JsonConvert.SerializeObject(person));
        Console.WriteLine(doc.OuterXml);

        // Deserialize
        var xml = "<Person><Name>Bob</Name><Age>25</Age></Person>";
        var json = JsonConvert.SerializeXmlNode(new XmlDocument().LoadXml(xml));
        var result = JsonConvert.DeserializeAnonymousType(json, new { Name = "", Age = 0 });
        Console.WriteLine($"{result.Name}, {result.Age}");
    }
}
```

---

## 6. **ServiceStack.Xml**
**Third-party, high-performance, attribute-driven**

```csharp
using System;
using ServiceStack;
using ServiceStack.Text;

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}

class Program
{
    static void Main()
    {
        var person = new Person { Name = "Alice", Age = 30 };

        // Serialize
        var xml = XmlSerializer.SerializeToString(person);
        Console.WriteLine(xml);

        // Deserialize
        var result = XmlSerializer.DeserializeFromString<Person>(xml);
        Console.WriteLine($"{result.Name}, {result.Age}");
    }
}
```

---

## 7. **Protobuf-net (with Xml support)**
**Third-party, supports multiple formats**

```csharp
using System;
using System.IO;
using protobuf_net;

[ProtoContract]
public class Person
{
    [ProtoMember(1)]
    public string Name { get; set; }
    [ProtoMember(2)]
    public int Age { get; set; }
}

class Program
{
    static void Main()
    {
        var person = new Person { Name = "Alice", Age = 30 };

        // Serialize
        using (var stream = new MemoryStream())
        {
            Serializer.Serialize(stream, person);
            stream.Position = 0;
            using (var reader = new StreamReader(stream))
            {
                Console.WriteLine(reader.ReadToEnd());
            }
        }

        // Deserialize
        var xml = "<Person><Name>Bob</Name><Age>25</Age></Person>";
        using (var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(xml)))
        {
            var result = Serializer.Deserialize<Person>(stream);
            Console.WriteLine($"{result.Name}, {result.Age}");
        }
    }
}
```

---

## 8. **YAXLib (Yet Another XML Library)**
**Third-party, attribute-driven, supports complex scenarios**

```csharp
using System;
using YAXLib;

public class Person
{
    [YAXAttributeForClass]
    public string Name { get; set; }
    [YAXAttributeFor("Age")]
    public int Age { get; set; }
}

class Program
{
    static void Main()
    {
        var person = new Person { Name = "Alice", Age = 30 };

        // Serialize
        var serializer = new YAXSerializer(typeof(Person));
        var xml = serializer.Serialize(person);
        Console.WriteLine(xml);

        // Deserialize
        var result = (Person)serializer.Deserialize(xml);
        Console.WriteLine($"{result.Name}, {result.Age}");
    }
}
```

---

## 9. **XmlSchemaClassGenerator (for XSD-based serialization)**
**Generate classes from XSD, then use XmlSerializer**

- Use the [xsd.exe](https://docs.microsoft.com/en-us/dotnet/standard/serialization/xml-schema-definition-tool-xsd-exe) tool to generate classes from XSD.
- Then use `XmlSerializer` as in section 1.

---

## 10. **Custom XML Serialization (IXmlSerializable)**
**Full control, implement your own logic**

```csharp
using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

public class Person : IXmlSerializable
{
    public string Name { get; set; }
    public int Age { get; set; }

    public XmlSchema GetSchema() => null;

    public void ReadXml(XmlReader reader)
    {
        reader.ReadStartElement();
        Name = reader.ReadElementContentAsString();
        Age = reader.ReadElementContentAsInt();
        reader.ReadEndElement();
    }

    public void WriteXml(XmlWriter writer)
    {
        writer.WriteElementString("Name", Name);
        writer.WriteElementString("Age", Age.ToString());
    }
}

class Program
{
    static void Main()
    {
        var person = new Person { Name = "Alice", Age = 30 };

        // Serialize
        var serializer = new XmlSerializer(typeof(Person));
        using (var writer = new StringWriter())
        {
            serializer.Serialize(writer, person);
            Console.WriteLine(writer.ToString());
        }

        // Deserialize
        var xml = "<Person><Name>Bob</Name><Age>25</Age></Person>";
        using (var reader = new StringReader(xml))
        {
            var result = (Person)serializer.Deserialize(reader);
            Console.WriteLine($"{result.Name}, {result.Age}");
        }
    }
}
```

---

## **Summary Table**

| Library/Method                | Built-in/Third-party | Attributes | Performance | Complexity | Notes                        |
|-------------------------------|----------------------|------------|-------------|------------|------------------------------|
| XmlSerializer                 | Built-in             | Yes        | Medium      | Low        | Simple, attribute-driven     |
| DataContractSerializer        | Built-in             | Yes        | Medium      | Medium     | Supports complex types       |
| XmlDocument/XmlReader/Writer  | Built-in             | No         | High        | High       | Full control, manual         |
| XDocument (LINQ to XML)       | Built-in             | No         | High        | Medium     | Modern, concise              |
| Newtonsoft.Json               | Third-party          | No         | Medium      | Medium     | JSON/XML interop             |
| ServiceStack.Xml              | Third-party          | Yes        | High        | Low        | High-performance             |
| Protobuf-net                  | Third-party          | Yes        | High        | Medium     | Multi-format                 |
| YAXLib                        | Third-party          | Yes        | Medium      | Low        | Attribute-driven             |
| IXmlSerializable              | Built-in             | No         | High        | High       | Full control                 |
| XmlSchemaClassGenerator       | Tool                 | Yes        | Medium      | Medium     | XSD-based                    |

---

**Which use case or library interests you most?**
Would you like a deeper dive into any of these, or are you looking for a recommendation based on a specific scenario?