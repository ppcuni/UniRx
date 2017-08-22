using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#if SystemReactive
namespace System.Reactive.Subjects
#else
namespace UniRx
#endif
{
    public interface ISubject<TSource, TResult> : IObserver<TSource>, IObservable<TResult>
    {
    }

    public interface ISubject<T> : ISubject<T, T>, IObserver<T>, IObservable<T>
    {
    }

    public static class SubscriptionCounter
    {
        public static Dictionary<string, long> ReferenceCounts = new Dictionary<string, long>();

        public static long GetReferenceCount(){ lock(ReferenceCounts){ return ReferenceCounts.Values.Sum();} }

        public static string GetReferences() { lock (ReferenceCounts) return string.Join("\n\n", ReferenceCounts.Where(x => x.Value > 0).OrderBy(x => x.Value).Select(x => $"{x.Value}: {x.Key}").ToArray()); }

        public static string CreateKey()
        {
            // AsyncMethodBuilder周りのログカット、およびStartCoroutine呼び出しで必ず通る部分のログカット
            var lines = new Diagnostics.StackTrace().ToString().Split('\n');
            var sb = new StringBuilder();
            foreach (var l in lines.Skip(3).Where(x => !x.StartsWith("   at System.Runtime.CompilerServices")))
                sb.AppendLine(l.TrimEnd());
            return sb.ToString();
        }

        public static void Add(string key)
        {
            lock(ReferenceCounts)
            {
                if (ReferenceCounts.ContainsKey(key))
                    ReferenceCounts[key]++;
                else
                    ReferenceCounts[key] = 1;
            }
        }

        public static void Remove(string key)
        {
            lock (ReferenceCounts)
            {
                ReferenceCounts[key]--;
            }
        }

    }
}