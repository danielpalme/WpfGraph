# WpfGraph
WPFGraph is a tool to create animations of graph algorithms using a WPF based 3D rendering engine.
You can create a graph by adding nodes and edges to the UI simply by using your mouse.
Then you can execute a graph algorithm like Dijkstra on the created graph.

Additional information about the implementation can be found under [Resources](#resources).

Author: Daniel Palme  
Blog: [www.palmmedia.de](http://www.palmmedia.de)  
Twitter: [@danielpalme](http://twitter.com/danielpalme)  

## Sample Animation

Watch a video of a sample animation:
[![Sample animation](http://img.youtube.com/vi/wQJBQB-Ajdc/0.jpg)](http://www.youtube.com/watch?v=wQJBQB-Ajdc "Sample animation")

## Usage

* By clicking on an empty area, a new node is created
* By clicking on two nodes within one second (not necessarily different nodes), a new edge is created
* By clicking at a node or edge, its properties can be edited in the panel on the right
* All graph algorithms can be executed by selecting the corresponding entry in the menu

## Implement custom graph algorithms
The application already contains several graph algorithms like Dijkstra or Kruskal. To create your own algorithms, add a new class to the solution and implement the interface IGraphAlgorithm. After recompiling the solution, your algorithm will be listed in the menu.

Creating animations is quite simple, some extension methods make things even easier. In the following example, we add two nodes to the graph, then we flash the two nodes for 3 seconds. After the flashing is finished, both nodes are moved to another position.

```csharp
public void Execute(IGraph<NodeData, EdgeData> graph)
{
    graph.Clear();

    this.node1 = graph.AddNode(new NodeData(new Point3D(0, 25, 0)));
    this.node2 = graph.AddNode(new NodeData(new Point3D(0, -25, 0)));

    this.graph.AddEdge(this.node1, this.node2);

    this.node1.Blink(3000);
    this.node2.Blink(3000, this.Callback);
}

private void Callback()
{
    this.node1.Move(new Point3D(25, 0, 0), 4000);
    this.node2.Move(new Point3D(-25, 0, 0), 4000);
}
```

## Resources

* http://www.codeproject.com/KB/WPF/WPFGraphAnimation.aspx
* http://www.palmmedia.de/Blog/2009/12/29/wpf-animation-of-graph-algorithms-part-1
* http://www.palmmedia.de/Blog/2009/12/29/wpf-animation-of-graph-algorithms-part-2
