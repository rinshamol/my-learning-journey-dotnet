using System.Diagnostics;
using System.Text.Json;
using System.Xml.Serialization;

public class Program
{
    public class Person
    {
       required public string UserName { get; set; }
       public int UserAge { get; set; }
    }

    static void Main()
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        using(var fs = new FileStream("person.dat",FileMode.Open))
        using(var reader = new BinaryReader(fs))
        {
            var deserializedPerson = new Person
            {
                UserName = reader.ReadString(),
                UserAge = reader.ReadInt32()
            };
            stopwatch.Stop();
            Console.WriteLine($"Binary Deserialization, UserName : {deserializedPerson.UserName} and UserAge: {deserializedPerson.UserAge}");
            Console.WriteLine($"Binary Deserialization took {stopwatch.ElapsedMilliseconds} ms");

        }
        var xmlData = "<Person><UserName>Rinsha</UserName><UserAge>22</UserAge></Person>";
        var serilizer = new XmlSerializer(typeof(Person));
        Stopwatch stopwatch1 = Stopwatch.StartNew();
        using(var reader = new StringReader(xmlData))
        {
            var deserializedPerson = (Person)serilizer.Deserialize(reader);
            stopwatch1.Stop();
            Console.WriteLine($"XML Deserialization, UserName : {deserializedPerson.UserName} and UserAge: {deserializedPerson.UserAge}");
            Console.WriteLine($"XML Deserialization took {stopwatch1.ElapsedMilliseconds} ms");
        }
        var jsonData = "{\"UserName\": \"Aadhil\",\"UserAge\":22}";
        Stopwatch stopwatch2 =Stopwatch.StartNew();
        var deserializedPerson1 = JsonSerializer.Deserialize<Person>(jsonData);
        stopwatch2.Stop();
        Console.WriteLine($"JSON Deserialization, UserName : {deserializedPerson1.UserName} and UserAge: {deserializedPerson1.UserAge}");
        Console.WriteLine($"JSON Deserialization took {stopwatch2.ElapsedMilliseconds} ms");
        

    }
}