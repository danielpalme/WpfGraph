using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Palmmedia.WpfGraph.UI.Elements3D.Tesselate
{
    /// <summary>
    /// Creates a tesselate <see cref="MeshGeometry3D">MeshGeometry3D</see>
    /// wich may be used within a <see cref="System.Windows.Controls.Viewport3D">Viewport3D</see>
    /// The Create() method should be used to create a new 
    /// <see cref="MeshGeometry3D">MeshGeometry3D</see>.
    /// </summary>
    internal static class SphereTesselate
    {
        /// <summary>
        /// Tessellates the sphere and returns a MeshGeometry3D representing the 
        /// tessellation based on the given parameters.
        /// </summary>
        /// <param name="tDiv">The number of theta divisions.</param>
        /// <param name="pDiv">The number of phi divisions.</param>
        /// <param name="radius">The radius.</param>
        /// <returns>The <see cref="MeshGeometry3D">MeshGeometry3D</see>.</returns>
        public static MeshGeometry3D Create(int tDiv, int pDiv, double radius)
        {
            double dt = MathHelper.DegToRad(360.0) / tDiv;
            double dp = MathHelper.DegToRad(180.0) / pDiv;

            MeshGeometry3D mesh = new MeshGeometry3D();

            for (int pi = 0; pi <= pDiv; pi++)
            {
                double phi = pi * dp;

                for (int ti = 0; ti <= tDiv; ti++)
                {
                    // we want to start the mesh on the x axis
                    double theta = ti * dt;

                    mesh.Positions.Add(GetPosition(theta, phi, radius));
                    mesh.Normals.Add(GetNormal(theta, phi));
                    mesh.TextureCoordinates.Add(GetTextureCoordinate(theta, phi));
                }
            }

            for (int pi = 0; pi < pDiv; pi++)
            {
                for (int ti = 0; ti < tDiv; ti++)
                {
                    int x0 = ti;
                    int x1 = ti + 1;
                    int y0 = pi * (tDiv + 1);
                    int y1 = (pi + 1) * (tDiv + 1);

                    mesh.TriangleIndices.Add(x0 + y0);
                    mesh.TriangleIndices.Add(x0 + y1);
                    mesh.TriangleIndices.Add(x1 + y0);

                    mesh.TriangleIndices.Add(x1 + y0);
                    mesh.TriangleIndices.Add(x0 + y1);
                    mesh.TriangleIndices.Add(x1 + y1);
                }
            }

            mesh.Freeze();
            return mesh;
        }

        /// <summary>
        /// Gets the position in cartesian coordinates.
        /// </summary>
        /// <param name="theta">The theta.</param>
        /// <param name="phi">The phi.</param>
        /// <param name="radius">The radius.</param>
        /// <returns>The position.</returns>
        private static Point3D GetPosition(double theta, double phi, double radius)
        {
            double x = radius * Math.Sin(theta) * Math.Sin(phi);
            double y = radius * Math.Cos(phi);
            double z = radius * Math.Cos(theta) * Math.Sin(phi);

            return new Point3D(x, y, z);
        }

        /// <summary>
        /// Gets the normal.
        /// </summary>
        /// <param name="theta">The theta.</param>
        /// <param name="phi">The phi.</param>
        /// <returns>The normal.</returns>
        private static Vector3D GetNormal(double theta, double phi)
        {
            return (Vector3D)GetPosition(theta, phi, 1.0);
        }

        /// <summary>
        /// Gets the texture coordinate.
        /// </summary>
        /// <param name="theta">The theta.</param>
        /// <param name="phi">The phi.</param>
        /// <returns>The texture coordinate.</returns>
        private static Point GetTextureCoordinate(double theta, double phi)
        {
            return new Point(((theta / (2 * Math.PI)) + 0.25) % 1, phi / Math.PI);
        }
    }
}
