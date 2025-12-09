using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

public class Program
{
    public class User
    {
       required public string Name { get; set; }
       required public string Email { get; set; }
       required public string Password { get; set; }

        public static string GenerateHash(string data)
        {
            using(SHA256 sHA256 = SHA256.Create())
            {
                byte[] hashBytes = sHA256.ComputeHash(Encoding.UTF8.GetBytes(data));
                return Convert.ToBase64String(hashBytes);
            }
        }
        public void EncryptData()
        {
            Password = Convert.ToBase64String(Encoding.UTF8.GetBytes(Password));
        }
    }
    public static User? DeserializeUserData(string jsonData, string original, bool isTrustedSource)
    {
        if (!isTrustedSource)
        {
            Console.WriteLine($"Deserialization blocked: Untrusted source");
            return null;
        }
        string incominHash = User.GenerateHash(jsonData);
        if(incominHash != original)
        {
            Console.WriteLine("Data integrity check failed! Possible tampering detected.");
            return null;
        }

        return JsonSerializer.Deserialize<User>(jsonData);
    }
    public static string SerializeUserData(User user,out string hash)
    {
        if(
            string.IsNullOrWhiteSpace(user.Name) || string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.Password))
        {
            Console.WriteLine($"Invalid data. Serialization aborted.");
             hash = string.Empty;
            return string.Empty;
        }
        user.EncryptData();
        string jsonData = JsonSerializer.Serialize(user);
        hash = User.GenerateHash(jsonData);
        return jsonData;
    }

    public static void Main()
    {
        User user = new User
        {
            Name = "Rinsha",
            Email = "rin@gmail.com",
            Password = "12345"
        };
        string hash;
        string serializedData = SerializeUserData(user, out hash);
        Console.WriteLine("Serialized Data:\n" + serializedData);
        Console.WriteLine("Stored Hash:\n" + hash);
        Console.WriteLine("Serialized data: \n" + serializedData);
        User? deserializeData = DeserializeUserData(serializedData,hash, true);
        if (deserializeData != null)
        {
            Console.WriteLine("Deserialization Successful");
            Console.WriteLine("Deserialized data: \n" + deserializeData.Email);
        }

        // Console.WriteLine("generated hash data: \n" + generatedHash);

    }

    

}