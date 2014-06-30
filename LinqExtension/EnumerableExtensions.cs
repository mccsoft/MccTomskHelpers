using System.Collections.Generic;
using System.Linq;

namespace LinqExtension
{
    public static class EnumerableExtensions
    {
        public static int IndexOf<T>(this IEnumerable<T> source, T value)
        {
            return IndexOf(source, value, EqualityComparer<T>.Default);
        }

        public static int IndexOf<T>(this IEnumerable<T> source, T value, EqualityComparer<T> equalityComparer)
        {
            var index = 0;
            
            foreach (var item in source)
            {
                if (equalityComparer.Equals(item, value)) return index;
                index++;
            }

            return -1;
        }

        public static IEnumerable<T> GetRange<T>(this IEnumerable<T> source, int offset, int duration)
        {
            return source.Skip(offset).Take(duration);
        }
    }
}
