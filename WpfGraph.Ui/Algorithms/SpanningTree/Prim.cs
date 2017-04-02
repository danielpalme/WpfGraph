using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using Palmmedia.WpfGraph.Common;
using Palmmedia.WpfGraph.Core;
using Palmmedia.WpfGraph.UI.Properties;
using Palmmedia.WpfGraph.UI.ViewModels;

namespace Palmmedia.WpfGraph.UI.Algorithms.SpanningTree
{
    /// <summary>
    /// The Prim algorithm.
    /// en.wikipedia.org/wiki/Prim's_algorithm
    /// </summary>
    public class Prim : IGraphAlgorithm
    {
        /// <summary>
        /// The name of the algorithm.
        /// </summary>
        private const string NAME = "Prim";

        /// <summary>
        /// The graph.
        /// </summary>
        private IGraph<NodeData, EdgeData> graph;

        /// <summary>
        /// Total number of nodes in graph.
        /// </summary>
        private int totalNumberOfNodes;

        /// <summary>
        /// The visited nodes.
        /// </summary>
        private HashSet<Node<NodeData, EdgeData>> visitedNodes = new HashSet<Node<NodeData, EdgeData>>();

        /// <summary>
        /// The edges that are not processed yet.
        /// </summary>
        private HashSet<Edge<NodeData, EdgeData>> unprocessedEdges;

        /// <summary>
        /// Gets the name of the algorithm.
        /// </summary>
        public string Name
        {
            get
            {
                return NAME;
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
                return Properties.Resources.SpanningTree;
            }
        }

        /// <summary>
        /// Executes the algorithm.
        /// </summary>
        /// <param name="graph">The graph.</param>
        /// <exception cref="System.InvalidOperationException">Thrown if graph does not meet special demands required by the graph algorithm.</exception>
        public void Execute(IGraph<NodeData, EdgeData> graph)
        {
            // Graph must not contain any undirected edges
            if (graph.Edges.Count(e => e.EdgeDirection != EdgeDirection.OmniDirectional) > 0)
            {
                throw new InvalidOperationException(GraphAlgorithmErrors.GraphContainsDirectedEdges);
            }

            // Weight of edges must be positive
            if (graph.Edges.Count(e => e.Data.Weight <= 0) > 0)
            {
                throw new InvalidOperationException(GraphAlgorithmErrors.EdgeWeightsNegative);
            }

            // Only one component allowed
            if (graph.NumberOfComponents() > 1)
            {
                throw new InvalidOperationException(GraphAlgorithmErrors.GraphSeveralComponents);
            }

            this.graph = graph;
            this.totalNumberOfNodes = graph.Nodes.Count();
            this.unprocessedEdges = graph.Edges.ToHashSet();

            var firstNode = this.graph.Nodes.FirstOrDefault();

            if (firstNode != null)
            {
                this.visitedNodes.Add(firstNode);
            }

            this.ProcessNextEdge();
        }

        /// <summary>
        /// Processes the next edge.
        /// </summary>
        private void ProcessNextEdge()
        {
            if (this.visitedNodes.Count < this.totalNumberOfNodes)
            {
                var edge = this.unprocessedEdges.Where(e => (this.visitedNodes.Contains(e.FirstNode) && !this.visitedNodes.Contains(e.SecondNode)) 
                    || (!this.visitedNodes.Contains(e.FirstNode) && this.visitedNodes.Contains(e.SecondNode))).OrderBy(e => e.Data.Weight).First();

                edge.Blink(() => this.MarkEdge(edge));
            }
            else
            {
                foreach (var edge in this.unprocessedEdges)
                {
                    this.graph.Remove(edge);
                }
            }
        }

        /// <summary>
        /// Marks the given edge.
        /// </summary>
        /// <param name="edge">The edge.</param>
        private void MarkEdge(Edge<NodeData, EdgeData> edge)
        {
            this.visitedNodes.Add(edge.FirstNode);
            this.visitedNodes.Add(edge.SecondNode);
            this.unprocessedEdges.Remove(edge);

            edge.ChangeColor(Colors.SteelBlue);
            edge.FirstNode.ChangeColor(Colors.SteelBlue);
            edge.SecondNode.ChangeColor(Colors.SteelBlue);

            this.ProcessNextEdge();
        }
    }
}
