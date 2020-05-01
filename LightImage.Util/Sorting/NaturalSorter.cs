using LightImage.Util.Collections;
using System;
using System.Collections.Generic;

namespace LightImage.Util.Sorting
{
    public class NaturalSorter : IComparer<string>
    {
        public static NaturalSorter Instance = new NaturalSorter();
        private readonly LruCache<string, NameWithIndex> _cache = new LruCache<string, NameWithIndex>(100);
        private readonly Func<string, NameWithIndex> _parserDelegate = Parse;

        public NaturalSorter()
        {
        }

        public int Compare(string x, string y)
        {
            var nx = Get(x);
            var ny = Get(y ?? "");
            if (nx.HasIndex && ny.HasIndex && nx.Name == ny.Name)
                return nx.Index.CompareTo(ny.Index);
            return nx.Full.CompareTo(ny.Full);
        }

        int IComparer<string>.Compare(string x, string y) => Compare(x, y);

        /// <summary>
        /// Normalize string by replacing NULL to avoid cache lookup errors.
        /// </summary>
        /// <param name="str">The original string.</param>
        /// <returns>The normalized string.</returns>
        private static string Normalize(string str) => str ?? string.Empty;

        private static NameWithIndex Parse(string str) => NameWithIndex.Parse(Normalize(str));

        private NameWithIndex Get(string str) => _cache.Get(Normalize(str), _parserDelegate);
    }
}