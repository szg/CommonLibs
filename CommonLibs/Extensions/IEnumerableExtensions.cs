using System.Collections.Generic;
using System.Linq;

namespace System
{
#pragma warning disable CS1591
    public static class IEnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source == null)
            {
                return;
            }
            var arr = source as T[];
            if (arr != null)
            {
                Array.ForEach(arr, action);
                return;
            }
            var list = source as List<T>;
            if (list != null)
            {
                list.ForEach(action);
                return;
            }
            foreach (var item in source)
            {
                action(item);
            }
        }

        public static IEnumerable<TResult> ConvertAllX<T, TResult>(this IEnumerable<T> sources, Converter<T, TResult> transformation)
        {
            var arr = sources as T[];
            if (arr != null)
            {
                return Array.ConvertAll(arr, transformation);
            }
            var list = sources as List<T>;
            if (list != null)
            {
                return list.ConvertAll(transformation);
            }

            return sources.Select(_ => transformation(_));
        }

        public static string Join(this IEnumerable<string> source, string separator = ",")
        {
            if (source == null || !source.Any())
                return string.Empty;
            return string.Join(separator, source);
        }
    }
#pragma warning restore CS1591
}
