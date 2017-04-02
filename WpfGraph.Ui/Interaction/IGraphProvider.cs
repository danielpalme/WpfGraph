using System.Windows.Media.Media3D;
using Palmmedia.WpfGraph.Core;
using Palmmedia.WpfGraph.UI.ViewModels;

namespace Palmmedia.WpfGraph.UI.Interaction
{
    /// <summary>
    /// Interface to enable access to currently displayed graph.
    /// </summary>
    public interface IGraphProvider
    {
        /// <summary>
        /// Gets or sets the graph.
        /// </summary>
        IGraph<NodeData, EdgeData> Graph { get; set; }

        /// <summary>
        /// Gets or sets the currently selected node or edge.
        /// </summary>
        GraphElement<NodeData, EdgeData> SelectedElement { get; set; }

        /// <summary>
        /// Gets or sets the container displaying the graph elements.
        /// </summary>
        ContainerUIElement3D Container { get; set; }
    }
}
