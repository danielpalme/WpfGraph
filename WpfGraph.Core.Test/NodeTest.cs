namespace Palmmedia.WpfGraph.Core.Test
{
    public class NodeTest
    {
        [Fact]
        public void ConstructorSetsData()
        {
            var obj = new object();

            var node = new Node<object, object>();
            Assert.Null(node.Data);

            node = new Node<object, object>(obj);
            Assert.Equal(obj, node.Data);
        }

        [Fact]
        public void Data_RaisesEvent()
        {
            var node = new Node<object, object>();
            var obj = new object();
            EventArgs? args = null;
            int counter = 0;
            node.DataChanged += new EventHandler<EventArgs>((sender, e) =>
            {
                counter++;
                args = e;
            });

            node.Data = obj;

            Assert.Equal(1, counter);
            Assert.NotNull(args);
            Assert.Equal(obj, node.Data);
        }
    }
}
