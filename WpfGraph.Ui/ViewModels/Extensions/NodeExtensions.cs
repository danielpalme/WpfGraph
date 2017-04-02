using System;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Palmmedia.WpfGraph.Core;

namespace Palmmedia.WpfGraph.UI.ViewModels
{
    /// <summary>
    /// Extension methods to simplify changing properties of data attached to a node.
    /// </summary>
    internal static class NodeExtensions
    {
        /// <summary>
        /// Changes the color of the node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="color">The color.</param>
        public static void ChangeColor(this Node<NodeData, EdgeData> node, Color color)
        {
            node.Data.Color = color;
        }

        /// <summary>
        /// Moves the node to the given target position.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="targetPosition">The target position.</param>
        public static void Move(this Node<NodeData, EdgeData> node, Point3D targetPosition)
        {
            node.Data.Move(targetPosition);
        }

        /// <summary>
        /// Moves the node to the given target position.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="targetPosition">The target position.</param>
        /// <param name="duration">The duration of the animation.</param>
        public static void Move(this Node<NodeData, EdgeData> node, Point3D targetPosition, double duration)
        {
            node.Data.Move(targetPosition, duration);
        }

        /// <summary>
        /// Moves the node to the given target position and executes the given <see cref="Action">callback</see> at the end of the animation.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="targetPosition">The target position.</param>
        /// <param name="duration">The duration of the animation.</param>
        /// <param name="callback">The <see cref="Action">callback</see> executed at the end of the animation.</param>
        public static void Move(this Node<NodeData, EdgeData> node, Point3D targetPosition, double duration, Action callback)
        {
            node.Data.Move(targetPosition, duration, callback);
        }

        /// <summary>
        /// Starts a blink animation.
        /// </summary>
        /// <param name="node">The node.</param>
        public static void Blink(this Node<NodeData, EdgeData> node)
        {
            node.Data.Blink();
        }

        /// <summary>
        /// Starts a blink animation with the given duration.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="duration">The duration of the animation.</param>
        public static void Blink(this Node<NodeData, EdgeData> node, double duration)
        {
            node.Data.Blink(duration);
        }

        /// <summary>
        /// Starts a blink animation and executes the given <see cref="Action">callback</see> at the end of the animation.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="callback">The <see cref="Action">callback</see> executed at the end of the animation.</param>
        public static void Blink(this Node<NodeData, EdgeData> node, Action callback)
        {
            node.Data.Blink(callback);
        }

        /// <summary>
        /// Starts a blink animation with the given duration and executes the given <see cref="Action">callback</see> at the end of the animation.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="duration">The duration of the animation.</param>
        /// <param name="callback">The <see cref="Action">callback</see> executed at the end of the animation.</param>
        public static void Blink(this Node<NodeData, EdgeData> node, double duration, Action callback)
        {
            node.Data.Blink(duration, callback);
        }
    }
}
