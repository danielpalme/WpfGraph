using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Xml.Linq;
using Palmmedia.WpfGraph.Common;
using Palmmedia.WpfGraph.Core;
using Palmmedia.WpfGraph.UI.ViewModels;

namespace Palmmedia.WpfGraph.UI.IO
{
    /// <summary>
    /// Helper class to serialize/deserialize <see cref="IGraph&lt;NodeData, EdgeData&gt;">graphs</see>.
    /// </summary>
    internal static class GraphSerializer
    {
        /// <summary>
        /// Saves the given graph as XML file.
        /// </summary>
        /// <param name="graph">The graph.</param>
        /// <param name="fileName">The name of the file.</param>
        public static void SaveAsXmlFile(IGraph<NodeData, EdgeData> graph, string fileName)
        {
            if (graph == null)
            {
                throw new ArgumentNullException("graph");
            }

            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("The name of the file must not be empty.");
            }

            if (graph.Nodes.Count(n => n.Data == null) > 0 || graph.Edges.Count(e => e.Data == null) > 0)
            {
                // This can only happen if a graph should be persisted, which was not displayed in the UI yet.
                throw new InvalidOperationException("Graph was not displayed yet.");
            }

            int idCounter = 0;
            var nodesDictionary = graph.Nodes.ToDictionary(n => n, n => idCounter++);

            var nodesElement = new XElement("nodes");
            var edgesElement = new XElement("edges");

            foreach (var node in graph.Nodes)
            {
                nodesElement.Add(new XElement(
                    "node",
                    new XAttribute("id", nodesDictionary[node].ToString(CultureInfo.InvariantCulture)),
                    new XAttribute("color", node.Data.Color.ToString(CultureInfo.InvariantCulture)),
                    new XAttribute("marked", node.Data.Marked.ToString(CultureInfo.InvariantCulture)),
                    new XAttribute("position", node.Data.Position.ToString(CultureInfo.InvariantCulture).Replace(';', ',')),
                    new XAttribute("text", node.Data.Text)));
            }

            foreach (var edge in graph.Edges)
            {
                edgesElement.Add(new XElement(
                    "edge", 
                    new XAttribute("firstnode", nodesDictionary[edge.FirstNode].ToString(CultureInfo.InvariantCulture)),
                    new XAttribute("secondnode", nodesDictionary[edge.SecondNode].ToString(CultureInfo.InvariantCulture)),
                    new XAttribute("color", edge.Data.Color.ToString(CultureInfo.InvariantCulture)),
                    new XAttribute("direction", edge.EdgeDirection),
                    new XAttribute("marked", edge.Data.Marked.ToString(CultureInfo.InvariantCulture)),
                    new XAttribute("weight", edge.Data.Weight.ToString(CultureInfo.InvariantCulture))));
            }

            var document = new XDocument();
            document.Declaration = new XDeclaration("1.0", "UTF-8", "yes");
            document.Add(new XElement(
                "graph", 
                new XAttribute("version", "1.0"),
                nodesElement,
                edgesElement));

            try
            {
                document.Save(fileName);
            }
            catch (Exception ex)
            {
                throw new GraphSerializationException(string.Format(CultureInfo.CurrentCulture, Palmmedia.WpfGraph.UI.Properties.Resources.SavingFailed, ex.Message), ex);
            }            
        }

        /// <summary>
        /// Reads a graph from a XML file.
        /// </summary>
        /// <param name="fileName">The name of the file.</param>
        /// <returns>The <see cref="IGraph&lt;NodeData, EdgeData&gt;">graphs</see>.</returns>
        public static IGraph<NodeData, EdgeData> ReadFromXmlFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("The name of the file must not be empty.");
            }

            try
            {
                var document = XDocument.Load(fileName);

                var graph = new Graph<NodeData, EdgeData>();
                var nodesDictionary = new Dictionary<int, Node<NodeData, EdgeData>>();

                foreach (var nodeElement in document.Root.Element("nodes").Descendants())
                {
                    var nodeData = new NodeData(Point3D.Parse(nodeElement.Attribute("position").Value));
                    nodeData.Color = ColorFromHexString(nodeElement.Attribute("color").Value);
                    nodeData.Text = nodeElement.Attribute("text").Value;
                    nodeData.Marked = bool.Parse(nodeElement.Attribute("marked").Value);
                    
                    var node = new Node<NodeData, EdgeData>(nodeData);
                    nodesDictionary.Add(int.Parse(nodeElement.Attribute("id").Value, CultureInfo.InvariantCulture), node);
                    graph.Add(node);
                }

                foreach (var edgeElement in document.Root.Element("edges").Descendants())
                {
                    var edgeData = new EdgeData();
                    edgeData.Color = ColorFromHexString(edgeElement.Attribute("color").Value);
                    edgeData.Weight = double.Parse(edgeElement.Attribute("weight").Value, CultureInfo.InvariantCulture);
                    edgeData.Marked = bool.Parse(edgeElement.Attribute("marked").Value);

                    var firstNode = nodesDictionary[int.Parse(edgeElement.Attribute("firstnode").Value, CultureInfo.InvariantCulture)];
                    var secondNode = nodesDictionary[int.Parse(edgeElement.Attribute("secondnode").Value, CultureInfo.InvariantCulture)];
                    var edgeDirection = (EdgeDirection)Enum.Parse(typeof(EdgeDirection), edgeElement.Attribute("direction").Value);

                    var edge = new Edge<NodeData, EdgeData>(firstNode, secondNode, edgeDirection, edgeData);
                    graph.Add(edge);
                }

                return graph;
            }
            catch (Exception ex)
            {
                throw new GraphSerializationException(string.Format(CultureInfo.CurrentCulture, Palmmedia.WpfGraph.UI.Properties.Resources.LoadingFailed, ex.Message), ex);
            } 
        }

        /// <summary>
        /// Converts a hex string into a <see cref="Color"/>.
        /// </summary>
        /// <param name="hexColor">The color as hex string (e.g. '#FFCC0000').</param>
        /// <returns>The <see cref="Color"/>.</returns>
        private static Color ColorFromHexString(string hexColor)
        {
            byte alpha = byte.Parse(hexColor.Substring(1, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            byte red = byte.Parse(hexColor.Substring(3, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            byte green = byte.Parse(hexColor.Substring(5, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            byte blue = byte.Parse(hexColor.Substring(7, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);

            return Color.FromArgb(alpha, red, green, blue);
        }
    }
}
