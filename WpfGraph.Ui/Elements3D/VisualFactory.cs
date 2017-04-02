using System.Windows;
using System.Windows.Media.Media3D;
using Palmmedia.WpfGraph.Core;
using Palmmedia.WpfGraph.UI.Interaction;
using Palmmedia.WpfGraph.UI.ViewModels;

namespace Palmmedia.WpfGraph.UI.Elements3D
{
    /// <summary>
    /// Factory class to instantiate <see cref="GraphUIElement">GraphUIElements</see>.
    /// </summary>
    internal static class VisualFactory
    {
        /// <summary>
        /// Creates the visual representation for an edge.
        /// </summary>
        /// <param name="edge">The edge.</param>
        /// <param name="graphProvider">The <see cref="IGraphProvider"/>.</param>
        /// <param name="translateTransform1">The <see cref="TranslateTransform3D"/> of the visual element representing the first node of the edge.</param>
        /// <param name="translateTransform2">The <see cref="TranslateTransform3D"/> of the visual element representing the second node of the edge.</param>
        /// <returns>The visual representation for an edge.</returns>
        public static UIElement3D CreateVisual(this Edge<NodeData, EdgeData> edge, IGraphProvider graphProvider, TranslateTransform3D translateTransform1, TranslateTransform3D translateTransform2)
        {
            if (edge.Data == null)
            {
                edge.Data = new EdgeData();
            }
            
            if (edge.FirstNode == edge.SecondNode)
            {
                return new SelfEdgeUIElement(graphProvider, edge, translateTransform1);
            }
            else
            {
                return new RegularEdgeUIElement(graphProvider, edge, translateTransform1, translateTransform2);
            }
        }

        /// <summary>
        /// Creates the visual representation for a node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="graphProvider">The <see cref="IGraphProvider"/>.</param>
        /// <returns>The visual representation for a node.</returns>
        public static NodeUIElement CreateVisual(this Node<NodeData, EdgeData> node, IGraphProvider graphProvider)
        {
            if (node.Data == null)
            {
                node.Data = new NodeData();
            }

            return new NodeUIElement(graphProvider, node);
        }
    }
}
