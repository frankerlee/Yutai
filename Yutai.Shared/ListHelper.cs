using System.Collections.Generic;
using System.Linq;

namespace Yutai.Shared
{
    public static class ListHelper
    {
        public static IEnumerator<T> GetEnumerator<T>(this IList<T> list)
        {
            for (int i = 0; i < list.Count(); i++)
            {
                yield return list[i];
            }
        }
    }
}
