using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Palmmedia.WpfGraph.Core;

namespace Palmmedia.WpfGraph.CoreTest
{
    [TestClass]
    public class GraphTest
    {
        private IGraph<object, object> graph1, graph2;

        private Node<object, object> node1, node2, node3, node4, node5;

        private Edge<object, object> edge1, edge2, edge3, edge4, edge5;

        [TestInitialize()]
        public void MyTestInitialize()
        {
            this.graph1 = new Graph<object, object>();
            this.graph2 = new Graph<object, object>();

            this.node1 = new Node<object, object>();
            this.node2 = new Node<object, object>();
            this.node3 = new Node<object, object>();
            this.node4 = new Node<object, object>();
            this.node5 = new Node<object, object>();

            this.edge1 = new Edge<object, object>(this.node1, this.node2);
            this.edge2 = new Edge<object, object>(this.node1, this.node3);
            this.edge3 = new Edge<object, object>(this.node1, this.node1);
            this.edge4 = new Edge<object, object>(this.node1, this.node2, EdgeDirection.First2Second);
            this.edge5 = new Edge<object, object>(this.node1, this.node2, EdgeDirection.Second2First);

            this.graph2.Add(this.node1);
            this.graph2.Add(this.node2);
            this.graph2.Add(this.node3);
            this.graph2.Add(this.node4);
            this.graph2.Add(edge1);
            this.graph2.Add(edge2);
            this.graph2.Add(edge3);
            this.graph2.Add(edge4);
            this.graph2.Add(edge5);
        }

        #region Adding/Removing nodes
        [TestMethod]
        public void Add_GraphContainsNodeAfterAdding()
        {
            this.graph1.Add(this.node1);

            Assert.IsTrue(this.graph1.Nodes.Contains(this.node1), "Graph does not contain node.");
        }

        [TestMethod]
        public void Add_AddingNodeRaisesEvent()
        {
            EventArgs args = null;
            int counter = 0;
            this.graph1.NodeAdded += new EventHandler<NodeEventArgs<object, object>>((sender, e) => { counter++; args = e; });

            this.graph1.Add(this.node1);

            Assert.AreEqual(1, counter, "Event not raised.");
            Assert.IsNotNull(args, "EventArgs must not be null.");
        }

        [TestMethod]
        public void Remove_GraphNotContainsNodeAfterRemoving()
        {
            this.graph1.Add(this.node1);

            this.graph1.Remove(this.node1);

            Assert.IsFalse(this.graph1.Nodes.Contains(this.node1), "Graph does contain node.");
        }

        [TestMethod]
        public void Remove_GraphNotContainsEdgeAfterRemovingNode()
        {
            this.graph1.Add(this.node1);
            this.graph1.Add(this.node2);
            this.graph1.Add(this.edge1);

            Assert.IsTrue(this.graph1.Edges.Contains(this.edge1), "Graph does not contain edge.");

            this.graph1.Remove(this.node1);

            Assert.IsFalse(this.graph1.Edges.Contains(this.edge1), "Graph does still contain edge.");
            Assert.IsFalse(this.graph1.Nodes.Contains(this.node1), "Graph does contain node.");
        }

        [TestMethod]
        public void Remove_RemovingNodeRaisesEvent()
        {
            this.graph1.Add(this.node1);
            EventArgs args = null;
            int counter = 0;
            this.graph1.NodeRemoved += new EventHandler<NodeEventArgs<object, object>>((sender, e) => { counter++; args = e; });

            this.graph1.Remove(this.node1);

            Assert.AreEqual(1, counter, "Event not raised.");
            Assert.IsNotNull(args, "EventArgs must not be null.");
        }
        #endregion

        #region Adding/Removing edge
        [TestMethod]
        public void Add_GraphContainsEdgeAfterAdding()
        {
            var edge = new Edge<object, object>(this.node1, this.node2);
            this.graph1.Add(this.node1);
            this.graph1.Add(this.node2);

            this.graph1.Add(edge);

            Assert.IsTrue(this.graph1.Edges.Contains(edge), "Graph does not contain edge.");
        }

        [TestMethod]
        public void Add_AddingEdgeRaisesEvent()
        {
            EventArgs args = null;
            int counter = 0;
            this.graph1.EdgeAdded += new EventHandler<EdgeEventArgs<object, object>>((sender, e) => { counter++; args = e; });
            var edge = new Edge<object, object>(this.node1, this.node2);

            this.graph1.Add(this.node1);
            this.graph1.Add(this.node2);
            this.graph1.Add(edge);

            Assert.AreEqual(1, counter, "Event not raised.");
            Assert.IsNotNull(args, "EventArgs must not be null.");
        }

        [TestMethod]
        public void Add_AddingEdgeWithoutAddingNodesFails()
        {
            var edge = new Edge<object, object>(this.node1, this.node2);

            try
            {
                this.graph1.Add(edge);
                Assert.Fail("Exception expected.");
            }
            catch (InvalidOperationException)
            {
            }

            this.graph1.Add(this.node1);

            try
            {
                this.graph1.Add(edge);
                Assert.Fail("Exception expected.");
            }
            catch (InvalidOperationException)
            {
            }

            this.graph1.Add(this.node2);
            this.graph1.Add(edge);

            Assert.IsTrue(this.graph1.Edges.Contains(edge), "Graph does not contain edge.");
        }

        [TestMethod]
        public void Remove_GraphNotContainsEdgeAfterRemoving()
        {
            var edge = new Edge<object, object>(this.node1, this.node2);
            this.graph1.Add(this.node1);
            this.graph1.Add(this.node2);
            this.graph1.Add(edge);

            this.graph1.Remove(edge);

            Assert.IsFalse(this.graph1.Edges.Contains(edge), "Graph does contain edge.");
        }

        [TestMethod]
        public void Remove_RemovingEdgeRaisesEvent()
        {
            var edge = new Edge<object, object>(this.node1, this.node2);
            this.graph1.Add(this.node1);
            this.graph1.Add(this.node2);
            this.graph1.Add(edge);
            EventArgs args = null;
            int counter = 0;
            this.graph1.EdgeRemoved += new EventHandler<EdgeEventArgs<object, object>>((sender, e) => { counter++; args = e; });

            this.graph1.Remove(edge);

            Assert.AreEqual(1, counter, "Event not raised.");
            Assert.IsNotNull(args, "EventArgs must not be null.");
        }
        #endregion

        #region Clear
        [TestMethod]
        public void Clear_GraphIsEmptyAfterClear()
        {
            this.graph2.Clear();

            Assert.AreEqual(0, this.graph2.Nodes.Count(), "Graph still contains nodes.");
            Assert.AreEqual(0, this.graph2.Edges.Count(), "Graph still contains edges.");
        }
        #endregion

        #region Edges
        [TestMethod]
        public void GetEdgesOfNode_ReturnsCorrectEdges()
        {
            var edges = this.graph2.GetEdgesOfNode(this.node1);

            Assert.AreEqual(5, edges.Count(), "Invalid number of edges.");
            Assert.IsTrue(edges.Contains(edge1), "Graph does not contain edge.");
            Assert.IsTrue(edges.Contains(edge2), "Graph does not contain edge.");
            Assert.IsTrue(edges.Contains(edge3), "Graph does not contain edge.");
            Assert.IsTrue(edges.Contains(edge4), "Graph does not contain edge.");
            Assert.IsTrue(edges.Contains(edge5), "Graph does not contain edge.");

            edges = this.graph2.GetEdgesOfNode(this.node2);
            Assert.AreEqual(3, edges.Count(), "Invalid number of edges.");
            Assert.IsTrue(edges.Contains(edge1), "Graph does not contain edge.");
            Assert.IsTrue(edges.Contains(edge4), "Graph does not contain edge.");
            Assert.IsTrue(edges.Contains(edge5), "Graph does not contain edge.");

            edges = this.graph2.GetEdgesOfNode(this.node3);
            Assert.AreEqual(1, edges.Count(), "Invalid number of edges.");
            Assert.IsTrue(edges.Contains(edge2), "Graph does not contain edge.");

            edges = this.graph2.GetEdgesOfNode(this.node4);
            Assert.AreEqual(0, edges.Count(), "Invalid number of edges.");
        }

        [TestMethod]
        public void GetIncomingEdgesOfNode_ReturnsCorrectEdges()
        {
            var edges = this.graph2.GetIncomingEdgesOfNode(this.node1);

            Assert.AreEqual(4, edges.Count(), "Invalid number of edges.");
            Assert.IsTrue(edges.Contains(edge1), "Graph does not contain edge.");
            Assert.IsTrue(edges.Contains(edge2), "Graph does not contain edge.");
            Assert.IsTrue(edges.Contains(edge3), "Graph does not contain edge.");
            Assert.IsTrue(edges.Contains(edge5), "Graph does not contain edge.");

            edges = this.graph2.GetIncomingEdgesOfNode(this.node2);
            Assert.AreEqual(2, edges.Count(), "Invalid number of edges.");
            Assert.IsTrue(edges.Contains(edge1), "Graph does not contain edge.");
            Assert.IsTrue(edges.Contains(edge4), "Graph does not contain edge.");

            edges = this.graph2.GetIncomingEdgesOfNode(this.node3);
            Assert.AreEqual(1, edges.Count(), "Invalid number of edges.");
            Assert.IsTrue(edges.Contains(edge2), "Graph does not contain edge.");

            edges = this.graph2.GetIncomingEdgesOfNode(this.node4);
            Assert.AreEqual(0, edges.Count(), "Invalid number of edges.");
        }

        [TestMethod]
        public void GetOutgoingEdgesOfNode_ReturnsCorrectEdges()
        {
            var edges = this.graph2.GetOutgoingEdgesOfNode(this.node1);

            Assert.AreEqual(4, edges.Count(), "Invalid number of edges.");
            Assert.IsTrue(edges.Contains(edge1), "Graph does not contain edge.");
            Assert.IsTrue(edges.Contains(edge2), "Graph does not contain edge.");
            Assert.IsTrue(edges.Contains(edge3), "Graph does not contain edge.");
            Assert.IsTrue(edges.Contains(edge4), "Graph does not contain edge.");

            edges = this.graph2.GetOutgoingEdgesOfNode(this.node2);
            Assert.AreEqual(2, edges.Count(), "Invalid number of edges.");
            Assert.IsTrue(edges.Contains(edge1), "Graph does not contain edge.");
            Assert.IsTrue(edges.Contains(edge5), "Graph does not contain edge.");

            edges = this.graph2.GetOutgoingEdgesOfNode(this.node3);
            Assert.AreEqual(1, edges.Count(), "Invalid number of edges.");
            Assert.IsTrue(edges.Contains(edge2), "Graph does not contain edge.");

            edges = this.graph2.GetOutgoingEdgesOfNode(this.node4);
            Assert.AreEqual(0, edges.Count(), "Invalid number of edges.");
        }

        [TestMethod]
        public void GetEdgesOfNode_FailsWhenNotAttachedToGraph()
        {
            try
            {
                var edges = this.node5.Edges;
                Assert.Fail("Exception expected.");
            }
            catch (InvalidOperationException)
            {
            }
        }
        #endregion

        #region Neighbors
        [TestMethod]
        public void GetNeighborsOfNode_ReturnsCorrectNodes()
        {
            var nodes = this.graph2.GetNeighborsOfNode(this.node1);

            Assert.AreEqual(3, nodes.Count(), "Invalid number of nodes.");
            Assert.IsTrue(nodes.Contains(node1), "Graph does not contain node.");
            Assert.IsTrue(nodes.Contains(node2), "Graph does not contain node.");
            Assert.IsTrue(nodes.Contains(node3), "Graph does not contain node.");

            nodes = this.graph2.GetNeighborsOfNode(this.node2);
            Assert.AreEqual(1, nodes.Count(), "Invalid number of nodes.");
            Assert.IsTrue(nodes.Contains(node1), "Graph does not contain node.");

            nodes = this.graph2.GetNeighborsOfNode(this.node3);
            Assert.AreEqual(1, nodes.Count(), "Invalid number of nodes.");
            Assert.IsTrue(nodes.Contains(node1), "Graph does not contain node.");

            nodes = this.graph2.GetNeighborsOfNode(this.node4);
            Assert.AreEqual(0, nodes.Count(), "Invalid number of nodes.");
        }

        [TestMethod]
        public void GetIncomingNeighborsOfNode_ReturnsCorrectNodes()
        {
            var nodes = this.graph2.GetIncomingNeighborsOfNode(this.node1);

            Assert.AreEqual(3, nodes.Count(), "Invalid number of nodes.");
            Assert.IsTrue(nodes.Contains(node1), "Graph does not contain node.");
            Assert.IsTrue(nodes.Contains(node2), "Graph does not contain node.");
            Assert.IsTrue(nodes.Contains(node3), "Graph does not contain node.");

            nodes = this.graph2.GetIncomingNeighborsOfNode(this.node2);
            Assert.AreEqual(1, nodes.Count(), "Invalid number of nodes.");
            Assert.IsTrue(nodes.Contains(node1), "Graph does not contain node.");

            nodes = this.graph2.GetIncomingNeighborsOfNode(this.node3);
            Assert.AreEqual(1, nodes.Count(), "Invalid number of nodes.");
            Assert.IsTrue(nodes.Contains(node1), "Graph does not contain node.");

            nodes = this.graph2.GetIncomingNeighborsOfNode(this.node4);
            Assert.AreEqual(0, nodes.Count(), "Invalid number of nodes.");
        }

        [TestMethod]
        public void GetOutgoingNeighborsOfNode_ReturnsCorrectNodes()
        {
            var nodes = this.graph2.GetOutgoingNeighborsOfNode(this.node1);

            Assert.AreEqual(3, nodes.Count(), "Invalid number of nodes.");
            Assert.IsTrue(nodes.Contains(node1), "Graph does not contain node.");
            Assert.IsTrue(nodes.Contains(node2), "Graph does not contain node.");
            Assert.IsTrue(nodes.Contains(node3), "Graph does not contain node.");

            nodes = this.graph2.GetOutgoingNeighborsOfNode(this.node2);
            Assert.AreEqual(1, nodes.Count(), "Invalid number of nodes.");
            Assert.IsTrue(nodes.Contains(node1), "Graph does not contain node.");

            nodes = this.graph2.GetOutgoingNeighborsOfNode(this.node3);
            Assert.AreEqual(1, nodes.Count(), "Invalid number of nodes.");
            Assert.IsTrue(nodes.Contains(node1), "Graph does not contain node.");

            nodes = this.graph2.GetOutgoingNeighborsOfNode(this.node4);
            Assert.AreEqual(0, nodes.Count(), "Invalid number of nodes.");

            this.edge1 = new Edge<object, object>(this.node1, this.node2);
            this.edge2 = new Edge<object, object>(this.node1, this.node3);
            this.edge3 = new Edge<object, object>(this.node1, this.node1);
            this.edge4 = new Edge<object, object>(this.node1, this.node2, EdgeDirection.First2Second);
            this.edge5 = new Edge<object, object>(this.node1, this.node2, EdgeDirection.Second2First);
        }

        [TestMethod]
        public void GetNeighborsOfNode_FailsWhenNotAttachedToGraph()
        {
            try
            {
                var nodes = this.node5.Neighbors;
                Assert.Fail("Exception expected.");
            }
            catch (InvalidOperationException)
            {
            }
        }
        #endregion
    }
}