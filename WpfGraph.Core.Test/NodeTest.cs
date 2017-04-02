using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Palmmedia.WpfGraph.Core;

namespace Palmmedia.WpfGraph.CoreTest
{
    [TestClass]
    public class NodeTest
    {
        [TestMethod]
        public void ConstructorSetsData()
        {            
            var obj = new object();

            var node = new Node<object, object>();
            Assert.IsNull(node.Data, "Data invalid.");
            
            node = new Node<object, object>(obj);
            Assert.AreEqual(obj, node.Data, "Data invalid.");
        }

        [TestMethod]
        public void Data_RaisesEvent()
        {
            var node = new Node<object, object>();
            var obj = new object();
            EventArgs args = null;
            int counter = 0;
            node.DataChanged += new EventHandler<EventArgs>((sender, e) => { counter++; args = e; });

            node.Data = obj;

            Assert.AreEqual(1, counter, "Event not raised.");
            Assert.IsNotNull(args, "EventArgs must not be null.");
            Assert.AreEqual(obj, node.Data, "Data invalid.");
        }
    }
}
