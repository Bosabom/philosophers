using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace Hotel_1._0
{
    public class DataSerialization
    {
        public DataSerialization() { }
        public void JsonSerialize(object data, string FilePath)
        {
            JsonSerializer jsonSerializer = new JsonSerializer();
            if (File.Exists(FilePath)) File.Delete(FilePath);
            StreamWriter sw = new StreamWriter(FilePath);
            JsonWriter jsonWriter = new JsonTextWriter(sw);

            jsonSerializer.Serialize(jsonWriter, data);
            jsonWriter.Close();
            sw.Close();

        }
        public object JsonDeserialize(Type dataType, string filepath)
        {
            JObject obj = null;
            JsonSerializer jsonSerializer = new JsonSerializer();
            if (File.Exists(filepath))
            {
                StreamReader sr = new StreamReader(filepath);
                JsonReader jsonReader = new JsonTextReader(sr);
                obj = jsonSerializer.Deserialize(jsonReader) as JObject;
                jsonReader.Close();
                sr.Close();
            }
            return obj.ToObject(dataType);
        }
    }
}
