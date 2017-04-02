using System;
using System.Windows.Media.Media3D;

namespace Palmmedia.WpfGraph.UI.ViewModels
{
    /// <summary>
    /// Provides data for the <see cref="E:NodeData.NodeMoved"/> event.
    /// </summary>
    public class NodeMovedEventArgs : AnimationEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NodeMovedEventArgs"/> class.
        /// </summary>
        /// <param name="duration">The duration of the animation.</param>
        /// <param name="oldPosition">The position before moving.</param>
        /// <param name="newPosition">The position after moving.</param>
        /// <param name="callback">The <see cref="Action">callback</see> executed at the end of an animation.</param>
        public NodeMovedEventArgs(double duration, Point3D oldPosition, Point3D newPosition, Action callback)
            : base(duration, callback)
        {
            this.OldPosition = oldPosition;
            this.NewPosition = newPosition;
        }

        /// <summary>
        /// Gets the position before moving.
        /// </summary>
        public Point3D OldPosition { get; private set; }

        /// <summary>
        /// Gets the position after moving.
        /// </summary>
        public Point3D NewPosition { get; private set; }
    }
}