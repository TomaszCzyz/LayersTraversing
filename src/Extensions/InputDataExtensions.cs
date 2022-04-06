using LayersTraversing.Graphs;
using LayersTraversing.Input;

namespace LayersTraversing.Extensions;

public static class InputDataExtensions
{
    public static Graph ToGraph(this InputData inputData)
    {
        var graph = new Graph();
        var abstractVertex = int.MinValue;
    
        graph.AddVertex(abstractVertex);
        foreach (var vertex in inputData.Layers.SelectMany(layer => layer))
        {
            graph.AddVertex(vertex);
        }
    
        var previousLayer = new List<int> {abstractVertex};
        foreach (var nextLayer in inputData.Layers)
        {
            AddEdges(previousLayer, nextLayer, graph);
            previousLayer = nextLayer;
        }
    
        return graph;
    }
    
    private static void AddEdges(List<int> previousLayer, List<int> nextLayer, Graph graph)
    {
        foreach (var v1 in previousLayer)
        {
            foreach (var v2 in nextLayer)
            {
                graph.AddEdge(Tuple.Create(v1, v2));
            }
        }
    }
}