// Note: SoapFormatter is obsolete in .NET 5+ and requires .NET Framework
// or the System.Runtime.Serialization.Formatters.Soap NuGet package

#if NET48 || NETFRAMEWORK

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Soap;

[Serializable]
public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}

public class SoapFormatterExample
{
    public static void Main()
    {
        var person = new Person
        {
            Name = "Henry Taylor",
            Age = 52
        };

        var formatter = new SoapFormatter();
        
        // Serialization
        using (var ms = new MemoryStream())
        {
            formatter.Serialize(ms, person);
            ms.Position = 0;
            
            using (var reader = new StreamReader(ms))
            {
                string soap = reader.ReadToEnd();
                Console.WriteLine("SOAP Serialized:");
                Console.WriteLine(soap);
            }
            
            // Deserialization
            ms.Position = 0;
            var deserializedPerson = (Person)formatter.Deserialize(ms);
            Console.WriteLine($"\nDeserialized: {deserializedPerson.Name}, Age: {deserializedPerson.Age}");
        }
    }
}

#else

public class SoapFormatterExample
{
    public static void Main()
    {
        Console.WriteLine("SoapFormatter is only available in .NET Framework.");
        Console.WriteLine("It has been marked as obsolete in .NET 5+ due to security concerns.");
    }
}

#endif