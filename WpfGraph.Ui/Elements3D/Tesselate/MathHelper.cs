using System;
using System.Windows.Media.Media3D;

namespace Palmmedia.WpfGraph.UI.Elements3D.Tesselate
{
    /// <summary>
    /// Provides some mathcematical helper methods.
    /// </summary>
    internal static class MathHelper
    {
        /// <summary>
        /// Converts deg to rad.
        /// </summary>
        /// <param name="deg">The degrees.</param>
        /// <returns>The rad.</returns>
        public static double DegToRad(double deg)
        {
            return (deg / 180.0) * Math.PI;
        }

        /// <summary>
        /// Calculates the crossproduct of the given <see cref="Vector3D">vectors</see>.
        /// </summary>
        /// <param name="first">The first <see cref="Vector3D"/>.</param>
        /// <param name="second">The second <see cref="Vector3D"/>.</param>
        /// <returns>The crossproduct.</returns>
        public static Vector3D CrossProduct(Vector3D first, Vector3D second)
        {
            double x = (first.Y * second.Z) - (first.Z * second.Y);
            double y = (first.Z * second.X) - (first.X * second.Z);
            double z = (first.X * second.Y) - (first.Y * second.X);

            return new Vector3D(x, y, z);
        }

        /// <summary>
        /// Calculates the scalarproduct of the given <see cref="Vector3D">vectors</see>.
        /// </summary>
        /// <param name="first">The first <see cref="Vector3D"/>.</param>
        /// <param name="second">The second <see cref="Vector3D"/>.</param>
        /// <returns>The scalarproduct.</returns>
        public static double ScalarProduct(Vector3D first, Vector3D second)
        {
            return Math.Acos(((first.X * second.X) + (first.Y * second.Y) + (first.Z * second.Z)) / (first.Length * second.Length));
        }
    }
}
