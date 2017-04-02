using System;

namespace Palmmedia.WpfGraph.Core
{
    /// <summary>
    /// An element that belongs to an <see cref="IGraph&lt;TNodeType, TEdgeType&gt;"/>.
    /// </summary>
    /// <typeparam name="TNodeType">The type of the data attached to a <see cref="Palmmedia.WpfGraph.Core.Node&lt;TNodeType, TEdgeType&gt;"/>.</typeparam>
    /// <typeparam name="TEdgeType">The type of the data attached to an <see cref="Palmmedia.WpfGraph.Core.Edge&lt;TNodeType, TEdgeType&gt;"/>.</typeparam>
    public abstract class GraphElement<TNodeType, TEdgeType>
    {
        /// <summary>
        /// The graph the <see cref="GraphElement&lt;TNodeType, TEdgeType&gt;"/> belongs to.
        /// </summary>
        private IGraph<TNodeType, TEdgeType> graph;

        /// <summary>
        /// Gets or sets the graph the <see cref="GraphElement&lt;TNodeType, TEdgeType&gt;"/> belongs to.
        /// </summary>
        protected internal IGraph<TNodeType, TEdgeType> Graph 
        {
            get
            {
                if (this.graph == null)
                {
                    throw new InvalidOperationException("The element is not attached to a graph yet. Add it to a graph first.");
                }

                return this.graph;
            }

            set
            {
                this.graph = value;
            }
        }
    }
}