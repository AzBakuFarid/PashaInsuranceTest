using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PashaInsuranceTest.Extensions
{
    public static class LINQExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action) where T : class
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            foreach (T element in source)
            {
                action(element);
            }
        }
    }
}
