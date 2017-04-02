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
    /// The Dijkstra algorithm.
    /// en.wikipedia.org/wiki/Dijkstra's_algorithm
    /// </summary>
    public class Dijkstra : IGraphAlgorithm
    {
        /// <summary>
        /// The name of the algorithm.
        /// </summary>
        private const string NAME = "Dijkstra";

        /// <summary>
        /// Dictionary containing the distance to each node.
        /// </summary>
        private Dictionary<Node<NodeData, EdgeData>, double> node2DistanceDictionary;

        /// <summary>
        /// The unvisited nodes.
        /// </summary>
        private HashSet<Node<NodeData, EdgeData>> unvisitedNodes;

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
            // Weight of edges must be positive
            if (graph.Edges.Count(e => e.Data.Weight <= 0) > 0)
            {
                throw new InvalidOperationException(GraphAlgorithmErrors.EdgeWeightsNegative);
            }

            var markedNodes = graph.Nodes.Where(n => n.Data.Marked);

            // One node must be marked
            if (markedNodes.Count() != 1)
            {
                throw new InvalidOperationException(GraphAlgorithmErrors.OneNodeMarked);
            }

            this.unvisitedNodes = graph.Nodes.ToHashSet();

            // Initialize distance with 'infinity'
            this.node2DistanceDictionary = graph.Nodes.ToDictionary(n => n, n => double.MaxValue);

            // Start algorithm at marked node
            var nextNode = markedNodes.First();
            this.node2DistanceDictionary[nextNode] = 0;

            nextNode.Blink(() => this.ProcessNode(nextNode));
        }

        /// <summary>
        /// Processes the next node.
        /// </summary>
        /// <param name="currentNode">The node.</param>
        private void ProcessNode(Node<NodeData, EdgeData> currentNode)
        {
            currentNode.ChangeColor(Colors.SteelBlue);

            double currentWeight = this.node2DistanceDictionary[currentNode];

            foreach (var edge in currentNode.OutgoingEdges.Where(e => e.FirstNode != e.SecondNode))
	        {
                var targetNode = edge.FirstNode == currentNode ? edge.SecondNode : edge.FirstNode;

                double newWeight = currentWeight + edge.Data.Weight;

                // If edge has lower distance to target node, then update to new distance
                if (newWeight < this.node2DistanceDictionary[targetNode])
                {
                    edge.ChangeColor(Colors.SteelBlue);
                    this.node2DistanceDictionary[targetNode] = newWeight;
                }
	        }

            this.unvisitedNodes.Remove(currentNode);

            // Continue with unvisited node with the minimum distance
            var nextNode = this.unvisitedNodes.OrderBy(n => this.node2DistanceDictionary[n]).FirstOrDefault();

            if (nextNode != null)
            {
                nextNode.Blink(() => this.ProcessNode(nextNode));
            }
        }
    }
}