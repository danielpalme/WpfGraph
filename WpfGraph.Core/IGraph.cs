using System;
using System.Collections.Generic;

namespace Palmmedia.WpfGraph.Core
{
    /// <summary>
    /// Represents a graph. A graph consists of <see cref="Node&lt;TNodeType, TEdgeType&gt;">nodes</see> and <see cref="Edge&lt;TNodeType, TEdgeType&gt;">edges</see>.
    /// <br/>
    /// </summary>
    /// <typeparam name="TNodeType">The type of the data attached to a <see cref="Palmmedia.WpfGraph.Core.Node&lt;TNodeType, TEdgeType&gt;"/>.</typeparam>
    /// <typeparam name="TEdgeType">The type of the data attached to an <see cref="Palmmedia.WpfGraph.Core.Edge&lt;TNodeType, TEdgeType&gt;"/>.</typeparam>
    public interface IGraph<TNodeType, TEdgeType>
    {
        /// <summary>
        /// Occurs after an <see cref="Edge&lt;TNodeType, TEdgeType&gt;"/> has been added.
        /// </summary>
        event EventHandler<EdgeEventArgs<TNodeType, TEdgeType>> EdgeAdded;

        /// <summary>
        /// Occurs after an <see cref="Edge&lt;TNodeType, TEdgeType&gt;"/> has been removed.
        /// </summary>
        event EventHandler<EdgeEventArgs<TNodeType, TEdgeType>> EdgeRemoved;

        /// <summary>
        /// Occurs after a <see cref="Node&lt;TNodeType, TEdgeType&gt;"/> has been added.
        /// </summary>
        event EventHandler<NodeEventArgs<TNodeType, TEdgeType>> NodeAdded;

        /// <summary>
        /// Occurs after a <see cref="Node&lt;TNodeType, TEdgeType&gt;"/> has been removed.
        /// </summary>
        event EventHandler<NodeEventArgs<TNodeType, TEdgeType>> NodeRemoved;

        /// <summary>
        /// Gets the <see cref="Edge&lt;TNodeType, TEdgeType&gt;">edges</see>.
        /// </summary>
        IEnumerable<Edge<TNodeType, TEdgeType>> Edges { get; }

        /// <summary>
        /// Gets the <see cref="Node&lt;TNodeType, TEdgeType&gt;">nodes</see>.
        /// </summary>
        IEnumerable<Node<TNodeType, TEdgeType>> Nodes { get; }

        /// <summary>
        /// Adds the given <see cref="Edge&lt;TNodeType, TEdgeType&gt;"/>.
        /// </summary>
        /// <param name="edge">The <see cref="Edge&lt;TNodeType, TEdgeType&gt;"/> to add.</param>
        void Add(Edge<TNodeType, TEdgeType> edge);

        /// <summary>
        /// Adds the given <see cref="Node&lt;TNodeType, TEdgeType&gt;"/>.
        /// </summary>
        /// <param name="node">The <see cref="Node&lt;TNodeType, TEdgeType&gt;"/> to add.</param>
        void Add(Node<TNodeType, TEdgeType> node);

        /// <summary>
        /// Removes the given <see cref="Edge&lt;TNodeType, TEdgeType&gt;"/>.
        /// </summary>
        /// <param name="edge">The <see cref="Edge&lt;TNodeType, TEdgeType&gt;"/> to remove.</param>
        void Remove(Edge<TNodeType, TEdgeType> edge);

        /// <summary>
        /// Removes the given <see cref="Node&lt;TNodeType, TEdgeType&gt;"/>.
        /// </summary>
        /// <param name="node">The <see cref="Node&lt;TNodeType, TEdgeType&gt;"/> to remove.</param>
        void Remove(Node<TNodeType, TEdgeType> node);

        /// <summary>
        /// Removes all <see cref="Node&lt;TNodeType, TEdgeType&gt;">nodes</see> and <see cref="Edge&lt;TNodeType, TEdgeType&gt;">edges</see>.
        /// </summary>
        void Clear();

        /// <summary>
        /// Gets all <see cref="Edge&lt;TNodeType, TEdgeType&gt;">edges</see> of the <see cref="Node&lt;TNodeType, TEdgeType&gt;"/>.
        /// </summary>
        /// <param name="node">The <see cref="Node&lt;TNodeType, TEdgeType&gt;"/>.</param>
        /// <returns>All <see cref="Edge&lt;TNodeType, TEdgeType&gt;">edges</see>.</returns>
        IEnumerable<Edge<TNodeType, TEdgeType>> GetEdgesOfNode(Node<TNodeType, TEdgeType> node);

        /// <summary>
        /// Gets the incoming <see cref="Edge&lt;TNodeType, TEdgeType&gt;">edges</see> of the <see cref="Node&lt;TNodeType, TEdgeType&gt;"/>.
        /// </summary>
        /// <param name="node">The <see cref="Node&lt;TNodeType, TEdgeType&gt;"/>.</param>
        /// <returns>All incoming <see cref="Edge&lt;TNodeType, TEdgeType&gt;">edges</see>.</returns>
        IEnumerable<Edge<TNodeType, TEdgeType>> GetIncomingEdgesOfNode(Node<TNodeType, TEdgeType> node);

        /// <summary>
        /// Gets the outgoing <see cref="Edge&lt;TNodeType, TEdgeType&gt;">edges</see> of the <see cref="Node&lt;TNodeType, TEdgeType&gt;"/>.
        /// </summary>
        /// <param name="node">The <see cref="Node&lt;TNodeType, TEdgeType&gt;"/>.</param>
        /// <returns>All outgoing <see cref="Edge&lt;TNodeType, TEdgeType&gt;">edges</see>.</returns>
        IEnumerable<Edge<TNodeType, TEdgeType>> GetOutgoingEdgesOfNode(Node<TNodeType, TEdgeType> node);

        /// <summary>
        /// Gets all <see cref="Node&lt;TNodeType, TEdgeType&gt;">nodes</see> connected to the <see cref="Node&lt;TNodeType, TEdgeType&gt;"/>.
        /// </summary>
        /// <param name="node">The <see cref="Node&lt;TNodeType, TEdgeType&gt;"/>.</param>
        /// <returns>All connected <see cref="Node&lt;TNodeType, TEdgeType&gt;">nodes</see>.</returns>
        IEnumerable<Node<TNodeType, TEdgeType>> GetNeighborsOfNode(Node<TNodeType, TEdgeType> node);

        /// <summary>
        /// Gets all <see cref="Node&lt;TNodeType, TEdgeType&gt;">nodes</see> connected to the <see cref="Node&lt;TNodeType, TEdgeType&gt;"/> by an incoming <see cref="Edge&lt;TNodeType, TEdgeType&gt;"/>.
        /// </summary>
        /// <param name="node">The <see cref="Node&lt;TNodeType, TEdgeType&gt;"/>.</param>
        /// <returns>All <see cref="Node&lt;TNodeType, TEdgeType&gt;">nodes</see> connected by an incoming <see cref="Edge&lt;TNodeType, TEdgeType&gt;"/>.</returns>
        IEnumerable<Node<TNodeType, TEdgeType>> GetIncomingNeighborsOfNode(Node<TNodeType, TEdgeType> node);

        /// <summary>
        /// Gets all <see cref="Node&lt;TNodeType, TEdgeType&gt;">nodes</see> connected to the <see cref="Node&lt;TNodeType, TEdgeType&gt;"/> by an outgoing <see cref="Edge&lt;TNodeType, TEdgeType&gt;"/>.
        /// </summary>
        /// <param name="node">The <see cref="Node&lt;TNodeType, TEdgeType&gt;"/>.</param>
        /// <returns>All <see cref="Node&lt;TNodeType, TEdgeType&gt;">nodes</see> connected by an outgoing <see cref="Edge&lt;TNodeType, TEdgeType&gt;"/>.</returns>
        IEnumerable<Node<TNodeType, TEdgeType>> GetOutgoingNeighborsOfNode(Node<TNodeType, TEdgeType> node);
    }
}