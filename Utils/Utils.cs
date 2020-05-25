using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectB.Utils
{
    public static class Utils
    {
        public static void PrintAllWithIndex<T, Y>(this IList<T> _objects, Func<T, Y> member)
        {
            var count = 1;
            foreach (var obj in _objects)
            {
                Console.WriteLine($"({count}) {member(obj)}");
                count++;
            }
        }
    }
}
