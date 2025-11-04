using System;
using System.IO;
using System.Runtime.Serialization;

[Serializable]
public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public Address HomeAddress { get; set; }
}

[Serializable]
public class Address
{
    public string Street { get; set; }
    public string City { get; set; }
}

public class NetDataContractExample
{
    public static void Main()
    {
        var person = new Person
        {
            Name = "George Harris",
            Age = 38,
            HomeAddress = new Address
            {
                Street = "123 Main St",
                City = "Boston"
            }
        };

        var serializer = new NetDataContractSerializer();
        
        // Serialization
        using (var ms = new MemoryStream())
        {
            serializer.Serialize(ms, person);
            ms.Position = 0;
            
            using (var reader = new StreamReader(ms))
            {
                string xml = reader.ReadToEnd();
                Console.WriteLine("Serialized XML (includes .NET type info):");
                Console.WriteLine(xml);
            }
            
            // Deserialization
            ms.Position = 0;
            var deserializedPerson = (Person)serializer.Deserialize(ms);
            Console.WriteLine($"\nDeserialized: {deserializedPerson.Name}, Age: {deserializedPerson.Age}");
            Console.WriteLine($"Address: {deserializedPerson.HomeAddress.Street}, {deserializedPerson.HomeAddress.City}");
        }
    }
}