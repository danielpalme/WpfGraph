using System.Globalization;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Palmmedia.WpfGraph.Ui.ViewModels
{
    /// <summary>
    /// Data attached to a <see cref="Palmmedia.WpfGraph.Core.Node&lt;TNodeType, TEdgeType&gt;"/>.
    /// </summary>
    public class NodeData : GraphDataBase
    {
        /// <summary>
        /// Logger instance.
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(NodeData));

        /// <summary>
        /// Counter to give each node a name in constructor.
        /// </summary>
        private static int counter = 1;

        /// <summary>
        /// The postion of the node.
        /// </summary>
        private Point3D position;

        /// <summary>
        /// Initializes a new instance of the <see cref="NodeData"/> class.
        /// </summary>
        public NodeData()
        {
            this.Text = (counter++).ToString(CultureInfo.InvariantCulture);
            this.Color = Colors.LightYellow;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NodeData"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        public NodeData(Point3D position)
            : this()
        {
            this.Position = position;
        }

        /// <summary>
        /// Occurs when the node has been moved.
        /// </summary>
        public event EventHandler<NodeMovedEventArgs>? NodeMoved;

        /// <summary>
        /// Gets or sets the X position.
        /// </summary>
        public double XPosition
        {
            get
            {
                return this.position.X;
            }

            set
            {
                var currentPosition = this.Position;
                currentPosition.X = value;
                this.Move(currentPosition, 0, null);
            }
        }

        /// <summary>
        /// Gets or sets the Y position.
        /// </summary>
        public double YPosition
        {
            get
            {
                return this.position.Y;
            }

            set
            {
                var currentPosition = this.Position;
                currentPosition.Y = value;
                this.Move(currentPosition, 0, null);
            }
        }

        /// <summary>
        /// Gets or sets the Z position.
        /// </summary>
        public double ZPosition
        {
            get
            {
                return this.position.Z;
            }

            set
            {
                var currentPosition = this.Position;
                currentPosition.Z = value;
                this.Move(currentPosition, 0, null);
            }
        }

        /// <summary>
        /// Gets the position of the node.
        /// </summary>
        public Point3D Position
        {
            get
            {
                return this.position;
            }

            private set
            {
                this.position = value;
                this.OnPropertyChanged(nameof(this.XPosition));
                this.OnPropertyChanged(nameof(this.YPosition));
                this.OnPropertyChanged(nameof(this.ZPosition));
                this.OnPropertyChanged(nameof(this.Position));
            }
        }

        /// <summary>
        /// Gets or sets the text of the node.
        /// </summary>
        public string Text
        {
            get
            {
                return field;
            }

            set
            {
                field = value;
                this.OnPropertyChanged(nameof(this.Text));
            }
        }

        /// <summary>
        /// Moves the node to the given target position.
        /// </summary>
        /// <param name="targetPosition">The target position.</param>
        public void Move(Point3D targetPosition)
        {
            this.Move(targetPosition, 0, null);
        }

        /// <summary>
        /// Moves the node to the given target position.
        /// </summary>
        /// <param name="targetPosition">The target position.</param>
        /// <param name="duration">The duration of the animation.</param>
        public void Move(Point3D targetPosition, double duration)
        {
            this.Move(targetPosition, duration, null);
        }

        /// <summary>
        /// Moves the node to the given target position and executes the given <see cref="Action">callback</see> at the end of the animation.
        /// </summary>
        /// <param name="targetPosition">The target position.</param>
        /// <param name="duration">The duration of the animation.</param>
        /// <param name="callback">The <see cref="Action">callback</see> executed at the end of the animation.</param>
        public void Move(Point3D targetPosition, double duration, Action? callback)
        {
            var nodeMovedEventArgs = new NodeMovedEventArgs(duration, this.Position, targetPosition, callback);

            this.OnNodeMoved(nodeMovedEventArgs);
            this.Position = targetPosition;
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return "Position: " + this.Position + ", Text: " + this.Text;
        }

        /// <summary>
        /// Raises the <see cref="E:NodeMoved"/> event.
        /// </summary>
        /// <param name="args">The <see cref="Palmmedia.WpfGraph.Ui.ViewModels.NodeMovedEventArgs"/> instance containing the event data.</param>
        protected virtual void OnNodeMoved(NodeMovedEventArgs args)
        {
            Logger.Debug("Moved node from: " + args.OldPosition + " to: " + args.NewPosition);

            this.NodeMoved?.Invoke(this, args);
        }
    }
}
