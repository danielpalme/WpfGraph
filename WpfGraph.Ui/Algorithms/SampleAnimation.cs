using System.Windows.Media.Media3D;
using Palmmedia.WpfGraph.Core;
using Palmmedia.WpfGraph.UI.ViewModels;

namespace Palmmedia.WpfGraph.UI.Algorithms.SpanningTree
{
    /// <summary>
    /// A sample animation.
    /// </summary>
    public class SampleAnimation : IGraphAlgorithm
    {
        /// <summary>
        /// The name of the algorithm.
        /// </summary>
        private const string NAME = "Dijkstra";

        /// <summary>
        /// Dictionary containing the distance to each node.
        /// </summary>
        private Node<NodeData, EdgeData> node1, node3;

        /// <summary>
        /// The graph.
        /// </summary>
        private IGraph<NodeData, EdgeData> graph;

        /// <summary>
        /// Gets the name of the algorithm.
        /// </summary>
        public string Name
        {
            get
            {
                return Properties.Resources.SampleAnimation;
            }
        }

        /// <summary>
        /// Gets the category of the algorithm.
        /// The category is used to generate a menu entry.
        /// Return <c>null</c> if algorithm should appear in root menu.
        /// </summary>
        public string Category
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// Executes the algorithm.
        /// </summary>
        /// <param name="graph">The graph.</param>
        /// <exception cref="System.InvalidOperationException">Thrown if graph does not meet special demands required by the graph algorithm.</exception>
        public void Execute(IGraph<NodeData, EdgeData> graph)
        {
            graph.Clear();
            this.graph = graph;

            this.node1 = graph.AddNode(new NodeData(new Point3D(25, 25, 0)));
            var node2 = graph.AddNode(new NodeData(new Point3D(25, -25, 0)));
            this.node3 = graph.AddNode(new NodeData(new Point3D(-25, 25, 0)));
            var node4 = graph.AddNode(new NodeData(new Point3D(-25, -25, 0)));

            this.graph.AddEdge(this.node1, node4);
            this.graph.AddEdge(node2, this.node3);

            this.node1.Move(new Point3D(35, -35, 0), 3000);
            node4.Move(new Point3D(35, 35, 0), 3000);
            node2.Move(new Point3D(-35, 35, 0), 3000);
            this.node3.Move(new Point3D(35, -35, 0), 3000, this.Callback1);
        }

        /// <summary>
        /// Callback 1.
        /// </summary>
        private void Callback1()
        {
            this.node1.Blink();
            this.node3.Blink(this.Callback2);
        }

        /// <summary>
        /// Callback 2.
        /// </summary>
        private void Callback2()
        {
            this.graph.AddEdge(this.node1, this.node3);
            this.node1.Move(new Point3D(10, 0, 20), 3000);
        }
    }
}