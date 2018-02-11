using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Common.Extensions
{
    public static class ObjectExtension
    {
        public static byte[] ToBytes(this object obj)
        {
            if (obj == null)
                return null;
            var bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public static object ToObject(this byte[] source)
        {
            using (var memStream = new MemoryStream())
            {
                var bf = new BinaryFormatter();
                memStream.Write(source, 0, source.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                var obj = bf.Deserialize(memStream);
                return obj;
            }
        }
    }
}
