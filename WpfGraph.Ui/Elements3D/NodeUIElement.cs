using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
using Palmmedia.WpfGraph.Core;
using Palmmedia.WpfGraph.Ui.Elements3D.Tesselate;
using Palmmedia.WpfGraph.Ui.Interaction;
using Palmmedia.WpfGraph.Ui.ViewModels;

namespace Palmmedia.WpfGraph.Ui.Elements3D
{
    /// <summary>
    /// Represents a node.
    /// </summary>
    public class NodeUIElement : GraphUIElement
    {
        /// <summary>
        /// <see cref="MeshGeometry3D"/> used as prototype.
        /// </summary>
        private static readonly MeshGeometry3D SpherePrototype = SphereTesselate.Create(20, 20, NODERADIUS);

        /// <summary>
        /// The time a node was clicked for the last time.
        /// </summary>
        private static DateTime lastClickTime = DateTime.MinValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="NodeUIElement"/> class.
        /// </summary>
        /// <param name="graphProvider">The <see cref="IGraphProvider"/>.</param>
        /// <param name="node">The node.</param>
        public NodeUIElement(IGraphProvider graphProvider, Node<NodeData, EdgeData> node)
            : base(graphProvider, node.Data!)
        {
            this.Node = node;
            this.TranslateTransform = new TranslateTransform3D((Vector3D)node.Data!.Position);
            this.Transform = this.TranslateTransform;

            node.Data.NodeMoved += new System.EventHandler<NodeMovedEventArgs>(this.NodeMoved);
        }

        /// <summary>
        /// Gets the reference to the <see cref="TranslateTransform3D"/> applied to the element.
        /// </summary>
        public TranslateTransform3D TranslateTransform { get; private set; }

        /// <summary>
        /// Gets the node.
        /// </summary>
        protected Node<NodeData, EdgeData> Node { get; private set; }

        /// <summary>
        /// Executed when node is moved.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Palmmedia.WpfGraph.Ui.ViewModels.NodeMovedEventArgs"/> instance containing the event data.</param>
        protected virtual void NodeMoved(object? sender, NodeMovedEventArgs e)
        {
            if (e.Duration > 0)
            {
                var transAnimationX = new DoubleAnimation
                {
                    Duration = TimeSpan.FromMilliseconds(e.Duration),
                    From = this.TranslateTransform.OffsetX,
                    To = e.NewPosition.X,
                    FillBehavior = FillBehavior.Stop
                };

                var transAnimationY = new DoubleAnimation
                {
                    Duration = TimeSpan.FromMilliseconds(e.Duration),
                    From = this.TranslateTransform.OffsetY,
                    To = e.NewPosition.Y,
                    FillBehavior = FillBehavior.Stop
                };

                var transAnimationZ = new DoubleAnimation
                {
                    Duration = TimeSpan.FromMilliseconds(e.Duration),
                    From = this.TranslateTransform.OffsetZ,
                    To = e.NewPosition.Z,
                    FillBehavior = FillBehavior.Stop
                };

                transAnimationZ.Completed += new EventHandler((s, a) => this.ApplyPosition(e.NewPosition, e.Callback));

                this.TranslateTransform.BeginAnimation(TranslateTransform3D.OffsetXProperty, transAnimationX);
                this.TranslateTransform.BeginAnimation(TranslateTransform3D.OffsetYProperty, transAnimationY);
                this.TranslateTransform.BeginAnimation(TranslateTransform3D.OffsetZProperty, transAnimationZ);
            }
            else
            {
                this.ApplyPosition(e.NewPosition, e.Callback);
            }
        }

        /// <summary>
        /// Invoked when an unhandled <see cref="E:System.Windows.UIElement3D.MouseLeftButtonDown"/> routed event is raised on this element. Implement this method to add class handling for this event.<br/>
        /// If a node was clicked a short time before, a new edge is added between this node and the previously clicked node.
        /// Otherwise the node is selected.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Input.MouseButtonEventArgs"/> that contains the event data. The event data reports that the left mouse button was pressed.</param>
        protected override void OnMouseLeftButtonDown(System.Windows.Input.MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            var selectedElement = this.GraphProvider.SelectedElement;
            if (selectedElement != null
                && selectedElement is Node<NodeData, EdgeData>
                && (DateTime.Now - lastClickTime).TotalSeconds < 1)
            {
                var firstNode = (Node<NodeData, EdgeData>)selectedElement;

                var edge = new Edge<NodeData, EdgeData>(firstNode, this.Node);
                this.GraphProvider.Graph.Add(edge);
                this.GraphProvider.SelectedElement = edge;
            }
            else
            {
                this.GraphProvider.SelectedElement = this.Node;
            }

            lastClickTime = DateTime.Now;
        }

        /// <summary>
        /// Invoked when an unhandled <see cref="E:System.Windows.UIElement3D.MouseRightButtonDown"/> routed event reaches an element in its route that is derived from this class. Implement this method to add class handling for this event.<br/>
        /// Marks the node.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Input.MouseButtonEventArgs"/> that contains the event data. The event data reports that the right mouse button was pressed.</param>
        protected override void OnMouseRightButtonDown(System.Windows.Input.MouseButtonEventArgs e)
        {
            base.OnMouseRightButtonDown(e);

            this.Node.Data!.Marked = !this.Node.Data.Marked;
        }

        /// <summary>
        /// Participates in rendering operations when overridden in a derived class.
        /// </summary>
        protected override void OnUpdateModel()
        {
            Brush? brush;

            if (this.Node.Data!.Marked)
            {
                brush = new RadialGradientBrush(Colors.Orange, this.Color);
            }
            else
            {
                brush = new SolidColorBrush(this.Color);
            }

            var model = new GeometryModel3D(
                SpherePrototype,
                new DiffuseMaterial(brush));

            this.Model = model;
        }

        /// <summary>
        /// Applies the position and executes the given callback.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="callback">The callback.</param>
        private void ApplyPosition(Point3D position, Action? callback)
        {
            this.TranslateTransform.OffsetX = position.X;
            this.TranslateTransform.OffsetY = position.Y;
            this.TranslateTransform.OffsetZ = position.Z;

            callback?.Invoke();
        }
    }
}
