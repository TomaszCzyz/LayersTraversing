using System.Text;

namespace LayersTraversing.Graphs;

public class GraphPath
{
    public List<int> PathVertices { get; } = new();

    public GraphPath()
    {
    }

    public GraphPath(List<int> pathVertices)
    {
        PathVertices = pathVertices;
    }

    public void Add(int vertex)
    {
        PathVertices.Add(vertex);
    }

    public override string ToString()
    {
        return string.Join(" >> ", PathVertices);
    }

    public string ToString(Dictionary<int, string> verticesNames)
    {
        var sb = new StringBuilder();

        var vertexNames = new List<string?>();
        foreach (var vertex in PathVertices)
        {
            var vertexName = verticesNames.TryGetValue(vertex, out var vertexValue) ? vertexValue : vertex.ToString();
            vertexNames.Add(vertexName);
        }

        sb.Append(string.Join(" >> ", vertexNames));

        return sb.ToString();
    }
}