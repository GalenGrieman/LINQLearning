using System;
using System.Collections.Generic;
using System.Text;

namespace Features.MyLinq
{
    public static class MyLinq
    {
        //Extension Method that takes the collection/sequence and counts how many there are.
        //Extension Methods are NameSpace Specific
        public static int Count<T>(this IEnumerable<T> sequence)
        {
            var count = 0;
            foreach (var item in sequence)
            {
                count += 1;
            }
            return count;
        }
    }
}
