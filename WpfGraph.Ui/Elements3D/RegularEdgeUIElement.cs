using System;
using System.ComponentModel;
using System.Windows.Media.Media3D;
using Palmmedia.WpfGraph.Core;
using Palmmedia.WpfGraph.UI.Elements3D.Tesselate;
using Palmmedia.WpfGraph.UI.Interaction;
using Palmmedia.WpfGraph.UI.ViewModels;

namespace Palmmedia.WpfGraph.UI.Elements3D
{
    /// <summary>
    /// Represents an edge.
    /// </summary>
    public class RegularEdgeUIElement : EdgeUIElement
    {
        /// <summary>
        /// <see cref="MeshGeometry3D"/> used as prototype.
        /// </summary>
        private static readonly MeshGeometry3D cylinderPrototype = CylinderTesselate.Create(10, 0.4, 1);

        /// <summary>
        /// Oberservers for dependency properties.
        /// </summary>
        private readonly PropertyDescriptor positionXDescriptor, positionYDescriptor, positionZDescriptor;

        /// <summary>
        /// The <see cref="TranslateTransform3D"/> of the visual elements representing the nodes of the edge.
        /// </summary>
        private readonly TranslateTransform3D translateTransform1, translateTransform2;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegularEdgeUIElement"/> class.
        /// </summary>
        /// <param name="graphProvider">The <see cref="IGraphProvider"/>.</param>
        /// <param name="edge">The edge.</param>
        /// <param name="translateTransform1">The <see cref="TranslateTransform3D"/> of the visual element representing the first node of the edge.</param>
        /// <param name="translateTransform2">The <see cref="TranslateTransform3D"/> of the visual element representing the second node of the edge.</param>
        public RegularEdgeUIElement(IGraphProvider graphProvider, Edge<NodeData, EdgeData> edge, TranslateTransform3D translateTransform1, TranslateTransform3D translateTransform2)
            : base(graphProvider, edge)
        {
            this.translateTransform1 = translateTransform1;
            this.translateTransform2 = translateTransform2;

            this.positionXDescriptor = DependencyPropertyDescriptor.FromProperty(TranslateTransform3D.OffsetXProperty, typeof(TranslateTransform3D));
            this.positionXDescriptor.AddValueChanged(translateTransform1, (s, e) => this.UpdatePosition());
            this.positionXDescriptor.AddValueChanged(translateTransform2, (s, e) => this.UpdatePosition());

            this.positionYDescriptor = DependencyPropertyDescriptor.FromProperty(TranslateTransform3D.OffsetYProperty, typeof(TranslateTransform3D));
            this.positionYDescriptor.AddValueChanged(translateTransform1, (s, e) => this.UpdatePosition());
            this.positionYDescriptor.AddValueChanged(translateTransform2, (s, e) => this.UpdatePosition());

            this.positionZDescriptor = DependencyPropertyDescriptor.FromProperty(TranslateTransform3D.OffsetZProperty, typeof(TranslateTransform3D));
            this.positionZDescriptor.AddValueChanged(translateTransform1, (s, e) => this.UpdatePosition());
            this.positionZDescriptor.AddValueChanged(translateTransform2, (s, e) => this.UpdatePosition());

            this.UpdatePosition();
        }

        /// <summary>
        /// Participates in rendering operations when overridden in a derived class.
        /// </summary>
        protected override void OnUpdateModel()
        {
            var model = new GeometryModel3D(
                cylinderPrototype,
                new DiffuseMaterial(this.CreateBrush()));

            this.Model = model;
        }

        /// <summary>
        /// Updates the position.
        /// </summary>
        protected virtual void UpdatePosition()
        {
            var position1 = new Point3D(this.translateTransform1.OffsetX, this.translateTransform1.OffsetY, this.translateTransform1.OffsetZ);
            var position2 = new Point3D(this.translateTransform2.OffsetX, this.translateTransform2.OffsetY, this.translateTransform2.OffsetZ);

            var transformGroup = new Transform3DGroup();
            Vector3D difference, normalizedDifference;
            difference = normalizedDifference = position2 - position1;
            normalizedDifference.Normalize();

            Vector3D rotationAngle = MathHelper.CrossProduct(new Vector3D(0, 0, 1), normalizedDifference);
            double rotation = MathHelper.ScalarProduct(new Vector3D(0, 0, 1), normalizedDifference);

            double length = difference.Length - (2 * GraphUIElement.NODERADIUS);

            // Scale to distance of the nodes
            transformGroup.Children.Add(new ScaleTransform3D(new Vector3D(1, 1, length), new Point3D(0, 0, 0)));

            // Rotate
            transformGroup.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(rotationAngle, rotation * 180 / Math.PI)));
            
            // Move to first node
            transformGroup.Children.Add(new TranslateTransform3D((Vector3D)position1 + (GraphUIElement.NODERADIUS * normalizedDifference)));

            this.Transform = transformGroup;
        }
    }
}
