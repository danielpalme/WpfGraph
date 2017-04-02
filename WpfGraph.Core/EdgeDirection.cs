using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Palmmedia.WpfGraph.Core
{
    /// <summary>
    /// The direction of an <see cref="Edge&lt;TNodeType, TEdgeType&gt;"/>.
    /// </summary>
    public enum EdgeDirection : int
    {
        /// <summary>
        /// Undirected edge.
        /// </summary>
        OmniDirectional = 0,

        /// <summary>
        /// Directed edge (from first to second <see cref="Node&lt;TNodeType, TEdgeType&gt;"/>).
        /// </summary>
        First2Second = 1,

        /// <summary>
        /// Directed edge (from second to first <see cref="Node&lt;TNodeType, TEdgeType&gt;"/>).
        /// </summary>
        Second2First = 2
    }
}
