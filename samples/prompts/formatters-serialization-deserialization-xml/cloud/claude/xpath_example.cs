using System;
using System.Xml;
using System.Xml.XPath;

public class XPathExample
{
    public static void Main()
    {
        string xml = @"<?xml version='1.0'?>
<People>
    <Person id='1'>
        <Name>John Doe</Name>
        <Age>30</Age>
        <City>New York</City>
    </Person>
    <Person id='2'>
        <Name>Jane Smith</Name>
        <Age>28</Age>
        <City>Los Angeles</City>
    </Person>
    <Person id='3'>
        <Name>Bob Wilson</Name>
        <Age>42</Age>
        <City>Chicago</City>
    </Person>
</People>";

        var doc = new XmlDocument();
        doc.LoadXml(xml);
        
        var navigator = doc.CreateNavigator();

        // Select all persons
        Console.WriteLine("All persons:");
        var personNodes = navigator.Select("//Person");
        while (personNodes.MoveNext())
        {
            var name = personNodes.Current.SelectSingleNode("Name")?.Value;
            var age = personNodes.Current.SelectSingleNode("Age")?.Value;
            Console.WriteLine($"  {name}, Age: {age}");
        }

        // Select persons older than 30
        Console.WriteLine("\nPersons older than 30:");
        var olderPersons = navigator.Select("//Person[Age > 30]");
        while (olderPersons.MoveNext())
        {
            var name = olderPersons.Current.SelectSingleNode("Name")?.Value;
            Console.WriteLine($"  {name}");
        }

        // Select person by attribute
        Console.WriteLine("\nPerson with id='2':");
        var specificPerson = navigator.SelectSingleNode("//Person[@id='2']/Name");
        Console.WriteLine($"  {specificPerson?.Value}");

        // Count total persons
        var count = navigator.Evaluate("count(//Person)");
        Console.WriteLine($"\nTotal persons: {count}");
    }
}