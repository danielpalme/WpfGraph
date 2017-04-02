using System;
using System.Globalization;
using Palmmedia.WpfGraph.Common;
using Palmmedia.WpfGraph.UI.Algorithms;
using Palmmedia.WpfGraph.UI.Interaction;

namespace Palmmedia.WpfGraph.UI.ViewModels.Menu
{
    /// <summary>
    /// MenuItem showing an <see cref="IGraphAlgorithm"/> for execution.
    /// </summary>
    public class IAlgorithmMenuItemViewModel : MenuItemViewModel
    {
        /// <summary>
        /// The <see cref="IGraphProvider"/>.
        /// </summary>
        private IGraphProvider graphProvider;

        /// <summary>
        /// The <see cref="IMessageHandler"/>.
        /// </summary>
        private IMessageHandler messageHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="IAlgorithmMenuItemViewModel"/> class.
        /// </summary>
        /// <param name="graphProvider">The <see cref="IGraphProvider"/>.</param>
        /// <param name="messageHandler">The <see cref="IMessageHandler"/>.</param>
        /// <param name="parentViewModel">The parent view model.</param>
        /// <param name="algorithm">The <see cref="IGraphAlgorithm"/>.</param>
        public IAlgorithmMenuItemViewModel(IGraphProvider graphProvider, IMessageHandler messageHandler, MenuItemViewModel parentViewModel, IGraphAlgorithm algorithm)
            : base(parentViewModel)
        {
            this.graphProvider = graphProvider;
            this.messageHandler = messageHandler;

            this.Header = algorithm.Name;
            this.Command = new RelayCommand(param => this.Execute(algorithm));
        }

        /// <summary>
        /// Executes the given <see cref="IGraphAlgorithm"/>.
        /// </summary>
        /// <param name="algorithm">The <see cref="IGraphAlgorithm"/> to execute.</param>
        private void Execute(IGraphAlgorithm algorithm)
        {
            try
            {
                algorithm.Execute(this.graphProvider.Graph);
            }
            catch (InvalidOperationException ex)
            {
                this.messageHandler.ShowError(string.Format(CultureInfo.CurrentCulture, Palmmedia.WpfGraph.UI.Properties.Resources.InvalidGraph, ex.Message));
            }            
        }
    }
}
