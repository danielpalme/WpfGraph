using System;
using System.Collections.Generic;
using System.Linq;

namespace Palmmedia.WpfGraph.Core
{
    /// <summary>
    /// Implementation of <see cref="IGraph&lt;TNodeType, TEdgeType&gt;"/> using hashsets to manage nodes and edges of the graph.<br/>
    /// If you need your own implemetation of <see cref="IGraph&lt;TNodeType, TEdgeType&gt;"/> (e.g. if you want to keep edges of a node in a dictionary for faster access) your should either derive from this class or inherit from <see cref="GraphBase&lt;TNodeType, TEdgeType&gt;"/>.
    /// </summary>
    /// <typeparam name="TNodeType">The type of the data attached to a <see cref="Palmmedia.WpfGraph.Core.Node&lt;TNodeType, TEdgeType&gt;"/>.</typeparam>
    /// <typeparam name="TEdgeType">The type of the data attached to an <see cref="Palmmedia.WpfGraph.Core.Edge&lt;TNodeType, TEdgeType&gt;"/>.</typeparam>
    public class Graph<TNodeType, TEdgeType>
        : GraphBase<TNodeType, TEdgeType>, IGraph<TNodeType, TEdgeType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Graph&lt;TNodeType, TEdgeType&gt;"/> class.
        /// </summary>
        public Graph()
        {
            this.EdgeSet = new HashSet<Edge<TNodeType, TEdgeType>>();
            this.NodeSet = new HashSet<Node<TNodeType, TEdgeType>>();
        }

        /// <summary>
        /// Gets the <see cref="Edge&lt;TNodeType, TEdgeType&gt;">edges</see>.
        /// </summary>
        /// <value></value>
        public IEnumerable<Edge<TNodeType, TEdgeType>> Edges
        {
            get
            {
                return this.EdgeSet;
            }
        }

        /// <summary>
        /// Gets the <see cref="Node&lt;TNodeType, TEdgeType&gt;">nodes</see>.
        /// </summary>
        /// <value></value>
        public IEnumerable<Node<TNodeType, TEdgeType>> Nodes
        {
            get
            {
                return this.NodeSet;
            }
        }

        /// <summary>
        /// Gets the set containing all <see cref="Edge&lt;TNodeType, TEdgeType&gt;">edges</see>.
        /// </summary>
        protected HashSet<Edge<TNodeType, TEdgeType>> EdgeSet { get; private set; }

        /// <summary>
        /// Gets the set containing all <see cref="Node&lt;TNodeType, TEdgeType&gt;">nodes</see>.
        /// </summary>
        protected HashSet<Node<TNodeType, TEdgeType>> NodeSet { get; private set; }

        /// <summary>
        /// Adds the given <see cref="Edge&lt;TNodeType, TEdgeType&gt;"/>.
        /// </summary>
        /// <param name="edge">The <see cref="Edge&lt;TNodeType, TEdgeType&gt;"/> to add.</param>
        public virtual void Add(Edge<TNodeType, TEdgeType> edge)
        {
            if (edge == null)
            {
                throw new ArgumentNullException("edge");
            }

            if (!this.NodeSet.Contains(edge.FirstNode) || !this.NodeSet.Contains(edge.SecondNode))
            {
                throw new InvalidOperationException("The graph does not contain one of the nodes the edge connects. Add the nodes to the graph before adding the edge.");
            }

            edge.Graph = this;
            this.EdgeSet.Add(edge);
            this.OnEdgeAdded(new EdgeEventArgs<TNodeType, TEdgeType>(edge));
        }

        /// <summary>
        /// Adds the given <see cref="Node&lt;TNodeType, TEdgeType&gt;"/>.
        /// </summary>
        /// <param name="node">The <see cref="Node&lt;TNodeType, TEdgeType&gt;"/> to add.</param>
        public virtual void Add(Node<TNodeType, TEdgeType> node)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            node.Graph = this;
            this.NodeSet.Add(node);
            this.OnNodeAdded(new NodeEventArgs<TNodeType, TEdgeType>(node));
        }

        /// <summary>
        /// Removes the given <see cref="Edge&lt;TNodeType, TEdgeType&gt;"/>.
        /// </summary>
        /// <param name="edge">The <see cref="Edge&lt;TNodeType, TEdgeType&gt;"/> to remove.</param>
        public virtual void Remove(Edge<TNodeType, TEdgeType> edge)
        {
            if (edge == null)
            {
                throw new ArgumentNullException("edge");
            }

            this.EdgeSet.Remove(edge);
            this.OnEdgeRemoved(new EdgeEventArgs<TNodeType, TEdgeType>(edge));
        }

        /// <summary>
        /// Removes the given <see cref="Node&lt;TNodeType, TEdgeType&gt;"/>.
        /// </summary>
        /// <param name="node">The <see cref="Node&lt;TNodeType, TEdgeType&gt;"/> to remove.</param>
        public virtual void Remove(Node<TNodeType, TEdgeType> node)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            foreach (var edge in this.EdgeSet.Where(e => e.FirstNode == node || e.SecondNode == node).ToArray())
            {
                this.Remove(edge);
            }

            this.NodeSet.Remove(node);
            this.OnNodeRemoved(new NodeEventArgs<TNodeType, TEdgeType>(node));
        }

        /// <summary>
        /// Removes all <see cref="Node&lt;TNodeType, TEdgeType&gt;">nodes</see> and <see cref="Edge&lt;TNodeType, TEdgeType&gt;">edges</see>.
        /// </summary>
        public virtual void Clear()
        {
            foreach (var edge in this.EdgeSet.ToArray())
            {
                this.Remove(edge);
            }

            foreach (var node in this.NodeSet.ToArray())
            {
                this.Remove(node);
            }
        }

        /// <summary>
        /// Gets all <see cref="Edge&lt;TNodeType, TEdgeType&gt;">edges</see> of the <see cref="Node&lt;TNodeType, TEdgeType&gt;"/>.
        /// </summary>
        /// <param name="node">The <see cref="Node&lt;TNodeType, TEdgeType&gt;"/>.</param>
        /// <returns>
        /// All <see cref="Edge&lt;TNodeType, TEdgeType&gt;">edges</see>.
        /// </returns>
        public virtual IEnumerable<Edge<TNodeType, TEdgeType>> GetEdgesOfNode(Node<TNodeType, TEdgeType> node)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            return this.EdgeSet.Where(e => e.FirstNode == node || e.SecondNode == node);
        }

        /// <summary>
        /// Gets the incoming <see cref="Edge&lt;TNodeType, TEdgeType&gt;">edges</see> of the <see cref="Node&lt;TNodeType, TEdgeType&gt;"/>.
        /// </summary>
        /// <param name="node">The <see cref="Node&lt;TNodeType, TEdgeType&gt;"/>.</param>
        /// <returns>
        /// All incoming <see cref="Edge&lt;TNodeType, TEdgeType&gt;">edges</see>.
        /// </returns>
        public virtual IEnumerable<Edge<TNodeType, TEdgeType>> GetIncomingEdgesOfNode(Node<TNodeType, TEdgeType> node)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            return this.EdgeSet.Where(e => (e.FirstNode == node && e.EdgeDirection != EdgeDirection.First2Second)
                || (e.SecondNode == node && e.EdgeDirection != EdgeDirection.Second2First));           
        }

        /// <summary>
        /// Gets the outgoing <see cref="Edge&lt;TNodeType, TEdgeType&gt;">edges</see> of the <see cref="Node&lt;TNodeType, TEdgeType&gt;"/>.
        /// </summary>
        /// <param name="node">The <see cref="Node&lt;TNodeType, TEdgeType&gt;"/>.</param>
        /// <returns>
        /// All outgoing <see cref="Edge&lt;TNodeType, TEdgeType&gt;">edges</see>.
        /// </returns>
        public virtual IEnumerable<Edge<TNodeType, TEdgeType>> GetOutgoingEdgesOfNode(Node<TNodeType, TEdgeType> node)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }
            
            return this.EdgeSet.Where(e => (e.FirstNode == node && e.EdgeDirection != EdgeDirection.Second2First)
                || (e.SecondNode == node && e.EdgeDirection != EdgeDirection.First2Second));           
        }

        /// <summary>
        /// Gets all <see cref="Node&lt;TNodeType, TEdgeType&gt;">nodes</see> connected to the <see cref="Node&lt;TNodeType, TEdgeType&gt;"/>.
        /// </summary>
        /// <param name="node">The <see cref="Node&lt;TNodeType, TEdgeType&gt;"/>.</param>
        /// <returns>
        /// All connected <see cref="Node&lt;TNodeType, TEdgeType&gt;">nodes</see>.
        /// </returns>
        public virtual IEnumerable<Node<TNodeType, TEdgeType>> GetNeighborsOfNode(Node<TNodeType, TEdgeType> node)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            var edges = this.GetEdgesOfNode(node);
            var neighbors = edges.Select(e => e.FirstNode == node ? e.SecondNode : e.FirstNode);
            return neighbors.Distinct();
        }

        /// <summary>
        /// Gets all <see cref="Node&lt;TNodeType, TEdgeType&gt;">nodes</see> connected to the <see cref="Node&lt;TNodeType, TEdgeType&gt;"/> by an incoming <see cref="Edge&lt;TNodeType, TEdgeType&gt;"/>.
        /// </summary>
        /// <param name="node">The <see cref="Node&lt;TNodeType, TEdgeType&gt;"/>.</param>
        /// <returns>
        /// All <see cref="Node&lt;TNodeType, TEdgeType&gt;">nodes</see> connected by an incoming <see cref="Edge&lt;TNodeType, TEdgeType&gt;"/>.
        /// </returns>
        public virtual IEnumerable<Node<TNodeType, TEdgeType>> GetIncomingNeighborsOfNode(Node<TNodeType, TEdgeType> node)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            var edges = this.GetIncomingEdgesOfNode(node);
            var neighbors = edges.Select(e => e.FirstNode == node ? e.SecondNode : e.FirstNode);
            return neighbors.Distinct();
        }

        /// <summary>
        /// Gets all <see cref="Node&lt;TNodeType, TEdgeType&gt;">nodes</see> connected to the <see cref="Node&lt;TNodeType, TEdgeType&gt;"/> by an outgoing <see cref="Edge&lt;TNodeType, TEdgeType&gt;"/>.
        /// </summary>
        /// <param name="node">The <see cref="Node&lt;TNodeType, TEdgeType&gt;"/>.</param>
        /// <returns>
        /// All <see cref="Node&lt;TNodeType, TEdgeType&gt;">nodes</see> connected by an outgoing <see cref="Edge&lt;TNodeType, TEdgeType&gt;"/>.
        /// </returns>
        public virtual IEnumerable<Node<TNodeType, TEdgeType>> GetOutgoingNeighborsOfNode(Node<TNodeType, TEdgeType> node)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            var edges = this.GetOutgoingEdgesOfNode(node);
            var neighbors = edges.Select(e => e.FirstNode == node ? e.SecondNode : e.FirstNode);
            return neighbors.Distinct();
        }

        /// <summary>
        /// Gibt einen <see cref="T:System.String"></see> zurück, der den aktuellen <see cref="T:System.Object"></see> darstellt.
        /// </summary>
        /// <returns>
        /// Ein <see cref="T:System.String"></see>, der den aktuellen <see cref="T:System.Object"></see> darstellt.
        /// </returns>
        public override string ToString()
        {
            return "Nodes: " + this.NodeSet.Count + ", Edges: " + this.EdgeSet.Count;
        }
    }
}