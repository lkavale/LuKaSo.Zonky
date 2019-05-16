using LuKaSo.Zonky.Models.Login;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;
using System.Text;

namespace LuKaSo.Zonky.ExampleApp
{
    public class SecretsJsonReader
    {
        private readonly string _path;

        public SecretsJsonReader()
        {
            _path = Path.GetDirectoryName(Assembly.GetAssembly(typeof(SecretsJsonReader)).Location) + "/secrets.json";
        }

        public User Read()
        {
            using (var stream = new FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var reader = new StreamReader(stream, Encoding.UTF8);
                using (var jsonReader = new JsonTextReader(reader))
                {
                    var serializer = new JsonSerializer();
                    return serializer.Deserialize<User>(jsonReader);
                }
            }
        }
    }
}
