using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using Palmmedia.WpfGraph.Core;
using Palmmedia.WpfGraph.UI.Interaction;
using Palmmedia.WpfGraph.UI.IO;
using Palmmedia.WpfGraph.UI.ViewModels;

namespace Palmmedia.WpfGraph.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Logger instance.
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(App));

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Application.Startup"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.StartupEventArgs"/> that contains the event data.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            Thread.CurrentThread.Name = "MainThread";

            base.OnStartup(e);

            // Init logger
            log4net.Config.XmlConfigurator.Configure();

            // Register handler for unhandled exceptions
            this.DispatcherUnhandledException += new System.Windows.Threading.DispatcherUnhandledExceptionEventHandler(this.App_DispatcherUnhandledException);

            Logger.Debug("Initializing main window.");
            var graphProvider = InitializeGraphProvider();
            var mainWindow = new MainWindow(graphProvider);
            graphProvider.Container = mainWindow.container;

            this.MainWindow = mainWindow;

            this.MainWindow.DataContext = new MainWindowViewModel(
                graphProvider,
                new FormFileSelector(),
                new FormMessageHandler());

            this.MainWindow.Show();
        }

        /// <summary>
        /// Initializes the <see cref="IGraphProvider"/>.
        /// </summary>
        /// <returns>The <see cref="IGraphProvider"/>.</returns>
        private static IGraphProvider InitializeGraphProvider()
        {
            var graphProvider = new GraphViewModel(new Graph<NodeData, EdgeData>());

            var graph = LoadGraph();
            if (graph != null)
            {
                graphProvider.Graph = graph;
            }

            return graphProvider;
        }

        /// <summary>
        /// Loads a graph from a file if path is supplied as argument.
        /// </summary>
        /// <returns><c>null</c> if no path is supplied, otherwise the graph.</returns>
        private static IGraph<NodeData, EdgeData> LoadGraph()
        {
            var path = Environment.GetCommandLineArgs().ElementAtOrDefault(1);

            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                return null;
            }
            else
            {
                try
                {
                    var graph = GraphSerializer.ReadFromXmlFile(path);
                    Logger.Debug("Loaded graph from path: " + path);
                    return graph;
                }
                catch (GraphSerializationException ex)
                {
                    Logger.Error("Loading graph form path '" + path + "' failed.", ex);
                    MessageBox.Show(ex.Message, Palmmedia.WpfGraph.UI.Properties.Resources.Error);
                    return null;
                }
            }
        }

        /// <summary>
        /// Handles the DispatcherUnhandledException event of the Application.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Threading.DispatcherUnhandledExceptionEventArgs"/> instance containing the event data.</param>
        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Logger.Error("Unhandled exception occured.", e.Exception);
            MessageBox.Show(string.Format(CultureInfo.CurrentCulture, Palmmedia.WpfGraph.UI.Properties.Resources.UnhandledException, e.Exception.Message), Palmmedia.WpfGraph.UI.Properties.Resources.Error);
            e.Handled = true;
        }
    }
}
