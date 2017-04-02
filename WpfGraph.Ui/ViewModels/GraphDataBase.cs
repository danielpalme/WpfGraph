using System;
using System.Windows.Media;

namespace Palmmedia.WpfGraph.UI.ViewModels
{
    /// <summary>
    /// Base class for Data attached to an <see cref="Palmmedia.WpfGraph.Core.GraphElement&lt;TNodeType, TEdgeType&gt;"/>.
    /// </summary>
    public abstract class GraphDataBase : ViewModelBase
    {
        /// <summary>
        /// Default duration of blink animation.
        /// </summary>
        private const double BLINKDURATION = 1000;

        /// <summary>
        /// The color.
        /// </summary>
        private Color color;

        /// <summary>
        /// Determines whether the element is in a selection state.
        /// </summary>
        private bool marked;

        /// <summary>
        /// Occurs when the element should blink.
        /// </summary>
        public event EventHandler<AnimationEventArgs> Blinking;

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        public Color Color
        {
            get
            {
                return this.color;
            }

            set
            {
                this.color = value;
                this.OnPropertyChanged("Color");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="GraphDataBase"/> is marked.
        /// Determines whether the element is in a selection state.
        /// The selection state can be used to start an <see cref="Palmmedia.WpfGraph.UI.Algorithms.IGraphAlgorithm"/>
        /// on a marked <see cref="Palmmedia.WpfGraph.Core.GraphElement&lt;TNodeType, TEdgeType&gt;"/>.
        /// </summary>
        public bool Marked
        {
            get
            {
                return this.marked;
            }

            set
            {
                this.marked = value;
                this.OnPropertyChanged("Marked");
            }
        }

        /// <summary>
        /// Starts a blink animation.
        /// </summary>
        public void Blink()
        {
            this.Blink(BLINKDURATION, null);
        }

        /// <summary>
        /// Starts a blink animation with the given duration.
        /// </summary>
        /// <param name="duration">The duration of the animation.</param>
        public void Blink(double duration)
        {
            this.Blink(duration, null);
        }

        /// <summary>
        /// Starts a blink animation and executes the given <see cref="Action">callback</see> at the end of the animation.
        /// </summary>
        /// <param name="callback">The <see cref="Action">callback</see> executed at the end of the animation.</param>
        public void Blink(Action callback)
        {
            this.Blink(BLINKDURATION, callback);
        }

        /// <summary>
        /// Starts a blink animation with the given duration and executes the given <see cref="Action">callback</see> at the end of the animation.
        /// </summary>
        /// <param name="duration">The duration of the animation.</param>
        /// <param name="callback">The <see cref="Action">callback</see> executed at the end of the animation.</param>
        public void Blink(double duration, Action callback)
        {
            this.OnBlinking(new AnimationEventArgs(duration, callback));
        }

        /// <summary>
        /// Raises the <see cref="E:Blinking"/> event.
        /// </summary>
        /// <param name="args">The <see cref="Palmmedia.WpfGraph.UI.ViewModels.AnimationEventArgs"/> instance containing the event data.</param>
        protected virtual void OnBlinking(AnimationEventArgs args)
        {
            if (this.Blinking != null)
            {
                this.Blinking(this, args);
            }
        }
    }
}
