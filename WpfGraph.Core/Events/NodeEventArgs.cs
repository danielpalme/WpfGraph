using System;

namespace Palmmedia.WpfGraph.Core
{
    /// <summary>
    /// Provides data for the <see cref="E:IGraph&lt;TNodeType, TEdgeType&gt;.NodeAdded"/> and <see cref="E:IGraph&lt;TNodeType, TEdgeType&gt;.NodeRemoved"/> events.
    /// </summary>
    /// <typeparam name="TNodeType">The type of the data attached to a <see cref="Palmmedia.WpfGraph.Core.Node&lt;TNodeType, TEdgeType&gt;"/>.</typeparam>
    /// <typeparam name="TEdgeType">The type of the data attached to an <see cref="Palmmedia.WpfGraph.Core.Edge&lt;TNodeType, TEdgeType&gt;"/>.</typeparam>
    public class NodeEventArgs<TNodeType, TEdgeType> : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NodeEventArgs&lt;TNodeType, TEdgeType&gt;"/> class.
        /// </summary>
        /// <param name="node">The <see cref="Palmmedia.WpfGraph.Core.Node&lt;TNodeType, TEdgeType&gt;"/>.</param>
        public NodeEventArgs(Node<TNodeType, TEdgeType> node)
        {
            this.Node = node;
        }

        /// <summary>
        /// Gets the <see cref="Palmmedia.WpfGraph.Core.Node&lt;TNodeType, TEdgeType&gt;"/>.
        /// </summary>
        public Node<TNodeType, TEdgeType> Node { get; private set; }
    }
}