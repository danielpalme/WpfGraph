using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using Palmmedia.WpfGraph.Core;
using Palmmedia.WpfGraph.UI.Elements3D.Tesselate;
using Palmmedia.WpfGraph.UI.Interaction;
using Palmmedia.WpfGraph.UI.ViewModels;

namespace Palmmedia.WpfGraph.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// The IGraphProvider.
        /// </summary>
        private IGraphProvider graphProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        /// <param name="graphProvider">The <see cref="IGraphProvider"/>.</param>
        public MainWindow(IGraphProvider graphProvider)
        {
            this.InitializeComponent();

            this.graphProvider = graphProvider;
        }

        /// <summary>
        /// Invoked when an unhandled <see cref="E:System.Windows.Input.Keyboard.KeyUp"/> attached event reaches an element in its route that is derived from this class. Implement this method to add class handling for this event.<br/>
        /// Deletes the currently selected node or edge from the graph if 'del' is pressed.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Input.KeyEventArgs"/> that contains the event data.</param>
        protected override void OnKeyUp(System.Windows.Input.KeyEventArgs e)
        {
            base.OnKeyUp(e);

            if (e.Key == System.Windows.Input.Key.Delete && !(e.OriginalSource is TextBox))
            {
                var selectedElement = this.graphProvider.SelectedElement;

                if (selectedElement != null)
                {
                    if (selectedElement as Edge<NodeData, EdgeData> != null)
                    {
                        this.graphProvider.Graph.Remove((Edge<NodeData, EdgeData>)selectedElement);
                    }
                    else if (selectedElement is Node<NodeData, EdgeData>)
                    {
                        this.graphProvider.Graph.Remove((Node<NodeData, EdgeData>)selectedElement);
                    }
                }
            }
        }

        /// <summary>
        /// Invoked when an unhandled <see cref="E:System.Windows.Input.Mouse.MouseWheel"/> attached event reaches an element in its route that is derived from this class. Implement this method to add class handling for this event.<br/>
        /// Zooms the camera.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Input.MouseWheelEventArgs"/> that contains the event data.</param>
        protected override void OnMouseWheel(System.Windows.Input.MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);

            this.slider.Value += e.Delta / 30;
        }

        /// <summary>
        /// If an empty postion of the viewport is hit, a new node is added to the graph.
        /// The postion of the node needs to be calculated depending on the position and field of view of the camera.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void OnViewportMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Calulate position of click in coordinate system of viewport 
            var point = e.GetPosition(this.viewport3D);
            point.Y = -point.Y;
            point.Offset(-this.viewport3D.ActualWidth / 2, this.viewport3D.ActualHeight / 2);

            // Calculate position in viewport according to distance/zoom of camera
            double widthOfCameraViewport = 2 * this.camera.Position.Z * Math.Tan(MathHelper.DegToRad(this.camera.FieldOfView / 2));
            double scale = widthOfCameraViewport / this.viewport3D.ActualWidth;
            point.X = Math.Round(scale * point.X);
            point.Y = Math.Round(scale * point.Y);

            // Add new node to graph
            var nodeData = new NodeData(new Point3D(point.X, point.Y, 0));
            var node = new Node<NodeData, EdgeData>(nodeData);

            this.graphProvider.Graph.Add(node);
            this.graphProvider.SelectedElement = node;
        }
    }
}
