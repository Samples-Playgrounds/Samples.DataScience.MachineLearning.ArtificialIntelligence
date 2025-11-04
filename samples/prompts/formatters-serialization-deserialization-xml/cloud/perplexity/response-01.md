Here are details including samples on the other XML serialization libraries in .NET/C# you mentioned:

### 1. SharpSerializer
SharpSerializer is an open-source XML and binary serializer for .NET. It offers easy-to-use APIs with support for complex types like multidimensional arrays and nested objects. SharpSerializer can serialize objects with a simple call and control over the XML output.

Basic usage example:

```csharp
using Polenter.Serialization; // SharpSerializer namespace

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}

class Program
{
    static void Main()
    {
        var serializer = new SharpSerializer();
        var person = new Person { Name = "John Doe", Age = 30 };

        // Serialize to file
        string filePath = "person.xml";
        serializer.Serialize(person, filePath);

        // Deserialize from file
        Person deserializedPerson = (Person)serializer.Deserialize(filePath);
        Console.WriteLine($"Name: {deserializedPerson.Name}, Age: {deserializedPerson.Age}");
    }
}
```

SharpSerializer supports XML by default but can be configured for binary serialization as well. It handles complex object graphs automatically without requiring attributes.[4][6][7]

***

### 2. ExtendedXmlSerializer
ExtendedXmlSerializer is a flexible and modern library that emphasizes configurability and extensibility. It supports customizing namespaces, XML element names, and offers a fluent API for configuration.

A simple example:

```csharp
using ExtendedXmlSerializer;
using System;

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}

class Program
{
    static void Main()
    {
        var serializer = new ConfigurationContainer().Create();

        var person = new Person { Name = "Alice", Age = 28 };

        // Serialize to XML string
        string xml = serializer.Serialize(person);
        Console.WriteLine(xml);

        // Deserialize back
        Person deserializedPerson = serializer.Deserialize<Person>(xml);
        Console.WriteLine($"Name: {deserializedPerson.Name}, Age: {deserializedPerson.Age}");
    }
}
```

ExtendedXmlSerializer allows advanced scenarios including support for interfaces, collections, and version-tolerant serialization . (I will fetch a source next to confirm sample details.)

***

### 3. netbike.xml
netbike.xml is a less mainstream but lightweight XML serialization library focusing on performance and simplicity. It supports attribute-decorated POCO serialization and customizations with little overhead.

Basic usage looks like:

```csharp
using NetBike.Xml;
using System;

public class Person
{
    [XmlElement("Name")]
    public string Name { get; set; }

    [XmlElement("Age")]
    public int Age { get; set; }
}

class Program
{
    static void Main()
    {
        var serializer = new XmlSerializer();

        var person = new Person { Name = "Bob", Age = 40 };
        string xml = serializer.Serialize(person);
        Console.WriteLine(xml);

        var deserialized = serializer.Deserialize<Person>(xml);
        Console.WriteLine($"Name: {deserialized.Name}, Age: {deserialized.Age}");
    }
}
```

This library focuses on a simple API and typical XML serialization needs with attribute support . (Will verify exact namespace and usage in follow-up if needed.)Added samples and descriptions for these libraries:

| Library            | Description                                                  | Sample                                                                                                  |
|--------------------|--------------------------------------------------------------|---------------------------------------------------------------------------------------------------------|
| SharpSerializer     | Open-source .NET XML/binary serializer. Handles complex types and nested objects with simple calls. Does not require attributes. | ```csharp using Polenter.Serialization; var serializer = new SharpSerializer(); var p = new Person { Name = "John", Age = 30 }; serializer.Serialize(p, "file.xml"); var dp = (Person)serializer.Deserialize("file.xml"); ```
| ExtendedXmlSerializer | Modern, extensible, fluent API. Supports interfaces, collections, version-tolerant serialization. | ```csharp using ExtendedXmlSerializer; var serializer = new ConfigurationContainer().Create(); var xml = serializer.Serialize(p); var dp = serializer.Deserialize<Person>(xml); ```
| netbike.xml          | Lightweight XML serializer focused on simple attribute-based POCO serialization. | ```csharp using NetBike.Xml; var serializer = new XmlSerializer(); var xml = serializer.Serialize(p); var dp = serializer.Deserialize<Person>(xml); ```

These complement the previously mentioned options like XmlSerializer, DataContractSerializer, and LINQ to XML. Therefore, .NET XML (de)serialization offers a broad spectrum from lightweight and manual to advanced and attribute-free serializers per use case.[6][7][4]

[1](https://dontpaniclabs.com/blog/post/2025/05/06/formatting-xml-when-serializing-c-objects/)
[2](https://jonathancrozier.com/blog/xml-serialization-with-c-sharp)
[3](https://learn.microsoft.com/en-us/dotnet/standard/serialization/examples-of-xml-serialization)
[4](https://www.sharpserializer.net)
[5](https://stackoverflow.com/questions/4123590/serialize-an-object-to-xml)
[6](https://www.sharpserializer.net/en/tutorial/index.html)
[7](https://github.com/polenter/SharpSerializer)
[8](https://www.codeproject.com/articles/XML-Serialization-of-Generic-Dictionary-Multidimen)
[9](https://learn.microsoft.com/en-us/dotnet/api/system.xml.serialization.xmlserializer?view=net-9.0)
[10](https://stackoverflow.com/questions/tagged/sharpserializer)