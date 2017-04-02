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
    internal static class CylinderTesselate
    {
        /// <summary>
        /// Tessellates the sphere and returns a MeshGeometry3D representing the 
        /// tessellation based on the given parameters.
        /// </summary>
        /// <param name="pDiv">The number of phi divisions.</param>
        /// <param name="radius">The radius.</param>
        /// <param name="height">The height.</param>
        /// <returns>The <see cref="MeshGeometry3D">MeshGeometry3D</see>.</returns>
        public static MeshGeometry3D Create(int pDiv, double radius, double height)
        {
            double dp = MathHelper.DegToRad(360.0) / pDiv;

            var mesh = new MeshGeometry3D();

            for (int pi = 0; pi <= pDiv; pi++)
            {
                double phi = pi * dp;

                // Bottom
                mesh.Positions.Add(GetPosition(phi, radius, 0));
                mesh.Normals.Add(GetNormal(phi));
                mesh.TextureCoordinates.Add(GetTextureCoordinate(phi, 0));

                // Top
                mesh.Positions.Add(GetPosition(phi, radius, height));
                mesh.Normals.Add(GetNormal(phi));
                mesh.TextureCoordinates.Add(GetTextureCoordinate(phi, height));
            }

            for (int pi = 0; pi < 2 * pDiv; pi++)
            {
                mesh.TriangleIndices.Add(pi);
                mesh.TriangleIndices.Add(pi + 2);
                mesh.TriangleIndices.Add(pi + 1);

                mesh.TriangleIndices.Add(pi + 1);
                mesh.TriangleIndices.Add(pi + 2);
                mesh.TriangleIndices.Add(pi + 3);
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
        /// Gets the normal.
        /// </summary>
        /// <param name="phi">The phi.</param>
        /// <returns>The normal.</returns>
        private static Vector3D GetNormal(double phi)
        {
            return (Vector3D)GetPosition(phi, 1, 0);
        }

        /// <summary>
        /// Gets the texture coordinate.
        /// </summary>
        /// <param name="phi">The phi.</param>
        /// <param name="height">The height.</param>
        /// <returns>The texture coordinate.</returns>
        private static Point GetTextureCoordinate(double phi, double height)
        {
            height = Math.Min(height, 1);
            return new Point(phi / (2 * Math.PI), height);
        }
    }
}
