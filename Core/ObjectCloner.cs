using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MccTomskHelpers.Core
{
    public static class ObjectCloner
    {
        /// <summary>
        /// Perform a deep Copy of the object.
        /// </summary>
        public static T Clone<T>(this T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            // Don't serialize a null object, simply return the default for that object
            if (ReferenceEquals(source, null))
            {
                return default(T);
            }

            var formatter = new BinaryFormatter();
            var stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }
    }
}
