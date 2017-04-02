using System.Windows.Media;

namespace Palmmedia.WpfGraph.UI.ViewModels
{
    /// <summary>
    /// Data attached to an <see cref="Palmmedia.WpfGraph.Core.Edge&lt;TNodeType, TEdgeType&gt;"/>.
    /// </summary>
    public class EdgeData : GraphDataBase
    {
        /// <summary>
        /// The weight of the edge.
        /// </summary>
        private double weight = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="EdgeData"/> class.
        /// </summary>
        public EdgeData()
        {
            this.Color = Colors.LightGray;
        }

        /// <summary>
        /// Gets or sets the weight of the edge.
        /// </summary>
        public double Weight
        {
            get
            {
                return this.weight;
            }

            set
            {
                this.weight = value;
                this.OnPropertyChanged("Weight");
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return "Weight: " + this.Weight;
        }
    }
}
