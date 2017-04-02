using System.Collections.Generic;
using System.Linq;
using Palmmedia.WpfGraph.Common;
using Palmmedia.WpfGraph.UI.ViewModels;

namespace Palmmedia.WpfGraph.Core
{
    /// <summary>
    /// Extension methods to simplify adding nodes and edges to a graph.
    /// </summary>
    internal static class GraphExtensions
    {
        /// <summary>
        /// Adds a node to the graph.
        /// </summary>
        /// <param name="graph">The graph.</param>
        /// <returns>The added node.</returns>
        public static Node<NodeData, EdgeData> AddNode(this IGraph<NodeData, EdgeData> graph)
        {
            var node = new Node<NodeData, EdgeData>();
            graph.Add(node);

            return node;
        }

        /// <summary>
        /// Adds a node to the graph.
        /// </summary>
        /// <param name="graph">The graph.</param>
        /// <param name="nodeData">The node data.</param>
        /// <returns>The added node.</returns>
        public static Node<NodeData, EdgeData> AddNode(this IGraph<NodeData, EdgeData> graph, NodeData nodeData)
        {
            var node = new Node<NodeData, EdgeData>(nodeData);
            graph.Add(node);

            return node;
        }

        /// <summary>
        /// Adds an edge to the graph.
        /// </summary>
        /// <param name="graph">The graph.</param>
        /// <param name="firstNode">The first node.</param>
        /// <param name="secondNode">The second node.</param>
        /// <returns>The added edge.</returns>
        public static Edge<NodeData, EdgeData> AddEdge(this IGraph<NodeData, EdgeData> graph, Node<NodeData, EdgeData> firstNode, Node<NodeData, EdgeData> secondNode)
        {
            var edge = new Edge<NodeData, EdgeData>(firstNode, secondNode); 
            graph.Add(edge);

            return edge;
        }

        /// <summary>
        /// Adds an edge to the graph.
        /// </summary>
        /// <param name="graph">The graph.</param>
        /// <param name="firstNode">The first node.</param>
        /// <param name="secondNode">The second node.</param>
        /// <param name="edgeDirection">The edge direction.</param>
        /// <returns>The added edge.</returns>
        public static Edge<NodeData, EdgeData> AddEdge(this IGraph<NodeData, EdgeData> graph, Node<NodeData, EdgeData> firstNode, Node<NodeData, EdgeData> secondNode, EdgeDirection edgeDirection)
        {
            var edge = new Edge<NodeData, EdgeData>(firstNode, secondNode, edgeDirection);
            graph.Add(edge);

            return edge;
        }

        /// <summary>
        /// Adds an edge to the graph.
        /// </summary>
        /// <param name="graph">The graph.</param>
        /// <param name="firstNode">The first node.</param>
        /// <param name="secondNode">The second node.</param>
        /// <param name="edgeData">The data attached to the edge.</param>
        /// <returns>The added edge.</returns>
        public static Edge<NodeData, EdgeData> AddEdge(this IGraph<NodeData, EdgeData> graph, Node<NodeData, EdgeData> firstNode, Node<NodeData, EdgeData> secondNode, EdgeData edgeData)
        {
            var edge = new Edge<NodeData, EdgeData>(firstNode, secondNode, edgeData);
            graph.Add(edge);

            return edge;
        }

        /// <summary>
        /// Adds an edge to the graph.
        /// </summary>
        /// <param name="graph">The graph.</param>
        /// <param name="firstNode">The first node.</param>
        /// <param name="secondNode">The second node.</param>
        /// <param name="edgeDirection">The edge direction.</param>
        /// <param name="edgeData">The data attached to the edge.</param>
        /// <returns>The added edge.</returns>
        public static Edge<NodeData, EdgeData> AddEdge(this IGraph<NodeData, EdgeData> graph, Node<NodeData, EdgeData> firstNode, Node<NodeData, EdgeData> secondNode, EdgeDirection edgeDirection, EdgeData edgeData)
        {
            var edge = new Edge<NodeData, EdgeData>(firstNode, secondNode, edgeDirection, edgeData);
            graph.Add(edge);

            return edge;
        }

        /// <summary>
        /// Calculates the number of components within the graph.
        /// </summary>
        /// <param name="graph">The graph.</param>
        /// <returns>The number of components.</returns>
        public static int NumberOfComponents(this IGraph<NodeData, EdgeData> graph)
        {
            int numberOfComponents = 0;

            var nodes = graph.Nodes.ToHashSet();

            var currentNode = nodes.FirstOrDefault();

            while (currentNode != null)
            {
                var visitedNodes = new HashSet<Node<NodeData, EdgeData>>();
                visitedNodes.Add(currentNode);
                var reachableNodes = ReachableNodes(currentNode, visitedNodes);

                foreach (var node in reachableNodes)
                {
                    nodes.Remove(node);
                }

                currentNode = nodes.FirstOrDefault();
                numberOfComponents++;
            }

            return numberOfComponents;
        }

        /// <summary>
        /// Returns all reachables nodes starting from the given node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="visitedNodes">The nodes that are already visited.</param>
        /// <returns>All reachables nodes.</returns>
        private static HashSet<Node<NodeData, EdgeData>> ReachableNodes(Node<NodeData, EdgeData> node, HashSet<Node<NodeData, EdgeData>> visitedNodes)
        {
            foreach (var currentNode in node.Neighbors)
            {
                if (!visitedNodes.Contains(currentNode))
                {
                    visitedNodes.Add(currentNode);
                    ReachableNodes(currentNode, visitedNodes);
                }
            }

            return visitedNodes;
        }
    }
}