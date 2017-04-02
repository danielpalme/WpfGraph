using System;

namespace Palmmedia.WpfGraph.Core
{
    /// <summary>
    /// Base class for all <see cref="IGraph&lt;TNodeType, TEdgeType&gt;"/> implementations.
    /// </summary>
    /// <typeparam name="TNodeType">The type of the data attached to a <see cref="Palmmedia.WpfGraph.Core.Node&lt;TNodeType, TEdgeType&gt;"/>.</typeparam>
    /// <typeparam name="TEdgeType">The type of the data attached to an <see cref="Palmmedia.WpfGraph.Core.Edge&lt;TNodeType, TEdgeType&gt;"/>.</typeparam>
    public abstract class GraphBase<TNodeType, TEdgeType>
    {
        /// <summary>
        /// Occurs after an <see cref="Edge&lt;TNodeType, TEdgeType&gt;"/> has been added.
        /// </summary>
        public event EventHandler<EdgeEventArgs<TNodeType, TEdgeType>> EdgeAdded;

        /// <summary>
        /// Occurs after an <see cref="Node&lt;TNodeType, TEdgeType&gt;"/> has been added.
        /// </summary>
        public event EventHandler<NodeEventArgs<TNodeType, TEdgeType>> NodeAdded;

        /// <summary>
        /// Occurs after an <see cref="Edge&lt;TNodeType, TEdgeType&gt;"/> has been removed.
        /// </summary>
        public event EventHandler<EdgeEventArgs<TNodeType, TEdgeType>> EdgeRemoved;

        /// <summary>
        /// Occurs after an <see cref="Node&lt;TNodeType, TEdgeType&gt;"/> has been removed.
        /// </summary>
        public event EventHandler<NodeEventArgs<TNodeType, TEdgeType>> NodeRemoved;

        /// <summary>
        /// Raises the <see cref="E:EdgeAdded"/> event.
        /// </summary>
        /// <param name="args">The <see cref="Palmmedia.WpfGraph.Core.EdgeEventArgs&lt;TNodeType,TEdgeType&gt;"/> instance containing the event data.</param>
        protected virtual void OnEdgeAdded(EdgeEventArgs<TNodeType, TEdgeType> args)
        {
            if (this.EdgeAdded != null)
            {
                this.EdgeAdded(this, args);
            }
        }

        /// <summary>
        /// Raises the <see cref="E:NodeAdded"/> event.
        /// </summary>
        /// <param name="args">The <see cref="Palmmedia.WpfGraph.Core.NodeEventArgs&lt;TNodeType,TEdgeType&gt;"/> instance containing the event data.</param>
        protected virtual void OnNodeAdded(NodeEventArgs<TNodeType, TEdgeType> args)
        {
            if (this.NodeAdded != null)
            {
                this.NodeAdded(this, args);
            }
        }

        /// <summary>
        /// Raises the <see cref="E:EdgeRemoved"/> event.
        /// </summary>
        /// <param name="args">The <see cref="Palmmedia.WpfGraph.Core.EdgeEventArgs&lt;TNodeType,TEdgeType&gt;"/> instance containing the event data.</param>
        protected virtual void OnEdgeRemoved(EdgeEventArgs<TNodeType, TEdgeType> args)
        {
            if (this.EdgeRemoved != null)
            {
                this.EdgeRemoved(this, args);
            }
        }

        /// <summary>
        /// Raises the <see cref="E:NodeRemoved"/> event.
        /// </summary>
        /// <param name="args">The <see cref="Palmmedia.WpfGraph.Core.NodeEventArgs&lt;TNodeType,TEdgeType&gt;"/> instance containing the event data.</param>
        protected virtual void OnNodeRemoved(NodeEventArgs<TNodeType, TEdgeType> args)
        {
            if (this.NodeRemoved != null)
            {
                this.NodeRemoved(this, args);
            }
        }
    }
}