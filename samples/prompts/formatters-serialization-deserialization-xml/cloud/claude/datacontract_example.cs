using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

[DataContract(Name = "Person", Namespace = "http://example.com")]
public class Person
{
    [DataMember(Order = 1)]
    public string Name { get; set; }
    
    [DataMember(Order = 2)]
    public int Age { get; set; }
    
    [DataMember(Order = 3)]
    public List<string> Hobbies { get; set; }
    
    // Can serialize private members
    [DataMember(Order = 4)]
    private DateTime CreatedDate { get; set; } = DateTime.Now;
}

public class DataContractExample
{
    public static void Main()
    {
        var person = new Person
        {
            Name = "Jane Smith",
            Age = 28,
            Hobbies = new List<string> { "Photography", "Cooking" }
        };

        var serializer = new DataContractSerializer(typeof(Person));
        
        // Serialization
        using (var ms = new MemoryStream())
        {
            serializer.WriteObject(ms, person);
            ms.Position = 0;
            
            using (var reader = new StreamReader(ms))
            {
                string xml = reader.ReadToEnd();
                Console.WriteLine("Serialized XML:");
                Console.WriteLine(xml);
            }
            
            // Deserialization
            ms.Position = 0;
            var deserializedPerson = (Person)serializer.ReadObject(ms);
            Console.WriteLine($"\nDeserialized: {deserializedPerson.Name}, Age: {deserializedPerson.Age}");
        }
    }
}