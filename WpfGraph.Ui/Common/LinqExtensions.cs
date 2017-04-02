using System;
using System.Collections.Generic;

namespace Palmmedia.WpfGraph.Common
{
    /// <summary>
    /// Linq extensions.
    /// </summary>
    public static class LinqExtensions
    {
        /// <summary>
        /// Creates a <see cref="HashSet&lt;T&gt;"/> from an <see cref="IEnumerable&lt;T&gt;"/>.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="input">The input.</param>
        /// <returns>A <see cref="HashSet&lt;T&gt;"/>.</returns>
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            var result = new HashSet<T>();

            foreach (var item in input)
            {
                result.Add(item);
            }

            return result;
        }

        /// <summary>
        /// Creates a <see cref="Queue&lt;T&gt;"/> from an <see cref="IEnumerable&lt;T&gt;"/>.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="input">The input.</param>
        /// <returns>A <see cref="Queue&lt;T&gt;"/>.</returns>
        public static Queue<T> ToQueue<T>(this IEnumerable<T> input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            var result = new Queue<T>();

            foreach (var item in input)
            {
                result.Enqueue(item);
            }

            return result;
        }
    }
}
