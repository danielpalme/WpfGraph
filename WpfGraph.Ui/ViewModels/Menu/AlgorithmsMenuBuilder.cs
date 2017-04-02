using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Palmmedia.WpfGraph.UI.Algorithms;
using Palmmedia.WpfGraph.UI.Interaction;

namespace Palmmedia.WpfGraph.UI.ViewModels.Menu
{
    /// <summary>
    /// Helper class to retrieve all implementations of <see cref="IGraphAlgorithm"/> by using reflection.
    /// That way there is no need to change the menu manually after adding new <see cref="IGraphAlgorithm">IAlgorithms</see>.
    /// </summary>
    internal static class AlgorithmsMenuBuilder
    {
        /// <summary>
        /// Gets the menu items.
        /// Retrieves all implementations of <see cref="IGraphAlgorithm"/> from this assembly in a hierarchical structure.
        /// </summary>
        /// <param name="graphProvider">The <see cref="IGraphProvider"/>.</param>
        /// <param name="messageHandler">The <see cref="IMessageHandler"/>.</param>
        /// <returns>The menu items.</returns>
        public static IEnumerable<MenuItemViewModel> GetMenuItems(IGraphProvider graphProvider, IMessageHandler messageHandler)
        {
            var menuItems = new ObservableCollection<MenuItemViewModel>();

            var algorithms = GetTypesByInterface(new Assembly[] { Assembly.GetExecutingAssembly() }, typeof(IGraphAlgorithm));
            var instances = algorithms.Select(a => (IGraphAlgorithm)Activator.CreateInstance(a)).OrderBy(a => a.Category).ThenBy(a => a.Name);

            var categories = instances.Select(a => a.Category).Where(c => c != null).Distinct();

            foreach (var category in categories)
            {
                menuItems.Add(new CategoryMenuItemViewModel(graphProvider, messageHandler, category, instances.Where(a => category.Equals(a.Category))));
            }

            foreach (var algorithm in instances.Where(a => a.Category == null))
            {
                menuItems.Add(new IAlgorithmMenuItemViewModel(graphProvider, messageHandler, null, algorithm));
            }

            return menuItems;
        }

        /// <summary>
        /// Gets all <see cref="Type">Types</see> from the given assemblies implementing the given interface.
        /// </summary>
        /// <param name="assemblies">The assemblies.</param>
        /// <param name="type">The interface.</param>
        /// <returns>All <see cref="Type">Types</see> implementing the given interface.</returns>
        private static IEnumerable<Type> GetTypesByInterface(IEnumerable<Assembly> assemblies, Type type)
        {
            return assemblies.SelectMany(asm => asm.GetTypes().Where(t => t.GetInterface(type.FullName) != null));
        }        
    }
}
