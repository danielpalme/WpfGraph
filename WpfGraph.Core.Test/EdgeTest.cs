namespace Palmmedia.WpfGraph.Core.Test
{
    public class EdgeTest
    {
        [Fact]
        public void ConstructorSetsData()
        {
            var node1 = new Node<object, object>();
            var node2 = new Node<object, object>();
            var obj = new object();

            var edge = new Edge<object, object>(node1, node2);
            Assert.Null(edge.Data);
            Assert.Equal(node1, edge.FirstNode);
            Assert.Equal(node2, edge.SecondNode);
            Assert.Equal(EdgeDirection.OmniDirectional, edge.EdgeDirection);

            edge = new Edge<object, object>(node1, node2, obj);
            Assert.Equal(obj, edge.Data);
            Assert.Equal(node1, edge.FirstNode);
            Assert.Equal(node2, edge.SecondNode);
            Assert.Equal(EdgeDirection.OmniDirectional, edge.EdgeDirection);

            edge = new Edge<object, object>(node1, node2, EdgeDirection.First2Second);
            Assert.Null(edge.Data);
            Assert.Equal(node1, edge.FirstNode);
            Assert.Equal(node2, edge.SecondNode);
            Assert.Equal(EdgeDirection.First2Second, edge.EdgeDirection);

            edge = new Edge<object, object>(node1, node2, EdgeDirection.First2Second, obj);
            Assert.Equal(obj, edge.Data);
            Assert.Equal(node1, edge.FirstNode);
            Assert.Equal(node2, edge.SecondNode);
            Assert.Equal(EdgeDirection.First2Second, edge.EdgeDirection);
        }

        [Fact]
        public void Data_RaisesEvent()
        {
            var node1 = new Node<object, object>();
            var node2 = new Node<object, object>();
            var edge = new Edge<object, object>(node1, node2);
            var obj = new object();
            EventArgs? args = null;
            int counter = 0;
            edge.DataChanged += new EventHandler<EventArgs>((sender, e) =>
            {
                counter++;
                args = e;
            });

            edge.Data = obj;

            Assert.Equal(1, counter);
            Assert.NotNull(args);
            Assert.Equal(obj, edge.Data);
        }
    }
}
