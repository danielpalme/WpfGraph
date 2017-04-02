using System.Collections.Generic;
using System.Linq;
using Palmmedia.WpfGraph.UI.Algorithms;
using Palmmedia.WpfGraph.UI.Interaction;

namespace Palmmedia.WpfGraph.UI.ViewModels.Menu
{
    /// <summary>
    /// MenuItem containing <see cref="IAlgorithmMenuItemViewModel">IAlgorithmMenuItemViewModels</see> as child items.
    /// </summary>
    public class CategoryMenuItemViewModel : MenuItemViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryMenuItemViewModel"/> class.
        /// </summary>
        /// <param name="graphProvider">The <see cref="IGraphProvider"/>.</param>
        /// <param name="messageHandler">The <see cref="IMessageHandler"/>.</param>
        /// <param name="header">The header.</param>
        /// <param name="algorithms">The <see cref="IGraphAlgorithm">algorithms</see>.</param>
        public CategoryMenuItemViewModel(IGraphProvider graphProvider, IMessageHandler messageHandler, string header, IEnumerable<IGraphAlgorithm> algorithms)
            : base(null)
        {
            this.Header = header;

            foreach (var algorithm in algorithms)
            {
                this.ChildMenuItems.Add(new IAlgorithmMenuItemViewModel(graphProvider, messageHandler, this, algorithm));
            }
        }
    }
}
