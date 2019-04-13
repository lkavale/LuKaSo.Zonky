using Newtonsoft.Json;
using System.IO;
using System.Reflection;
using System.Text;

namespace LuKaSo.Zonky.Tests.Production
{
    public class SecretsJsonReader
    {
        private readonly string _path;

        public SecretsJsonReader()
        {
            _path = Path.GetDirectoryName(Assembly.GetAssembly(typeof(SecretsJsonReader)).Location) + "/secrets.json";
        }

        public Secrets Read()
        {
            using (var stream = new FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var serializer = new JsonSerializer();
                return serializer.Deserialize<Secrets>(jsonReader);
            }
        }
    }
}
