using System;
using System.Windows.Media.Media3D;

namespace Palmmedia.WpfGraph.UI.ViewModels
{
    /// <summary>
    /// Provides data for the <see cref="E:GraphDataBase.Blinking"/> event.
    /// </summary>
    public class AnimationEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnimationEventArgs"/> class.
        /// </summary>
        /// <param name="duration">The duration of the animation.</param>
        /// <param name="callback">The <see cref="Action">callback</see> executed at the end of an animation.</param>
        public AnimationEventArgs(double duration, Action callback)
        {
            this.Duration = duration;
            this.Callback = callback;
        }

        /// <summary>
        /// Gets the duration of the animation.
        /// </summary>
        public double Duration { get; private set; }

        /// <summary>
        /// Gets the  <see cref="Action">callback</see> which gets executed at the end of an animation.
        /// </summary>
        public Action Callback { get; private set; }
    }
}