using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lirva.Enumerable
{
    public static class ForEach
    {
        public static void Sysnc<TSource>(this IEnumerable<TSource> Source, Action<TSource> Action)
        {
            foreach (TSource Item in Source)
            {
                Action(Item);
            }
        }
        public static Task Async<TSource>(this IEnumerable<TSource> Source, int Partition, Func<TSource, Task> Body)
        {
            return Task.WhenAll(Partitioner.Create(Source).GetPartitions(Partition)
                .Select(Partitions => Task.Run(async () => 
                {
                    while (Partitions.MoveNext())
                    {
                        await Body(Partitions.Current);
                    }
                })));
        }
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> Source, Func<TSource, TKey> KeySelector)
        {
            var SeenKeys = new HashSet<TKey>();

            foreach (TSource Element in Source)
            {
                if (SeenKeys.Add(KeySelector(Element)))
                {
                    yield return Element;
                }
            }
        }
    }
}
