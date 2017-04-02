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
    /// The Kruskal algorithm.
    /// en.wikipedia.org/wiki/Kruskal's_algorithm
    /// </summary>
    public class Kruskal : IGraphAlgorithm
    {
        /// <summary>
        /// The name of the algorithm.
        /// </summary>
        private const string NAME = "Kruskal";

        /// <summary>
        /// The graph.
        /// </summary>
        private IGraph<NodeData, EdgeData> graph;

        /// <summary>
        /// The edges of the spanning tree.
        /// </summary>
        private HashSet<Edge<NodeData, EdgeData>> spanningTreeEdges = new HashSet<Edge<NodeData, EdgeData>>();

        /// <summary>
        /// The edges that are not processed yet.
        /// </summary>
        private Queue<Edge<NodeData, EdgeData>> edgeQueue;

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

            // Add edges to queue (sorted by weight)
            this.edgeQueue = graph.Edges.OrderBy(e => e.Data.Weight).ToQueue();
            
            this.ProcessNextEdge();
        }

        /// <summary>
        /// Processes the next edge.
        /// </summary>
        private void ProcessNextEdge()
        {
            if (this.edgeQueue.Count > 0)
            {
                var edge = this.edgeQueue.Dequeue();
                edge.Blink(() => this.RemoveOrKeepEdge(edge));
            }
        }

        /// <summary>
        /// Removes the edge if a circle would be created.
        /// </summary>
        /// <param name="edge">The edge.</param>
        private void RemoveOrKeepEdge(Edge<NodeData, EdgeData> edge)
        {
            if (edge.FirstNode != edge.SecondNode && !this.DoesPathExist(edge.FirstNode, edge.SecondNode, edge))
            {
                this.spanningTreeEdges.Add(edge);
                edge.ChangeColor(Colors.SteelBlue);
                edge.FirstNode.ChangeColor(Colors.SteelBlue);
                edge.SecondNode.ChangeColor(Colors.SteelBlue);
            }
            else
            {
                this.graph.Remove(edge);
            }

            this.ProcessNextEdge();
        }

        /// <summary>
        /// Determines whether a path between the <paramref name="currentNode"/> and the <paramref name="targetNode"/> exists.
        /// </summary>
        /// <param name="currentNode">The current node.</param>
        /// <param name="targetNode">The target node.</param>
        /// <param name="edgeToExclude">The edge to exclude.</param>
        /// <returns><c>True</c> if a path exists, otherwise <c>false</c>.</returns>
        private bool DoesPathExist(Node<NodeData, EdgeData> currentNode, Node<NodeData, EdgeData> targetNode, Edge<NodeData, EdgeData> edgeToExclude)
        {
            foreach (var currentEdge in currentNode.Edges.Where(e => this.spanningTreeEdges.Contains(e) && e != edgeToExclude))
            {
                if (currentEdge.FirstNode == targetNode || currentEdge.SecondNode == targetNode)
                {
                    return true;
                }
                else
                {
                    var nodeToContinue = currentEdge.FirstNode == currentNode ? currentEdge.SecondNode : currentEdge.FirstNode;
                    bool doesPathExist = this.DoesPathExist(nodeToContinue, targetNode, currentEdge);
                    if (doesPathExist)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
