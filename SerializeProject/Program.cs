using System.Xml;
using System.Xml.Serialization;
using System.Text.Json;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;

namespace SerializeProject
{
    [Serializable]
    public class Address
    {
        public string? City { set; get; }
        public string? Street { set; get; }
    }

    [Serializable]
    public class User
    {
        public string? Name { set; get; }
        public int Age { set; get; }
        public Address? Address { set; get; }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            List<User> users = new List<User>()
            {
                new User(){ Name = "Joe", Age = 24,
                    Address = new Address(){ City = "Moscow", Street = "Tverskaya"},
                },
                new User(){ Name = "Tim", Age = 33,
                    Address = new Address(){ City = "Tula", Street = "Boldina"},
                },
                new User(){ Name = "Bob", Age = 42,
                    Address = new Address(){ City = "Orel", Street = "Lenina"},
                },
                new User(){ Name = "Sam", Age = 39,
                    Address = new Address(){ City = "St Peterbug", Street = "Nevsky"},
                },
                new User(){ Name = "Leo", Age = 26,
                    Address = new Address(){ City = "Voroneg", Street = "Sovetskaya"}
                },
            };

            XmlSerializer serializer = new XmlSerializer(typeof(List<User>));

            //using (FileStream file = new FileStream("users.xml", FileMode.Create))
            //{
            //    XmlWriterSettings settings = new();
            //    //settings.Indent = true;
            //    //settings.IndentChars = " ";
            //    XmlWriter writer = XmlWriter.Create(file, settings);

            //    serializer.Serialize(writer, users);
            //}

            List<User> usersXml;
            using (FileStream file = new FileStream("users.xml", FileMode.Open))
            {
                usersXml = (serializer.Deserialize(file) as List<User>);
            }
            foreach(User user in usersXml)
                Console.WriteLine($"{user.Name}: {user.Age} -> {user.Address.City} / {user.Address.Street}");
            Console.WriteLine();

            //string serializeStrJson = JsonSerializer.Serialize(users);
            //Console.WriteLine(serializeStrJson);

            //using(FileStream fileJson = new FileStream("users.json", FileMode.Create))
            //{
            //    JsonSerializer.Serialize(fileJson, users, new JsonSerializerOptions() { WriteIndented = true});
            //}

            List<User> usersJson;
            using (FileStream fileJson = new FileStream("users.json", FileMode.Open))
            {
                usersJson = JsonSerializer.Deserialize<List<User>>(fileJson);
            }
            foreach (User user in usersJson)
                Console.WriteLine($"{user.Name}: {user.Age} -> {user.Address.City} / {user.Address.Street}");
            Console.WriteLine();

            BinaryFormatter formatter = new();

            //using (FileStream fileBin = new FileStream("users.dat", FileMode.Create))
            //{
            //    formatter.Serialize(fileBin, users);
            //}
            List<User> usersBin;
            using (FileStream fileBin = new FileStream("users.dat", FileMode.Open))
            {
                usersBin = (formatter.Deserialize(fileBin) as List<User>);
            }
            foreach (User user in usersBin)
                Console.WriteLine($"{user.Name}: {user.Age} -> {user.Address.City} / {user.Address.Street}");
            Console.WriteLine();
        }
    }
}