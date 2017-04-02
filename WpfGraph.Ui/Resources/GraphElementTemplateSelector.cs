using System.Windows;
using System.Windows.Controls;
using Palmmedia.WpfGraph.Core;
using Palmmedia.WpfGraph.UI.ViewModels;

namespace Palmmedia.WpfGraph.UI.Resources
{
    /// <summary>
    /// Helper class to select <see cref="T:System.Windows.DataTemplate"/> according to currently selected node or edge.
    /// Normally this is done simply by specifing a type in XAML, but since generics are not supported this approach is used as a workaround.
    /// </summary>
    public class GraphElementTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// When overridden in a derived class, returns a <see cref="T:System.Windows.DataTemplate"/> based on custom logic.
        /// </summary>
        /// <param name="item">The data object for which to select the template.</param>
        /// <param name="container">The data-bound object.</param>
        /// <returns>
        /// Returns a <see cref="T:System.Windows.DataTemplate"/> or null. The default value is null.
        /// </returns>
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item == null)
            {
                return null;
            }

            var window = App.Current.MainWindow;

            if (item is Edge<NodeData, EdgeData>)
            {
                return window.FindResource("EdgeDataTemplate") as DataTemplate;
            }
            else
            {
                return window.FindResource("NodeDataTemplate") as DataTemplate;
            }
        }
    }
}
