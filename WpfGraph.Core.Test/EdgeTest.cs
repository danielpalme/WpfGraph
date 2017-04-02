using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Palmmedia.WpfGraph.Core;

namespace Palmmedia.WpfGraph.CoreTest
{
    [TestClass]
    public class EdgeTest
    {
        [TestMethod]
        public void ConstructorSetsData()
        {
            var node1 = new Node<object, object>();
            var node2 = new Node<object, object>();            
            var obj = new object();

            var edge = new Edge<object, object>(node1, node2);
            Assert.IsNull(edge.Data, "Data invalid.");
            Assert.AreEqual(node1, edge.FirstNode, "FirstNode invalid.");
            Assert.AreEqual(node2, edge.SecondNode, "SecondNode invalid.");
            Assert.AreEqual(EdgeDirection.OmniDirectional, edge.EdgeDirection, "EdgeDirection invalid.");
            
            edge = new Edge<object, object>(node1, node2, obj);
            Assert.AreEqual(obj, edge.Data, "Data invalid.");
            Assert.AreEqual(node1, edge.FirstNode, "FirstNode invalid.");
            Assert.AreEqual(node2, edge.SecondNode, "SecondNode invalid.");
            Assert.AreEqual(EdgeDirection.OmniDirectional, edge.EdgeDirection, "EdgeDirection invalid.");
            
            edge = new Edge<object, object>(node1, node2, EdgeDirection.First2Second);
            Assert.IsNull(edge.Data, "Data invalid.");
            Assert.AreEqual(node1, edge.FirstNode, "FirstNode invalid.");
            Assert.AreEqual(node2, edge.SecondNode, "SecondNode invalid.");
            Assert.AreEqual(EdgeDirection.First2Second, edge.EdgeDirection, "EdgeDirection invalid.");

            edge = new Edge<object, object>(node1, node2, EdgeDirection.First2Second, obj);
            Assert.AreEqual(obj, edge.Data, "Data invalid.");
            Assert.AreEqual(node1, edge.FirstNode, "FirstNode invalid.");
            Assert.AreEqual(node2, edge.SecondNode, "SecondNode invalid.");
            Assert.AreEqual(EdgeDirection.First2Second, edge.EdgeDirection, "EdgeDirection invalid.");
        }

        [TestMethod]
        public void Data_RaisesEvent()
        {
            var node1 = new Node<object, object>();
            var node2 = new Node<object, object>();
            var edge = new Edge<object, object>(node1, node2);
            var obj = new object();
            EventArgs args = null;
            int counter = 0;
            edge.DataChanged += new EventHandler<EventArgs>((sender, e) => { counter++; args = e; });

            edge.Data = obj;

            Assert.AreEqual(1, counter, "Event not raised.");
            Assert.IsNotNull(args, "EventArgs must not be null.");
            Assert.AreEqual(obj, edge.Data, "Data invalid.");
        }
    }
}
