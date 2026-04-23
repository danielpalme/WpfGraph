namespace Palmmedia.WpfGraph.Core.Test
{
    public class GraphTest
    {
        private Graph<object, object> graph1;
        private Graph<object, object> graph2;

        private Node<object, object> node1;
        private Node<object, object> node2;
        private Node<object, object> node3;
        private Node<object, object> node4;
        private Node<object, object> node5;

        private Edge<object, object> edge1;
        private Edge<object, object> edge2;
        private Edge<object, object> edge3;
        private Edge<object, object> edge4;
        private Edge<object, object> edge5;

        public GraphTest()
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
            this.graph2.Add(this.edge1);
            this.graph2.Add(this.edge2);
            this.graph2.Add(this.edge3);
            this.graph2.Add(this.edge4);
            this.graph2.Add(this.edge5);
        }

        #region Adding/Removing nodes
        [Fact]
        public void Add_GraphContainsNodeAfterAdding()
        {
            this.graph1.Add(this.node1);

            Assert.Contains(this.node1, this.graph1.Nodes);
        }

        [Fact]
        public void Add_AddingNodeRaisesEvent()
        {
            EventArgs? args = null;
            int counter = 0;
            this.graph1.NodeAdded += new EventHandler<NodeEventArgs<object, object>>((sender, e) =>
            {
                counter++;
                args = e;
            });

            this.graph1.Add(this.node1);

            Assert.Equal(1, counter);
            Assert.NotNull(args);
        }

        [Fact]
        public void Remove_GraphNotContainsNodeAfterRemoving()
        {
            this.graph1.Add(this.node1);

            this.graph1.Remove(this.node1);

            Assert.DoesNotContain(this.node1, this.graph1.Nodes);
        }

        [Fact]
        public void Remove_GraphNotContainsEdgeAfterRemovingNode()
        {
            this.graph1.Add(this.node1);
            this.graph1.Add(this.node2);
            this.graph1.Add(this.edge1);

            Assert.Contains(this.edge1, this.graph1.Edges);

            this.graph1.Remove(this.node1);

            Assert.DoesNotContain(this.edge1, this.graph1.Edges);
            Assert.DoesNotContain(this.node1, this.graph1.Nodes);
        }

        [Fact]
        public void Remove_RemovingNodeRaisesEvent()
        {
            this.graph1.Add(this.node1);
            EventArgs? args = null;
            int counter = 0;
            this.graph1.NodeRemoved += new EventHandler<NodeEventArgs<object, object>>((sender, e) =>
            {
                counter++;
                args = e;
            });

            this.graph1.Remove(this.node1);

            Assert.Equal(1, counter);
            Assert.NotNull(args);
        }
        #endregion

        #region Adding/Removing edge
        [Fact]
        public void Add_GraphContainsEdgeAfterAdding()
        {
            var edge = new Edge<object, object>(this.node1, this.node2);
            this.graph1.Add(this.node1);
            this.graph1.Add(this.node2);

            this.graph1.Add(edge);

            Assert.Contains(edge, this.graph1.Edges);
        }

        [Fact]
        public void Add_AddingEdgeRaisesEvent()
        {
            EventArgs? args = null;
            int counter = 0;
            this.graph1.EdgeAdded += new EventHandler<EdgeEventArgs<object, object>>((sender, e) =>
            {
                counter++;
                args = e;
            });
            var edge = new Edge<object, object>(this.node1, this.node2);

            this.graph1.Add(this.node1);
            this.graph1.Add(this.node2);
            this.graph1.Add(edge);

            Assert.Equal(1, counter);
            Assert.NotNull(args);
        }

        [Fact]
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

            Assert.Contains(edge, this.graph1.Edges);
        }

        [Fact]
        public void Remove_GraphNotContainsEdgeAfterRemoving()
        {
            var edge = new Edge<object, object>(this.node1, this.node2);
            this.graph1.Add(this.node1);
            this.graph1.Add(this.node2);
            this.graph1.Add(edge);

            this.graph1.Remove(edge);

            Assert.DoesNotContain(edge, this.graph1.Edges);
        }

        [Fact]
        public void Remove_RemovingEdgeRaisesEvent()
        {
            var edge = new Edge<object, object>(this.node1, this.node2);
            this.graph1.Add(this.node1);
            this.graph1.Add(this.node2);
            this.graph1.Add(edge);
            EventArgs? args = null;
            int counter = 0;
            this.graph1.EdgeRemoved += new EventHandler<EdgeEventArgs<object, object>>((sender, e) =>
            {
                counter++;
                args = e;
            });

            this.graph1.Remove(edge);

            Assert.Equal(1, counter);
            Assert.NotNull(args);
        }
        #endregion

        #region Clear
        [Fact]
        public void Clear_GraphIsEmptyAfterClear()
        {
            this.graph2.Clear();

            Assert.Empty(this.graph2.Nodes);
            Assert.Empty(this.graph2.Edges);
        }
        #endregion

        #region Edges
        [Fact]
        public void GetEdgesOfNode_ReturnsCorrectEdges()
        {
            var edges = this.graph2.GetEdgesOfNode(this.node1);

            Assert.Equal(5, edges.Count());
            Assert.Contains(this.edge1, edges);
            Assert.Contains(this.edge2, edges);
            Assert.Contains(this.edge3, edges);
            Assert.Contains(this.edge4, edges);
            Assert.Contains(this.edge5, edges);

            edges = this.graph2.GetEdgesOfNode(this.node2);
            Assert.Equal(3, edges.Count());
            Assert.Contains(this.edge1, edges);
            Assert.Contains(this.edge4, edges);
            Assert.Contains(this.edge5, edges);

            edges = this.graph2.GetEdgesOfNode(this.node3);
            Assert.Single(edges);
            Assert.Contains(this.edge2, edges);

            edges = this.graph2.GetEdgesOfNode(this.node4);
            Assert.Empty(edges);
        }

        [Fact]
        public void GetIncomingEdgesOfNode_ReturnsCorrectEdges()
        {
            var edges = this.graph2.GetIncomingEdgesOfNode(this.node1);

            Assert.Equal(4, edges.Count());
            Assert.Contains(this.edge1, edges);
            Assert.Contains(this.edge2, edges);
            Assert.Contains(this.edge3, edges);
            Assert.Contains(this.edge5, edges);

            edges = this.graph2.GetIncomingEdgesOfNode(this.node2);
            Assert.Equal(2, edges.Count());
            Assert.Contains(this.edge1, edges);
            Assert.Contains(this.edge4, edges);

            edges = this.graph2.GetIncomingEdgesOfNode(this.node3);
            Assert.Single(edges);
            Assert.Contains(this.edge2, edges);

            edges = this.graph2.GetIncomingEdgesOfNode(this.node4);
            Assert.Empty(edges);
        }

        [Fact]
        public void GetOutgoingEdgesOfNode_ReturnsCorrectEdges()
        {
            var edges = this.graph2.GetOutgoingEdgesOfNode(this.node1);

            Assert.Equal(4, edges.Count());
            Assert.Contains(this.edge1, edges);
            Assert.Contains(this.edge2, edges);
            Assert.Contains(this.edge3, edges);
            Assert.Contains(this.edge4, edges);

            edges = this.graph2.GetOutgoingEdgesOfNode(this.node2);
            Assert.Equal(2, edges.Count());
            Assert.Contains(this.edge1, edges);
            Assert.Contains(this.edge5, edges);

            edges = this.graph2.GetOutgoingEdgesOfNode(this.node3);
            Assert.Single(edges);
            Assert.Contains(this.edge2, edges);

            edges = this.graph2.GetOutgoingEdgesOfNode(this.node4);
            Assert.Empty(edges);
        }

        [Fact]
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
        [Fact]
        public void GetNeighborsOfNode_ReturnsCorrectNodes()
        {
            var nodes = this.graph2.GetNeighborsOfNode(this.node1);

            Assert.Equal(3, nodes.Count());
            Assert.Contains(this.node1, nodes);
            Assert.Contains(this.node2, nodes);
            Assert.Contains(this.node3, nodes);

            nodes = this.graph2.GetNeighborsOfNode(this.node2);
            Assert.Single(nodes);
            Assert.Contains(this.node1, nodes);

            nodes = this.graph2.GetNeighborsOfNode(this.node3);
            Assert.Single(nodes);
            Assert.Contains(this.node1, nodes);

            nodes = this.graph2.GetNeighborsOfNode(this.node4);
            Assert.Empty(nodes);
        }

        [Fact]
        public void GetIncomingNeighborsOfNode_ReturnsCorrectNodes()
        {
            var nodes = this.graph2.GetIncomingNeighborsOfNode(this.node1);

            Assert.Equal(3, nodes.Count());
            Assert.Contains(this.node1, nodes);
            Assert.Contains(this.node2, nodes);
            Assert.Contains(this.node3, nodes);

            nodes = this.graph2.GetIncomingNeighborsOfNode(this.node2);
            Assert.Single(nodes);
            Assert.Contains(this.node1, nodes);

            nodes = this.graph2.GetIncomingNeighborsOfNode(this.node3);
            Assert.Single(nodes);
            Assert.Contains(this.node1, nodes);

            nodes = this.graph2.GetIncomingNeighborsOfNode(this.node4);
            Assert.Empty(nodes);
        }

        [Fact]
        public void GetOutgoingNeighborsOfNode_ReturnsCorrectNodes()
        {
            var nodes = this.graph2.GetOutgoingNeighborsOfNode(this.node1);

            Assert.Equal(3, nodes.Count());
            Assert.Contains(this.node1, nodes);
            Assert.Contains(this.node2, nodes);
            Assert.Contains(this.node3, nodes);

            nodes = this.graph2.GetOutgoingNeighborsOfNode(this.node2);
            Assert.Single(nodes);
            Assert.Contains(this.node1, nodes);

            nodes = this.graph2.GetOutgoingNeighborsOfNode(this.node3);
            Assert.Single(nodes);
            Assert.Contains(this.node1, nodes);

            nodes = this.graph2.GetOutgoingNeighborsOfNode(this.node4);
            Assert.Empty(nodes);

            this.edge1 = new Edge<object, object>(this.node1, this.node2);
            this.edge2 = new Edge<object, object>(this.node1, this.node3);
            this.edge3 = new Edge<object, object>(this.node1, this.node1);
            this.edge4 = new Edge<object, object>(this.node1, this.node2, EdgeDirection.First2Second);
            this.edge5 = new Edge<object, object>(this.node1, this.node2, EdgeDirection.Second2First);
        }

        [Fact]
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