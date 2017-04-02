using System;

namespace Palmmedia.WpfGraph.Core
{
    /// <summary>
    /// Provides data for the <see cref="E:IGraph&lt;TNodeType, TEdgeType&gt;.EdgeAdded"/> and <see cref="E:IGraph&lt;TNodeType, TEdgeType&gt;.EdgeRemoved"/> events.
    /// </summary>
    /// <typeparam name="TNodeType">The type of the data attached to a <see cref="Palmmedia.WpfGraph.Core.Node&lt;TNodeType, TEdgeType&gt;"/>.</typeparam>
    /// <typeparam name="TEdgeType">The type of the data attached to an <see cref="Palmmedia.WpfGraph.Core.Edge&lt;TNodeType, TEdgeType&gt;"/>.</typeparam>
    public class EdgeEventArgs<TNodeType, TEdgeType> : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EdgeEventArgs&lt;TNodeType, TEdgeType&gt;"/> class.
        /// </summary>
        /// <param name="edge">The <see cref="Palmmedia.WpfGraph.Core.Edge&lt;TNodeType, TEdgeType&gt;"/>.</param>
        public EdgeEventArgs(Edge<TNodeType, TEdgeType> edge)
        {
            this.Edge = edge;
        }

        /// <summary>
        /// Gets the <see cref="Palmmedia.WpfGraph.Core.Edge&lt;TNodeType, TEdgeType&gt;"/>.
        /// </summary>
        public Edge<TNodeType, TEdgeType> Edge { get; private set; }
    }
}