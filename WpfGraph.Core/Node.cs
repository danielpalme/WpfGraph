using System;
using System.Collections.Generic;

namespace Palmmedia.WpfGraph.Core
{
    /// <summary>
    /// Represents a node.
    /// </summary>
    /// <typeparam name="TNodeType">The type of the data attached to a <see cref="Palmmedia.WpfGraph.Core.Node&lt;TNodeType, TEdgeType&gt;"/>.</typeparam>
    /// <typeparam name="TEdgeType">The type of the data attached to an <see cref="Palmmedia.WpfGraph.Core.Edge&lt;TNodeType, TEdgeType&gt;"/>.</typeparam>
    public class Node<TNodeType, TEdgeType> : GraphElement<TNodeType, TEdgeType>
    {
        /// <summary>
        /// The data attached to the node.
        /// </summary>
        private TNodeType data;

        /// <summary>
        /// Initializes a new instance of the <see cref="Node&lt;TNodeType, TEdgeType&gt;"/> class.
        /// </summary>
        public Node()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Node&lt;TNodeType, TEdgeType&gt;"/> class.
        /// </summary>
        /// <param name="data">The data attached to the node.</param>
        public Node(TNodeType data)
        {
            this.Data = data;
        }

        /// <summary>
        /// Occurs when the data has been changed.
        /// </summary>
        public event EventHandler<EventArgs> DataChanged;

        /// <summary>
        /// Gets or sets the data attached to the <see cref="Node&lt;TNodeType, TEdgeType&gt;"/>.
        /// </summary>
        public virtual TNodeType Data 
        { 
            get
            {
                return this.data;
            }
                
            set
            {
                this.data = value;
                this.OnDataChanged(EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets all <see cref="Edge&lt;TNodeType, TEdgeType&gt;">edges</see> of the <see cref="Node&lt;TNodeType, TEdgeType&gt;"/>.
        /// </summary>
        /// <returns>All <see cref="Edge&lt;TNodeType, TEdgeType&gt;">edges</see>.</returns>
        public virtual IEnumerable<Edge<TNodeType, TEdgeType>> Edges
        {
            get
            {
                return this.Graph.GetEdgesOfNode(this);
            }
        }

        /// <summary>
        /// Gets the incoming <see cref="Edge&lt;TNodeType, TEdgeType&gt;">edges</see> of the <see cref="Node&lt;TNodeType, TEdgeType&gt;"/>.
        /// </summary>
        /// <returns>All incoming <see cref="Edge&lt;TNodeType, TEdgeType&gt;">edges</see>.</returns>
        public virtual IEnumerable<Edge<TNodeType, TEdgeType>> IncomingEdges
        {
            get
            {
                return this.Graph.GetIncomingEdgesOfNode(this);
            }
        }

        /// <summary>
        /// Gets the outgoing <see cref="Edge&lt;TNodeType, TEdgeType&gt;">edges</see> of the <see cref="Node&lt;TNodeType, TEdgeType&gt;"/>.
        /// </summary>
        /// <returns>All outgoing <see cref="Edge&lt;TNodeType, TEdgeType&gt;">edges</see>.</returns>
        public virtual IEnumerable<Edge<TNodeType, TEdgeType>> OutgoingEdges
        {
            get
            {
                return this.Graph.GetOutgoingEdgesOfNode(this);
            }
        }

        /// <summary>
        /// Gets all <see cref="Node&lt;TNodeType, TEdgeType&gt;">nodes</see> connected to the <see cref="Node&lt;TNodeType, TEdgeType&gt;"/>.
        /// </summary>
        /// <returns>All connected <see cref="Node&lt;TNodeType, TEdgeType&gt;">nodes</see>.</returns>
        public virtual IEnumerable<Node<TNodeType, TEdgeType>> Neighbors
        {
            get
            {
                return this.Graph.GetNeighborsOfNode(this);
            }
        }

        /// <summary>
        /// Gets all <see cref="Node&lt;TNodeType, TEdgeType&gt;">nodes</see> connected to the <see cref="Node&lt;TNodeType, TEdgeType&gt;"/> by an incoming <see cref="Edge&lt;TNodeType, TEdgeType&gt;"/>.
        /// </summary>
        /// <returns>All <see cref="Node&lt;TNodeType, TEdgeType&gt;">nodes</see> connected by an incoming <see cref="Edge&lt;TNodeType, TEdgeType&gt;"/>.</returns>
        public virtual IEnumerable<Node<TNodeType, TEdgeType>> IncomingNeighbors
        {
            get
            {
                return this.Graph.GetIncomingNeighborsOfNode(this);
            }
        }

        /// <summary>
        /// Gets all <see cref="Node&lt;TNodeType, TEdgeType&gt;">nodes</see> connected to the <see cref="Node&lt;TNodeType, TEdgeType&gt;"/> by an outgoing <see cref="Edge&lt;TNodeType, TEdgeType&gt;"/>.
        /// </summary>
        /// <returns>All <see cref="Node&lt;TNodeType, TEdgeType&gt;">nodes</see> connected by an outgoing <see cref="Edge&lt;TNodeType, TEdgeType&gt;"/>.</returns>
        public virtual IEnumerable<Node<TNodeType, TEdgeType>> OutgoingNeighbors
        {
            get
            {
                return this.Graph.GetOutgoingNeighborsOfNode(this);
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.Data == null ? base.ToString() : this.Data.ToString();
        }

        /// <summary>
        /// Raises the <see cref="E:DataChanged"/> event.
        /// </summary>
        /// <param name="args">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected virtual void OnDataChanged(EventArgs args)
        {
            if (this.DataChanged != null)
            {
                this.DataChanged(this, args);
            }
        }
    }
}