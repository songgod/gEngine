using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gEngine.Util
{
    public static class ExtensionsHelper
    {
        #region Dictionary方面

        public static void TryAdd<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            if (key == null) return;
            if (dict.ContainsKey(key) == false) dict.Add(key, value);
        }

        public static bool IsContainsKey<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key)
        {
            bool result = false;
            if (key != null)
                result = dict.ContainsKey(key);
            return result;
        }

        #endregion
    }
}
