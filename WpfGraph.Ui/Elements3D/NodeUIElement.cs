using System;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
using Palmmedia.WpfGraph.Common;
using Palmmedia.WpfGraph.Core;
using Palmmedia.WpfGraph.UI.Elements3D.Tesselate;
using Palmmedia.WpfGraph.UI.Interaction;
using Palmmedia.WpfGraph.UI.ViewModels;

namespace Palmmedia.WpfGraph.UI.Elements3D
{
    /// <summary>
    /// Represents a node.
    /// </summary>
    public class NodeUIElement : GraphUIElement
    {
        /// <summary>
        /// <see cref="MeshGeometry3D"/> used as prototype.
        /// </summary>
        private static readonly MeshGeometry3D spherePrototype = SphereTesselate.Create(20, 20, NODERADIUS);

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
            : base(graphProvider, node.Data)
        {
            this.Node = node;
            this.TranslateTransform = new TranslateTransform3D((Vector3D)node.Data.Position);
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
        /// <param name="e">The <see cref="Palmmedia.WpfGraph.UI.ViewModels.NodeMovedEventArgs"/> instance containing the event data.</param>
        protected virtual void NodeMoved(object sender, NodeMovedEventArgs e)
        {
            if (e.Duration > 0)
            {
                var transAnimationX = new DoubleAnimation();
                transAnimationX.Duration = TimeSpan.FromMilliseconds(e.Duration);
                transAnimationX.From = this.TranslateTransform.OffsetX;
                transAnimationX.To = e.NewPosition.X;
                transAnimationX.FillBehavior = FillBehavior.Stop;

                var transAnimationY = new DoubleAnimation();
                transAnimationY.Duration = TimeSpan.FromMilliseconds(e.Duration);
                transAnimationY.From = this.TranslateTransform.OffsetY;
                transAnimationY.To = e.NewPosition.Y;
                transAnimationY.FillBehavior = FillBehavior.Stop;

                var transAnimationZ = new DoubleAnimation();
                transAnimationZ.Duration = TimeSpan.FromMilliseconds(e.Duration);
                transAnimationZ.From = this.TranslateTransform.OffsetZ;
                transAnimationZ.To = e.NewPosition.Z;
                transAnimationZ.FillBehavior = FillBehavior.Stop;

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

            this.Node.Data.Marked = !this.Node.Data.Marked;
        }

        /// <summary>
        /// Participates in rendering operations when overridden in a derived class.
        /// </summary>
        protected override void OnUpdateModel()
        {
            Brush brush = null;

            if (this.Node.Data.Marked)
            {
                brush = new RadialGradientBrush(Colors.Orange, this.Color);
            }
            else
            {
                brush = new SolidColorBrush(this.Color);
            }

            var model = new GeometryModel3D(
                spherePrototype,
                new DiffuseMaterial(brush));

            this.Model = model;
        }

        /// <summary>
        /// Applies the position and executes the given callback.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="callback">The callback.</param>
        private void ApplyPosition(Point3D position, Action callback)
        {
            this.TranslateTransform.OffsetX = position.X;
            this.TranslateTransform.OffsetY = position.Y;
            this.TranslateTransform.OffsetZ = position.Z;

            if (callback != null)
            {
                callback();
            }
        }
    }
}
