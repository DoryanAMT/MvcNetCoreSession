using System.Runtime.Serialization.Formatters.Binary;


namespace MvcNetCoreSession.Helpers
{
    public class HelperBinarySession
    {
        //  VAMOS A REAR DOS METODOS static
        //  PORQUE NO NECESITAMOS REALIZAR NEW PARA
        //  UTILIZAR LOS METOOS DE CONVERSION QUE CREAREMOS
        //  EN ESTA CLASE
        //  CONVERTIMOS UN OBJETO EN BYTE[]
        public static byte[] ObjectToByte(Object objeto)
        {
            BinaryFormatter formatter =
                new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, objeto);
                return stream.ToArray();
            }
        }

        //  CONVERSOR DE BY A OBJETO
        public static Object ByteToObject(Byte[] data)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(data, 0, data.Length);
                stream.Seek(0, SeekOrigin.Begin);
                Object objeto = (Object)
                    formatter.Deserialize(stream);
                return objeto;
            }
        }
    }
}
