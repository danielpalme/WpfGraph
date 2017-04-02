using System;
using System.Windows.Media;
using Palmmedia.WpfGraph.Core;

namespace Palmmedia.WpfGraph.UI.ViewModels
{
    /// <summary>
    /// Extension methods to simplify changing properties of data attached to an edge.
    /// </summary>
    internal static class EdgeExtensions
    {
        /// <summary>
        /// Changes the color of the edge.
        /// </summary>
        /// <param name="edge">The edge.</param>
        /// <param name="color">The color.</param>
        public static void ChangeColor(this Edge<NodeData, EdgeData> edge, Color color)
        {
            edge.Data.Color = color;
        }

        /// <summary>
        /// Starts a blink animation.
        /// </summary>
        /// <param name="edge">The edge.</param>
        public static void Blink(this Edge<NodeData, EdgeData> edge)
        {
            edge.Data.Blink();
        }

        /// <summary>
        /// Starts a blink animation with the given duration.
        /// </summary>
        /// <param name="edge">The edge.</param>
        /// <param name="duration">The duration of the animation.</param>
        public static void Blink(this Edge<NodeData, EdgeData> edge, double duration)
        {
            edge.Data.Blink(duration);
        }

        /// <summary>
        /// Starts a blink animation and executes the given <see cref="Action">callback</see> at the end of the animation.
        /// </summary>
        /// <param name="edge">The edge.</param>
        /// <param name="callback">The <see cref="Action">callback</see> executed at the end of the animation.</param>
        public static void Blink(this Edge<NodeData, EdgeData> edge, Action callback)
        {
            edge.Data.Blink(callback);
        }

        /// <summary>
        /// Starts a blink animation with the given duration and executes the given <see cref="Action">callback</see> at the end of the animation.
        /// </summary>
        /// <param name="edge">The edge.</param>
        /// <param name="duration">The duration of the animation.</param>
        /// <param name="callback">The <see cref="Action">callback</see> executed at the end of the animation.</param>
        public static void Blink(this Edge<NodeData, EdgeData> edge, double duration, Action callback)
        {
            edge.Data.Blink(duration, callback);
        }

        /// <summary>
        /// Determines whether the edge starts or ends with the given node.
        /// </summary>
        /// <param name="edge">The edge.</param>
        /// <param name="node">The node.</param>
        /// <returns><c>true</c> if edge starts with given node.</returns>
        public static bool StartsOrEndsWith(this Edge<NodeData, EdgeData> edge, Node<NodeData, EdgeData> node)
        {
            return edge.FirstNode == node || edge.SecondNode == node;
        }
    }
}