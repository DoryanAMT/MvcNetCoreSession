using Newtonsoft.Json;

namespace MvcNetCoreSession.Helpers
{
    public class HelperJsonSession
    {
        //  VAMOS A UTILIZAR EL METODO GetString() COMO HERRAMIENTO
        //  ALMACENAREMOS OBJETOS CON Serialize de JSON { nombre: "aa", raza: ""}
        public static string SerializerObject<T>(T data)
        {
            //  CONVERTIMOS EL OBJETO A STRING MEIANTE Newtonsoft
            string json = JsonConvert.SerializeObject(data);
            return json;
        }
        //  RECIBIREMOS UN string Y LO CONVERTIMOS A CUALQUIER OBJETO T
        public static T DeserealizeObject<T>
            (string data)
        {
            //  DESERAILZAMOS EL STRING A CUALQUIER OBJETO
            T objeto = JsonConvert.DeserializeObject<T>(data);
            return objeto;
        }

    }
}
