using System;

namespace Palmmedia.WpfGraph.Core
{
    /// <summary>
    /// Represents an edge.
    /// </summary>
    /// <typeparam name="TNodeType">The type of the data attached to a <see cref="Palmmedia.WpfGraph.Core.Node&lt;TNodeType, TEdgeType&gt;"/>.</typeparam>
    /// <typeparam name="TEdgeType">The type of the data attached to an <see cref="Palmmedia.WpfGraph.Core.Edge&lt;TNodeType, TEdgeType&gt;"/>.</typeparam>
    public class Edge<TNodeType, TEdgeType> : GraphElement<TNodeType, TEdgeType>
    {
        /// <summary>
        /// The data attached to the edge.
        /// </summary>
        private TEdgeType data;

        /// <summary>
        /// The edge direction.
        /// </summary>
        private EdgeDirection edgeDirection;

        /// <summary>
        /// Initializes a new instance of the <see cref="Edge&lt;TNodeType, TEdgeType&gt;"/> class.
        /// </summary>
        /// <param name="firstNode">The first node.</param>
        /// <param name="secondNode">The second node.</param>
        public Edge(Node<TNodeType, TEdgeType> firstNode, Node<TNodeType, TEdgeType> secondNode)
        {
            this.FirstNode = firstNode ?? throw new ArgumentNullException("firstNode");
            this.SecondNode = secondNode ?? throw new ArgumentNullException("secondNode");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Edge&lt;TNodeType, TEdgeType&gt;"/> class.
        /// </summary>
        /// <param name="firstNode">The first node.</param>
        /// <param name="secondNode">The second node.</param>
        /// <param name="edgeDirection">The edge direction.</param>
        public Edge(Node<TNodeType, TEdgeType> firstNode, Node<TNodeType, TEdgeType> secondNode, EdgeDirection edgeDirection)
            : this(firstNode, secondNode)
        {
            this.EdgeDirection = edgeDirection;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Edge&lt;TNodeType, TEdgeType&gt;"/> class.
        /// </summary>
        /// <param name="firstNode">The first node.</param>
        /// <param name="secondNode">The second node.</param>
        /// <param name="data">The data attached to the edge.</param>
        public Edge(Node<TNodeType, TEdgeType> firstNode, Node<TNodeType, TEdgeType> secondNode, TEdgeType data)
            : this(firstNode, secondNode, EdgeDirection.OmniDirectional, data)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Edge&lt;TNodeType, TEdgeType&gt;"/> class.
        /// </summary>
        /// <param name="firstNode">The first <see cref="Node&lt;TNodeType, TEdgeType&gt;"/>.</param>
        /// <param name="secondNode">The second <see cref="Node&lt;TNodeType, TEdgeType&gt;"/>.</param>
        /// <param name="edgeDirection">The edge direction.</param>
        /// <param name="data">The data attached to the <see cref="Edge&lt;TNodeType, TEdgeType&gt;"/>.</param>
        public Edge(Node<TNodeType, TEdgeType> firstNode, Node<TNodeType, TEdgeType> secondNode, EdgeDirection edgeDirection, TEdgeType data)
            : this(firstNode, secondNode)
        {
            this.EdgeDirection = edgeDirection;
            this.Data = data;
        }

        /// <summary>
        /// Occurs when the data has been changed.
        /// </summary>
        public event EventHandler<EventArgs> DataChanged;

        /// <summary>
        /// Occurs when the edge direction has been changed.
        /// </summary>
        public event EventHandler<EventArgs> EdgeDirectionChanged;

        /// <summary>
        /// Gets the first <see cref="Node&lt;TNodeType, TEdgeType&gt;"/>.
        /// </summary>
        public Node<TNodeType, TEdgeType> FirstNode { get; private set; }

        /// <summary>
        /// Gets the second <see cref="Node&lt;TNodeType, TEdgeType&gt;"/>.
        /// </summary>
        public Node<TNodeType, TEdgeType> SecondNode { get; private set; }

        /// <summary>
        /// Gets or sets the edge direction.
        /// </summary>
        public virtual EdgeDirection EdgeDirection
        {
            get
            {
                return this.edgeDirection;
            }

            set
            {
                this.edgeDirection = value;
                this.OnEdgeDirectionChanged(EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets or sets the data attached to the <see cref="Edge&lt;TNodeType, TEdgeType&gt;"/>.
        /// </summary>
        public virtual TEdgeType Data
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

        /// <summary>
        /// Raises the <see cref="E:EdgeDirectionChanged"/> event.
        /// </summary>
        /// <param name="args">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected virtual void OnEdgeDirectionChanged(EventArgs args)
        {
            if (this.EdgeDirectionChanged != null)
            {
                this.EdgeDirectionChanged(this, args);
            }
        }
    }
}