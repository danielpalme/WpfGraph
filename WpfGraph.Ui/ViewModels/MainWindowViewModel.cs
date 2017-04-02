using System;
using System.Collections.Generic;
using System.Windows.Input;
using Palmmedia.WpfGraph.Common;
using Palmmedia.WpfGraph.UI.Interaction;
using Palmmedia.WpfGraph.UI.IO;
using Palmmedia.WpfGraph.UI.ViewModels.Menu;

namespace Palmmedia.WpfGraph.UI.ViewModels
{
    /// <summary>
    /// The viewmodel for the main window.
    /// </summary>
    public class MainWindowViewModel : ViewModelBase
    {
        /// <summary>
        /// Logger instance.
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(MainWindowViewModel));

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        /// <param name="graphProvider">The <see cref="IGraphProvider"/>.</param>
        /// <param name="fileSelector">The <see cref="IFileSelector"/>.</param>
        /// <param name="messageHandler">The <see cref="IMessageHandler"/>.</param>
        public MainWindowViewModel(IGraphProvider graphProvider, IFileSelector fileSelector, IMessageHandler messageHandler)
        {
            this.GraphViewModel = graphProvider ?? throw new ArgumentNullException("graphProvider");
            this.FileSelector = fileSelector ?? throw new ArgumentNullException("fileSelector");
            this.MessageHandler = messageHandler ?? throw new ArgumentNullException("messageHandler");

            this.LoadGraphCommand = new RelayCommand(param => this.LoadGraph());
            this.SaveGraphCommand = new RelayCommand(param => this.SaveGraph());
            this.ExitCommand = new RelayCommand(param => App.Current.Shutdown());

            this.AlgorithmMenuItems = AlgorithmsMenuBuilder.GetMenuItems(this.GraphViewModel, messageHandler);
        }

        /// <summary>
        /// Gets or sets the file selector.
        /// </summary>
        public IFileSelector FileSelector { get; set; }

        /// <summary>
        /// Gets or sets the message handler.
        /// </summary>
        public IMessageHandler MessageHandler { get; set; }

        /// <summary>
        /// Gets the load graph command.
        /// </summary>
        public ICommand LoadGraphCommand { get; private set; }

        /// <summary>
        /// Gets the save graph command.
        /// </summary>
        public ICommand SaveGraphCommand { get; private set; }

        /// <summary>
        /// Gets the exit command.
        /// </summary>
        public ICommand ExitCommand { get; private set; }

        /// <summary>
        /// Gets the graph view model.
        /// </summary>
        public IGraphProvider GraphViewModel { get; private set; }

        /// <summary>
        /// Gets the algorithm menu items.
        /// </summary>
        public IEnumerable<MenuItemViewModel> AlgorithmMenuItems { get; private set; }

        /// <summary>
        /// Loads a graph from a file.
        /// </summary>
        private void LoadGraph()
        {
            string path = this.FileSelector.GetFileNameForOpening();

            if (!string.IsNullOrEmpty(path))
            {
                try
                {
                    var graph = GraphSerializer.ReadFromXmlFile(path);
                    this.GraphViewModel.Graph = graph;
                }
                catch (GraphSerializationException ex)
                {
                    Logger.Error("Loading graph form path '" + path + "' failed.", ex);
                    this.MessageHandler.ShowError(ex.Message);
                }
            }
        }

        /// <summary>
        /// Saves a graph to a file.
        /// </summary>
        private void SaveGraph()
        {
            string path = this.FileSelector.GetFileNameForSaving();

            if (!string.IsNullOrEmpty(path))
            {
                try
                {
                    var graph = this.GraphViewModel.Graph;
                    GraphSerializer.SaveAsXmlFile(graph, path);
                }
                catch (GraphSerializationException ex)
                {
                    Logger.Error("Saving graph to path '" + path + "' failed.", ex);
                    this.MessageHandler.ShowError(ex.Message);
                }
            }
        }
    }
}
