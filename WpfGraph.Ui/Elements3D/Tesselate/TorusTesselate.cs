using System;
using System.Windows;
using System.Windows.Media.Media3D;

namespace Palmmedia.WpfGraph.UI.Elements3D.Tesselate
{
    /// <summary>
    /// Creates a tesselate <see cref="MeshGeometry3D">MeshGeometry3D</see>
    /// wich may be used within a <see cref="System.Windows.Controls.Viewport3D">Viewport3D</see>
    /// The Create() method should be used to create a new 
    /// <see cref="MeshGeometry3D">MeshGeometry3D</see>
    /// </summary>
    internal static class TorusTesselate
    {
        /// <summary>
        /// Tessellates the sphere and returns a MeshGeometry3D representing the 
        /// tessellation based on the given parameters.
        /// </summary>
        /// <param name="pDiv">The number of phi divisions.</param>
        /// <param name="tDiv">The number of theta divisions.</param>
        /// <param name="radius">The radius.</param>
        /// <param name="outerRadius">The outer radius.</param>
        /// <returns>The <see cref="MeshGeometry3D">MeshGeometry3D</see>.</returns>
        public static MeshGeometry3D Create(int pDiv, int tDiv, double radius, double outerRadius)
        {
            double centerradius = outerRadius - radius;

            double dp = MathHelper.DegToRad(270.0) / pDiv;
            double dt = MathHelper.DegToRad(360.0) / tDiv;

            var mesh = new MeshGeometry3D();

            var positionOffset = new Vector3D(-outerRadius, outerRadius, 0);

            for (int pi = 0; pi <= pDiv; pi++)
            {
                double phi = pi * dp;

                for (int ti = 0; ti <= tDiv; ti++)
                {
                    double theta = ti * dt;
                    
                    var circlePosition = GetPosition(theta, radius, 0);
                    var position = GetPosition(phi, centerradius + circlePosition.X, circlePosition.Y);

                    mesh.Positions.Add(position + positionOffset);
                    mesh.Normals.Add((Vector3D)(position - GetPosition(phi, centerradius, 0)));
                    mesh.TextureCoordinates.Add(GetTextureCoordinate(phi, theta));
                }
            }

            for (int pi = 0; pi < pDiv; pi++)
            {
                for (int ti = 0; ti < tDiv; ti++)
                {
                    int offset = pi * (tDiv + 1);

                    mesh.TriangleIndices.Add(offset + ti);
                    mesh.TriangleIndices.Add(offset + ti + tDiv + 1);
                    mesh.TriangleIndices.Add(offset + ti + 1);

                    mesh.TriangleIndices.Add(offset + ti + 1);
                    mesh.TriangleIndices.Add(offset + ti + tDiv + 1);
                    mesh.TriangleIndices.Add(offset + ti + tDiv + 2);
                }
            }

            mesh.Freeze();
            return mesh;
        }

        /// <summary>
        /// Gets the position in cartesian coordinates.
        /// </summary>
        /// <param name="phi">The phi.</param>
        /// <param name="radius">The radius.</param>
        /// <param name="height">The height.</param>
        /// <returns>The position.</returns>
        private static Point3D GetPosition(double phi, double radius, double height)
        {
            double x = radius * Math.Cos(phi);
            double y = radius * Math.Sin(phi);

            return new Point3D(x, y, height);
        }

        /// <summary>
        /// Gets the texture coordinate.
        /// </summary>
        /// <param name="phi">The phi.</param>
        /// <param name="theta">The theta.</param>
        /// <returns>The texture coordinate.</returns>
        private static Point GetTextureCoordinate(double phi, double theta)
        {
            return new Point(theta / (2 * Math.PI), (2 * phi) / (3 * Math.PI));
        }
    }
}
