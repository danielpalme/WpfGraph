using System.Windows.Media.Media3D;
using Palmmedia.WpfGraph.Core;
using Palmmedia.WpfGraph.UI.Elements3D.Tesselate;
using Palmmedia.WpfGraph.UI.Interaction;
using Palmmedia.WpfGraph.UI.ViewModels;

namespace Palmmedia.WpfGraph.UI.Elements3D
{
    /// <summary>
    /// Represents an edge starting and ending at the same node.
    /// </summary>
    public class SelfEdgeUIElement : EdgeUIElement
    {
        /// <summary>
        /// <see cref="MeshGeometry3D"/> used as prototype.
        /// </summary>
        private static readonly MeshGeometry3D torusPrototype = TorusTesselate.Create(30, 10, 0.3, NODERADIUS);

        /// <summary>
        /// Initializes a new instance of the <see cref="SelfEdgeUIElement"/> class.
        /// </summary>
        /// <param name="graphProvider">The <see cref="IGraphProvider"/>.</param>
        /// <param name="edge">The edge.</param>
        /// <param name="translateTransform">The <see cref="TranslateTransform3D"/> of the visual element representing the node of the edge.</param>
        public SelfEdgeUIElement(IGraphProvider graphProvider, Edge<NodeData, EdgeData> edge, TranslateTransform3D translateTransform)
            : base(graphProvider, edge)
        {
            this.Transform = translateTransform;
        }

        /// <summary>
        /// Participates in rendering operations when overridden in a derived class.
        /// </summary>
        protected override void OnUpdateModel()
        {
            var model = new GeometryModel3D(
                torusPrototype,
                new DiffuseMaterial(this.CreateBrush()));

            this.Model = model;
        }
    }
}
