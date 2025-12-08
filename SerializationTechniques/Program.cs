
using System.Text.Json;
using System.Xml.Serialization;

public class Project
{
    public class Person
    {
       required public string UserName { get;set; }
       required public int UserAge { get;set; }

    }

    static void Main()
    {
        Person samplePerson = new Person { UserName = "Rinsha", UserAge = 22};

        using(FileStream fs = new FileStream("person.dat", FileMode.Create))
        {
            BinaryWriter writer = new BinaryWriter(fs);
            writer.Write(samplePerson.UserName);
            writer.Write(samplePerson.UserAge);
        }
        Console.WriteLine($"Binary serialization Completed");
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Person));
        using(StreamWriter writer = new StreamWriter("person.xml"))
        {
            xmlSerializer.Serialize(writer, samplePerson);
        }
        Console.WriteLine($"XML serialization Completed");
        string jsonStiring = JsonSerializer.Serialize(samplePerson);
        File.WriteAllText("person.json",jsonStiring);
        Console.WriteLine($"Json serialization Completed");
    }
}