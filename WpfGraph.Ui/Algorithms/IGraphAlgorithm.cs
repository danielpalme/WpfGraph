using Palmmedia.WpfGraph.Core;
using Palmmedia.WpfGraph.UI.ViewModels;

namespace Palmmedia.WpfGraph.UI.Algorithms
{
    /// <summary>
    /// All classes that execute an arbitrary graph algorithm/animation have to implement this interface.
    /// That way the UI can show all <see cref="IGraphAlgorithm">algorithms</see> in the menu.
    /// </summary>
    public interface IGraphAlgorithm
    {
        /// <summary>
        /// Gets the name of the algorithm.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the category of the algorithm.
        /// The category is used to generate a menu entry.
        /// Return <c>null</c> if algorithm should appear in root menu.
        /// </summary>
        string Category { get; }

        /// <summary>
        /// Executes the algorithm.
        /// </summary>
        /// <param name="graph">The graph.</param>
        /// <exception cref="System.InvalidOperationException">Thrown if graph does not meet special demands required by the graph algorithm.</exception>
        void Execute(IGraph<NodeData, EdgeData> graph);
    }
}
